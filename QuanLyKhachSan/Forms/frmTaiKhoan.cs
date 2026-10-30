using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmTaiKhoan : Form
    {
        public frmTaiKhoan()
        {
            InitializeComponent();
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

            BuildUI();
            LoadTaiKhoan();
        }

        private void BuildUI()
        {
            SuspendLayout();
            Padding = new Padding(24);

            cardMain = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(24)
            };
            cardMain.Paint += (s, e) => UITheme.DrawCard(s, e, 12);

            // 1. Header
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 84,
                BackColor = Color.Transparent
            };

            lblTitle = new Label
            {
                Text = "👤 QUẢN LÝ NHÂN SỰ & TÀI KHOẢN HỆ THỐNG",
                Font = new Font("Segoe UI", 13.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(0, 0),
                AutoSize = true
            };

            lblSubtitle = new Label
            {
                Text = "Danh sách nhân viên khách sạn, tài khoản đăng nhập và phân quyền truy cập hệ thống",
                Font = new Font("Segoe UI", 9F),
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

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(pnlStatsPills);
            cardMain.Controls.Add(pnlHeader);

            // 2. Toolbar (Tìm kiếm bên trái + Nút hành động bên phải)
            pnlToolbar = new Panel
            {
                Location = new Point(24, 114),
                Height = 42,
                Width = cardMain.ClientSize.Width - 48,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent
            };

            pnlSearch = new Panel
            {
                Dock = DockStyle.Fill,   // lấp đầy phần bên trái, nhường chỗ cho nhóm nút bên phải
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
            pnlToolbar.Controls.Add(pnlSearch);

            // Nút Thêm mới (Màu cam nổi bật)
            btnThem = new Button
            {
                Text = "➕ Thêm nhân sự mới",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(175, 40),
                Padding = new Padding(10, 0, 10, 0),
                Margin = new Padding(8, 0, 0, 0),
                UseMnemonic = false,
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.PrimaryOrange,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnThem.FlatAppearance.BorderSize = 0;
            btnThem.Click += (_, __) => MoModalThem();

            // Nút Chỉnh sửa (Màu xanh dương)
            btnSua = new Button
            {
                Text = "✏️ Chỉnh sửa",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(120, 40),
                Padding = new Padding(10, 0, 10, 0),
                Margin = new Padding(8, 0, 0, 0),
                UseMnemonic = false,
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.PrimaryBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.Click += (_, __) => MoModalSua();

            // Nút Xóa
            btnXoa = new Button
            {
                Text = "🗑 Xóa",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(95, 40),
                Padding = new Padding(10, 0, 10, 0),
                Margin = new Padding(8, 0, 0, 0),
                UseMnemonic = false,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(254, 242, 242),
                ForeColor = Color.FromArgb(220, 38, 38),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnXoa.FlatAppearance.BorderColor = Color.FromArgb(254, 202, 202);
            btnXoa.Click += (_, __) => XoaTaiKhoan();

            // Nút Làm mới
            btnLamMoi = new Button
            {
                Text = "🔄 Làm mới",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(100, 40),
                Padding = new Padding(10, 0, 10, 0),
                Margin = new Padding(8, 0, 0, 0),
                UseMnemonic = false,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = UITheme.DarkNavy,
                Font = new Font("Segoe UI", 9.5F),
                Cursor = Cursors.Hand
            };
            btnLamMoi.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnLamMoi.Click += (_, __) => { txtTim.Clear(); LoadTaiKhoan(); };

            // Nhóm nút bám mép phải, tự giãn theo chữ. Thêm SAU pnlSearch để được xếp bố cục trước,
            // phần còn lại bên trái mới thuộc về ô tìm kiếm (trước đây ô tìm kiếm tràn đè lên các nút Sửa/Xóa/Làm mới).
            var flowNut = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                BackColor = Color.Transparent
            };
            flowNut.Controls.Add(btnThem);     // ngoài cùng bên phải
            flowNut.Controls.Add(btnSua);
            flowNut.Controls.Add(btnXoa);
            flowNut.Controls.Add(btnLamMoi);
            pnlToolbar.Controls.Add(flowNut);
            cardMain.Controls.Add(pnlToolbar);

            // 3. Bọc bảng DataGridView Full 100%
            pnlGridWrapper = new Panel
            {
                Location = new Point(24, 168),
                Size = new Size(cardMain.ClientSize.Width - 48, cardMain.ClientSize.Height - 192),
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
            grid.CellDoubleClick += (_, __) => MoModalSua();

            pnlGridWrapper.Controls.Add(grid);
            cardMain.Controls.Add(pnlGridWrapper);

            Controls.Add(cardMain);
            ResumeLayout(false);
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
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, lbl.Width - 1, lbl.Height - 1);
                using var path = UITheme.GetRoundedRectPath(rect, 6);
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1F);
                e.Graphics.DrawPath(pen, path);
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

        private void MoModalThem()
        {
            using var modal = new frmTaiKhoanModal(0, 0);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                LoadTaiKhoan();
            }
        }

        private void MoModalSua()
        {
            if (grid.CurrentRow == null || grid.CurrentRow.Cells["MaTaiKhoan"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên trong danh sách để chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idTK = Convert.ToInt32(grid.CurrentRow.Cells["MaTaiKhoan"].Value);
            int idNV = Convert.ToInt32(grid.CurrentRow.Cells["MaNhanVien"].Value);

            using var modal = new frmTaiKhoanModal(idTK, idNV);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                LoadTaiKhoan();
            }
        }

        private void XoaTaiKhoan()
        {
            if (grid.CurrentRow == null || grid.CurrentRow.Cells["MaTaiKhoan"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản trong danh sách để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idTK = Convert.ToInt32(grid.CurrentRow.Cells["MaTaiKhoan"].Value);
            int idNV = Convert.ToInt32(grid.CurrentRow.Cells["MaNhanVien"].Value);
            string user = grid.CurrentRow.Cells["Tên đăng nhập"]?.Value?.ToString() ?? "";
            string hoTen = grid.CurrentRow.Cells["Nhân viên"]?.Value?.ToString() ?? "";

            if (user.Equals(Session.TenDangNhap, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Bạn không thể xóa tài khoản của chính mình đang đăng nhập!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa tài khoản [{user}] của nhân viên [{hoTen}]?\nThao tác này sẽ thu hồi quyền đăng nhập vào hệ thống.",
                "Xác nhận xóa tài khoản", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using var conn = DatabaseHelper.GetConnection();
                conn.Open();
                using var tx = conn.BeginTransaction();

                // Không được xóa Admin cuối cùng: kiểm tra ngay trong transaction ngay trước khi xóa
                string vaiTroMucTieu;
                using (var cmdVai = new SqlCommand("SELECT VaiTro FROM TaiKhoan WHERE MaTaiKhoan = @id", conn, tx))
                {
                    cmdVai.Parameters.AddWithValue("@id", idTK);
                    vaiTroMucTieu = Convert.ToString(cmdVai.ExecuteScalar()) ?? "";
                }

                if (vaiTroMucTieu.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    int soAdminKhac;
                    using (var cmdSo = new SqlCommand(
                        "SELECT COUNT(*) FROM TaiKhoan WHERE UPPER(VaiTro) = 'ADMIN' AND MaTaiKhoan <> @id", conn, tx))
                    {
                        cmdSo.Parameters.AddWithValue("@id", idTK);
                        soAdminKhac = Convert.ToInt32(cmdSo.ExecuteScalar());
                    }

                    if (soAdminKhac == 0)
                    {
                        tx.Rollback();
                        MessageBox.Show("Không thể xóa Admin cuối cùng. Hệ thống phải còn ít nhất một tài khoản Admin.",
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                using (var cmd = new SqlCommand("DELETE FROM TaiKhoan WHERE MaTaiKhoan = @id", conn, tx))
                {
                    cmd.Parameters.AddWithValue("@id", idTK);
                    cmd.ExecuteNonQuery();
                }

                // Hồ sơ nhân viên có thể đang được phiếu đặt phòng/hóa đơn tham chiếu nên chưa chắc xóa được.
                // PostgreSQL: một lệnh lỗi làm HỎNG CẢ transaction (kể cả lệnh xóa tài khoản ở trên), nên không thể
                // dùng try/catch trống như SQL Server. Dùng SAVEPOINT để chỉ hoàn tác riêng lệnh xóa hồ sơ.
                bool daXoaHoSo = true;
                tx.Save("sp_xoa_nhanvien");
                try
                {
                    using (var cmd = new SqlCommand("DELETE FROM NhanVien WHERE MaNhanVien = @nv", conn, tx))
                    {
                        cmd.Parameters.AddWithValue("@nv", idNV);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (PostgresException)
                {
                    tx.Rollback("sp_xoa_nhanvien");
                    daXoaHoSo = false;
                }

                tx.Commit();
                BaoMatHelper.GhiNhatKy(Session.TenDangNhap, "XOA_TAI_KHOAN", $"Xóa tài khoản [{user}] của nhân viên [{hoTen}] (Mã TK: {idTK})");
                MessageBox.Show(daXoaHoSo
                        ? "Đã xóa tài khoản và hồ sơ nhân sự thành công."
                        : "Đã xóa tài khoản đăng nhập.\nHồ sơ nhân viên được giữ lại vì còn gắn với dữ liệu đặt phòng/hóa đơn.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTaiKhoan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xóa tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
