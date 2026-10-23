using System;
using System.Drawing;
using System.Windows.Forms;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public class frmApplePayModal : Form
    {
        private readonly string _maDon;
        private readonly decimal _soTien;
        private readonly string _thongTin;

        private Panel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;

        private Panel pnlCard;
        private Label lblAppleIcon;
        private Label lblTienTitle;
        private Label lblTienVal;

        private Panel pnlPosInfo;
        private Label lblPosTitle;
        private Label lblPosDevice;
        private Label lblPosRefTitle;
        private TextBox txtPosRef;
        private Label lblGuide;

        private Panel pnlButtons;
        private Button btnXacNhan;
        private Button btnHuy;

        public string MaThamChieuPOS => txtPosRef?.Text?.Trim() ?? "";

        public frmApplePayModal(string maDon, decimal soTien, string thongTin = null)
        {
            _maDon = string.IsNullOrWhiteSpace(maDon) ? "AP" + DateTime.Now.ToString("yyMMddHHmm") : maDon;
            _soTien = soTien;
            _thongTin = string.IsNullOrWhiteSpace(thongTin) ? "Thanh toán phòng khách sạn" : thongTin;

            InitializeComponentCustom();
        }

        private void InitializeComponentCustom()
        {
            Text = "Thanh toán Apple Pay / Chạm Contactless POS";
            Size = new Size(500, 620);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(18, 18, 20); // Apple Dark theme
            Font = new Font("Segoe UI", 9.5F);

            // 1. Header
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 84,
                BackColor = Color.FromArgb(28, 28, 30),
                Padding = new Padding(24, 16, 24, 14)
            };
            pnlHeader.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(44, 44, 46), 1f);
                e.Graphics.DrawLine(pen, 0, pnlHeader.Height - 1, pnlHeader.Width, pnlHeader.Height - 1);
            };

            lblHeaderTitle = new Label
            {
                Text = "🍎 APPLE PAY / CONTACTLESS POS",
                Font = new Font("Segoe UI", 13.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(24, 14),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblHeaderTitle);

            lblHeaderSubtitle = new Label
            {
                Text = "Chạm thiết bị iPhone, Apple Watch hoặc thẻ chip không tiếp xúc vào đầu đọc POS",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(161, 161, 170),
                Location = new Point(24, 44),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblHeaderSubtitle);
            Controls.Add(pnlHeader);

            // 2. Footer Buttons
            pnlButtons = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 74,
                BackColor = Color.FromArgb(28, 28, 30),
                Padding = new Padding(24, 14, 24, 14)
            };
            pnlButtons.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(44, 44, 46), 1f);
                e.Graphics.DrawLine(pen, 0, 0, pnlButtons.Width, 0);
            };

            btnHuy = new Button
            {
                Text = "✕ Hủy bỏ",
                Location = new Point(24, 14),
                Size = new Size(110, 46),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(39, 39, 42),
                ForeColor = Color.FromArgb(212, 212, 216),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            pnlButtons.Controls.Add(btnHuy);

            btnXacNhan = new Button
            {
                Text = "✓ XÁC NHẬN ĐÃ CHẠM POS THÀNH CÔNG",
                Location = new Point(144, 14),
                Size = new Size(310, 46),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 122, 255), // Apple Blue
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
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
            pnlButtons.Controls.Add(btnXacNhan);
            Controls.Add(pnlButtons);

            // 3. Body Card
            pnlCard = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(18, 18, 20),
                Padding = new Padding(24, 20, 24, 16)
            };
            Controls.Add(pnlCard);

            // Apple Pay Wave Icon Badge
            lblAppleIcon = new Label
            {
                Text = " Pay  •)))",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point((500 - 300) / 2 - 10, 16),
                Size = new Size(300, 46)
            };
            pnlCard.Controls.Add(lblAppleIcon);

            lblTienTitle = new Label
            {
                Text = "TỔNG TIỀN THANH TOÁN QUA POS:",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(161, 161, 170),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(24, 70),
                Size = new Size(440, 20)
            };
            pnlCard.Controls.Add(lblTienTitle);

            lblTienVal = new Label
            {
                Text = _soTien.ToString("N0") + " VND",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 199, 89), // Apple Green
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(24, 92),
                Size = new Size(440, 38)
            };
            pnlCard.Controls.Add(lblTienVal);

            // POS Info Panel
            pnlPosInfo = new Panel
            {
                Location = new Point(24, 142),
                Size = new Size(440, 280),
                BackColor = Color.FromArgb(28, 28, 30)
            };
            pnlPosInfo.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(44, 44, 46), 1f);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlPosInfo.Width - 1, pnlPosInfo.Height - 1);
            };
            pnlCard.Controls.Add(pnlPosInfo);

            int py = 16;
            lblPosTitle = new Label
            {
                Text = "THIẾT BỊ ĐẦU ĐỌC:",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(113, 113, 122),
                Location = new Point(16, py),
                AutoSize = true
            };
            pnlPosInfo.Controls.Add(lblPosTitle);

            lblPosDevice = new Label
            {
                Text = "🟢 SmartPOS Contactless NFC (Terminal #01)",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(16, py + 20),
                Size = new Size(400, 22)
            };
            pnlPosInfo.Controls.Add(lblPosDevice);
            py += 52;

            lblPosRefTitle = new Label
            {
                Text = "MÃ THAM CHIẾU GIAO DỊCH POS (REF CODE):",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(113, 113, 122),
                Location = new Point(16, py),
                AutoSize = true
            };
            pnlPosInfo.Controls.Add(lblPosRefTitle);

            string autoRef = "AP-" + DateTime.Now.ToString("yyMMdd") + "-" + new Random().Next(1000, 9999);
            txtPosRef = new TextBox
            {
                Text = autoRef,
                Location = new Point(16, py + 22),
                Size = new Size(405, 30),
                BackColor = Color.FromArgb(18, 18, 20),
                ForeColor = Color.FromArgb(244, 244, 245),
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlPosInfo.Controls.Add(txtPosRef);
            py += 64;

            lblGuide = new Label
            {
                Text = "📌 Hướng dẫn cho lễ tân:\n" +
                       "1. Hướng dẫn khách đưa iPhone hoặc Apple Watch gần biểu tượng sóng NFC trên máy POS.\n" +
                       "2. Khách hàng xác nhận bằng Face ID / Touch ID.\n" +
                       "3. Khi máy POS phát tiếng bíp và in bill, bấm nút xác nhận bên dưới.",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(161, 161, 170),
                Location = new Point(16, py),
                Size = new Size(405, 80)
            };
            pnlPosInfo.Controls.Add(lblGuide);
        }
    }
}
