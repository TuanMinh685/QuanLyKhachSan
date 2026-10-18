using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    /// <summary>
    /// Màn hình "Quản lý khách hàng" (Chức năng 3).
    /// Form CHỈ lo giao diện: đọc/ghi control, hiển thị lỗi. Mọi quy tắc nghiệp vụ nằm ở KhachHangBLL,
    /// mọi câu SQL nằm ở KhachHangDAL.
    /// Luồng: thao tác UI -> frmKhachHang -> KhachHangBLL (chuẩn hóa + kiểm tra) -> KhachHangDAL (SQL) -> kết quả quay ngược lại.
    /// </summary>
    public partial class frmKhachHang : Form
    {
        private static readonly CultureInfo Vi = new CultureInfo("vi-VN");

        private readonly KhachHangBLL bll = new KhachHangBLL();

        private List<KhachHang> dsHienThi = new List<KhachHang>();   // dữ liệu đang hiện trên lưới trái
        private KhachHang khDangChon;                                // hồ sơ đang hiển thị ở panel phải (null khi đang thêm mới)
        private bool cheDoThem;                                      // true = panel phải đang ở chế độ "Thêm khách hàng mới"
        private bool dangNap;                                        // cờ chặn sự kiện dây chuyền khi chính code đang nạp dữ liệu

        public frmKhachHang()
        {
            InitializeComponent();
        }

        // ==================================================================
        // KHỞI TẠO
        // ==================================================================
        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // Bảo đảm vùng thông tin khách hàng có thanh cuộn dọc trên màn hình DPI cao
            // hoặc khi form được nhúng vào vùng làm việc có chiều cao hạn chế.
            pnlThongTin.AutoScroll = true;
            // Tính vùng cuộn từ vị trí thực tế sau khi WinForms đã áp dụng DPI scaling.
            // Không dùng Anchor.Bottom cho nút trong nội dung cuộn vì chúng sẽ bị ghim vào đáy viewport.
            btnHuy.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnCapNhat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlThongTin.PerformLayout();
            int contentBottom = 0;
            foreach (Control control in pnlThongTin.Controls)
            {
                if (control.Visible)
                    contentBottom = Math.Max(contentBottom, control.Bottom);
            }
            pnlThongTin.AutoScrollMinSize = new Size(0, contentBottom + 24);
            pnlThongTin.VerticalScroll.Visible = true;
            pnlThongTin.PerformLayout();

            lblNgayHienTai.Text = DateTime.Today.ToString("dddd, dd/MM/yyyy", Vi);

            // Ngày sinh không được ở tương lai: chặn ngay từ control
            dtpNgaySinh.Value = DateTime.Today;
            dtpNgaySinh.MaxDate = DateTime.Today;
            dtpNgaySinh.Checked = false;                // bỏ tick = không nhập ngày sinh (NULL)

            // Tinh chỉnh kiểu hiển thị cột (cho giống thiết kế Figma)
            colMaKH.DefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            colMaKH.DefaultCellStyle.ForeColor = Color.FromArgb(30, 64, 120);
            colSoGiayTo.DefaultCellStyle.ForeColor = Color.FromArgb(100, 116, 139);
            colSoLanLuuTru.DefaultCellStyle.ForeColor = Color.FromArgb(100, 116, 139);
            colSoLanLuuTru.DefaultCellStyle.Format = "0 \"lần\"";
            colLsPhong.DefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            colLsThoiGian.DefaultCellStyle.ForeColor = Color.FromArgb(100, 116, 139);
            colLsTongTien.DefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);

            dangNap = true;
            cboNhomKhach.SelectedIndex = 0;             // "Tất cả nhóm khách"
            dangNap = false;

            TaiDanhSach();
            if (dsHienThi.Count == 0) BatDauThemMoi();  // CSDL chưa có khách nào -> mở sẵn form thêm mới
        }

        /// <summary>Ctrl+F: nhảy tới ô tìm kiếm (như gợi ý "Ctrl+F" trên thiết kế).</summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                txtTimKiem.Focus();
                txtTimKiem.SelectAll();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ==================================================================
        // DANH SÁCH BÊN TRÁI: tải / tìm kiếm / lọc nhóm
        // ==================================================================

        /// <summary>
        /// Nạp lại lưới danh sách theo từ khóa + nhóm đang chọn.
        /// chonMa: hồ sơ cần chọn sau khi nạp (dùng sau Thêm/Cập nhật). null = giữ hồ sơ đang xem.
        /// </summary>
        private void TaiDanhSach(int? chonMa = null)
        {
            int? giuMa = chonMa ?? ((!cheDoThem && khDangChon != null) ? (int?)khDangChon.MaKhachHang : null);
            KhachHang muc = null;

            dangNap = true;     // trong lúc gán DataSource, lưới tự "chọn dòng đầu" -> phải chặn SelectionChanged
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();
                string nhom = LayNhomDangLoc();

                dsHienThi = bll.TimKiem(tuKhoa, nhom);                // -> BLL -> DAL -> SQL (view vw_KhachHangTongQuan)
                dgvKhachHang.DataSource = dsHienThi;

                lblSoKetQua.Text = (tuKhoa.Length == 0 && nhom.Length == 0)
                    ? dsHienThi.Count + " khách hàng gần đây"
                    : dsHienThi.Count + " kết quả";
                lblTongHoSo.Text = bll.DemHoSo().ToString("N0", Vi) + " hồ sơ";

                // Quyết định hồ sơ nào được chọn
                if (giuMa.HasValue) muc = dsHienThi.Find(k => k.MaKhachHang == giuMa.Value);
                if (muc == null && chonMa.HasValue) muc = bll.LayTheoMa(chonMa.Value);          // hồ sơ mới nhưng không nằm trong kết quả lọc
                if (muc == null && !cheDoThem && dsHienThi.Count > 0) muc = dsHienThi[0];        // mặc định: dòng đầu

                if (muc == null || !ChonDong(muc.MaKhachHang))
                {
                    dgvKhachHang.CurrentCell = null;
                    dgvKhachHang.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                HienLoiHeThong("Không thể tải danh sách khách hàng", ex);
                return;
            }
            finally
            {
                dangNap = false;
            }

            // Nếu chỉ là "làm mới do tìm kiếm" và hồ sơ đang sửa vẫn còn trong danh sách -> KHÔNG đổ lại form (tránh mất chữ đang gõ)
            bool giuNguyenForm = !chonMa.HasValue && !cheDoThem && khDangChon != null && muc != null
                                 && muc.MaKhachHang == khDangChon.MaKhachHang;
            if (muc != null && !giuNguyenForm) HienThiKhachHang(muc);
        }

        private string LayNhomDangLoc()
        {
            return cboNhomKhach.SelectedIndex <= 0 ? "" : Convert.ToString(cboNhomKhach.SelectedItem);
        }

        private bool ChonDong(int maKhachHang)
        {
            foreach (DataGridViewRow row in dgvKhachHang.Rows)
            {
                if (row.DataBoundItem is KhachHang k && k.MaKhachHang == maKhachHang)
                {
                    dgvKhachHang.ClearSelection();
                    row.Selected = true;
                    dgvKhachHang.CurrentCell = row.Cells[0];
                    return true;
                }
            }
            return false;
        }

        // Debounce: gõ xong 300ms mới truy vấn (khỏi gửi 1 câu SQL cho mỗi phím bấm)
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dangNap) return;
            tmrTimKiem.Stop();
            tmrTimKiem.Start();
        }

        private void tmrTimKiem_Tick(object sender, EventArgs e)
        {
            tmrTimKiem.Stop();
            TaiDanhSach();
        }

        private void cboNhomKhach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangNap) return;
            TaiDanhSach();
        }

        // Người dùng bấm chọn 1 dòng -> hiển thị hồ sơ sang panel phải
        private void dgvKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dangNap || dgvKhachHang.CurrentRow == null) return;
            if (!(dgvKhachHang.CurrentRow.DataBoundItem is KhachHang kh)) return;
            if (!cheDoThem && khDangChon != null && khDangChon.MaKhachHang == kh.MaKhachHang) return;   // chọn lại đúng dòng đang xem

            try { HienThiKhachHang(kh); }
            catch (Exception ex) { HienLoiHeThong("Không thể tải hồ sơ khách hàng", ex); }
        }

        // ==================================================================
        // PANEL PHẢI: hiển thị / xóa trắng / đọc dữ liệu form
        // ==================================================================
        private void HienThiKhachHang(KhachHang kh)
        {
            cheDoThem = false;
            khDangChon = kh;
            XoaLoi();

            lblTieuDeThongTin.Text = "Thông tin khách hàng";
            btnCapNhat.Text = "Cập nhật";

            txtHoTen.Text = kh.HoTen;
            txtDienThoai.Text = kh.DienThoai;
            cboGioiTinh.SelectedIndex = kh.GioiTinh == null ? -1 : cboGioiTinh.Items.IndexOf(kh.GioiTinh);
            DatNgaySinh(kh.NgaySinh);
            txtQuocTich.Text = kh.QuocTich;
            txtEmail.Text = kh.Email ?? "";
            txtDiaChi.Text = kh.DiaChi ?? "";
            txtSoGiayTo.Text = kh.SoGiayTo ?? "";
            txtGhiChu.Text = kh.GhiChu ?? "";
            chkNhanUuDai.Checked = kh.DongYNhanUuDai;

            // Badge nhóm khách (VIP / Thân thiết / Mới)
            MauNhom(kh.NhomKhach, out Color nen, out Color chu);
            lblNhomBadge.Text = kh.NhomKhach;
            lblNhomBadge.BackColor = nen;
            lblNhomBadge.ForeColor = chu;
            lblNhomBadge.Visible = true;

            // Các tab bên dưới danh sách
            lblThongTinChung.Text =
                "Mã khách hàng:" + "\t\t" + kh.MaKH + Environment.NewLine + Environment.NewLine +
                "Nhóm khách:" + "\t\t" + kh.NhomKhach + " (" + kh.SoLanLuuTru + " lần lưu trú)" + Environment.NewLine + Environment.NewLine +
                "Lần lưu trú gần nhất:" + "\t" + (kh.LanLuuTruGanNhat.HasValue ? kh.LanLuuTruGanNhat.Value.ToString("dd/MM/yyyy") : "—") + Environment.NewLine + Environment.NewLine +
                "Ngày tạo hồ sơ:" + "\t\t" + kh.NgayTao.ToString("dd/MM/yyyy HH:mm");
            lblGhiChuXem.Text = string.IsNullOrWhiteSpace(kh.GhiChu) ? "(Không có ghi chú)" : kh.GhiChu;
            NapLichSu(kh.MaKhachHang);
        }

        private void NapLichSu(int? maKhachHang)
        {
            if (!maKhachHang.HasValue)
            {
                dgvLichSu.DataSource = null;
                tpLichSu.Text = "Lịch sử lưu trú (0)";
                return;
            }

            List<LichSuLuuTru> ls = bll.LayLichSu(maKhachHang.Value);
            dgvLichSu.DataSource = ls;
            tpLichSu.Text = "Lịch sử lưu trú (" + ls.Count + ")";
        }

        private void DatNgaySinh(DateTime? ngaySinh)
        {
            if (ngaySinh.HasValue)
            {
                DateTime v = ngaySinh.Value.Date;
                if (v < dtpNgaySinh.MinDate) v = dtpNgaySinh.MinDate;
                if (v > dtpNgaySinh.MaxDate) v = dtpNgaySinh.MaxDate;
                dtpNgaySinh.Value = v;
                dtpNgaySinh.Checked = true;
            }
            else
            {
                dtpNgaySinh.Checked = false;
            }
        }

        /// <summary>Đọc toàn bộ control thành 1 đối tượng KhachHang (dữ liệu THÔ – BLL sẽ chuẩn hóa).</summary>
        private KhachHang DocTuForm()
        {
            return new KhachHang
            {
                MaKhachHang = (cheDoThem || khDangChon == null) ? 0 : khDangChon.MaKhachHang,
                HoTen = txtHoTen.Text,
                DienThoai = txtDienThoai.Text,
                GioiTinh = cboGioiTinh.SelectedItem as string,
                NgaySinh = dtpNgaySinh.Checked ? (DateTime?)dtpNgaySinh.Value.Date : null,
                QuocTich = txtQuocTich.Text,
                Email = txtEmail.Text,
                DiaChi = txtDiaChi.Text,
                SoGiayTo = txtSoGiayTo.Text,
                GhiChu = txtGhiChu.Text,
                DongYNhanUuDai = chkNhanUuDai.Checked
            };
        }

        private void XoaForm()
        {
            XoaLoi();
            txtHoTen.Clear();
            txtDienThoai.Clear();
            cboGioiTinh.SelectedIndex = -1;
            dtpNgaySinh.Checked = false;
            txtQuocTich.Text = "Việt Nam";
            txtEmail.Clear();
            txtDiaChi.Clear();
            txtSoGiayTo.Clear();
            txtGhiChu.Clear();
            chkNhanUuDai.Checked = false;
        }

        private void BatDauThemMoi()
        {
            cheDoThem = true;
            khDangChon = null;

            dangNap = true;
            try
            {
                dgvKhachHang.CurrentCell = null;    // bỏ chọn dòng -> lần sau bấm lại đúng dòng cũ vẫn kích hoạt SelectionChanged
                dgvKhachHang.ClearSelection();
            }
            finally { dangNap = false; }

            XoaForm();
            lblTieuDeThongTin.Text = "Thêm khách hàng mới";
            btnCapNhat.Text = "Lưu khách hàng";
            lblNhomBadge.Visible = false;
            lblThongTinChung.Text = "Hồ sơ mới – chưa có thông tin lưu trú.";
            lblGhiChuXem.Text = "";
            NapLichSu(null);
            txtHoTen.Focus();
        }

        // ==================================================================
        // CÁC NÚT
        // ==================================================================
        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            // Đảm bảo thao tác thêm mới luôn đưa panel chi tiết về trạng thái nhập liệu,
            // kể cả khi form đang hiển thị một hồ sơ hoặc đang lọc danh sách.
            try
            {
                tmrTimKiem.Stop();
                pnlThongTin.Visible = true;
                pnlThongTin.Enabled = true;
                pnlThongTin.BringToFront();
                BatDauThemMoi();
                txtHoTen.Enabled = true;
                txtHoTen.Focus();
            }
            catch (Exception ex)
            {
                HienLoiHeThong("Không thể mở biểu mẫu thêm khách hàng", ex);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (!cheDoThem && khDangChon != null)
            {
                HienThiKhachHang(khDangChon);               // đang sửa: đổ lại dữ liệu gốc, bỏ mọi thay đổi chưa lưu
            }
            else if (dsHienThi.Count > 0)
            {
                dangNap = true;
                try { ChonDong(dsHienThi[0].MaKhachHang); } finally { dangNap = false; }
                HienThiKhachHang(dsHienThi[0]);             // đang thêm mới: bỏ form, quay về hồ sơ đầu danh sách
            }
            else
            {
                XoaForm();
            }
        }

        /// <summary>Nút "Cập nhật" (sửa) / "Lưu khách hàng" (thêm mới).</summary>
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            XoaLoi();
            KhachHang kh = DocTuForm();
            bool themMoi = kh.MaKhachHang == 0;

            KetQuaLuu kq;
            try
            {
                kq = themMoi ? bll.Them(kh) : bll.CapNhat(kh);   // toàn bộ chuẩn hóa + kiểm tra + SQL nằm trong BLL/DAL
            }
            catch (Exception ex)
            {
                HienLoiHeThong("Không thể lưu hồ sơ khách hàng", ex);
                return;
            }

            if (!kq.ThanhCong)
            {
                HienLoiTheoTruong(kq.LoiTheoTruong);
                if (kq.LoiTheoTruong.Count == 0)
                    MessageBox.Show(kq.ThongBao, "Không lưu được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(kq.ThongBao, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (themMoi)
            {
                // Xóa bộ lọc để chắc chắn khách vừa thêm hiện trong danh sách
                dangNap = true;
                txtTimKiem.Clear();
                cboNhomKhach.SelectedIndex = 0;
                dangNap = false;
            }
            TaiDanhSach(kq.MaKhachHang);                         // nạp lại danh sách và chọn đúng hồ sơ vừa lưu
        }

        // ==================================================================
        // HIỂN THỊ LỖI
        // ==================================================================
        private void XoaLoi()
        {
            errorProvider.Clear();
            HienLoiSoGiayTo(null);
        }

        private void HienLoiTheoTruong(Dictionary<string, string> loi)
        {
            Control dauTien = null;
            foreach (KeyValuePair<string, string> kv in loi)
            {
                if (kv.Key == "SoGiayTo")
                {
                    HienLoiSoGiayTo(kv.Value);                   // theo thiết kế Figma: chữ đỏ dưới ô giấy tờ
                    dauTien = dauTien ?? txtSoGiayTo;
                    continue;
                }
                Control c = ControlTheoTruong(kv.Key);
                if (c != null)
                {
                    errorProvider.SetError(c, kv.Value);
                    dauTien = dauTien ?? c;
                }
            }
            if (dauTien != null) dauTien.Focus();
        }

        private Control ControlTheoTruong(string truong)
        {
            switch (truong)
            {
                case "HoTen": return txtHoTen;
                case "DienThoai": return txtDienThoai;
                case "GioiTinh": return cboGioiTinh;
                case "NgaySinh": return dtpNgaySinh;
                case "QuocTich": return txtQuocTich;
                case "Email": return txtEmail;
                case "DiaChi": return txtDiaChi;
                case "GhiChu": return txtGhiChu;
                default: return null;
            }
        }

        private void HienLoiSoGiayTo(string thongBao)
        {
            if (string.IsNullOrEmpty(thongBao))
            {
                lblLoiSoGiayTo.Visible = false;
                txtSoGiayTo.BackColor = Color.White;
            }
            else
            {
                lblLoiSoGiayTo.Text = thongBao;
                lblLoiSoGiayTo.Visible = true;
                txtSoGiayTo.BackColor = Color.FromArgb(254, 242, 242);
            }
        }

        private void HienLoiHeThong(string tieuDe, Exception ex)
        {
            MessageBox.Show(
                tieuDe + ".\n\nChi tiết: " + ex.Message +
                "\n\nHãy kiểm tra SQL Server LocalDB đã chạy script SQL và chuỗi kết nối trong DatabaseHelper.cs.",
                "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ==================================================================
        // SỰ KIỆN Ô NHẬP LIỆU
        // ==================================================================

        // Ô điện thoại chỉ cho gõ số và + khoảng trắng . - ( )
        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && "+ .-()".IndexOf(e.KeyChar) < 0)
                e.Handled = true;
        }

        // Gõ lại số giấy tờ thì xóa cảnh báo cũ
        private void txtSoGiayTo_TextChanged(object sender, EventArgs e)
        {
            HienLoiSoGiayTo(null);
        }

        // Rời ô giấy tờ -> kiểm tra trùng NGAY (giống thông báo đỏ trên thiết kế), không phải đợi bấm Lưu
        private void txtSoGiayTo_Leave(object sender, EventArgs e)
        {
            if (dangNap || string.IsNullOrWhiteSpace(txtSoGiayTo.Text)) return;
            try
            {
                int? maHienTai = (!cheDoThem && khDangChon != null) ? khDangChon.MaKhachHang : (int?)null;
                if (bll.KiemTraTrungGiayTo(txtSoGiayTo.Text, maHienTai))
                    HienLoiSoGiayTo("Số giấy tờ đã tồn tại trong hồ sơ khách hàng khác.");
            }
            catch
            {
                // Bỏ qua: khi bấm Lưu, BLL sẽ kiểm tra lại và báo lỗi đầy đủ.
            }
        }

        // ==================================================================
        // VẼ "NHÃN" (badge) TRONG LƯỚI: VIP / Thân thiết / Mới / Đang lưu trú ...
        // ==================================================================
        private void dgvKhachHang_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colNhomKhach.Index) return;
            string nhom = Convert.ToString(e.Value);
            MauNhom(nhom, out Color nen, out Color chu);
            VeNhan(e, nhom, nen, chu);
        }

        private void dgvLichSu_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colLsTrangThai.Index) return;
            string tt = Convert.ToString(e.Value);
            if (tt == "Đang lưu trú") VeNhan(e, tt, Color.FromArgb(254, 226, 226), Color.FromArgb(185, 28, 28));
            else VeNhan(e, tt, Color.FromArgb(226, 232, 240), Color.FromArgb(71, 85, 105));
        }

        private static void MauNhom(string nhom, out Color nen, out Color chu)
        {
            switch (nhom)
            {
                case "VIP":
                    nen = Color.FromArgb(254, 243, 199); chu = Color.FromArgb(180, 83, 9); break;
                case "Thân thiết":
                    nen = Color.FromArgb(209, 250, 229); chu = Color.FromArgb(4, 120, 87); break;
                default:
                    nen = Color.FromArgb(219, 234, 254); chu = Color.FromArgb(29, 78, 216); break;
            }
        }

        private static void VeNhan(DataGridViewCellPaintingEventArgs e, string text, Color nen, Color chu)
        {
            // Vẽ nền ô + đường kẻ + màu chọn như bình thường, rồi vẽ thêm viên thuốc (pill) lên trên
            e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.SelectionBackground);

            if (!string.IsNullOrEmpty(text))
            {
                using (var font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold))
                {
                    Size kichThuoc = TextRenderer.MeasureText(e.Graphics, text, font);
                    var rect = new Rectangle(e.CellBounds.X + 6, e.CellBounds.Y + (e.CellBounds.Height - 22) / 2, kichThuoc.Width + 20, 22);

                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    using (GraphicsPath path = TaoBoGoc(rect, 22))
                    using (var brush = new SolidBrush(nen))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                    TextRenderer.DrawText(e.Graphics, text, font, rect, chu,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
                }
            }
            e.Handled = true;
        }

        private static GraphicsPath TaoBoGoc(Rectangle r, int duongKinh)
        {
            var p = new GraphicsPath();
            p.AddArc(r.X, r.Y, duongKinh, duongKinh, 180, 90);
            p.AddArc(r.Right - duongKinh, r.Y, duongKinh, duongKinh, 270, 90);
            p.AddArc(r.Right - duongKinh, r.Bottom - duongKinh, duongKinh, duongKinh, 0, 90);
            p.AddArc(r.X, r.Bottom - duongKinh, duongKinh, duongKinh, 90, 90);
            p.CloseFigure();
            return p;
        }
    }
}
