using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmDanhSachPhong : Form
    {
        private DataTable dtPhong;
        private DataView dvPhong;

        // Header & Thẻ thống kê
        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private FlowLayoutPanel pnlStats;
        private Label lblPillTong;
        private Label lblPillHoatDong;
        private Label lblPillBaoTri;

        // Toolbar tìm kiếm & chức năng
        private Panel pnlToolbar;
        private TextBox txtTim;
        private ComboBox cboLocLoai;
        private ComboBox cboLocTrangThai;
        private Button btnThem;
        private Button btnXemChiTiet;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;

        // DataGridView chính
        private Panel pnlGridContainer;
        private DataGridView grid;
        private ContextMenuStrip contextMenuGrid;

        public frmDanhSachPhong()
        {
            InitializeComponent();
            UITheme.ApplyTheme(this);
            KhoiTaoGiaoDien();
            LoadLoaiPhongFilter();
            LoadPhong();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Quản lý buồng phòng";
            Dock = DockStyle.Fill;
            BackColor = UITheme.BackgroundGray;
            Font = UITheme.MainFont;

            // 1. HEADER PANEL
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 84,
                BackColor = Color.White,
                Padding = new Padding(24, 12, 24, 10)
            };

            lblTitle = new Label
            {
                Text = "🚪 QUẢN LÝ BUỒNG PHÒNG & HÌNH ẢNH CHI TIẾT",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(24, 12),
                AutoSize = true
            };

            lblSubtitle = new Label
            {
                Text = "Tra cứu thông tin, hình ảnh thực tế, tiện nghi buồng phòng và trạng thái theo thời gian thực",
                Font = new Font("Segoe UI", 8.8F),
                ForeColor = UITheme.TextMuted,
                Location = new Point(24, 38),
                AutoSize = true
            };

            // Thẻ thống kê (Pills)
            pnlStats = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(Width - 490, 20),
                Size = new Size(460, 36),
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                BackColor = Color.Transparent
            };

            lblPillBaoTri = TaoThePill("🔴 Bảo trì: 0", Color.FromArgb(254, 226, 226), Color.FromArgb(220, 38, 38));
            lblPillHoatDong = TaoThePill("🟢 Hoạt động: 0", Color.FromArgb(220, 252, 231), Color.FromArgb(22, 163, 74));
            lblPillTong = TaoThePill("🏨 Tổng: 0 phòng", Color.FromArgb(241, 245, 249), UITheme.DarkNavy);

            pnlStats.Controls.AddRange(new Control[] { lblPillBaoTri, lblPillHoatDong, lblPillTong });

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(pnlStats);

            // Divider dưới Header
            var pnlDivider = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = UITheme.BorderColor
            };
            pnlHeader.Controls.Add(pnlDivider);

            // 2. TOOLBAR TÌM KIẾM & NÚT HÀNH ĐỘNG
            pnlToolbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 56,
                BackColor = Color.White,
                Padding = new Padding(24, 10, 24, 10)
            };

            // Ô tìm kiếm
            txtTim = new TextBox
            {
                Location = new Point(24, 13),
                Size = new Size(240, 29),
                Font = new Font("Segoe UI", 9.5F),
                PlaceholderText = "🔍 Tìm số phòng, loại phòng..."
            };
            txtTim.TextChanged += (_, __) => ApDungBoLoc();

            // Lọc loại phòng
            cboLocLoai = new ComboBox
            {
                Location = new Point(274, 13),
                Size = new Size(160, 29),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F)
            };
            cboLocLoai.SelectedIndexChanged += (_, __) => ApDungBoLoc();

            // Lọc trạng thái
            cboLocTrangThai = new ComboBox
            {
                Location = new Point(444, 13),
                Size = new Size(135, 29),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F)
            };
            cboLocTrangThai.Items.AddRange(new object[] { "-- Tất cả trạng thái --", "Hoạt động", "Bảo trì" });
            cboLocTrangThai.SelectedIndex = 0;
            cboLocTrangThai.SelectedIndexChanged += (_, __) => ApDungBoLoc();

            // Các nút hành động bên phải
            var flowActions = new FlowLayoutPanel
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(Width - 580, 8),
                Size = new Size(550, 40),
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                BackColor = Color.Transparent
            };

            btnLamMoi = TaoNutThaoTac("🔄 Làm mới", UITheme.TextMuted, Color.White, 90);
            btnLamMoi.Click += (_, __) =>
            {
                txtTim.Clear();
                if (cboLocLoai.Items.Count > 0) cboLocLoai.SelectedIndex = 0;
                cboLocTrangThai.SelectedIndex = 0;
                LoadPhong();
            };

            btnXoa = TaoNutThaoTac("🗑️ Xóa", Color.FromArgb(239, 68, 68), Color.White, 85);
            btnXoa.Click += (_, __) => XoaPhong();

            btnSua = TaoNutThaoTac("✏️ Sửa", Color.FromArgb(15, 37, 55), Color.White, 85);
            btnSua.Click += (_, __) => SuaPhong();

            btnXemChiTiet = TaoNutThaoTac("👁️ Chi tiết & Ảnh", Color.FromArgb(37, 99, 235), Color.White, 135);
            btnXemChiTiet.Click += (_, __) => XemChiTietPhong();

            btnThem = TaoNutThaoTac("➕ Thêm phòng", Color.FromArgb(234, 88, 12), Color.White, 125);
            btnThem.Click += (_, __) => ThemPhong();

            flowActions.Controls.AddRange(new Control[] { btnLamMoi, btnXoa, btnSua, btnXemChiTiet, btnThem });

            pnlToolbar.Controls.Add(txtTim);
            pnlToolbar.Controls.Add(cboLocLoai);
            pnlToolbar.Controls.Add(cboLocTrangThai);
            pnlToolbar.Controls.Add(flowActions);

            var pnlDivider2 = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = UITheme.BorderColor
            };
            pnlToolbar.Controls.Add(pnlDivider2);

            // 3. CONTAINER CHỨA GRID TOÀN MÀN HÌNH
            pnlGridContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24, 16, 24, 18),
                BackColor = UITheme.BackgroundGray
            };

            grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            UITheme.FormatDataGridView(grid);

            // Menu ngữ cảnh chuột phải trên dòng
            contextMenuGrid = new ContextMenuStrip();
            var itemChiTiet = new ToolStripMenuItem("👁️ Xem chi tiết & Hình ảnh");
            itemChiTiet.Click += (_, __) => XemChiTietPhong();
            var itemSua = new ToolStripMenuItem("✏️ Chỉnh sửa thông tin");
            itemSua.Click += (_, __) => SuaPhong();
            var itemXoa = new ToolStripMenuItem("🗑️ Xóa phòng này");
            itemXoa.Click += (_, __) => XoaPhong();

            contextMenuGrid.Items.AddRange(new ToolStripItem[] {
                itemChiTiet,
                itemSua,
                new ToolStripSeparator(),
                itemXoa
            });
            grid.ContextMenuStrip = contextMenuGrid;

            grid.CellDoubleClick += (_, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    XemChiTietPhong();
                }
            };

            grid.CellFormatting += Grid_CellFormatting;

            pnlGridContainer.Controls.Add(grid);

            // Gắn vào Form
            Controls.Add(pnlGridContainer);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeader);

            Resize += (_, __) =>
            {
                pnlStats.Location = new Point(Math.Max(300, Width - 490), 20);
                flowActions.Location = new Point(Math.Max(500, Width - 580), 8);
            };
        }

        private static Label TaoThePill(string text, Color bg, Color fg)
        {
            var lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 8.8F, FontStyle.Bold),
                ForeColor = fg,
                BackColor = bg,
                AutoSize = true,
                Padding = new Padding(10, 5, 10, 5),
                Margin = new Padding(4, 0, 0, 0)
            };
            return lbl;
        }

        private static Button TaoNutThaoTac(string text, Color bg, Color fg, int width)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(width, 34),
                BackColor = bg,
                ForeColor = fg,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(4, 0, 0, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void LoadLoaiPhongFilter()
        {
            try
            {
                var dt = DatabaseHelper.ExecuteQuery("SELECT MaLoai, TenLoai FROM LoaiPhong ORDER BY TenLoai");
                var rowAll = dt.NewRow();
                rowAll["MaLoai"] = 0;
                rowAll["TenLoai"] = "-- Tất cả loại phòng --";
                dt.Rows.InsertAt(rowAll, 0);

                cboLocLoai.DataSource = dt;
                cboLocLoai.DisplayMember = "TenLoai";
                cboLocLoai.ValueMember = "MaLoai";
                cboLocLoai.SelectedIndex = 0;
            }
            catch { }
        }

        private void LoadPhong()
        {
            try
            {
                string sql = @"
                    SELECT 
                        p.MaPhong,
                        p.SoPhong AS ""Số phòng"",
                        p.Tang AS ""Tầng"",
                        l.TenLoai AS ""Loại phòng"",
                        l.GiaMotDem AS ""Giá/đêm"",
                        l.SoNguoiToiDa AS ""Sức chứa"",
                        COALESCE(p.MoTaGiuong, '') AS ""Quy cách giường"",
                        p.DienTich AS ""Diện tích"",
                        p.TinhTrang AS ""Trạng thái"",
                        COALESCE(p.TienNghi, '') AS ""Tiện nghi"",
                        COALESCE(p.GhiChu, '') AS ""Ghi chú"",
                        COALESCE(p.HinhAnh, '') AS ""HinhAnh"",
                        p.MaLoai
                    FROM Phong p
                    JOIN LoaiPhong l ON p.MaLoai = l.MaLoai
                    ORDER BY p.SoPhong";

                dtPhong = DatabaseHelper.ExecuteQuery(sql);
                dvPhong = dtPhong.DefaultView;
                grid.DataSource = dvPhong;

                DinhDangCotGrid();
                CapNhatThongKe();
                ApDungBoLoc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được danh sách phòng từ Supabase:\n" + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DinhDangCotGrid()
        {
            if (grid.Columns.Contains("MaPhong")) grid.Columns["MaPhong"].Visible = false;
            if (grid.Columns.Contains("HinhAnh")) grid.Columns["HinhAnh"].Visible = false;
            if (grid.Columns.Contains("MaLoai")) grid.Columns["MaLoai"].Visible = false;
            if (grid.Columns.Contains("GhiChu")) grid.Columns["GhiChu"].Visible = false;

            if (grid.Columns.Contains("Số phòng"))
            {
                grid.Columns["Số phòng"].Width = 100;
                grid.Columns["Số phòng"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["Số phòng"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                grid.Columns["Số phòng"].DefaultCellStyle.ForeColor = UITheme.DarkNavy;
            }

            if (grid.Columns.Contains("Tầng"))
            {
                grid.Columns["Tầng"].Width = 70;
                grid.Columns["Tầng"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (grid.Columns.Contains("Loại phòng"))
            {
                grid.Columns["Loại phòng"].Width = 150;
                grid.Columns["Loại phòng"].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }

            if (grid.Columns.Contains("Giá/đêm"))
            {
                grid.Columns["Giá/đêm"].Width = 130;
                grid.Columns["Giá/đêm"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grid.Columns["Giá/đêm"].DefaultCellStyle.Format = "#,##0 đ";
                grid.Columns["Giá/đêm"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                grid.Columns["Giá/đêm"].DefaultCellStyle.ForeColor = UITheme.PrimaryOrange;
            }

            if (grid.Columns.Contains("Sức chứa"))
            {
                grid.Columns["Sức chứa"].Width = 95;
                grid.Columns["Sức chứa"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (grid.Columns.Contains("Diện tích"))
            {
                grid.Columns["Diện tích"].Width = 95;
                grid.Columns["Diện tích"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grid.Columns["Diện tích"].DefaultCellStyle.Format = "0.## m²";
            }

            if (grid.Columns.Contains("Trạng thái"))
            {
                grid.Columns["Trạng thái"].Width = 120;
                grid.Columns["Trạng thái"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns["Trạng thái"].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }

            if (grid.Columns.Contains("Quy cách giường"))
            {
                grid.Columns["Quy cách giường"].Width = 160;
            }

            if (grid.Columns.Contains("Tiện nghi"))
            {
                grid.Columns["Tiện nghi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= grid.Rows.Count) return;

            string colName = grid.Columns[e.ColumnIndex].Name;

            if (colName == "Trạng thái" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status.Equals("Hoạt động", StringComparison.OrdinalIgnoreCase))
                {
                    e.Value = "🟢 Hoạt động";
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                }
                else
                {
                    e.Value = "🔴 Bảo trì";
                    e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                }
                e.FormattingApplied = true;
            }
            else if (colName == "Sức chứa" && e.Value != null && int.TryParse(e.Value.ToString(), out int sc))
            {
                e.Value = $"{sc} người";
                e.FormattingApplied = true;
            }
            else if (colName == "Tầng" && e.Value != null)
            {
                e.Value = $"Tầng {e.Value}";
                e.FormattingApplied = true;
            }
        }

        private void CapNhatThongKe()
        {
            if (dtPhong == null) return;

            int tong = dtPhong.Rows.Count;
            int hoatDong = 0;
            int baoTri = 0;

            foreach (DataRow r in dtPhong.Rows)
            {
                string tt = r["Trạng thái"]?.ToString() ?? "";
                if (tt.Equals("Hoạt động", StringComparison.OrdinalIgnoreCase))
                    hoatDong++;
                else
                    baoTri++;
            }

            lblPillTong.Text = $"🏨 Tổng: {tong} phòng";
            lblPillHoatDong.Text = $"🟢 Hoạt động: {hoatDong}";
            lblPillBaoTri.Text = $"🔴 Bảo trì: {baoTri}";
        }

        private void ApDungBoLoc()
        {
            if (dvPhong == null) return;

            string filter = "1=1";

            // Lọc theo từ khóa tìm kiếm
            string kw = txtTim.Text.Trim().Replace("'", "''");
            if (!string.IsNullOrWhiteSpace(kw))
            {
                filter += $" AND ([Số phòng] LIKE '%{kw}%' OR [Loại phòng] LIKE '%{kw}%' OR [Tiện nghi] LIKE '%{kw}%')";
            }

            // Lọc theo loại phòng
            if (cboLocLoai.SelectedValue != null && int.TryParse(cboLocLoai.SelectedValue.ToString(), out int maLoai) && maLoai > 0)
            {
                filter += $" AND MaLoai = {maLoai}";
            }

            // Lọc theo trạng thái
            if (cboLocTrangThai.SelectedIndex > 0)
            {
                string tt = cboLocTrangThai.SelectedItem.ToString();
                filter += $" AND [Trạng thái] = '{tt}'";
            }

            dvPhong.RowFilter = filter;
        }

        private int LayMaPhongDangChon()
        {
            if (grid.CurrentRow == null || !grid.Columns.Contains("MaPhong") || grid.CurrentRow.Cells["MaPhong"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn một phòng trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            return Convert.ToInt32(grid.CurrentRow.Cells["MaPhong"].Value);
        }

        private void ThemPhong()
        {
            using var modal = new frmPhongModal(0);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                LoadPhong();
            }
        }

        private void XemChiTietPhong()
        {
            int id = LayMaPhongDangChon();
            if (id <= 0) return;

            using var modal = new frmChiTietPhongModal(id);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                LoadPhong();
            }
        }

        private void SuaPhong()
        {
            int id = LayMaPhongDangChon();
            if (id <= 0) return;

            using var modal = new frmPhongModal(id);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                LoadPhong();
            }
        }

        private void XoaPhong()
        {
            int id = LayMaPhongDangChon();
            if (id <= 0) return;

            string soPhong = grid.CurrentRow.Cells["Số phòng"].Value?.ToString() ?? id.ToString();

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa phòng {soPhong} khỏi hệ thống không?\n\nLưu ý: Nếu phòng đã có lịch sử đặt phòng thì không nên xóa.",
                "Xác nhận xóa phòng", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                int affected = DatabaseHelper.ExecuteNonQuery("DELETE FROM Phong WHERE MaPhong = @id", new[] { new SqlParameter("@id", id) });
                if (affected > 0)
                {
                    MessageBox.Show($"Đã xóa phòng {soPhong} thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPhong();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể xóa phòng {soPhong}. Phòng này có thể đang liên kết với các giao dịch hoặc phiếu đặt phòng trong CSDL.\nChi tiết lỗi: {ex.Message}",
                    "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
