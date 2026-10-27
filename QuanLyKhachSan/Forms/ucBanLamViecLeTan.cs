using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class ucBanLamViecLeTan : UserControl
    {
        private static readonly CultureInfo Vi = new CultureInfo("vi-VN");

        // Action events to communicate with parent Form (frmTrangChu)
        public event EventHandler YeuCauDatPhong;
        public event EventHandler YeuCauNhanPhong;
        public event EventHandler YeuCauTraPhong;
        public event EventHandler YeuCauTraCuuKhach;
        public event EventHandler YeuCauQuanLyPhong;

        private static string GhiChuCaHienTai = "Khách phòng 305 sẽ đến muộn sau 21:00. Phòng 108 yêu cầu xuất hóa đơn công ty khi trả phòng.";

        public ucBanLamViecLeTan()
        {
            InitializeComponent();
            InitializeComponentCustom();
            TaiDuLieuHoatDong();
        }

        private void InitializeComponentCustom()
        {
            BackColor = Color.FromArgb(248, 250, 252);
            Dock = DockStyle.Fill;
            AutoScroll = true;
            Padding = new Padding(24, 18, 24, 20);
            Font = UITheme.MainFont;

            // 1. HEADER
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 62,
                BackColor = Color.Transparent
            };

            lblTitle = new Label
            {
                Text = "Bàn làm việc lễ tân",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(0, 0),
                AutoSize = true
            };

            lblSubtitle = new Label
            {
                Text = $"Ca chiều • 14:00–22:00 • Quầy lễ tân 01",
                Font = new Font("Segoe UI", 9.2F, FontStyle.Regular),
                ForeColor = UITheme.TextMuted,
                Location = new Point(0, 32),
                AutoSize = true
            };

            lblDatePill = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Text = "📅 " + DateTime.Today.ToString("dddd, dd/MM/yyyy", Vi),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                BackColor = Color.White,
                Padding = new Padding(12, 6, 12, 6),
                AutoSize = true
            };
            lblDatePill.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle r = new Rectangle(0, 0, lblDatePill.Width - 1, lblDatePill.Height - 1);
                using var path = UITheme.GetRoundedRectPath(r, 6);
                using var pen = new Pen(UITheme.BorderColor, 1F);
                e.Graphics.DrawPath(pen, path);
            };

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblDatePill);
            pnlHeader.Resize += (_, __) =>
            {
                lblDatePill.Location = new Point(pnlHeader.Width - lblDatePill.Width, 4);
            };

            // 2. 4 QUICK ACTION CARDS
            tblQuickActions = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 82,
                ColumnCount = 4,
                RowCount = 1,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 10, 0, 14)
            };
            for (int i = 0; i < 4; i++)
            {
                tblQuickActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }

            // Card 1: Tạo đặt phòng (Orange highlighted)
            var card1 = TaoTheQuickAction("Tạo đặt phòng", "Ctrl+N", "＋", true, () => YeuCauDatPhong?.Invoke(this, EventArgs.Empty));

            // Card 2: Nhận phòng
            var card2 = TaoTheQuickAction("Nhận phòng", "F5", "→]", false, () => YeuCauNhanPhong?.Invoke(this, EventArgs.Empty));

            // Card 3: Trả phòng
            var card3 = TaoTheQuickAction("Trả phòng", "F6", "[→", false, () => YeuCauTraPhong?.Invoke(this, EventArgs.Empty));

            // Card 4: Tra cứu khách
            var card4 = TaoTheQuickAction("Tra cứu khách", "Ctrl+F", "🔍", false, () => YeuCauTraCuuKhach?.Invoke(this, EventArgs.Empty));

            tblQuickActions.Controls.Add(card1, 0, 0);
            tblQuickActions.Controls.Add(card2, 1, 0);
            tblQuickActions.Controls.Add(card3, 2, 0);
            tblQuickActions.Controls.Add(card4, 3, 0);

            // 3. MAIN SECTION: 2 COLUMNS (Left 66%, Right 34%)
            tblMainColumns = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 10, 0, 10),
                Padding = new Padding(0, 6, 0, 6)
            };
            tblMainColumns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66F));
            tblMainColumns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));

            // ==========================================
            // LEFT CARD: HOẠT ĐỘNG HÔM NAY
            // ==========================================
            cardActivities = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20, 16, 20, 14),
                Margin = new Padding(0, 0, 10, 0)
            };
            cardActivities.Paint += (s, e) => UITheme.DrawCard(s, e, 10);

            var pnlActHeader = new Panel { Dock = DockStyle.Top, Height = 34, BackColor = Color.Transparent };
            lblActivitiesTitle = new Label
            {
                Text = "Hoạt động hôm nay",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(0, 0),
                AutoSize = true
            };
            lblActivitiesUpdated = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Text = $"Cập nhật lúc {DateTime.Now:HH:mm}",
                Font = new Font("Segoe UI", 8.8F, FontStyle.Italic),
                ForeColor = UITheme.TextMuted,
                AutoSize = true
            };
            pnlActHeader.Controls.Add(lblActivitiesTitle);
            pnlActHeader.Controls.Add(lblActivitiesUpdated);
            pnlActHeader.Resize += (_, __) =>
            {
                lblActivitiesUpdated.Location = new Point(pnlActHeader.Width - lblActivitiesUpdated.Width, 4);
            };

            // Bottom action buttons inside Left Card
            flowActivityButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 44,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 8, 0, 0)
            };

            btnBottomTaoDat = new Button
            {
                Text = "＋ Tạo đặt phòng",
                Height = 34,
                AutoSize = true,
                BackColor = UITheme.PrimaryOrange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 10, 0)
            };
            btnBottomTaoDat.FlatAppearance.BorderSize = 0;
            btnBottomTaoDat.Click += (_, __) => YeuCauDatPhong?.Invoke(this, EventArgs.Empty);

            btnBottomNhanPhong = new Button
            {
                Text = "→] Nhận phòng",
                Height = 34,
                AutoSize = true,
                BackColor = Color.White,
                ForeColor = UITheme.DarkNavy,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 10, 0)
            };
            btnBottomNhanPhong.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnBottomNhanPhong.Click += (_, __) => YeuCauNhanPhong?.Invoke(this, EventArgs.Empty);

            btnBottomTraPhong = new Button
            {
                Text = "[→ Trả phòng",
                Height = 34,
                AutoSize = true,
                BackColor = Color.White,
                ForeColor = UITheme.DarkNavy,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnBottomTraPhong.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnBottomTraPhong.Click += (_, __) => YeuCauTraPhong?.Invoke(this, EventArgs.Empty);

            flowActivityButtons.Controls.AddRange(new Control[] { btnBottomTaoDat, btnBottomNhanPhong, btnBottomTraPhong });

            // DataGridView Hoạt động
            dgvActivities = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(241, 245, 249),
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            UITheme.FormatDataGridView(dgvActivities);
            dgvActivities.RowTemplate.Height = 38;
            dgvActivities.CellFormatting += DgvActivities_CellFormatting;

            cardActivities.Controls.Add(dgvActivities);
            cardActivities.Controls.Add(flowActivityButtons);
            cardActivities.Controls.Add(pnlActHeader);

            pnlActHeader.SendToBack();
            flowActivityButtons.SendToBack();
            dgvActivities.BringToFront();

            // ==========================================
            // RIGHT CARD: CẦN XỬ LÝ (ALERTS)
            // ==========================================
            cardAlerts = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20, 16, 20, 14),
                Margin = new Padding(10, 0, 0, 0)
            };
            cardAlerts.Paint += (s, e) => UITheme.DrawCard(s, e, 10);

            var pnlAlertHeader = new Panel { Dock = DockStyle.Top, Height = 34, BackColor = Color.Transparent };
            lblAlertsTitle = new Label
            {
                Text = "Cần xử lý",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(0, 0),
                AutoSize = true
            };
            lblAlertsBadge = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Text = "9 việc",
                Font = new Font("Segoe UI", 8.8F, FontStyle.Bold),
                ForeColor = UITheme.TextMuted,
                AutoSize = true
            };
            pnlAlertHeader.Controls.Add(lblAlertsTitle);
            pnlAlertHeader.Controls.Add(lblAlertsBadge);
            pnlAlertHeader.Resize += (_, __) =>
            {
                lblAlertsBadge.Location = new Point(pnlAlertHeader.Width - lblAlertsBadge.Width, 4);
            };

            // 3 Alert Cards
            pnlAlertOverdue = TaoTheCanXuLy("3 phòng quá giờ trả", "P.108, P.209, P.405", "⚠️",
                Color.FromArgb(254, 242, 242), Color.FromArgb(254, 202, 202), Color.FromArgb(220, 38, 38),
                () => YeuCauQuanLyPhong?.Invoke(this, EventArgs.Empty));

            pnlAlertCleaning = TaoTheCanXuLy("4 phòng đang dọn", "Dự kiến xong trước 16:30", "❄️",
                Color.FromArgb(239, 246, 255), Color.FromArgb(191, 219, 254), Color.FromArgb(37, 99, 235),
                () => YeuCauQuanLyPhong?.Invoke(this, EventArgs.Empty));

            pnlAlertMaintenance = TaoTheCanXuLy("2 phòng bảo trì", "P.302 và P.511", "🔧",
                Color.FromArgb(248, 250, 252), Color.FromArgb(226, 232, 240), Color.FromArgb(71, 85, 105),
                () => YeuCauQuanLyPhong?.Invoke(this, EventArgs.Empty));

            cardAlerts.Controls.Add(pnlAlertMaintenance);
            cardAlerts.Controls.Add(pnlAlertCleaning);
            cardAlerts.Controls.Add(pnlAlertOverdue);
            cardAlerts.Controls.Add(pnlAlertHeader);

            tblMainColumns.Controls.Add(cardActivities, 0, 0);
            tblMainColumns.Controls.Add(cardAlerts, 1, 0);

            // 4. BOTTOM SECTION: GHI CHÚ BÀN GIAO CA
            cardShiftNote = new Panel
            {
                Dock = DockStyle.Top,
                Height = 84,
                BackColor = Color.White,
                Padding = new Padding(20, 14, 20, 14),
                Margin = new Padding(0, 14, 0, 10)
            };
            cardShiftNote.Paint += (s, e) => UITheme.DrawCard(s, e, 10);

            var pnlNoteIcon = new Panel
            {
                Size = new Size(44, 44),
                Location = new Point(20, 20),
                BackColor = Color.FromArgb(239, 246, 255)
            };
            pnlNoteIcon.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle r = new Rectangle(0, 0, 43, 43);
                using var path = UITheme.GetRoundedRectPath(r, 22);
                using var brush = new SolidBrush(Color.FromArgb(239, 246, 255));
                e.Graphics.FillPath(brush, path);
            };
            var lblNoteGlyph = new Label
            {
                Text = "📝",
                Font = new Font("Segoe UI", 14F),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            pnlNoteIcon.Controls.Add(lblNoteGlyph);

            lblShiftNoteTitle = new Label
            {
                Text = "Ghi chú bàn giao ca",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(76, 18),
                AutoSize = true
            };

            lblShiftNoteText = new Label
            {
                Text = GhiChuCaHienTai,
                Font = new Font("Segoe UI", 9F),
                ForeColor = UITheme.TextMuted,
                Location = new Point(76, 40),
                AutoSize = true
            };

            btnUpdateNote = new Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Text = "✏️ Cập nhật",
                Size = new Size(120, 36),
                Location = new Point(cardShiftNote.Width - 145, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = UITheme.DarkNavy,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnUpdateNote.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnUpdateNote.Click += BtnUpdateNote_Click;

            cardShiftNote.Controls.Add(pnlNoteIcon);
            cardShiftNote.Controls.Add(lblShiftNoteTitle);
            cardShiftNote.Controls.Add(lblShiftNoteText);
            cardShiftNote.Controls.Add(btnUpdateNote);
            cardShiftNote.Resize += (_, __) =>
            {
                btnUpdateNote.Location = new Point(cardShiftNote.Width - 145, 20);
            };

            // Gắn toàn bộ vào UserControl theo thứ tự docking chuẩn:
            // pnlHeader (Top) -> tblQuickActions (Top) -> cardShiftNote (Top) -> tblMainColumns (Fill)
            Controls.Add(tblMainColumns);
            Controls.Add(cardShiftNote);
            Controls.Add(tblQuickActions);
            Controls.Add(pnlHeader);

            cardShiftNote.SendToBack();
            tblQuickActions.SendToBack();
            pnlHeader.SendToBack();
            tblMainColumns.BringToFront();
        }

        private Panel TaoTheQuickAction(string tieuDe, string phimTat, string iconText, bool highlighted, Action onClick = null)
        {
            var pnl = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = highlighted ? Color.FromArgb(255, 251, 235) : Color.White,
                Cursor = Cursors.Hand,
                Padding = new Padding(14, 12, 14, 12),
                Margin = new Padding(0, 0, 12, 0)
            };
            pnl.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle r = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                using var path = UITheme.GetRoundedRectPath(r, 8);
                using var pen = new Pen(highlighted ? Color.FromArgb(252, 211, 77) : UITheme.BorderColor, 1.2F);
                e.Graphics.DrawPath(pen, path);
            };

            var pnlIconBox = new Panel
            {
                Size = new Size(42, 42),
                Location = new Point(14, 18),
                BackColor = highlighted ? UITheme.PrimaryOrange : Color.FromArgb(239, 246, 255)
            };
            pnlIconBox.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle r = new Rectangle(0, 0, 41, 41);
                using var path = UITheme.GetRoundedRectPath(r, 6);
                using var brush = new SolidBrush(highlighted ? UITheme.PrimaryOrange : Color.FromArgb(239, 246, 255));
                e.Graphics.FillPath(brush, path);
            };
            var lblIcon = new Label
            {
                Text = iconText,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = highlighted ? Color.White : UITheme.PrimaryBlue,
                BackColor = Color.Transparent
            };
            pnlIconBox.Controls.Add(lblIcon);

            var lblT = new Label
            {
                Text = tieuDe,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(66, 18),
                AutoSize = true
            };

            var lblSub = new Label
            {
                Text = phimTat,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = UITheme.TextMuted,
                Location = new Point(66, 40),
                AutoSize = true
            };

            pnl.Controls.Add(pnlIconBox);
            pnl.Controls.Add(lblT);
            pnl.Controls.Add(lblSub);

            // Click trên bất cứ control con nào cũng kích hoạt
            pnl.Click += (_, __) => onClick?.Invoke();
            foreach (Control c in new Control[] { pnlIconBox, lblIcon, lblT, lblSub })
            {
                c.Click += (_, __) => onClick?.Invoke();
            }

            return pnl;
        }

        private Panel TaoTheCanXuLy(string tieuDe, string moTa, string icon, Color bg, Color border, Color textCol, Action onClick = null)
        {
            var pnl = new Panel
            {
                Dock = DockStyle.Top,
                Height = 68,
                BackColor = bg,
                Cursor = Cursors.Hand,
                Padding = new Padding(12, 10, 12, 10),
                Margin = new Padding(0, 0, 0, 10)
            };
            pnl.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle r = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                using var path = UITheme.GetRoundedRectPath(r, 8);
                using var pen = new Pen(border, 1.2F);
                e.Graphics.DrawPath(pen, path);
            };

            var lblIcon = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI", 14F),
                Location = new Point(10, 16),
                Size = new Size(30, 30),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblT = new Label
            {
                Text = tieuDe,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = textCol,
                Location = new Point(46, 14),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblM = new Label
            {
                Text = moTa,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = UITheme.TextMuted,
                Location = new Point(46, 36),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblArrow = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Text = "›",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = textCol,
                Size = new Size(24, 30),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnl.Controls.Add(lblIcon);
            pnl.Controls.Add(lblT);
            pnl.Controls.Add(lblM);
            pnl.Controls.Add(lblArrow);
            pnl.Resize += (_, __) =>
            {
                lblArrow.Location = new Point(pnl.Width - 32, 18);
            };

            pnl.Click += (_, __) => onClick?.Invoke();
            foreach (Control c in new Control[] { lblIcon, lblT, lblM, lblArrow })
            {
                c.Click += (_, __) => onClick?.Invoke();
            }

            return pnl;
        }

        public void TaiDuLieuHoatDong()
        {
            try
            {
                string sql = @"
                    SELECT 
                        p.SoPhong, 
                        kh.HoTen, 
                        dp.NgayNhan, 
                        dp.NgayTra, 
                        dp.TrangThai,
                        dp.MaDatPhong
                    FROM DatPhong dp
                    JOIN KhachHang kh ON dp.MaKH = kh.MaKH
                    JOIN Phong p ON dp.MaPhong = p.MaPhong
                    WHERE dp.TrangThai IN ('Đang ở', 'Đã đặt', 'Đã nhận', 'Đang sử dụng')
                    ORDER BY 
                        CASE WHEN dp.TrangThai IN ('Đang ở', 'Đang sử dụng') THEN 0 ELSE 1 END,
                        dp.NgayNhan DESC
                    LIMIT 20";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql);

                DataTable dtHienThi = new DataTable();
                dtHienThi.Columns.Add("Phòng", typeof(string));
                dtHienThi.Columns.Add("Khách hàng", typeof(string));
                dtHienThi.Columns.Add("Thời gian", typeof(string));
                dtHienThi.Columns.Add("Trạng thái", typeof(string));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        string phong = "P. " + r["SoPhong"].ToString();
                        string khach = r["HoTen"].ToString();
                        DateTime nn = Convert.ToDateTime(r["NgayNhan"]);
                        DateTime nt = Convert.ToDateTime(r["NgayTra"]);
                        string thoiGian = $"{nn:dd/MM} - {nt:dd/MM}";

                        string ttRaw = r["TrangThai"].ToString();
                        string tt = "Đang sử dụng";
                        if (ttRaw.Contains("đặt") || ttRaw.Contains("Đã đặt")) tt = "Đã đặt";
                        else if (ttRaw.Contains("nhận") || ttRaw.Contains("Chờ")) tt = "Chờ nhận phòng";
                        else if (nt.Date == DateTime.Today) tt = "Sắp trả phòng";

                        dtHienThi.Rows.Add(phong, khach, thoiGian, tt);
                    }
                }
                else
                {
                    // Dữ liệu mẫu chuẩn Figma khi CSDL trống
                    dtHienThi.Rows.Add("P. 201", "Nguyễn Văn Hùng", "28/09 - 30/09", "Đang sử dụng");
                    dtHienThi.Rows.Add("P. 305", "Đỗ Thu Trang", "29/09 - 02/10", "Đã đặt");
                    dtHienThi.Rows.Add("P. 412", "Phạm Quốc Bảo", "29/09 - 30/09", "Chờ nhận phòng");
                    dtHienThi.Rows.Add("P. 108", "Hoàng Minh Khoa", "27/09 - 29/09", "Sắp trả phòng");
                }

                dgvActivities.DataSource = dtHienThi;

                if (dgvActivities.Columns["Phòng"] != null)
                {
                    dgvActivities.Columns["Phòng"].FillWeight = 85;
                    dgvActivities.Columns["Phòng"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    dgvActivities.Columns["Phòng"].DefaultCellStyle.ForeColor = UITheme.DarkNavy;
                }
                if (dgvActivities.Columns["Khách hàng"] != null)
                {
                    dgvActivities.Columns["Khách hàng"].FillWeight = 160;
                    dgvActivities.Columns["Khách hàng"].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                }
                if (dgvActivities.Columns["Thời gian"] != null)
                {
                    dgvActivities.Columns["Thời gian"].FillWeight = 110;
                    dgvActivities.Columns["Thời gian"].DefaultCellStyle.ForeColor = UITheme.TextMuted;
                }
                if (dgvActivities.Columns["Trạng thái"] != null)
                {
                    dgvActivities.Columns["Trạng thái"].FillWeight = 120;
                    dgvActivities.Columns["Trạng thái"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                lblActivitiesUpdated.Text = $"Cập nhật lúc {DateTime.Now:HH:mm}";
            }
            catch
            {
                // Fallback nếu có lỗi kết nối
            }
        }

        private void DgvActivities_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var col = dgvActivities.Columns[e.ColumnIndex];

            if (col.Name == "Trạng thái" && e.Value != null)
            {
                string tt = e.Value.ToString();
                e.CellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);

                if (tt.Contains("sử dụng") || tt.Contains("Đang ở"))
                {
                    e.Value = "● Đang sử dụng";
                    e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                    e.CellStyle.BackColor = Color.FromArgb(254, 226, 226);
                }
                else if (tt.Contains("Đã đặt"))
                {
                    e.Value = "● Đã đặt";
                    e.CellStyle.ForeColor = Color.FromArgb(217, 119, 6);
                    e.CellStyle.BackColor = Color.FromArgb(254, 243, 199);
                }
                else if (tt.Contains("Chờ nhận"))
                {
                    e.Value = "● Chờ nhận phòng";
                    e.CellStyle.ForeColor = Color.FromArgb(37, 99, 235);
                    e.CellStyle.BackColor = Color.FromArgb(219, 234, 254);
                }
                else if (tt.Contains("Sắp trả"))
                {
                    e.Value = "● Sắp trả phòng";
                    e.CellStyle.ForeColor = Color.FromArgb(124, 58, 237);
                    e.CellStyle.BackColor = Color.FromArgb(237, 233, 254);
                }
                e.FormattingApplied = true;
            }
        }

        private void BtnUpdateNote_Click(object sender, EventArgs e)
        {
            using var formPrompt = new Form
            {
                Text = "Cập nhật ghi chú bàn giao ca",
                Size = new Size(500, 240),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                Font = UITheme.MainFont,
                BackColor = Color.White
            };

            var lblPrompt = new Label { Text = "Nội dung ghi chú ca trực lễ tân:", Location = new Point(20, 16), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            var txtPrompt = new TextBox { Location = new Point(20, 42), Size = new Size(445, 80), Multiline = true, Text = GhiChuCaHienTai, Font = new Font("Segoe UI", 9.5F) };

            var btnLuuPrompt = new Button { Text = "💾 Lưu ghi chú", Size = new Size(130, 36), Location = new Point(335, 140), BackColor = UITheme.PrimaryOrange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Cursor = Cursors.Hand };
            btnLuuPrompt.FlatAppearance.BorderSize = 0;
            btnLuuPrompt.Click += (_, __) =>
            {
                GhiChuCaHienTai = txtPrompt.Text.Trim();
                lblShiftNoteText.Text = GhiChuCaHienTai;
                formPrompt.DialogResult = DialogResult.OK;
            };

            var btnHuyPrompt = new Button { Text = "Hủy", Size = new Size(80, 36), Location = new Point(245, 140), BackColor = Color.White, ForeColor = UITheme.TextMuted, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            btnHuyPrompt.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnHuyPrompt.Click += (_, __) => formPrompt.Close();

            formPrompt.Controls.AddRange(new Control[] { lblPrompt, txtPrompt, btnLuuPrompt, btnHuyPrompt });
            formPrompt.ShowDialog(this);
        }
    }
}
