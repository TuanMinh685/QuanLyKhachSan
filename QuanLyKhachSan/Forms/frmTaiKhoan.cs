using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public class frmTaiKhoan : Form
    {
        // Layout chính 2 card
        private TableLayoutPanel tblMainLayout;
        private Panel cardLeft;
        private Panel cardRight;

        // Controls bên trái (Danh sách)
        private Panel pnlHeaderLeft;
        private Label lblTitleLeft;
        private Label lblSubtitleLeft;
        private FlowLayoutPanel pnlStatsPills;
        private Label lblPillTotal;
        private Label lblPillAdmin;
        private Label lblPillLeTan;

        private Panel pnlSearch;
        private Label lblIconSearch;
        private TextBox txtTim;

        private Panel pnlGridWrapper;
        private DataGridView grid;

        // Controls bên phải (Form nhập liệu)
        private Label lblRightTitle;
        private Label lblRightSubtitle;
        private Panel pnlDivider;

        private Panel pnlHoTenWrap;
        private TextBox txtHoTen;

        private Panel pnlChucVuWrap;
        private ComboBox cboChucVu;

        private Panel pnlSDTWrap;
        private TextBox txtSDT;

        private Panel pnlTenDangNhapWrap;
        private TextBox txtTenDangNhap;

        private Panel pnlMatKhauWrap;
        private TextBox txtMatKhau;
        private Label lblHintMatKhau;

        private Panel pnlVaiTroWrap;
        private ComboBox cboVaiTro;

        private Panel pnlBottomButtons;
        private Button btnLuu;
        private Button btnLamMoi;
        private Button btnXoa;

        private int maTaiKhoan = 0;
        private int maNhanVien = 0;

        public frmTaiKhoan()
        {
            Text = "Quản lý nhân sự & Phân quyền";
            Dock = DockStyle.Fill;
            BackColor = UITheme.BackgroundGray;
            Font = UITheme.MainFont;

            if (!Session.LaAdmin)
            {
                Shown += (_, __) =>
                {
                    MessageBox.Show("Chỉ Quản trị viên (Admin) mới có quyền truy cập trang quản lý nhân sự.",
                        "Không đủ quyền hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                };
                return;
            }

            BuildModernUI();
            LoadTaiKhoan();
        }

        private void BuildModernUI()
        {
            SuspendLayout();

            // 1. Root TableLayoutPanel chia 2 cột: Trái (62%), Phải (38%)
            tblMainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(24),
                BackColor = UITheme.BackgroundGray
            };
            tblMainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
            tblMainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));

            // =========================================================================
            // CARD TRÁI: DANH SÁCH NHÂN SỰ
            // =========================================================================
            cardLeft = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(24),
                Margin = new Padding(0, 0, 16, 0)
            };
            cardLeft.Paint += (s, e) => UITheme.DrawCard(s, e, 12);

            // Header trái
            pnlHeaderLeft = new Panel
            {
                Dock = DockStyle.Top,
                Height = 84,
                BackColor = Color.Transparent
            };

            lblTitleLeft = new Label
            {
                Text = "👤 QUẢN LÝ NHÂN SỰ & TÀI KHOẢN",
                Font = new Font("Segoe UI", 13.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(0, 0),
                AutoSize = true
            };

            lblSubtitleLeft = new Label
            {
                Text = "Danh sách nhân viên khách sạn, tài khoản đăng nhập và phân cấp quyền hạn",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
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

            lblPillTotal = TaoPill("Tổng: 0 nhân sự", Color.FromArgb(241, 245, 249), UITheme.DarkNavy);
            lblPillAdmin = TaoPill("👑 Quản trị (Admin): 0", Color.FromArgb(238, 242, 255), Color.FromArgb(79, 70, 229));
            lblPillLeTan = TaoPill("💼 Lễ tân (LeTan): 0", Color.FromArgb(220, 252, 231), Color.FromArgb(22, 163, 74));

            pnlStatsPills.Controls.Add(lblPillTotal);
            pnlStatsPills.Controls.Add(lblPillAdmin);
            pnlStatsPills.Controls.Add(lblPillLeTan);

            pnlHeaderLeft.Controls.Add(lblTitleLeft);
            pnlHeaderLeft.Controls.Add(lblSubtitleLeft);
            pnlHeaderLeft.Controls.Add(pnlStatsPills);
            cardLeft.Controls.Add(pnlHeaderLeft);

            // Thanh tìm kiếm
            pnlSearch = new Panel
            {
                Location = new Point(24, 114),
                Size = new Size(cardLeft.ClientSize.Width - 48, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.White
            };
            pnlSearch.Paint += (s, e) => UITheme.DrawInputBox(s, e, 6);

            lblIconSearch = new Label
            {
                Text = "🔍",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = UITheme.TextMuted,
                Size = new Size(32, 26),
                Location = new Point(8, 7),
                TextAlign = ContentAlignment.MiddleCenter
            };

            txtTim = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = UITheme.TextMain,
                Location = new Point(42, 10),
                Width = pnlSearch.Width - 52,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                PlaceholderText = "Tìm theo tên nhân viên, tên đăng nhập, chức vụ, vai trò..."
            };
            txtTim.TextChanged += (_, __) => LoadTaiKhoan(txtTim.Text.Trim());

            pnlSearch.Controls.Add(lblIconSearch);
            pnlSearch.Controls.Add(txtTim);
            cardLeft.Controls.Add(pnlSearch);

            // Bọc bảng dữ liệu
            pnlGridWrapper = new Panel
            {
                Location = new Point(24, 166),
                Size = new Size(cardLeft.ClientSize.Width - 48, cardLeft.ClientSize.Height - 190),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.White
            };

            grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false
            };
            UITheme.StyleModernGrid(grid);
            grid.CellFormatting += Grid_CellFormatting;
            grid.SelectionChanged += (_, __) => ChonTaiKhoan();

            pnlGridWrapper.Controls.Add(grid);
            cardLeft.Controls.Add(pnlGridWrapper);

            // =========================================================================
            // CARD PHẢI: FORM THÔNG TIN NHÂN SỰ & TÀI KHOẢN
            // =========================================================================
            cardRight = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(24),
                Margin = new Padding(0)
            };
            cardRight.Paint += (s, e) => UITheme.DrawCard(s, e, 12);

            lblRightTitle = new Label
            {
                Text = "CHI TIẾT NHÂN SỰ & PHÂN QUYỀN",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(24, 20),
                AutoSize = true
            };

            lblRightSubtitle = new Label
            {
                Text = "Điền thông tin nhân viên và thiết lập tài khoản truy cập",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = UITheme.TextMuted,
                Location = new Point(24, 46),
                AutoSize = true
            };

            pnlDivider = new Panel
            {
                Location = new Point(24, 72),
                Size = new Size(cardRight.ClientSize.Width - 48, 1),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = UITheme.BorderColor
            };

            cardRight.Controls.Add(lblRightTitle);
            cardRight.Controls.Add(lblRightSubtitle);
            cardRight.Controls.Add(pnlDivider);

            int startY = 88;
            int gapY = 62;

            // 1. Họ tên nhân viên
            TaoNhanInput(cardRight, "Họ và tên nhân viên *", 24, startY);
            pnlHoTenWrap = TaoInputBoxWrap(cardRight, 24, startY + 22);
            txtHoTen = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F),
                Dock = DockStyle.Fill,
                PlaceholderText = "Ví dụ: Nguyễn Văn A"
            };
            pnlHoTenWrap.Controls.Add(txtHoTen);

            // 2. Chức vụ & Số điện thoại (2 cột)
            int row2Y = startY + gapY;
            int halfW = (cardRight.ClientSize.Width - 58) / 2;

            TaoNhanInput(cardRight, "Chức vụ *", 24, row2Y);
            pnlChucVuWrap = new Panel
            {
                Location = new Point(24, row2Y + 22),
                Size = new Size(halfW, 36),
                BackColor = Color.White,
                Padding = new Padding(6, 4, 6, 4)
            };
            pnlChucVuWrap.Paint += (s, e) => UITheme.DrawInputBox(s, e, 6);
            cboChucVu = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F)
            };
            cboChucVu.Items.AddRange(new object[] { "Quản lý", "Lễ tân", "Kế toán", "Buồng phòng", "Bảo vệ" });
            cboChucVu.SelectedIndex = 1;
            pnlChucVuWrap.Controls.Add(cboChucVu);
            cardRight.Controls.Add(pnlChucVuWrap);

            TaoNhanInput(cardRight, "Số điện thoại", 24 + halfW + 10, row2Y);
            pnlSDTWrap = new Panel
            {
                Location = new Point(24 + halfW + 10, row2Y + 22),
                Size = new Size(halfW, 36),
                BackColor = Color.White,
                Padding = new Padding(8, 7, 8, 4),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            pnlSDTWrap.Paint += (s, e) => UITheme.DrawInputBox(s, e, 6);
            txtSDT = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F),
                Dock = DockStyle.Fill,
                PlaceholderText = "09xxxxxxxx"
            };
            pnlSDTWrap.Controls.Add(txtSDT);
            cardRight.Controls.Add(pnlSDTWrap);

            // 3. Tên đăng nhập
            int row3Y = row2Y + gapY;
            TaoNhanInput(cardRight, "Tên đăng nhập *", 24, row3Y);
            pnlTenDangNhapWrap = TaoInputBoxWrap(cardRight, 24, row3Y + 22);
            txtTenDangNhap = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F),
                Dock = DockStyle.Fill,
                PlaceholderText = "Tối thiểu 4 ký tự"
            };
            pnlTenDangNhapWrap.Controls.Add(txtTenDangNhap);

            // 4. Mật khẩu
            int row4Y = row3Y + gapY;
            TaoNhanInput(cardRight, "Mật khẩu truy cập *", 24, row4Y);
            pnlMatKhauWrap = TaoInputBoxWrap(cardRight, 24, row4Y + 22);
            txtMatKhau = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F),
                Dock = DockStyle.Fill,
                UseSystemPasswordChar = true,
                PlaceholderText = "Tối thiểu 8 ký tự (hoặc để trống khi sửa)"
            };
            pnlMatKhauWrap.Controls.Add(txtMatKhau);

            lblHintMatKhau = new Label
            {
                Text = "💡 Mẹo: Khi cập nhật tài khoản, để trống ô mật khẩu nếu muốn giữ nguyên mật khẩu cũ.",
                Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                ForeColor = UITheme.TextMuted,
                Location = new Point(24, row4Y + 62),
                Size = new Size(cardRight.ClientSize.Width - 48, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            cardRight.Controls.Add(lblHintMatKhau);

            // 5. Vai trò phân quyền
            int row5Y = row4Y + gapY + 24;
            TaoNhanInput(cardRight, "Vai trò & Quyền hạn hệ thống *", 24, row5Y);
            pnlVaiTroWrap = new Panel
            {
                Location = new Point(24, row5Y + 22),
                Size = new Size(cardRight.ClientSize.Width - 48, 36),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.White,
                Padding = new Padding(6, 4, 6, 4)
            };
            pnlVaiTroWrap.Paint += (s, e) => UITheme.DrawInputBox(s, e, 6);
            cboVaiTro = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F)
            };
            cboVaiTro.Items.AddRange(new object[] { "Admin - Quản trị viên (Toàn quyền)", "LeTan - Lễ tân thu ngân (Nghiệp vụ phòng)" });
            cboVaiTro.SelectedIndex = 1;
            pnlVaiTroWrap.Controls.Add(cboVaiTro);
            cardRight.Controls.Add(pnlVaiTroWrap);

            // Buttons dưới cùng
            pnlBottomButtons = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.Transparent
            };

            btnLuu = new Button
            {
                Text = "💾 Lưu tài khoản",
                Size = new Size(130, 42),
                Location = new Point(0, 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.PrimaryOrange,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Click += (_, __) => LuuTaiKhoan();

            btnLamMoi = new Button
            {
                Text = "🔄 Làm mới",
                Size = new Size(100, 42),
                Location = new Point(138, 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = UITheme.DarkNavy,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnLamMoi.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnLamMoi.Click += (_, __) => ResetForm();

            btnXoa = new Button
            {
                Text = "🗑 Xóa",
                Size = new Size(85, 42),
                Location = new Point(246, 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(254, 242, 242),
                ForeColor = Color.FromArgb(220, 38, 38),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnXoa.FlatAppearance.BorderColor = Color.FromArgb(254, 202, 202);
            btnXoa.Click += (_, __) => XoaTaiKhoan();

            pnlBottomButtons.Controls.Add(btnLuu);
            pnlBottomButtons.Controls.Add(btnLamMoi);
            pnlBottomButtons.Controls.Add(btnXoa);
            cardRight.Controls.Add(pnlBottomButtons);

            // Đưa vào TableLayoutPanel
            tblMainLayout.Controls.Add(cardLeft, 0, 0);
            tblMainLayout.Controls.Add(cardRight, 1, 0);
            Controls.Add(tblMainLayout);

            ResumeLayout(false);
        }

        private static Label TaoNhanInput(Control parent, string text, int x, int y)
        {
            var lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                Location = new Point(x, y),
                AutoSize = true
            };
            parent.Controls.Add(lbl);
            return lbl;
        }

        private static Panel TaoInputBoxWrap(Control parent, int x, int y)
        {
            var pnl = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(parent.ClientSize.Width - (x * 2), 36),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.White,
                Padding = new Padding(10, 8, 10, 4)
            };
            pnl.Paint += (s, e) => UITheme.DrawInputBox(s, e, 6);
            parent.Controls.Add(pnl);
            return pnl;
        }

        private Label TaoPill(string text, Color bg, Color fg)
        {
            var lbl = new Label
            {
                Text = text,
                BackColor = bg,
                ForeColor = fg,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
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
                using (Pen pen = new Pen(Color.FromArgb(226, 232, 240), 1F))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            };
            return lbl;
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string colName = grid.Columns[e.ColumnIndex].Name;

            if (colName == "Vai trò" && e.Value != null)
            {
                string role = e.Value.ToString();
                if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    e.Value = "👑 Quản trị viên";
                    e.CellStyle.ForeColor = Color.FromArgb(79, 70, 229);
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
                else
                {
                    e.Value = "💼 Lễ tân";
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
                }
                e.FormattingApplied = true;
            }
        }

        private void LoadTaiKhoan(string kw = "")
        {
            try
            {
                string sql = @"
                    SELECT 
                        t.MaTaiKhoan,
                        t.MaNhanVien,
                        n.HoTen AS ""Nhân viên"",
                        t.TenDangNhap AS ""Tên đăng nhập"",
                        t.VaiTro AS ""Vai trò"",
                        n.ChucVu AS ""Chức vụ"",
                        n.SoDienThoai AS ""Số điện thoại""
                    FROM TaiKhoan t
                    JOIN NhanVien n ON n.MaNhanVien = t.MaNhanVien
                    WHERE t.TenDangNhap ILIKE @kw 
                       OR n.HoTen ILIKE @kw 
                       OR t.VaiTro ILIKE @kw 
                       OR n.ChucVu ILIKE @kw
                    ORDER BY t.MaTaiKhoan DESC";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, new[] { new SqlParameter("@kw", "%" + kw + "%") });
                grid.DataSource = dt;

                if (grid.Columns.Contains("MaTaiKhoan")) grid.Columns["MaTaiKhoan"].Visible = false;
                if (grid.Columns.Contains("MaNhanVien")) grid.Columns["MaNhanVien"].Visible = false;

                // Cập nhật thống kê Pills
                int total = dt.Rows.Count;
                int adminCount = 0;
                int leTanCount = 0;

                foreach (DataRow r in dt.Rows)
                {
                    string v = r["Vai trò"]?.ToString() ?? "";
                    if (v.Equals("Admin", StringComparison.OrdinalIgnoreCase)) adminCount++;
                    else leTanCount++;
                }

                lblPillTotal.Text = $"Tổng: {total} nhân sự";
                lblPillAdmin.Text = $"👑 Quản trị: {adminCount}";
                lblPillLeTan.Text = $"💼 Lễ tân: {leTanCount}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được dữ liệu nhân sự: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChonTaiKhoan()
        {
            if (grid.CurrentRow == null || grid.CurrentRow.Cells["MaTaiKhoan"].Value == null) return;

            try
            {
                maTaiKhoan = Convert.ToInt32(grid.CurrentRow.Cells["MaTaiKhoan"].Value);
                maNhanVien = Convert.ToInt32(grid.CurrentRow.Cells["MaNhanVien"].Value);

                string sql = @"
                    SELECT t.TenDangNhap, t.VaiTro, n.HoTen, n.ChucVu, n.SoDienThoai
                    FROM TaiKhoan t
                    JOIN NhanVien n ON n.MaNhanVien = t.MaNhanVien
                    WHERE t.MaTaiKhoan = @id";

                using var conn = DatabaseHelper.GetConnection();
                conn.Open();
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", maTaiKhoan);
                using var r = cmd.ExecuteReader();

                if (r.Read())
                {
                    txtTenDangNhap.Text = r["TenDangNhap"].ToString();
                    txtHoTen.Text = r["HoTen"].ToString();
                    txtSDT.Text = r["SoDienThoai"] == DBNull.Value ? "" : r["SoDienThoai"].ToString();

                    string chucVu = r["ChucVu"].ToString();
                    int idxCv = cboChucVu.FindStringExact(chucVu);
                    cboChucVu.SelectedIndex = idxCv >= 0 ? idxCv : 1;

                    string vaiTro = r["VaiTro"].ToString();
                    cboVaiTro.SelectedIndex = vaiTro.Equals("Admin", StringComparison.OrdinalIgnoreCase) ? 0 : 1;

                    txtMatKhau.Clear();
                    btnLuu.Text = "💾 Cập nhật";
                    btnLuu.BackColor = UITheme.PrimaryBlue;

                    // Cho phép xóa nếu không phải chính tài khoản đang đăng nhập
                    bool isSelf = txtTenDangNhap.Text.Trim().Equals(Session.TenDangNhap, StringComparison.OrdinalIgnoreCase);
                    btnXoa.Enabled = !isSelf;
                }
            }
            catch { }
        }

        private static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 210000, HashAlgorithmName.SHA256, 32);
            return "PBKDF2-SHA256$210000$" + Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(hash);
        }

        private string LayVaiTroDaChon()
        {
            return cboVaiTro.SelectedIndex == 0 ? "Admin" : "LeTan";
        }

        private void LuuTaiKhoan()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên nhân viên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) || txtTenDangNhap.Text.Trim().Length < 4)
            {
                MessageBox.Show("Tên đăng nhập bắt buộc và phải có ít nhất 4 ký tự.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }

            string vaiTro = LayVaiTroDaChon();
            string chucVu = cboChucVu.Text;
            string hoTen = txtHoTen.Text.Trim();
            string sdt = string.IsNullOrWhiteSpace(txtSDT.Text) ? null : txtSDT.Text.Trim();
            string user = txtTenDangNhap.Text.Trim();

            if (maTaiKhoan == 0)
            {
                // TẠO MỚI
                if (string.IsNullOrWhiteSpace(txtMatKhau.Text) || txtMatKhau.Text.Length < 8)
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu ban đầu (ít nhất 8 ký tự).", "Mật khẩu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    return;
                }

                try
                {
                    using var conn = DatabaseHelper.GetConnection();
                    conn.Open();
                    using var tx = conn.BeginTransaction();

                    // 1. Thêm hồ sơ nhân viên
                    int newMaNV;
                    string sqlNV = "INSERT INTO NhanVien (HoTen, ChucVu, SoDienThoai) VALUES (@ten, @cv, @sdt) RETURNING MaNhanVien";
                    using (var cmd = new SqlCommand(sqlNV, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@ten", hoTen);
                        cmd.Parameters.AddWithValue("@cv", chucVu);
                        cmd.Parameters.AddWithValue("@sdt", (object)sdt ?? DBNull.Value);
                        newMaNV = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 2. Thêm tài khoản đăng nhập
                    string sqlTK = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, MaNhanVien) VALUES (@user, @pass, @role, @nv)";
                    using (var cmd = new SqlCommand(sqlTK, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@pass", HashPassword(txtMatKhau.Text));
                        cmd.Parameters.AddWithValue("@role", vaiTro);
                        cmd.Parameters.AddWithValue("@nv", newMaNV);
                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                    MessageBox.Show($"Tạo mới nhân viên & tài khoản [{user}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTaiKhoan();
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tạo tài khoản. Tên đăng nhập có thể đã tồn tại trong hệ thống.\n" + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // CẬP NHẬT
                try
                {
                    using var conn = DatabaseHelper.GetConnection();
                    conn.Open();
                    using var tx = conn.BeginTransaction();

                    // 1. Cập nhật nhân viên
                    string sqlNV = "UPDATE NhanVien SET HoTen = @ten, ChucVu = @cv, SoDienThoai = @sdt WHERE MaNhanVien = @nv";
                    using (var cmd = new SqlCommand(sqlNV, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@ten", hoTen);
                        cmd.Parameters.AddWithValue("@cv", chucVu);
                        cmd.Parameters.AddWithValue("@sdt", (object)sdt ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@nv", maNhanVien);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Cập nhật tài khoản
                    string sqlTK;
                    if (!string.IsNullOrWhiteSpace(txtMatKhau.Text))
                    {
                        if (txtMatKhau.Text.Length < 8)
                        {
                            throw new Exception("Mật khẩu mới phải có ít nhất 8 ký tự.");
                        }
                        sqlTK = "UPDATE TaiKhoan SET TenDangNhap = @user, VaiTro = @role, MatKhau = @pass WHERE MaTaiKhoan = @id";
                    }
                    else
                    {
                        sqlTK = "UPDATE TaiKhoan SET TenDangNhap = @user, VaiTro = @role WHERE MaTaiKhoan = @id";
                    }

                    using (var cmd = new SqlCommand(sqlTK, conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@role", vaiTro);
                        cmd.Parameters.AddWithValue("@id", maTaiKhoan);
                        if (!string.IsNullOrWhiteSpace(txtMatKhau.Text))
                        {
                            cmd.Parameters.AddWithValue("@pass", HashPassword(txtMatKhau.Text));
                        }
                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                    MessageBox.Show($"Cập nhật thông tin nhân viên [{hoTen}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTaiKhoan();
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể cập nhật nhân sự: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void XoaTaiKhoan()
        {
            if (maTaiKhoan == 0) return;

            if (txtTenDangNhap.Text.Trim().Equals(Session.TenDangNhap, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Bạn không thể xóa tài khoản của chính mình đang đăng nhập!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa tài khoản [{txtTenDangNhap.Text}] của nhân viên [{txtHoTen.Text}]?\nThao tác này sẽ thu hồi quyền đăng nhập vào hệ thống.",
                "Xác nhận xóa tài khoản", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using var conn = DatabaseHelper.GetConnection();
                conn.Open();
                using var tx = conn.BeginTransaction();

                // 1. Xóa tài khoản đăng nhập
                using (var cmd = new SqlCommand("DELETE FROM TaiKhoan WHERE MaTaiKhoan = @id", conn, tx))
                {
                    cmd.Parameters.AddWithValue("@id", maTaiKhoan);
                    cmd.ExecuteNonQuery();
                }

                // 2. Thử xóa hồ sơ nhân viên nếu chưa từng phụ trách đơn đặt phòng nào
                try
                {
                    using (var cmd = new SqlCommand("DELETE FROM NhanVien WHERE MaNhanVien = @nv", conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@nv", maNhanVien);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch
                {
                    // Nếu nhân viên đã gắn với dữ liệu đặt phòng trước đó, giữ lại hồ sơ NhanVien để bảo toàn lịch sử giao dịch
                }

                tx.Commit();
                MessageBox.Show("Đã xóa tài khoản nhân sự thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTaiKhoan();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xóa tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            maTaiKhoan = 0;
            maNhanVien = 0;
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            cboChucVu.SelectedIndex = 1;
            cboVaiTro.SelectedIndex = 1;

            btnLuu.Text = "💾 Lưu tài khoản";
            btnLuu.BackColor = UITheme.PrimaryOrange;
            btnXoa.Enabled = false;

            grid.ClearSelection();
            txtHoTen.Focus();
        }
    }
}
