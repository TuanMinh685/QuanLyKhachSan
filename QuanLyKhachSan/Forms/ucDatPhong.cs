using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    /// <summary>
    /// Chức năng 4: tìm phòng trống, đặt phòng, nhận phòng, trả phòng, hủy phòng.
    /// Lớp này CHỈ lo giao diện, mọi nghiệp vụ và SQL nằm ở BLL/DAL.
    /// </summary>
    public partial class ucDatPhong : UserControl
    {
        private static readonly CultureInfo Vi = new CultureInfo("vi-VN");

        private int _maKH = 0;               // 0 = chưa chọn khách
        private bool _boQuaSuKien = true;    // true khi đang khởi tạo, tránh sự kiện chạy sớm

        public ucDatPhong()
        {
            InitializeComponent();
            GanSuKien();
        }

        // =====================================================================
        // GẮN SỰ KIỆN BẰNG CODE
        // Mỗi sự kiện gỡ (-=) rồi gắn (+=): nếu Designer đã gắn sẵn (do bấm đôi)
        // thì không bị chạy 2 lần; nếu chưa gắn thì vẫn hoạt động.
        // =====================================================================
        private void GanSuKien()
        {
            Load -= ucDatPhong_Load; Load += ucDatPhong_Load;

            btnTimPhong.Click -= btnTimPhong_Click; btnTimPhong.Click += btnTimPhong_Click;
            btnDatPhong.Click -= btnDatPhong_Click; btnDatPhong.Click += btnDatPhong_Click;
            btnNhanPhong.Click -= btnNhanPhong_Click; btnNhanPhong.Click += btnNhanPhong_Click;
            btnTraPhong.Click -= btnTraPhong_Click; btnTraPhong.Click += btnTraPhong_Click;
            btnHuyPhong.Click -= btnHuyPhong_Click; btnHuyPhong.Click += btnHuyPhong_Click;
            btnDoiKhach.Click -= btnDoiKhach_Click; btnDoiKhach.Click += btnDoiKhach_Click;

            dgvPhong.SelectionChanged -= dgvPhong_SelectionChanged;
            dgvPhong.SelectionChanged += dgvPhong_SelectionChanged;

            dtpNgayNhan.ValueChanged -= DieuKienTimThayDoi; dtpNgayNhan.ValueChanged += DieuKienTimThayDoi;
            dtpNgayTra.ValueChanged -= DieuKienTimThayDoi; dtpNgayTra.ValueChanged += DieuKienTimThayDoi;
            cboNguoiLon.SelectedIndexChanged -= DieuKienTimThayDoi; cboNguoiLon.SelectedIndexChanged += DieuKienTimThayDoi;
            cboTreEm.SelectedIndexChanged -= DieuKienTimThayDoi; cboTreEm.SelectedIndexChanged += DieuKienTimThayDoi;
            cboLoaiPhong.SelectedIndexChanged -= DieuKienTimThayDoi; cboLoaiPhong.SelectedIndexChanged += DieuKienTimThayDoi;
        }

        // =====================================================================
        // KHỞI TẠO
        // =====================================================================
        private void ucDatPhong_Load(object sender, EventArgs e)
        {
            // Designer của form khác cũng chạy Load khi hiển thị control lúc thiết kế
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode) return;

            _boQuaSuKien = true;
            try
            {
                lblNgayHienTai.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy", Vi);

                CauHinhLuoi();
                NapLoaiPhong();

                // Mặc định: nhận hôm nay, trả ngày mai (ngày trả luôn >= ngày nhận + 1)
                dtpNgayNhan.MinDate = DateTime.Today;
                dtpNgayNhan.Value = DateTime.Today;
                dtpNgayTra.MinDate = DateTime.Today.AddDays(1);
                dtpNgayTra.Value = DateTime.Today.AddDays(1);

                cboNguoiLon.SelectedIndex = 1;   // mặc định "2 người lớn"
                cboTreEm.SelectedIndex = 0;      // mặc định "0 trẻ em"
            }
            catch (Exception ex)
            {
                Loi("Không khởi tạo được màn hình đặt phòng: " + ex.Message);
            }
            finally
            {
                _boQuaSuKien = false;
            }

            XoaKetQuaCu();
        }

        private void NapLoaiPhong()
        {
            DataTable dt = LeTanDatPhongBLL.LayLoaiPhong();

            DataRow tatCa = dt.NewRow();
            tatCa["MaLoai"] = 0;                       // 0 nghĩa là "không lọc"
            tatCa["TenLoai"] = "Tất cả loại phòng";
            dt.Rows.InsertAt(tatCa, 0);

            cboLoaiPhong.DisplayMember = "TenLoai";    // đặt Display/Value TRƯỚC DataSource
            cboLoaiPhong.ValueMember = "MaLoai";
            cboLoaiPhong.DataSource = dt;
        }

        private void CauHinhLuoi()
        {
            if (dgvPhong.Columns.Count > 0) return;    // đã cấu hình rồi

            dgvPhong.AutoGenerateColumns = false;      // chỉ hiện cột mình khai báo
            dgvPhong.ReadOnly = true;
            dgvPhong.AllowUserToAddRows = false;
            dgvPhong.AllowUserToDeleteRows = false;
            dgvPhong.AllowUserToResizeRows = false;
            dgvPhong.RowHeadersVisible = false;
            dgvPhong.MultiSelect = false;
            dgvPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhong.BackgroundColor = Color.White;
            dgvPhong.BorderStyle = BorderStyle.None;
            dgvPhong.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPhong.GridColor = ColorTranslator.FromHtml("#D8E0E8");
            dgvPhong.RowTemplate.Height = 52;

            dgvPhong.EnableHeadersVisualStyles = false;
            dgvPhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPhong.ColumnHeadersHeight = 30;
            dgvPhong.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F3F6F9");
            dgvPhong.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#647487");
            dgvPhong.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#E3EEF9");
            dgvPhong.DefaultCellStyle.SelectionForeColor = Color.Black;

            ThemCot("SoPhong", "Phòng", 70, false, DataGridViewContentAlignment.MiddleLeft, true);
            ThemCot("TenLoai", "Loại", 90, false, DataGridViewContentAlignment.MiddleLeft, false);
            ThemCot("MoTa", "Mô tả", 0, true, DataGridViewContentAlignment.MiddleLeft, false);
            ThemCot("GiaHienThi", "Giá / đêm", 130, false, DataGridViewContentAlignment.MiddleRight, true);
            ThemCot("ConPhong", "Còn", 100, false, DataGridViewContentAlignment.MiddleCenter, false);
        }

        private void ThemCot(string prop, string tieuDe, int rong, bool fill,
                             DataGridViewContentAlignment canh, bool dam)
        {
            var c = new DataGridViewTextBoxColumn
            {
                DataPropertyName = prop,               // cột này lấy dữ liệu từ cột nào của DataTable
                HeaderText = tieuDe,
                Name = "col" + prop,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            if (fill) c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            else c.Width = rong;
            c.DefaultCellStyle.Alignment = canh;
            if (dam) c.DefaultCellStyle.Font = new Font(dgvPhong.Font, FontStyle.Bold);
            dgvPhong.Columns.Add(c);
        }

        // =====================================================================
        // TÌM PHÒNG TRỐNG
        // =====================================================================
        private void btnTimPhong_Click(object sender, EventArgs e)
        {
            TimPhong();
        }

        private void TimPhong()
        {
            DateTime nhan = dtpNgayNhan.Value.Date;
            DateTime tra = dtpNgayTra.Value.Date;
            int nguoiLon = SoDauTien(cboNguoiLon.Text);
            int treEm = SoDauTien(cboTreEm.Text);
            int maLoai;
            int.TryParse(Convert.ToString(cboLoaiPhong.SelectedValue), out maLoai);

            try
            {
                DataTable dt = LeTanDatPhongBLL.TimPhongTrong(nhan, tra, nguoiLon, treEm, maLoai);

                // Thêm các cột chữ đã định dạng để lưới chỉ việc hiển thị
                dt.Columns.Add("MoTa", typeof(string));
                dt.Columns.Add("GiaHienThi", typeof(string));
                dt.Columns.Add("ConPhong", typeof(string));
                foreach (DataRow r in dt.Rows)
                {
                    r["MoTa"] = $"{r["MoTaGiuong"]} • {r["DienTich"]} m² • Tầng {r["Tang"]}";
                    r["GiaHienThi"] = Tien(Convert.ToDecimal(r["GiaMotDem"])) + " / đêm";
                    r["ConPhong"] = $"Còn {r["SoPhongConLai"]} phòng";
                }

                dgvPhong.DataSource = dt;
                dgvPhong.ClearSelection();             // bắt người dùng chủ động chọn phòng

                lblSoPhongTrong.Text = dt.Rows.Count + " phòng trống";
                lblGhiChuGia.Text = $"Giá cho {LeTanDatPhongBLL.TinhSoDem(nhan, tra)} đêm • Chưa gồm thuế & phí";
                CapNhatTomTat();

                if (dt.Rows.Count == 0)
                    ThongBao("Không có phòng trống phù hợp. Hãy thử đổi ngày hoặc loại phòng.");
            }
            catch (NghiepVuException ex)
            {
                CanhBao(ex.Message);
            }
            catch (Exception ex)
            {
                Loi("Lỗi khi tìm phòng: " + ex.Message);
            }
        }

        // Đổi bất kỳ điều kiện tìm nào thì kết quả cũ không còn đúng -> xóa đi
        private void DieuKienTimThayDoi(object sender, EventArgs e)
        {
            if (_boQuaSuKien) return;

            if (sender == dtpNgayNhan)
            {
                _boQuaSuKien = true;   // tránh xóa kết quả 2 lần khi MinDate làm đổi ngày trả
                try { dtpNgayTra.MinDate = dtpNgayNhan.Value.Date.AddDays(1); }
                finally { _boQuaSuKien = false; }
            }
            XoaKetQuaCu();
        }

        private void XoaKetQuaCu()
        {
            dgvPhong.DataSource = null;
            lblSoPhongTrong.Text = "0 phòng trống";
            lblGhiChuGia.Text = "Nhấn \"Tìm phòng trống\" để xem kết quả";
            CapNhatTomTat();
        }

        // =====================================================================
        // TÓM TẮT CHI PHÍ
        // =====================================================================
        private void dgvPhong_SelectionChanged(object sender, EventArgs e)
        {
            CapNhatTomTat();
        }

        private DataRowView LayPhongDangChon()
        {
            if (dgvPhong.SelectedRows.Count == 0) return null;
            return dgvPhong.SelectedRows[0].DataBoundItem as DataRowView;
        }

        private void CapNhatTomTat()
        {
            DataRowView r = LayPhongDangChon();
            if (r == null)
            {
                lblDongPhong.Text = "Chưa chọn phòng";
                lblTienPhong.Text = "0 đ";
                lblPhuThuTien.Text = "0 đ";
                lblThueTien.Text = "0 đ";
                lblTongCongTien.Text = "0 đ";
                return;
            }

            // Công thức tính tiền nằm ở BLL, giao diện chỉ hiển thị
            ChiPhiDatPhong cp = LeTanDatPhongBLL.TinhChiPhi(
                Convert.ToDecimal(r["GiaMotDem"]), dtpNgayNhan.Value, dtpNgayTra.Value);

            lblDongPhong.Text = $"Phòng {r["TenLoai"]} {r["SoPhong"]}";
            lblTienPhong.Text = $"{Tien(cp.GiaMotDem)} × {cp.SoDem}";
            lblPhuThuTien.Text = Tien(cp.PhuThu);
            lblThueTien.Text = Tien(cp.ThueVaPhi);
            lblTongCongTien.Text = Tien(cp.TongTien);
        }

        // =====================================================================
        // CHỌN KHÁCH
        // =====================================================================
        private void btnDoiKhach_Click(object sender, EventArgs e)
        {
            using (var f = new frmChonKhach())
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    _maKH = f.MaKH;
                    lblTenKhach.Text = f.HoTen;
                    lblSdtMa.Text = $"{f.SoDienThoai} • {f.MaKHHienThi}";
                    lblAvatar.Text = VietTat(f.HoTen);
                }
            }
        }

        private void ResetKhach()
        {
            _maKH = 0;
            lblTenKhach.Text = "Chưa chọn khách";
            lblSdtMa.Text = "—";
            lblAvatar.Text = "?";
        }

        // =====================================================================
        // ĐẶT PHÒNG
        // =====================================================================
        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            DataRowView r = LayPhongDangChon();
            if (r == null)
            {
                CanhBao("Vui lòng chọn một phòng trong danh sách.");
                return;
            }
            if (_maKH == 0)
            {
                CanhBao("Vui lòng chọn khách đặt phòng (bấm biểu tượng bút ở mục Khách đặt phòng).");
                return;
            }

            DateTime nhan = dtpNgayNhan.Value.Date;
            DateTime tra = dtpNgayTra.Value.Date;
            int nguoiLon = SoDauTien(cboNguoiLon.Text);
            int treEm = SoDauTien(cboTreEm.Text);
            int maPhong = Convert.ToInt32(r["MaPhong"]);

            ChiPhiDatPhong cp = LeTanDatPhongBLL.TinhChiPhi(
                Convert.ToDecimal(r["GiaMotDem"]), nhan, tra);

            string noiDung =
                $"Phòng {r["SoPhong"]} ({r["TenLoai"]})\n" +
                $"Khách: {lblTenKhach.Text}\n" +
                $"Từ {nhan:dd/MM/yyyy} đến {tra:dd/MM/yyyy} ({cp.SoDem} đêm)\n" +
                $"Tổng tiền: {Tien(cp.TongTien)}";

            if (MessageBox.Show(noiDung, "Xác nhận đặt phòng",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                // maNhanVien = null cho tới khi nhóm ghép phần đăng nhập
                string maDat = LeTanDatPhongBLL.TaoDatPhong(
                    _maKH, maPhong, nhan, tra, nguoiLon, treEm, null);

                MessageBox.Show("Đặt phòng thành công!\nMã đặt phòng: " + maDat, "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetKhach();
                TimPhong();    // làm mới: phòng vừa đặt sẽ biến khỏi danh sách
            }
            catch (NghiepVuException ex)    // lỗi nghiệp vụ (trùng lịch, thiếu dữ liệu...)
            {
                CanhBao(ex.Message);
                TimPhong();
            }
            catch (Exception ex)            // lỗi hệ thống / CSDL
            {
                Loi("Lỗi khi đặt phòng: " + ex.Message);
            }
        }

        // =====================================================================
        // NHẬN / TRẢ / HỦY PHÒNG (chức năng 4, phần B)
        // Cả 3 nút dùng chung một quy trình: chọn đơn -> xác nhận -> gọi BLL -> làm mới.
        // =====================================================================
        private void btnNhanPhong_Click(object sender, EventArgs e) { XuLyLuuTru(ThaoTacDatPhong.NhanPhong); }
        private void btnTraPhong_Click(object sender, EventArgs e) { XuLyLuuTru(ThaoTacDatPhong.TraPhong); }
        private void btnHuyPhong_Click(object sender, EventArgs e) { XuLyLuuTru(ThaoTacDatPhong.HuyPhong); }

        private void XuLyLuuTru(ThaoTacDatPhong thaoTac)
        {
            // 1) Mở hộp thoại cho lễ tân chọn đơn; đóng form thì lấy kết quả ra rồi hủy form
            int maDatPhong = 0;
            string maDat = "", tenKhach = "", soPhong = "", lyDo = "";
            using (var f = new frmChonDatPhong(thaoTac))
            {
                if (f.ShowDialog(this) != DialogResult.OK) return;
                maDatPhong = f.MaDatPhong;
                maDat = f.MaDat;
                tenKhach = f.TenKhach;
                soPhong = f.SoPhong;
                lyDo = f.LyDo;
            }

            // 2) Hỏi xác nhận lần cuối (đổi trạng thái đơn là thao tác quan trọng)
            string tenViec = thaoTac == ThaoTacDatPhong.NhanPhong ? "nhận phòng"
                           : thaoTac == ThaoTacDatPhong.TraPhong ? "trả phòng"
                           : "hủy phòng";
            string hoi = $"Xác nhận {tenViec}?\n\nĐơn: {maDat}\nKhách: {tenKhach}\nPhòng: {soPhong}";
            if (thaoTac == ThaoTacDatPhong.HuyPhong) hoi += $"\nLý do: {lyDo}";

            if (MessageBox.Show(hoi, "Xác nhận " + tenViec,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // 3) Gọi BLL (kiểm tra quy tắc + ghi CSDL)
            try
            {
                switch (thaoTac)
                {
                    case ThaoTacDatPhong.NhanPhong: LeTanLuuTruBLL.NhanPhong(maDatPhong); break;
                    case ThaoTacDatPhong.TraPhong: LeTanLuuTruBLL.TraPhong(maDatPhong); break;
                    case ThaoTacDatPhong.HuyPhong: LeTanLuuTruBLL.HuyPhong(maDatPhong, lyDo); break;
                }

                ThongBao($"Đã {tenViec} thành công.\nĐơn: {maDat}");

                // Trả / hủy làm phòng trống lại: nếu đang hiện kết quả tìm thì làm mới cho đúng
                if (dgvPhong.DataSource != null) TimPhong();
            }
            catch (NghiepVuException ex)    // lỗi nghiệp vụ: sai trạng thái, chưa đến ngày nhận...
            {
                CanhBao(ex.Message);
            }
            catch (Exception ex)            // lỗi hệ thống / CSDL
            {
                Loi("Lỗi khi " + tenViec + ": " + ex.Message);
            }
        }

        // Hàm trống: giữ lại phòng khi Designer đã tham chiếu (do lỡ bấm đôi vào lưới)
        private void dgvPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        // =====================================================================
        // PHÍM TẮT F4 / F5 / F6
        // =====================================================================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F4) { btnDatPhong.PerformClick(); return true; }
            if (keyData == Keys.F5) { btnNhanPhong.PerformClick(); return true; }
            if (keyData == Keys.F6) { btnTraPhong.PerformClick(); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // =====================================================================
        // HÀM TIỆN ÍCH
        // =====================================================================
        private static string Tien(decimal v)
        {
            return v.ToString("#,##0", Vi) + " đ";       // 1450000 -> "1.450.000 đ"
        }

        private static int SoDauTien(string text)
        {
            int n;
            string dau = (text ?? "").Split(' ')[0];
            return int.TryParse(dau, out n) ? n : 0;     // "2 người lớn" -> 2
        }

        private static string VietTat(string ten)        // "Đỗ Thu Trang" -> "ĐT"
        {
            var p = (ten ?? "").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length == 0) return "?";
            if (p.Length == 1) return p[0].Substring(0, 1).ToUpper(Vi);
            return (p[0].Substring(0, 1) + p[p.Length - 1].Substring(0, 1)).ToUpper(Vi);
        }

        private static void ThongBao(string s)
        {
            MessageBox.Show(s, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void CanhBao(string s)
        {
            MessageBox.Show(s, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static void Loi(string s)
        {
            MessageBox.Show(s, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
