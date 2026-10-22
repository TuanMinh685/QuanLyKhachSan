using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmHoaDon : Form
    {
        private HoaDonBLL hoaDonBLL = new HoaDonBLL();
        private ChiTietHoaDonDAL chiTietDAL = new ChiTietHoaDonDAL();
        private DichVuDAL dichVuDAL = new DichVuDAL();
        private PhuongThucThanhToanDAL ptttDAL = new PhuongThucThanhToanDAL();
        private VoucherBLL voucherBLL = new VoucherBLL();

        private string maHDDangChon = null;

        private List<HoaDon> danhSachHoaDon = new List<HoaDon>();
        private List<DichVu> danhSachDichVu = new List<DichVu>();
        private DataTable dtChiTietTam = new DataTable();
        private int? maVoucherDaApDung = null;

        // Controls giao diện nâng cấp hiện đại
        private Panel pnlHeaderLeft;
        private Label lblTitleLeft;
        private Label lblSubtitleLeft;
        private FlowLayoutPanel pnlStatsPills;
        private Label lblPillCount;
        private Label lblPillRevenue;

        public frmHoaDon()
        {
            InitializeComponent();
            UITheme.ApplyTheme(this);
            KhoiTaoGiaoDienNangCao();
        }

        private void KhoiTaoGiaoDienNangCao()
        {
            // 1. Header và Thẻ thống kê (Stats Pills) ở bên trái
            pnlHeaderLeft = new Panel
            {
                Dock = DockStyle.Top,
                Height = 82,
                BackColor = Color.Transparent
            };

            lblTitleLeft = new Label
            {
                Text = "🧾 LỊCH SỬ HÓA ĐƠN & THU NGÂN",
                Font = new Font("Segoe UI", 12.5f, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(0, 0),
                AutoSize = true
            };

            lblSubtitleLeft = new Label
            {
                Text = "Tra cứu giao dịch, xem chi tiết hóa đơn và in biên lai thanh toán",
                Font = new Font("Segoe UI", 9f, FontStyle.Regular),
                ForeColor = UITheme.TextMuted,
                Location = new Point(0, 26),
                AutoSize = true
            };

            pnlStatsPills = new FlowLayoutPanel
            {
                Location = new Point(0, 52),
                Height = 30,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent
            };

            lblPillCount = TaoTheThongKe("Tổng: 0 hóa đơn", Color.FromArgb(241, 245, 249), UITheme.DarkNavy);
            lblPillRevenue = TaoTheThongKe("💰 Thực thu: 0 đ", Color.FromArgb(255, 247, 237), UITheme.PrimaryOrange);

            pnlStatsPills.Controls.Add(lblPillCount);
            pnlStatsPills.Controls.Add(lblPillRevenue);

            pnlHeaderLeft.Controls.Add(lblTitleLeft);
            pnlHeaderLeft.Controls.Add(lblSubtitleLeft);
            pnlHeaderLeft.Controls.Add(pnlStatsPills);

            cardLeft.Controls.Add(pnlHeaderLeft);

            // Cân đối vị trí tìm kiếm và bảng
            pnlSearch.Location = new Point(20, 108);
            pnlSearch.Height = 40;

            pnlGridWrapper.Location = new Point(20, 160);
            pnlGridWrapper.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            cardLeft.Resize += (s, e) =>
            {
                pnlGridWrapper.Size = new Size(cardLeft.ClientSize.Width - 40, cardLeft.ClientSize.Height - 180);
            };

            // 2. Nâng cấp các nút thao tác
            btnVietQR.Text = "📱 Mã VietQR";
            btnVietQR.BackColor = UITheme.DarkNavy;
            btnVietQR.ForeColor = Color.White;

            btnInHoaDon.Text = "🖨️ In bill";
            btnInHoaDon.BackColor = Color.FromArgb(248, 250, 252);
            btnInHoaDon.ForeColor = UITheme.DarkNavy;

            btnLamMoi.Text = "🔄 Làm mới";
            btnLamMoi.BackColor = Color.FromArgb(248, 250, 252);
            btnLamMoi.ForeColor = UITheme.TextMain;

            btnThanhToan.Text = "💳 Xác nhận thu";
            btnThanhToan.BackColor = UITheme.PrimaryOrange;
            btnThanhToan.ForeColor = Color.White;

            btnThemDV.Text = "➕ Thêm DV";
            btnGoiYVoucher.Text = "💡 Gợi ý";
            btnApDungVoucher.Text = "✓ Áp dụng";

            dgvHoaDon.CellFormatting += DgvHoaDon_CellFormatting;
        }

        private Label TaoTheThongKe(string text, Color bg, Color fg)
        {
            var lbl = new Label
            {
                Text = text,
                BackColor = bg,
                ForeColor = fg,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(10, 4, 10, 4),
                Margin = new Padding(0, 0, 8, 0),
                Cursor = Cursors.Default
            };
            lbl.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, lbl.Width - 1, lbl.Height - 1);
                using (GraphicsPath path = UITheme.GetRoundedRectPath(rect, 6))
                using (Pen pen = new Pen(Color.FromArgb(226, 232, 240), 1f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            };
            return lbl;
        }

        private void DgvHoaDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string col = dgvHoaDon.Columns[e.ColumnIndex].Name;

            if (col == "TongTien" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal val))
                {
                    e.Value = val.ToString("N0") + " đ";
                    e.CellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    e.CellStyle.ForeColor = UITheme.PrimaryOrange;
                    e.FormattingApplied = true;
                }
            }
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            UITheme.StyleModernGrid(dgvHoaDon);
            UITheme.StyleModernGrid(dgvChiTietDV);
            KhoiTaoBangChiTietTam();

            btnVietQR.Text = "Mã VietQR";
            btnInHoaDon.Text = "In bill";
            btnGoiYVoucher.Text = "Gợi ý";
            btnApDungVoucher.Text = "Áp dụng";
            btnThemDV.Text = "Thêm DV";

            TaiDanhSachPhuongThucTT();
            TaiDanhSachDichVu();
            TaiDanhSachDatPhong();
            TaiDanhSachHoaDon();

            LamMoiForm();
        }

        private void DrawFigmaCard(object sender, PaintEventArgs e) => UITheme.DrawCard(sender, e);
        private void DrawFigmaInputBox(object sender, PaintEventArgs e) => UITheme.DrawInputBox(sender, e);

        private void DrawOrangeAccentBox(object sender, PaintEventArgs e)
        {
            if (sender is Panel pnl)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 8;
                Rectangle rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                using (GraphicsPath path = UITheme.GetRoundedRectPath(rect, radius))
                using (Pen pen = new Pen(ColorTranslator.FromHtml("#FDBA74"), 1.2f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private void DrawPillBorder(object sender, PaintEventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Region = new Region(UITheme.GetRoundedRectPath(new Rectangle(0, 0, btn.Width, btn.Height), 6));
            }
        }

        private void KhoiTaoBangChiTietTam()
        {
            dtChiTietTam = new DataTable();
            dtChiTietTam.Columns.Add("Mã DV", typeof(string));
            dtChiTietTam.Columns.Add("Tên Dịch Vụ", typeof(string));
            dtChiTietTam.Columns.Add("SL", typeof(int));
            dtChiTietTam.Columns.Add("Đơn Giá", typeof(decimal));
            dtChiTietTam.Columns.Add("Thành Tiền", typeof(decimal));
            dgvChiTietDV.DataSource = dtChiTietTam;

            if (dgvChiTietDV.Columns["Mã DV"] != null) dgvChiTietDV.Columns["Mã DV"].FillWeight = 16;
            if (dgvChiTietDV.Columns["Tên Dịch Vụ"] != null) dgvChiTietDV.Columns["Tên Dịch Vụ"].FillWeight = 36;
            if (dgvChiTietDV.Columns["SL"] != null)
            {
                dgvChiTietDV.Columns["SL"].FillWeight = 12;
                dgvChiTietDV.Columns["SL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvChiTietDV.Columns["Đơn Giá"] != null)
            {
                dgvChiTietDV.Columns["Đơn Giá"].FillWeight = 18;
                dgvChiTietDV.Columns["Đơn Giá"].DefaultCellStyle.Format = "N0";
                dgvChiTietDV.Columns["Đơn Giá"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvChiTietDV.Columns["Thành Tiền"] != null)
            {
                dgvChiTietDV.Columns["Thành Tiền"].FillWeight = 18;
                dgvChiTietDV.Columns["Thành Tiền"].DefaultCellStyle.Format = "N0";
                dgvChiTietDV.Columns["Thành Tiền"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void TaiDanhSachPhuongThucTT()
        {
            cboPTTT.DataSource = null;
            cboPTTT.Items.Clear();
            try
            {
                var dsPTTT = ptttDAL.GetAll().Where(x => x.KichHoat).ToList();
                if (dsPTTT.Count > 0)
                {
                    cboPTTT.DisplayMember = "TenPhuongThuc";
                    cboPTTT.ValueMember = "TenPhuongThuc";
                    cboPTTT.DataSource = dsPTTT;
                    cboPTTT.SelectedIndex = 0;
                    return;
                }
            }
            catch { }

            cboPTTT.Items.Add(new { Text = "Tiền mặt (VND)", Value = "TM" });
            cboPTTT.Items.Add(new { Text = "Chuyển khoản VietQR", Value = "CK" });
            cboPTTT.Items.Add(new { Text = "Thẻ POS / Tín dụng", Value = "POS" });
            cboPTTT.DisplayMember = "Text";
            cboPTTT.ValueMember = "Value";
            if (cboPTTT.Items.Count > 0) cboPTTT.SelectedIndex = 0;
        }

        private void TaiDanhSachDichVu()
        {
            try
            {
                danhSachDichVu = dichVuDAL.GetAll();
                cboDichVu.DataSource = danhSachDichVu;
                cboDichVu.DisplayMember = "TenDichVu";
                cboDichVu.ValueMember = "MaDichVu";
            }
            catch { }
        }

        private void TaiDanhSachDatPhong()
        {
            cboDatPhong.Items.Clear();
            try
            {
                var dsChuaTT = ThanhToanDAL.GetDanhSachChuaThanhToan();
                foreach (var item in dsChuaTT)
                {
                    cboDatPhong.Items.Add(item);
                }
            }
            catch { }

            if (cboDatPhong.Items.Count == 0)
            {
                cboDatPhong.Items.Add("DP01 - Phòng P206 (Đỗ Thu Trang)");
                cboDatPhong.Items.Add("DP02 - Phòng P301 (Nguyễn Văn Hùng)");
            }
            if (cboDatPhong.Items.Count > 0) cboDatPhong.SelectedIndex = 0;
        }

        private void TaiDanhSachHoaDon()
        {
            try
            {
                danhSachHoaDon = hoaDonBLL.LayDanhSachHoaDon();
                dgvHoaDon.DataSource = null;
                dgvHoaDon.DataSource = danhSachHoaDon;

                // Ẩn bớt các cột chi tiết phụ để bảng thoáng, không bị co rút chữ thành dấu ...
                string[] anCot = { "NgayLap", "TienPhong", "TienDichVu", "TienGiamGia", "TienDatCoc", "MaPTTT", "MaVoucher", "TrangThai" };
                foreach (string col in anCot)
                {
                    if (dgvHoaDon.Columns[col] != null)
                        dgvHoaDon.Columns[col].Visible = false;
                }

                if (dgvHoaDon.Columns["MaHoaDon"] != null)
                {
                    dgvHoaDon.Columns["MaHoaDon"].HeaderText = "MÃ HĐ";
                    dgvHoaDon.Columns["MaHoaDon"].FillWeight = 24;
                    dgvHoaDon.Columns["MaHoaDon"].DefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                }
                if (dgvHoaDon.Columns["MaDatPhong"] != null)
                {
                    dgvHoaDon.Columns["MaDatPhong"].HeaderText = "ĐẶT PHÒNG";
                    dgvHoaDon.Columns["MaDatPhong"].FillWeight = 26;
                }
                if (dgvHoaDon.Columns["NgayThanhToan"] != null)
                {
                    dgvHoaDon.Columns["NgayThanhToan"].HeaderText = "NGÀY TT";
                    dgvHoaDon.Columns["NgayThanhToan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    dgvHoaDon.Columns["NgayThanhToan"].FillWeight = 25;
                }
                if (dgvHoaDon.Columns["TongTien"] != null)
                {
                    dgvHoaDon.Columns["TongTien"].HeaderText = "TỔNG THU";
                    dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                    dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    dgvHoaDon.Columns["TongTien"].DefaultCellStyle.ForeColor = UITheme.PrimaryOrange;
                    dgvHoaDon.Columns["TongTien"].FillWeight = 25;
                }

                int count = danhSachHoaDon.Count;
                decimal tongThu = danhSachHoaDon.Sum(x => x.TongTien);
                if (lblPillCount != null) lblPillCount.Text = $"Tổng số: {count} hóa đơn";
                if (lblPillRevenue != null) lblPillRevenue.Text = $"💰 Thực thu: {tongThu:N0} đ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải hóa đơn: " + ex.Message);
            }
        }

        private void cboDatPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Khi chọn lượt đặt phòng cần thanh toán, tự động bật lại nút thu tiền và tạo mã mới
            if (maHDDangChon != null)
            {
                maHDDangChon = null;
                txtMaHD.Text = "HD" + DateTime.Now.ToString("yyMMddHHmmss");
            }

            btnThanhToan.Enabled = true;
            btnThanhToan.BackColor = UITheme.PrimaryOrange;
            btnThanhToan.Text = "Xác nhận thu";

            if (cboDatPhong.SelectedItem is ThongTinDatPhongThanhToan dp)
            {
                txtTienPhong.Text = dp.TienPhong.ToString("N0");
                txtTienDatCoc.Text = dp.TienCoc.ToString("N0");
            }
            else if (cboDatPhong.SelectedIndex == 0)
            {
                txtTienPhong.Text = "2,900,000";
                txtTienDatCoc.Text = "1,000,000";
            }
            else
            {
                txtTienPhong.Text = "1,450,000";
                txtTienDatCoc.Text = "500,000";
            }
            TinhTongTienTuDong(null, null);
        }

        private void btnThemDV_Click(object sender, EventArgs e)
        {
            var dv = cboDichVu.SelectedItem as DichVu;
            if (dv == null) return;

            int sl = (int)nudSoLuongDV.Value;
            if (sl <= 0) return;

            decimal donGia = dv.DonGia;
            decimal thanhTien = sl * donGia;

            DataRow existing = null;
            foreach (DataRow r in dtChiTietTam.Rows)
            {
                if (r["Mã DV"].ToString() == dv.MaDichVu)
                {
                    existing = r;
                    break;
                }
            }

            if (existing != null)
            {
                int currentSl = Convert.ToInt32(existing["SL"]);
                existing["SL"] = currentSl + sl;
                existing["Thành Tiền"] = (currentSl + sl) * donGia;
            }
            else
            {
                dtChiTietTam.Rows.Add(dv.MaDichVu, dv.TenDichVu, sl, donGia, thanhTien);
            }

            decimal tongDV = 0;
            foreach (DataRow r in dtChiTietTam.Rows)
            {
                tongDV += Convert.ToDecimal(r["Thành Tiền"]);
            }
            txtTienDV.Text = tongDV.ToString("N0");
            TinhTongTienTuDong(null, null);
        }

        private void dgvChiTietDV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dtChiTietTam.Rows.Count)
            {
                dtChiTietTam.Rows.RemoveAt(e.RowIndex);
                decimal tongDV = 0;
                foreach (DataRow r in dtChiTietTam.Rows)
                {
                    tongDV += Convert.ToDecimal(r["Thành Tiền"]);
                }
                txtTienDV.Text = tongDV.ToString("N0");
                TinhTongTienTuDong(null, null);
            }
        }

        private void btnGoiYVoucher_Click(object sender, EventArgs e)
        {
            cmsVoucher.Items.Clear();
            try
            {
                var vouchers = voucherBLL.LayDanhSachVoucher();
                var activeVouchers = vouchers.FindAll(v => v.TrangThai && DateTime.Now.Date <= v.NgayHetHan.Date && DateTime.Now.Date >= v.NgayBatDau.Date);

                if (activeVouchers.Count == 0)
                {
                    cmsVoucher.Items.Add(new ToolStripMenuItem("Không có mã voucher nào đang khả dụng") { Enabled = false });
                }
                else
                {
                    decimal tienPhong = ParseMoney(txtTienPhong.Text);
                    foreach (var v in activeVouchers)
                    {
                        string giamStr = v.LoaiGiam == "PhanTram" ? $"Giảm {v.GiaTri}%" : $"Giảm {v.GiaTri:N0} đ";
                        string minStr = v.DonToiThieu > 0 ? $"(Đơn từ {v.DonToiThieu:N0} đ)" : "(Không giới hạn)";
                        string itemText = $"Mã {v.Code} — {v.TenChuongTrinh} [{giamStr} {minStr}]";

                        var item = new ToolStripMenuItem(itemText);
                        item.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
                        if (v.DonToiThieu > 0 && tienPhong < v.DonToiThieu)
                        {
                            item.ForeColor = Color.Gray;
                            item.ToolTipText = $"Cần tối thiểu {v.DonToiThieu:N0} đ tiền phòng";
                        }
                        else
                        {
                            item.ForeColor = UITheme.PrimaryOrange;
                            item.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                        }

                        item.Click += (s, ev) =>
                        {
                            txtVoucherCode.Text = v.Code;
                            btnApDungVoucher_Click(null, null);
                        };
                        cmsVoucher.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                cmsVoucher.Items.Add(new ToolStripMenuItem("Lỗi tải voucher: " + ex.Message) { Enabled = false });
            }

            cmsVoucher.Show(btnGoiYVoucher, new Point(0, btnGoiYVoucher.Height));
        }

        private void btnApDungVoucher_Click(object sender, EventArgs e)
        {
            string code = txtVoucherCode.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Vui lòng nhập mã Voucher.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var v = voucherBLL.TimVoucherTheoCode(code);

            if (v == null)
            {
                MessageBox.Show("Mã Voucher không tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!v.TrangThai || DateTime.Now.Date < v.NgayBatDau.Date || DateTime.Now.Date > v.NgayHetHan.Date)
            {
                MessageBox.Show("Mã Voucher đã hết hạn hoặc tạm khóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal tienPhong = ParseMoney(txtTienPhong.Text);
            if (v.DonToiThieu > 0 && tienPhong < v.DonToiThieu)
            {
                MessageBox.Show($"Tiền phòng chưa đạt đơn tối thiểu ({v.DonToiThieu:N0} đ)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal giamGia = hoaDonBLL.TinhGiamGiaVoucher(v, tienPhong);
            maVoucherDaApDung = v.MaVoucher;
            txtTienGiamGia.Text = giamGia.ToString("N0");
            TinhTongTienTuDong(null, null);

            MessageBox.Show($"Áp dụng thành công [{v.Code}]! Đã giảm: {giamGia:N0} đ", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TinhTongTienTuDong(object sender, EventArgs e)
        {
            decimal tp = ParseMoney(txtTienPhong.Text);
            decimal dv = ParseMoney(txtTienDV.Text);
            decimal gg = ParseMoney(txtTienGiamGia.Text);
            decimal coc = ParseMoney(txtTienDatCoc.Text);

            decimal canThu = hoaDonBLL.TinhTongThanhToan(tp, dv, gg, coc);

            lblTongTienValue.Text = canThu.ToString("N0") + " đ";
        }

        private decimal ParseMoney(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt)) return 0;
            // Dùng Regex lấy tất cả ký tự số và dấu trừ, tự động bỏ qua ký hiệu đ, dấu chấm, phẩy
            string clean = Regex.Replace(txt, @"[^\d\-]", "");
            if (decimal.TryParse(clean, out decimal val))
                return val;
            return 0;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string k = txtTimKiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(k))
            {
                dgvHoaDon.DataSource = danhSachHoaDon;

                string[] anCot = { "NgayLap", "TienPhong", "TienDichVu", "TienGiamGia", "TienDatCoc", "MaPTTT", "MaVoucher", "TrangThai" };
                foreach (string col in anCot)
                {
                    if (dgvHoaDon.Columns[col] != null)
                        dgvHoaDon.Columns[col].Visible = false;
                }
            }
            else
            {
                var loc = danhSachHoaDon.Where(h =>
                    (h.MaHoaDon != null && h.MaHoaDon.ToLower().Contains(k)) ||
                    (h.MaDatPhong != null && h.MaDatPhong.ToLower().Contains(k))).ToList();
                dgvHoaDon.DataSource = loc;
            }
        }

        private void dgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow == null) return;
            var h = dgvHoaDon.CurrentRow.DataBoundItem as HoaDon;
            if (h == null) return;

            maHDDangChon = h.MaHoaDon;
            txtMaHD.Text = h.MaHoaDon;
            txtTienPhong.Text = h.TienPhong.ToString("N0");
            txtTienDV.Text = h.TienDichVu.ToString("N0");
            txtTienGiamGia.Text = h.TienGiamGia.ToString("N0");
            txtTienDatCoc.Text = h.TienDatCoc.ToString("N0");
            lblTongTienValue.Text = h.TongTien.ToString("N0") + " đ";

            lblCardTitle.Text = $"🧾 Hóa đơn đã thu: [{h.MaHoaDon}]";
            lblCardSubtitle.Text = $"Thanh toán lúc {h.NgayThanhToan:dd/MM/yyyy HH:mm} (Chế độ xem lịch sử)";

            // Khi click xem một hóa đơn cũ đã thanh toán xong:
            btnThanhToan.Enabled = false;
            btnThanhToan.BackColor = Color.FromArgb(148, 163, 184);
            btnThanhToan.Text = "Đã thanh toán";

            DataTable dtChiTiet = chiTietDAL.LayChiTietTheoHoaDon(h.MaHoaDon);
            dtChiTietTam.Rows.Clear();
            foreach (DataRow r in dtChiTiet.Rows)
            {
                dtChiTietTam.Rows.Add(r["Mã DV"], r["Tên Dịch Vụ"], r["SL"], r["Đơn Giá"], r["Thành Tiền"]);
            }
        }

        private string LayMaPTTT()
        {
            if (cboPTTT.SelectedItem is PhuongThucThanhToanModel m)
            {
                string ten = (m.TenPhuongThuc ?? "").ToLower();
                if (ten.Contains("chuyển khoản") || ten.Contains("qr")) return "CK";
                if (ten.Contains("thẻ") || ten.Contains("pos")) return "POS";
                if (ten.Contains("tiền mặt")) return "TM";
                return m.TenPhuongThuc.Length <= 10 ? m.TenPhuongThuc : m.TenPhuongThuc.Substring(0, 10);
            }
            return cboPTTT.SelectedValue?.ToString() ?? "TM";
        }

        private void btnVietQR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaHD.Text))
            {
                txtMaHD.Text = "HD" + DateTime.Now.ToString("yyMMddHHmmss");
            }

            decimal canThu = ParseMoney(lblTongTienValue.Text);
            if (canThu <= 0)
            {
                MessageBox.Show("Số tiền thanh toán phải lớn hơn 0 để tạo mã VietQR.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string noiDung = $"THANH TOAN {txtMaHD.Text.Trim().ToUpper()}";
            using (var qrForm = new frmVietQR(txtMaHD.Text.Trim().ToUpper(), canThu, noiDung))
            {
                if (qrForm.ShowDialog(this) == DialogResult.OK)
                {
                    for (int i = 0; i < cboPTTT.Items.Count; i++)
                    {
                        string itemStr = cboPTTT.GetItemText(cboPTTT.Items[i]);
                        if (itemStr.IndexOf("chuyển khoản", StringComparison.OrdinalIgnoreCase) >= 0 ||
                            itemStr.IndexOf("qr", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            cboPTTT.SelectedIndex = i;
                            break;
                        }
                    }

                    var hoi = MessageBox.Show($"Khách đã quét VietQR thành công ({canThu:N0} đ).\nBạn có muốn xác nhận hoàn tất thanh toán hóa đơn ngay bây giờ?", "Xác nhận thu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (hoi == DialogResult.Yes)
                    {
                        btnThanhToan_Click(sender, e);
                    }
                }
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaHD.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maHD = txtMaHD.Text.Trim().ToUpper();

            // Kiểm tra trùng lặp khóa chính
            if (danhSachHoaDon.Any(x => string.Equals(x.MaHoaDon, maHD, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show($"Hóa đơn [{maHD}] đã tồn tại và đã được thanh toán trước đó!\nNếu muốn lập hóa đơn mới cho phòng khác, vui lòng bấm nút 'Làm mới'.", "Cảnh báo trùng hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maDP = (cboDatPhong.SelectedItem is ThongTinDatPhongThanhToan dpItem)
                ? dpItem.MaHienThi
                : cboDatPhong.Text.Split(' ')[0];

            decimal canThu = ParseMoney(lblTongTienValue.Text);
            var hd = new HoaDon
            {
                MaHoaDon = maHD,
                MaDatPhong = maDP,
                TienPhong = ParseMoney(txtTienPhong.Text),
                TienDichVu = ParseMoney(txtTienDV.Text),
                TienGiamGia = ParseMoney(txtTienGiamGia.Text),
                TienDatCoc = ParseMoney(txtTienDatCoc.Text),
                TongTien = canThu,
                NgayThanhToan = DateTime.Now,
                MaPTTT = LayMaPTTT(),
                MaVoucher = maVoucherDaApDung,
                TrangThai = "Đã thanh toán"
            };

            if (LayMaPTTT() == "CK")
            {
                using (var qr = new frmVietQR(hd.MaHoaDon, canThu, $"THANH TOAN {hd.MaHoaDon}"))
                {
                    if (qr.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }
                }
            }

            if (hoaDonBLL.ThanhToanVaTraPhong(hd, maDP))
            {
                foreach (DataRow r in dtChiTietTam.Rows)
                {
                    string maDV = r["Mã DV"].ToString();
                    int sl = Convert.ToInt32(r["SL"]);
                    decimal donGia = Convert.ToDecimal(r["Đơn Giá"]);
                    chiTietDAL.LuuChiTiet(hd.MaHoaDon, maDV, sl, donGia);
                }

                MessageBox.Show($"Thanh toán hóa đơn [{hd.MaHoaDon}] thành công! Thực thu: {canThu:N0} đ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachDatPhong();
                TaiDanhSachHoaDon();
                LamMoiForm();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e) => LamMoiForm();

        private void LamMoiForm()
        {
            maHDDangChon = null;
            maVoucherDaApDung = null;
            txtMaHD.Text = "HD" + DateTime.Now.ToString("yyMMddHHmmss");
            txtVoucherCode.Clear();
            txtTienGiamGia.Text = "0";
            txtTienDV.Text = "0";
            dtChiTietTam.Rows.Clear();
            cboDatPhong_SelectedIndexChanged(null, null);
            txtTimKiem.Clear();
            dgvHoaDon.ClearSelection();

            lblCardTitle.Text = "LẬP HÓA ĐƠN & THANH TOÁN";
            lblCardSubtitle.Text = "Chọn lượt đặt phòng, cộng dịch vụ và xác nhận thu ngân";

            btnThanhToan.Enabled = true;
            btnThanhToan.BackColor = UITheme.PrimaryOrange;
            btnThanhToan.Text = "Xác nhận thu";
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHD.Text))
            {
                MessageBox.Show("Vui lòng chọn hoặc lập hóa đơn trước khi in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("Bill80mm", 315, 780);
                printPreviewDlg.StartPosition = FormStartPosition.CenterScreen;
                printPreviewDlg.Width = 480;
                printPreviewDlg.Height = 700;
                printPreviewDlg.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in hóa đơn: " + ex.Message, "Lỗi in", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fHeader = new Font("Segoe UI", 13, FontStyle.Bold);
            Font fBold = new Font("Segoe UI", 9, FontStyle.Bold);
            Font fRegular = new Font("Segoe UI", 8.5f, FontStyle.Regular);
            Font fSmall = new Font("Segoe UI", 8, FontStyle.Italic);

            Brush b = Brushes.Black;
            Pen pDash = new Pen(Color.Black, 1) { DashStyle = DashStyle.Dash };

            int y = 20;
            int w = 285;

            StringFormat sfCenter = new StringFormat { Alignment = StringAlignment.Center };
            g.DrawString("AN NHIÊN HOTEL", fHeader, b, new RectangleF(0, y, w, 24), sfCenter);
            y += 24;
            g.DrawString("615 Âu Cơ, Phường Tân Phú, TP. Hồ Chí Minh", fSmall, b, new RectangleF(0, y, w, 18), sfCenter);
            y += 18;
            g.DrawString("Hotline: 0901 234 567", fSmall, b, new RectangleF(0, y, w, 18), sfCenter);
            y += 25;

            g.DrawString("PHIẾU THANH TOÁN", fBold, b, new RectangleF(0, y, w, 20), sfCenter);
            y += 22;
            g.DrawLine(pDash, 10, y, w - 10, y);
            y += 10;

            g.DrawString($"Số HĐ: {txtMaHD.Text}", fRegular, b, 15, y); y += 18;
            g.DrawString($"Ngày lập: {DateTime.Now:dd/MM/yyyy HH:mm}", fRegular, b, 15, y); y += 18;
            g.DrawString($"Hồ sơ: {cboDatPhong.Text}", fRegular, b, 15, y); y += 22;
            g.DrawLine(pDash, 10, y, w - 10, y);
            y += 10;

            if (dtChiTietTam.Rows.Count > 0)
            {
                g.DrawString("DỊCH VỤ SỬ DỤNG:", fBold, b, 15, y);
                y += 18;
                foreach (DataRow r in dtChiTietTam.Rows)
                {
                    string ten = r["Tên Dịch Vụ"].ToString();
                    string tt = $"{Convert.ToDecimal(r["Thành Tiền"]):N0} đ";

                    g.DrawString(ten, fRegular, b, 15, y);
                    g.DrawString(tt, fRegular, b, w - 15 - (int)g.MeasureString(tt, fRegular).Width, y);
                    y += 16;
                }
                g.DrawLine(pDash, 10, y, w - 10, y);
                y += 10;
            }

            DrawRowBill(g, fRegular, "Tiền phòng:", $"{ParseMoney(txtTienPhong.Text):N0} đ", y, w); y += 18;
            DrawRowBill(g, fRegular, "Tiền dịch vụ:", $"{ParseMoney(txtTienDV.Text):N0} đ", y, w); y += 18;
            DrawRowBill(g, fRegular, "Giảm giá (Voucher):", $"-{ParseMoney(txtTienGiamGia.Text):N0} đ", y, w); y += 18;
            DrawRowBill(g, fRegular, "Đã đặt cọc:", $"-{ParseMoney(txtTienDatCoc.Text):N0} đ", y, w); y += 22;

            g.DrawLine(Pens.Black, 10, y, w - 10, y);
            y += 8;

            DrawRowBill(g, fBold, "THỰC THU:", lblTongTienValue.Text, y, w);
            y += 24;

            g.DrawLine(pDash, 10, y, w - 10, y);
            y += 8;

            g.DrawString("QUÉT MÃ THANH TOÁN VIETQR", fBold, b, new RectangleF(0, y, w, 18), sfCenter);
            y += 18;

            using (Bitmap qrBmp = frmVietQR.TaoAnhQROffline(100))
            {
                int qrX = (w - 100) / 2;
                g.DrawImage(qrBmp, qrX, y, 100, 100);
            }
            y += 105;

            g.DrawString("MB Bank: 0901234567 | KHACH SAN LUXURY", fSmall, b, new RectangleF(0, y, w, 16), sfCenter);
            y += 16;
            g.DrawString($"Nội dung: THANH TOAN {txtMaHD.Text}", fSmall, b, new RectangleF(0, y, w, 16), sfCenter);
            y += 20;

            g.DrawLine(pDash, 10, y, w - 10, y);
            y += 10;

            g.DrawString("Cảm ơn Quý khách & Hẹn gặp lại!", fBold, b, new RectangleF(0, y, w, 20), sfCenter);
        }

        private void DrawRowBill(Graphics g, Font font, string leftText, string rightText, int y, int w)
        {
            g.DrawString(leftText, font, Brushes.Black, 15, y);
            int textW = (int)g.MeasureString(rightText, font).Width;
            g.DrawString(rightText, font, Brushes.Black, w - 15 - textW, y);
        }
    }
}
