using System;
using System.Drawing;
using System.Windows.Forms;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmApplePayModal : Form
    {
        private readonly string _maDon;
        private readonly decimal _soTien;
        private readonly string _thongTin;

        public string MaThamChieuPOS => txtPosRef?.Text?.Trim() ?? "";

        public frmApplePayModal(string maDon, decimal soTien, string thongTin = null)
        {
            _maDon = string.IsNullOrWhiteSpace(maDon) ? "AP" + DateTime.Now.ToString("yyMMddHHmm") : maDon;
            _soTien = soTien;
            _thongTin = string.IsNullOrWhiteSpace(thongTin) ? "Thanh toán phòng khách sạn" : thongTin;

            InitializeComponent();
            SetupCustomUI();
        }

        private void SetupCustomUI()
        {
            // 1. Header
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 84;
            pnlHeader.BackColor = Color.FromArgb(28, 28, 30);
            pnlHeader.Padding = new Padding(24, 16, 24, 14);
            pnlHeader.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(44, 44, 46), 1f);
                e.Graphics.DrawLine(pen, 0, pnlHeader.Height - 1, pnlHeader.Width, pnlHeader.Height - 1);
            };

            lblHeaderTitle.Text = "🍎 APPLE PAY / CONTACTLESS POS";
            lblHeaderTitle.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(24, 14);
            lblHeaderTitle.AutoSize = true;

            lblHeaderSubtitle.Text = "Chạm thiết bị iPhone, Apple Watch hoặc thẻ chip không tiếp xúc vào đầu đọc POS";
            lblHeaderSubtitle.Font = new Font("Segoe UI", 8.5F);
            lblHeaderSubtitle.ForeColor = Color.FromArgb(161, 161, 170);
            lblHeaderSubtitle.Location = new Point(24, 44);
            lblHeaderSubtitle.AutoSize = true;

            // 2. Footer Buttons
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Height = 74;
            pnlButtons.BackColor = Color.FromArgb(28, 28, 30);
            pnlButtons.Padding = new Padding(24, 14, 24, 14);
            pnlButtons.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(44, 44, 46), 1f);
                e.Graphics.DrawLine(pen, 0, 0, pnlButtons.Width, 0);
            };

            btnHuy.Text = "✕ Hủy bỏ";
            btnHuy.Location = new Point(24, 14);
            btnHuy.Size = new Size(110, 46);
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.BackColor = Color.FromArgb(39, 39, 42);
            btnHuy.ForeColor = Color.FromArgb(212, 212, 216);
            btnHuy.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnHuy.Cursor = Cursors.Hand;
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            btnXacNhan.Text = "✓ XÁC NHẬN ĐÃ CHẠM POS THÀNH CÔNG";
            btnXacNhan.Location = new Point(144, 14);
            btnXacNhan.Size = new Size(310, 46);
            btnXacNhan.FlatStyle = FlatStyle.Flat;
            btnXacNhan.BackColor = Color.FromArgb(0, 122, 255);
            btnXacNhan.ForeColor = Color.White;
            btnXacNhan.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnXacNhan.Cursor = Cursors.Hand;
            btnXacNhan.FlatAppearance.BorderSize = 0;
            btnXacNhan.Click += (s, e) =>
            {
                MessageBox.Show(this,
                    $"Đã ghi nhận thanh toán Apple Pay thành công!\nMã POS: {MaThamChieuPOS}\nSố tiền: {_soTien:N0} VND",
                    "Thanh toán Apple Pay thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            };

            // 3. Body Card
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.BackColor = Color.FromArgb(18, 18, 20);
            pnlCard.Padding = new Padding(24, 20, 24, 16);

            lblAppleIcon.Text = " Pay  •)))";
            lblAppleIcon.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblAppleIcon.ForeColor = Color.White;
            lblAppleIcon.TextAlign = ContentAlignment.MiddleCenter;
            lblAppleIcon.Location = new Point((500 - 300) / 2 - 10, 16);
            lblAppleIcon.Size = new Size(300, 46);

            lblTienTitle.Text = "TỔNG TIỀN THANH TOÁN QUA POS:";
            lblTienTitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblTienTitle.ForeColor = Color.FromArgb(161, 161, 170);
            lblTienTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTienTitle.Location = new Point(24, 70);
            lblTienTitle.Size = new Size(440, 20);

            lblTienVal.Text = _soTien.ToString("N0") + " VND";
            lblTienVal.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTienVal.ForeColor = Color.FromArgb(52, 199, 89);
            lblTienVal.TextAlign = ContentAlignment.MiddleCenter;
            lblTienVal.Location = new Point(24, 92);
            lblTienVal.Size = new Size(440, 38);

            // POS Info Panel
            pnlPosInfo.Location = new Point(24, 142);
            pnlPosInfo.Size = new Size(440, 280);
            pnlPosInfo.BackColor = Color.FromArgb(28, 28, 30);
            pnlPosInfo.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(44, 44, 46), 1f);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlPosInfo.Width - 1, pnlPosInfo.Height - 1);
            };

            int py = 16;
            lblPosTitle.Text = "THIẾT BỊ ĐẦU ĐỌC:";
            lblPosTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblPosTitle.ForeColor = Color.FromArgb(113, 113, 122);
            lblPosTitle.Location = new Point(16, py);
            lblPosTitle.AutoSize = true;

            lblPosDevice.Text = "🟢 SmartPOS Contactless NFC (Terminal #01)";
            lblPosDevice.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPosDevice.ForeColor = Color.White;
            lblPosDevice.Location = new Point(16, py + 20);
            lblPosDevice.Size = new Size(400, 22);
            py += 52;

            lblPosRefTitle.Text = "MÃ THAM CHIẾU GIAO DỊCH POS (REF CODE):";
            lblPosRefTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblPosRefTitle.ForeColor = Color.FromArgb(113, 113, 122);
            lblPosRefTitle.Location = new Point(16, py);
            lblPosRefTitle.AutoSize = true;

            string autoRef = "AP-" + DateTime.Now.ToString("yyMMdd") + "-" + new Random().Next(1000, 9999);
            txtPosRef.Text = autoRef;
            txtPosRef.Location = new Point(16, py + 22);
            txtPosRef.Size = new Size(405, 30);
            txtPosRef.BackColor = Color.FromArgb(18, 18, 20);
            txtPosRef.ForeColor = Color.FromArgb(244, 244, 245);
            txtPosRef.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            txtPosRef.BorderStyle = BorderStyle.FixedSingle;
            py += 64;

            lblGuide.Text = "📌 Hướng dẫn cho lễ tân:\n" +
                           "1. Hướng dẫn khách đưa iPhone hoặc Apple Watch gần biểu tượng sóng NFC trên máy POS.\n" +
                           "2. Khách hàng xác nhận bằng Face ID / Touch ID.\n" +
                           "3. Khi máy POS phát tiếng bíp và in bill, bấm nút xác nhận bên dưới.";
            lblGuide.Font = new Font("Segoe UI", 8.5F);
            lblGuide.ForeColor = Color.FromArgb(161, 161, 170);
            lblGuide.Location = new Point(16, py);
            lblGuide.Size = new Size(405, 80);
        }
    }
}
