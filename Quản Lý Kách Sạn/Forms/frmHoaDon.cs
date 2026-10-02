using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;
using QuanLyKhachSan.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmHoaDon : Form
    {
        private HoaDonBLL bll = new HoaDonBLL();
        private VoucherDAL voucherDal = new VoucherDAL();
        private Voucher currentVoucher = null;

        private decimal tienPhong = 1450000m * 2;
        private decimal tienCoc = 1000000m;
        private List<ChiTietHoaDon> listDV = new List<ChiTietHoaDon>();

        private Label lblTienPhongVal = new Label();
        private Label lblTienDVVal = new Label();
        private Label lblCocVal = new Label();
        private Label lblGiamGiaVal = new Label();
        private Label lblTongCongVal = new Label();
        private TextBox txtMaVoucherInput = new TextBox();
        private ComboBox cboPTTT = new ComboBox();
        private DataGridView dgvDichVu = new DataGridView();

        public frmHoaDon()
        {
            InitializeComponent();
            SetupCustomUI();
            LoadMockData();
            CapNhatChiPhi();
        }

        private void SetupCustomUI()
        {
            this.Text = "Hóa Đơn & Thanh Toán";
            this.Size = new Size(1180, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = UITheme.BgContent;

            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = UITheme.HeaderSidebarNavy };
            Label lblTitle = new Label
            {
                Text = "KHÁCH SẠN AN NHIÊN - THANH TOÁN TRẢ PHÒNG",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(20, 16),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblTitle);
            this.Controls.Add(pnlHeader);

            Panel pnlLeft = new Panel { Location = new Point(25, 75), Size = new Size(680, 580), BackColor = Color.White };
            pnlLeft.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, pnlLeft.ClientRectangle, UITheme.BorderColor, ButtonBorderStyle.Solid);

            Label lblInfo = new Label
            {
                Text = "Chi tiết tiêu dùng - Phòng 206 (Deluxe)",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = UITheme.TextDark,
                Location = new Point(15, 15),
                AutoSize = true
            };
            pnlLeft.Controls.Add(lblInfo);

            dgvDichVu.Location = new Point(15, 55);
            dgvDichVu.Size = new Size(650, 500);
            UITheme.StyleModernGrid(dgvDichVu);
            pnlLeft.Controls.Add(dgvDichVu);
            this.Controls.Add(pnlLeft);

            Panel pnlRight = new Panel { Location = new Point(725, 75), Size = new Size(410, 580), BackColor = Color.White };
            pnlRight.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, pnlRight.ClientRectangle, UITheme.BorderColor, ButtonBorderStyle.Solid);

            Label lblRightTitle = new Label
            {
                Text = "Tóm tắt chi phí",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = UITheme.TextDark,
                Location = new Point(20, 15),
                AutoSize = true
            };
            pnlRight.Controls.Add(lblRightTitle);

            int y = 65;
            AddSummaryRow(pnlRight, "Tiền phòng (2 đêm):", lblTienPhongVal, ref y, null);
            AddSummaryRow(pnlRight, "Tiền dịch vụ:", lblTienDVVal, ref y, null);
            AddSummaryRow(pnlRight, "Đã đặt cọc / tạm ứng:", lblCocVal, ref y, Color.ForestGreen);

            Label lblVoucher = new Label { Text = "Mã Voucher:", Location = new Point(20, y + 5), AutoSize = true, ForeColor = UITheme.TextMuted, Font = new Font("Segoe UI", 9.5F) };
            txtMaVoucherInput.Location = new Point(130, y);
            txtMaVoucherInput.Size = new Size(140, 26);
            txtMaVoucherInput.Font = new Font("Segoe UI", 9.5F);

            Button btnApply = new Button { Text = "Áp dụng", Location = new Point(280, y - 1), Size = new Size(100, 28) };
            UITheme.StylePrimaryButton(btnApply);
            btnApply.BackColor = UITheme.AccentBlue;
            btnApply.Click += BtnApply_Click;

            pnlRight.Controls.AddRange(new Control[] { lblVoucher, txtMaVoucherInput, btnApply });
            y += 40;

            AddSummaryRow(pnlRight, "Giảm trừ voucher:", lblGiamGiaVal, ref y, Color.Crimson);

            Panel line = new Panel { Location = new Point(20, y + 10), Size = new Size(365, 1), BackColor = UITheme.BorderColor };
            pnlRight.Controls.Add(line);
            y += 25;

            Label lblTongLabel = new Label { Text = "TỔNG THANH TOÁN:", Location = new Point(20, y), AutoSize = true, Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = UITheme.TextDark };
            lblTongCongVal.Location = new Point(180, y - 2);
            lblTongCongVal.Size = new Size(205, 30);
            lblTongCongVal.TextAlign = ContentAlignment.MiddleRight;
            lblTongCongVal.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTongCongVal.ForeColor = UITheme.AccentOrange;
            pnlRight.Controls.AddRange(new Control[] { lblTongLabel, lblTongCongVal });
            y += 45;

            Label lblPT = new Label { Text = "Phương thức:", Location = new Point(20, y + 5), AutoSize = true, ForeColor = UITheme.TextMuted, Font = new Font("Segoe UI", 9.5F) };
            cboPTTT.Location = new Point(130, y);
            cboPTTT.Size = new Size(250, 28);
            cboPTTT.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPTTT.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản QR", "Thẻ tín dụng / POS" });
            cboPTTT.SelectedIndex = 0;
            pnlRight.Controls.AddRange(new Control[] { lblPT, cboPTTT });
            y += 55;

            Button btnCheckout = new Button { Text = "XÁC NHẬN THANH TOÁN & IN", Location = new Point(20, y), Size = new Size(365, 45) };
            UITheme.StylePrimaryButton(btnCheckout);
            btnCheckout.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnCheckout.Click += BtnCheckout_Click;
            pnlRight.Controls.Add(btnCheckout);

            this.Controls.Add(pnlRight);
        }

        private void AddSummaryRow(Panel pnl, string title, Label lblVal, ref int y, Color? valColor)
        {
            Label lblT = new Label { Text = title, Location = new Point(20, y), AutoSize = true, ForeColor = UITheme.TextMuted, Font = new Font("Segoe UI", 9.5F) };
            lblVal.Location = new Point(190, y);
            lblVal.Size = new Size(195, 20);
            lblVal.TextAlign = ContentAlignment.MiddleRight;
            lblVal.ForeColor = valColor.HasValue ? valColor.Value : UITheme.TextDark;
            lblVal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            pnl.Controls.AddRange(new Control[] { lblT, lblVal });
            y += 35;
        }

        private void LoadMockData()
        {
            listDV = new List<ChiTietHoaDon>
            {
                new ChiTietHoaDon { TenDichVu = "Nước ngọt lon", SoLuong = 3, DonGia = 20000 },
                new ChiTietHoaDon { TenDichVu = "Giặt ủi quần áo", SoLuong = 2, DonGia = 45000 },
                new ChiTietHoaDon { TenDichVu = "Bữa sáng Buffet", SoLuong = 2, DonGia = 120000 }
            };

            dgvDichVu.DataSource = null;
            dgvDichVu.DataSource = listDV;
            if (dgvDichVu.Columns["MaHoaDon"] != null) dgvDichVu.Columns["MaHoaDon"].Visible = false;
            if (dgvDichVu.Columns["MaDichVu"] != null) dgvDichVu.Columns["MaDichVu"].Visible = false;
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            string code = txtMaVoucherInput.Text.Trim();
            if (string.IsNullOrEmpty(code))
            {
                currentVoucher = null;
                CapNhatChiPhi();
                return;
            }

            try
            {
                currentVoucher = voucherDal.GetByCode(code);
            }
            catch { }

            if (currentVoucher == null)
            {
                currentVoucher = new Voucher { Code = code, LoaiGiam = "PhanTram", GiaTri = 10, TrangThai = true, NgayHetHan = DateTime.Now.AddDays(10) };
            }
            MessageBox.Show("Áp dụng mã ưu đãi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CapNhatChiPhi();
        }

        private void CapNhatChiPhi()
        {
            decimal tienDV = bll.TinhTongTienDichVu(listDV);
            decimal tienGiam = bll.TinhGiamGiaVoucher(currentVoucher, tienPhong + tienDV);
            decimal tong = bll.TinhTongThanhToan(tienPhong, tienDV, tienGiam, tienCoc);

            lblTienPhongVal.Text = string.Format("{0:N0} đ", tienPhong);
            lblTienDVVal.Text = string.Format("{0:N0} đ", tienDV);
            lblCocVal.Text = string.Format("- {0:N0} đ", tienCoc);
            lblGiamGiaVal.Text = string.Format("- {0:N0} đ", tienGiam);
            lblTongCongVal.Text = string.Format("{0:N0} đ", tong);
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            string pttt = cboPTTT.SelectedItem != null ? cboPTTT.SelectedItem.ToString() : "Tiền mặt";
            DialogResult dr = MessageBox.Show(string.Format("Xác nhận thanh toán số tiền {0} qua [{1}] và trả phòng?", lblTongCongVal.Text, pttt), "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                MessageBox.Show("Thanh toán thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}