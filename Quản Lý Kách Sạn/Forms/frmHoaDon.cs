using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmHoaDon : Form
    {
        private string maHDDangChon = null;
        private HoaDonDAL hoaDonDAL = new HoaDonDAL();
        private DichVuDAL dichVuDAL = new DichVuDAL();
        private VoucherDAL voucherDAL = new VoucherDAL();
        private ChiTietHoaDonDAL chiTietDAL = new ChiTietHoaDonDAL();

        private List<HoaDon> danhSachHoaDon = new List<HoaDon>();
        private List<DichVu> danhSachDichVu = new List<DichVu>();
        private DataTable dtChiTietTam = new DataTable();

        private int? maVoucherDaApDung = null;

        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            ThietKeBangChuanFigma(dgvHoaDon);
            ThietKeBangChuanFigma(dgvChiTietDV);
            KhoiTaoBangChiTietTam();

            TaiDanhSachPhuongThucTT();
            TaiDanhSachDichVu();
            TaiDanhSachDatPhong();
            TaiDanhSachHoaDon();

            LamMoiForm();
        }

        #region Styling & Format

        private void DrawFigmaCard(object sender, PaintEventArgs e)
        {
            if (sender is Panel pnl)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 10;
                Rectangle rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                using (GraphicsPath path = GetRoundedRectPath(rect, radius))
                using (Pen pen = new Pen(ColorTranslator.FromHtml("#E2E8F0"), 1.2f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private void DrawFigmaInputBox(object sender, PaintEventArgs e)
        {
            if (sender is Panel pnl)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 6;
                Rectangle rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                using (GraphicsPath path = GetRoundedRectPath(rect, radius))
                using (Pen pen = new Pen(ColorTranslator.FromHtml("#CBD5E1"), 1f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private void DrawOrangeAccentBox(object sender, PaintEventArgs e)
        {
            if (sender is Panel pnl)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 8;
                Rectangle rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                using (GraphicsPath path = GetRoundedRectPath(rect, radius))
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
                btn.Region = new Region(GetRoundedRectPath(new Rectangle(0, 0, btn.Width, btn.Height), 6));
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2F;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void ThietKeBangChuanFigma(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = ColorTranslator.FromHtml("#F1F5F9");
            dgv.BackgroundColor = Color.White;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F8FAFC");
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#475569");
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 6, 8, 6);
            dgv.ColumnHeadersHeight = 36;

            dgv.RowTemplate.Height = 34;
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#1E293B");
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9f);
            dgv.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#EFF6FF");
            dgv.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#1D4ED8");
        }

        #endregion

        #region Init & Load Data

        private void KhoiTaoBangChiTietTam()
        {
            dtChiTietTam = new DataTable();
            dtChiTietTam.Columns.Add("Mã DV", typeof(string));
            dtChiTietTam.Columns.Add("Tên Dịch Vụ", typeof(string));
            dtChiTietTam.Columns.Add("SL", typeof(int));
            dtChiTietTam.Columns.Add("Đơn Giá", typeof(decimal));
            dtChiTietTam.Columns.Add("Thành Tiền", typeof(decimal));
            dgvChiTietDV.DataSource = dtChiTietTam;
        }

        private void TaiDanhSachPhuongThucTT()
        {
            cboPTTT.Items.Clear();
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
            cboDatPhong.Items.Add("DP01 - Phòng P206 (Đỗ Thu Trang)");
            cboDatPhong.Items.Add("DP02 - Phòng P301 (Nguyễn Văn Hùng)");
            if (cboDatPhong.Items.Count > 0) cboDatPhong.SelectedIndex = 0;
        }

        private void TaiDanhSachHoaDon()
        {
            try
            {
                danhSachHoaDon = hoaDonDAL.GetAll();
                dgvHoaDon.DataSource = null;
                dgvHoaDon.DataSource = danhSachHoaDon;

                if (dgvHoaDon.Columns["MaHoaDon"] != null)
                {
                    dgvHoaDon.Columns["MaHoaDon"].HeaderText = "MÃ HĐ";
                    dgvHoaDon.Columns["MaHoaDon"].FillWeight = 85;
                    dgvHoaDon.Columns["MaHoaDon"].DefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                }
                if (dgvHoaDon.Columns["TongTien"] != null)
                {
                    dgvHoaDon.Columns["TongTien"].HeaderText = "TỔNG THU";
                    dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                    dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    dgvHoaDon.Columns["TongTien"].DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#EA580C");
                }
                if (dgvHoaDon.Columns["NgayThanhToan"] != null)
                {
                    dgvHoaDon.Columns["NgayThanhToan"].HeaderText = "NGÀY TT";
                    dgvHoaDon.Columns["NgayThanhToan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải hóa đơn: " + ex.Message);
            }
        }

        #endregion

        #region Business Logic & Voucher

        private void cboDatPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDatPhong.SelectedIndex == 0)
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
            decimal donGia = dv.DonGia;
            decimal thanhTien = sl * donGia;

            // Tìm xem món đã có trong bảng tạm chưa
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
                int slMoi = Convert.ToInt32(existing["SL"]) + sl;
                existing["SL"] = slMoi;
                existing["Thành Tiền"] = slMoi * donGia;
            }
            else
            {
                dtChiTietTam.Rows.Add(dv.MaDichVu, dv.TenDichVu, sl, donGia, thanhTien);
            }

            // Tính tổng tiền dịch vụ và hiển thị
            decimal tongDV = 0;
            foreach (DataRow r in dtChiTietTam.Rows)
            {
                tongDV += Convert.ToDecimal(r["Thành Tiền"]);
            }
            txtTienDV.Text = tongDV.ToString("N0");
            TinhTongTienTuDong(null, null);
        }

        private void btnApDungVoucher_Click(object sender, EventArgs e)
        {
            string code = txtVoucherCode.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Vui lòng nhập mã Voucher.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dsVoucher = voucherDAL.GetAll();
            var v = dsVoucher.FirstOrDefault(x => x.Code != null && x.Code.ToUpper() == code);

            if (v == null)
            {
                MessageBox.Show("Mã Voucher không tồn tại trên hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!v.TrangThai || DateTime.Now.Date < v.NgayBatDau.Date || DateTime.Now.Date > v.NgayHetHan.Date)
            {
                MessageBox.Show("Mã Voucher này hiện không hoạt động hoặc đã hết hạn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal tienPhong = ParseMoney(txtTienPhong.Text);
            if (v.DonToiThieu > 0 && tienPhong < v.DonToiThieu)
            {
                MessageBox.Show($"Đơn phòng chưa đạt mức tối thiểu ({v.DonToiThieu:N0} đ) để áp dụng voucher này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal giamGia = 0;
            if (v.LoaiGiam == "PhanTram")
            {
                giamGia = tienPhong * (v.GiaTri / 100);
            }
            else
            {
                giamGia = v.GiaTri;
            }

            maVoucherDaApDung = v.MaVoucher;
            txtTienGiamGia.Text = giamGia.ToString("N0");
            TinhTongTienTuDong(null, null);

            MessageBox.Show($"Áp dụng thành công Voucher [{v.Code}]! Đã giảm: {giamGia:N0} đ", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TinhTongTienTuDong(object sender, EventArgs e)
        {
            decimal tp = ParseMoney(txtTienPhong.Text);
            decimal dv = ParseMoney(txtTienDV.Text);
            decimal gg = ParseMoney(txtTienGiamGia.Text);
            decimal coc = ParseMoney(txtTienDatCoc.Text);

            decimal canThu = (tp + dv) - gg - coc;
            if (canThu < 0) canThu = 0;

            lblTongTienValue.Text = canThu.ToString("N0") + " đ";
        }

        private decimal ParseMoney(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt)) return 0;
            if (decimal.TryParse(txt.Replace(",", "").Replace(".", "").Trim(), out decimal val))
                return val;
            return 0;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string k = txtTimKiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(k))
            {
                dgvHoaDon.DataSource = danhSachHoaDon;
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

            // Tải chi tiết dịch vụ đã gọi từ database
            DataTable dtChiTiet = chiTietDAL.LayChiTietTheoHoaDon(h.MaHoaDon);
            dtChiTietTam.Rows.Clear();
            foreach (DataRow r in dtChiTiet.Rows)
            {
                dtChiTietTam.Rows.Add(r["Mã DV"], r["Tên Dịch Vụ"], r["SL"], r["Đơn Giá"], r["Thành Tiền"]);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaHD.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn thanh toán.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal canThu = ParseMoney(lblTongTienValue.Text.Replace(" đ", ""));
            var hd = new HoaDon
            {
                MaHoaDon = txtMaHD.Text.Trim().ToUpper(),
                MaDatPhong = cboDatPhong.Text.Split(' ')[0],
                TienPhong = ParseMoney(txtTienPhong.Text),
                TienDichVu = ParseMoney(txtTienDV.Text),
                TienGiamGia = ParseMoney(txtTienGiamGia.Text),
                TienDatCoc = ParseMoney(txtTienDatCoc.Text),
                TongTien = canThu,
                NgayThanhToan = DateTime.Now,
                MaVoucher = maVoucherDaApDung,
                TrangThai = "Đã thanh toán"
            };

            if (hoaDonDAL.Insert(hd))
            {
                // Lưu từng dòng dịch vụ vào CSDL
                foreach (DataRow r in dtChiTietTam.Rows)
                {
                    string maDV = r["Mã DV"].ToString();
                    int sl = Convert.ToInt32(r["SL"]);
                    decimal donGia = Convert.ToDecimal(r["Đơn Giá"]);
                    chiTietDAL.LuuChiTiet(hd.MaHoaDon, maDV, sl, donGia);
                }

                MessageBox.Show($"Thanh toán hóa đơn [{hd.MaHoaDon}] thành công! Thực thu: {canThu:N0} đ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachHoaDon();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e) => LamMoiForm();

        private void LamMoiForm()
        {
            maHDDangChon = null;
            maVoucherDaApDung = null;
            txtMaHD.Text = "HD" + DateTime.Now.ToString("yyMMddHHmm");
            txtVoucherCode.Clear();
            txtTienGiamGia.Text = "0";
            txtTienDV.Text = "0";
            dtChiTietTam.Rows.Clear();
            cboDatPhong_SelectedIndexChanged(null, null);
            txtTimKiem.Clear();
            dgvHoaDon.ClearSelection();
        }

        #endregion

        #region Print Preview & In Bill Nhiệt 80mm

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHD.Text))
            {
                MessageBox.Show("Vui lòng chọn hoặc lập hóa đơn trước khi in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("Bill80mm", 315, 600); // Khổ giấy 80mm
                printPreviewDlg.StartPosition = FormStartPosition.CenterScreen;
                printPreviewDlg.WindowState = FormWindowState.Normal;
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
            int w = 285; // Chiều rộng vùng in

            // 1. Tiêu đề khách sạn
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

            // 2. Thông tin hóa đơn & phòng
            g.DrawString($"Số HĐ: {txtMaHD.Text}", fRegular, b, 15, y);
            y += 18;
            g.DrawString($"Ngày lập: {DateTime.Now:dd/MM/yyyy HH:mm}", fRegular, b, 15, y);
            y += 18;
            g.DrawString($"Hồ sơ: {cboDatPhong.Text}", fRegular, b, 15, y);
            y += 22;
            g.DrawLine(pDash, 10, y, w - 10, y);
            y += 10;

            // 3. Danh sách dịch vụ sử dụng
            if (dtChiTietTam.Rows.Count > 0)
            {
                g.DrawString("DỊCH VỤ SỬ DỤNG:", fBold, b, 15, y);
                y += 18;
                foreach (DataRow r in dtChiTietTam.Rows)
                {
                    string ten = r["Tên Dịch Vụ"].ToString();
                    string slGia = $"{r["SL"]} x {Convert.ToDecimal(r["Đơn Giá"]):N0}";
                    string tt = $"{Convert.ToDecimal(r["Thành Tiền"]):N0} đ";

                    g.DrawString(ten, fRegular, b, 15, y);
                    g.DrawString(tt, fRegular, b, w - 15 - (int)g.MeasureString(tt, fRegular).Width, y);
                    y += 16;
                }
                g.DrawLine(pDash, 10, y, w - 10, y);
                y += 10;
            }

            // 4. Chi tiết các khoản thanh toán
            DrawRowBill(g, fRegular, "Tiền phòng:", $"{ParseMoney(txtTienPhong.Text):N0} đ", y, w); y += 18;
            DrawRowBill(g, fRegular, "Tiền dịch vụ:", $"{ParseMoney(txtTienDV.Text):N0} đ", y, w); y += 18;
            DrawRowBill(g, fRegular, "Giảm giá (Voucher):", $"-{ParseMoney(txtTienGiamGia.Text):N0} đ", y, w); y += 18;
            DrawRowBill(g, fRegular, "Đã đặt cọc:", $"-{ParseMoney(txtTienDatCoc.Text):N0} đ", y, w); y += 22;

            g.DrawLine(Pens.Black, 10, y, w - 10, y);
            y += 8;

            // 5. Tổng cần thu
            DrawRowBill(g, fBold, "THỰC THU:", lblTongTienValue.Text, y, w);
            y += 28;

            // 6. Lời cảm ơn
            g.DrawString("Cảm ơn Quý khách & Hẹn gặp lại!", fSmall, b, new RectangleF(0, y, w, 20), sfCenter);
        }

        private void DrawRowBill(Graphics g, Font font, string leftText, string rightText, int y, int w)
        {
            g.DrawString(leftText, font, Brushes.Black, 15, y);
            int textW = (int)g.MeasureString(rightText, font).Width;
            g.DrawString(rightText, font, Brushes.Black, w - 15 - textW, y);
        }

        #endregion
    }
}