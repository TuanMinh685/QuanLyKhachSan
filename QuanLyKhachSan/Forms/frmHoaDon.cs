using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmHoaDon : Form
    {
        private string maHDDangChon = null;
        private HoaDonBLL hoaDonBLL = new HoaDonBLL();
        private DichVuDAL dichVuDAL = new DichVuDAL();
        private VoucherBLL voucherBLL = new VoucherBLL();
        private ChiTietHoaDonDAL chiTietDAL = new ChiTietHoaDonDAL();
        private PhuongThucThanhToanDAL ptttDAL = new PhuongThucThanhToanDAL();

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
            UITheme.StyleModernGrid(dgvHoaDon);
            UITheme.StyleModernGrid(dgvChiTietDV);
            KhoiTaoBangChiTietTam();

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
                    dgvHoaDon.Columns["TongTien"].DefaultCellStyle.ForeColor = UITheme.PrimaryOrange;
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

        private void cboDatPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                int slMoi = Convert.ToInt32(existing["SL"]) + sl;
                existing["SL"] = slMoi;
                existing["Thành Tiền"] = slMoi * donGia;
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
                txtMaHD.Text = "HD" + DateTime.Now.ToString("yyMMddHHmm");
            }

            decimal canThu = ParseMoney(lblTongTienValue.Text.Replace(" Ä‘", ""));
            if (canThu <= 0)
            {
                MessageBox.Show("Sá»‘ tiá»n thanh toÃ¡n pháº£i lá»›n hÆ¡n 0 Ä‘á»ƒ táº¡o mÃ£ VietQR.", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        if (itemStr.IndexOf("chuyá»ƒn khoáº£n", StringComparison.OrdinalIgnoreCase) >= 0 ||
                            itemStr.IndexOf("qr", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            cboPTTT.SelectedIndex = i;
                            break;
                        }
                    }

                    var hoi = MessageBox.Show($"KhÃ¡ch Ä‘Ã£ quÃ©t VietQR thÃ nh cÃ´ng ({canThu:N0} Ä‘).\nBáº¡n cÃ³ muá»‘n xÃ¡c nháº­n hoÃ n táº¥t thanh toÃ¡n hÃ³a Ä‘Æ¡n ngay bÃ¢y giá»?", "XÃ¡c nháº­n thu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

            string maDP = (cboDatPhong.SelectedItem is ThongTinDatPhongThanhToan dpItem)
                ? dpItem.MaHienThi
                : cboDatPhong.Text.Split(' ')[0];

            decimal canThu = ParseMoney(lblTongTienValue.Text.Replace(" đ", ""));
            var hd = new HoaDon
            {
                MaHoaDon = txtMaHD.Text.Trim().ToUpper(),
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

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHD.Text))
            {
                MessageBox.Show("Vui lòng chọn hoặc lập hóa đơn trước khi in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("Bill80mm", 315, 600);
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
            y += 28;

            g.DrawString("Cảm ơn Quý khách & Hẹn gặp lại!", fSmall, b, new RectangleF(0, y, w, 20), sfCenter);
        }

        private void DrawRowBill(Graphics g, Font font, string leftText, string rightText, int y, int w)
        {
            g.DrawString(leftText, font, Brushes.Black, 15, y);
            int textW = (int)g.MeasureString(rightText, font).Width;
            g.DrawString(rightText, font, Brushes.Black, w - 15 - textW, y);
        }
    }
}