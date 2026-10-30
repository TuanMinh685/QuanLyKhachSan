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
            KhoiTaoGiaoDien();
            UITheme.ApplyTheme(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadLoaiPhongFilter();
            LoadPhong();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible && !DesignMode && IsHandleCreated && dtPhong != null)
            {
                LoadPhong();
            }
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
                AutoSize = true,
                UseMnemonic = false,
                Margin = new Padding(0, 0, 0, 2)
            };

            lblSubtitle = new Label
            {
                Text = "Tra cứu thông tin, hình ảnh thực tế, tiện nghi buồng phòng và trạng thái theo thời gian thực",
                Font = new Font("Segoe UI", 8.8F),
                ForeColor = UITheme.TextMuted,
                AutoSize = true,
                UseMnemonic = false,
                Margin = new Padding(0)
            };

            // Tiêu đề + phụ đề xếp dọc, bên trái (không còn đè lên nhau)
            var pnlTieuDe = new FlowLayoutPanel
            {
                Dock = DockStyle.Left,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.Transparent
            };
            pnlTieuDe.Controls.Add(lblTitle);
            pnlTieuDe.Controls.Add(lblSubtitle);

            // Thẻ thống kê (Pills): bám mép phải, tự giãn theo nội dung (không bị cắt chữ)
            pnlStats = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Padding = new Padding(0, 8, 0, 0),
                BackColor = Color.Transparent
            };

            lblPillBaoTri = TaoThePill("🔴 Bảo trì: 0", Color.FromArgb(254, 226, 226), Color.FromArgb(220, 38, 38));
            lblPillHoatDong = TaoThePill("🟢 Hoạt động: 0", Color.FromArgb(220, 252, 231), Color.FromArgb(22, 163, 74));
            lblPillTong = TaoThePill("🏨 Tổng: 0 phòng", Color.FromArgb(241, 245, 249), UITheme.DarkNavy);

            pnlStats.Controls.AddRange(new Control[] { lblPillBaoTri, lblPillHoatDong, lblPillTong });

            // Divider dưới Header. Thứ tự Add quan trọng: control Add SAU được xếp bố cục TRƯỚC,
            // nên divider (Bottom) thêm cuối để chiếm 1px đáy trước, rồi trái/phải mới chia phần còn lại.
            var pnlDivider = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = UITheme.BorderColor
            };
            pnlHeader.Controls.Add(pnlStats);
            pnlHeader.Controls.Add(pnlTieuDe);
            pnlHeader.Controls.Add(pnlDivider);

            // 2. TOOLBAR TÌM KIẾM & NÚT HÀNH ĐỘNG
            pnlToolbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 56,
                BackColor = Color.White,
                Padding = new Padding(24, 0, 24, 0)
            };

            // Ô tìm kiếm
            txtTim = new TextBox
            {
                Size = new Size(260, 29),
                Font = new Font("Segoe UI", 9.5F),
                PlaceholderText = "🔍 Tìm số phòng, loại phòng...",
                Margin = new Padding(0, 0, 10, 0)
            };
            txtTim.TextChanged += (_, __) => ApDungBoLoc();

            // Lọc loại phòng
            cboLocLoai = new ComboBox
            {
                Size = new Size(200, 29),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F),
                Margin = new Padding(0, 0, 10, 0)
            };
            cboLocLoai.SelectedIndexChanged += (_, __) => ApDungBoLoc();

            // Lọc trạng thái
            cboLocTrangThai = new ComboBox
            {
                Size = new Size(180, 29),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F),
                Margin = new Padding(0)
            };
            cboLocTrangThai.Items.AddRange(new object[] { "-- Tất cả trạng thái --", "Hoạt động", "Bảo trì" });
            cboLocTrangThai.SelectedIndex = 0;
            cboLocTrangThai.SelectedIndexChanged += (_, __) => ApDungBoLoc();

            // Nhóm ô tìm kiếm + bộ lọc bên trái
            var flowLoc = new FlowLayoutPanel
            {
                Dock = DockStyle.Left,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 12, 0, 0),
                BackColor = Color.Transparent
            };
            flowLoc.Controls.AddRange(new Control[] { txtTim, cboLocLoai, cboLocTrangThai });

            // Các nút hành động bên phải: bám mép phải, tự giãn theo độ dài chữ
            var flowActions = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Padding = new Padding(0, 10, 0, 0),
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

            var pnlDivider2 = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = UITheme.BorderColor
            };
            pnlToolbar.Controls.Add(flowActions);
            pnlToolbar.Controls.Add(flowLoc);
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

            // PHÂN QUYỀN: Admin được thêm/sửa/xóa. Lễ tân chỉ xem và đổi trạng thái phòng.
            if (!Session.LaAdmin)
            {
                btnThem.Visible = false;
                btnXoa.Visible = false;
                itemXoa.Visible = false;
                contextMenuGrid.Items[2].Visible = false; // đường kẻ phân cách trước mục Xóa
                btnSua.Text = "🔄 Đổi trạng thái";
                itemSua.Text = "🔄 Đổi trạng thái phòng";
            }

            grid.CellDoubleClick += (_, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    XemChiTietPhong();
                }
            };

            grid.CellFormatting += Grid_CellFormatting;

            pnlGridContainer.Controls.Add(grid);

            // Gắn các control vào Form theo thứ tự phân lớp
            Controls.Add(pnlGridContainer);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeader);
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
                AutoSize = true,                       // giãn theo độ dài chữ, không bị cắt ("Chi tiết & Ảnh", "Làm mới")
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(width, 34),
                Padding = new Padding(10, 0, 10, 0),
                BackColor = bg,
                ForeColor = fg,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false,
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
            if (grid.Columns == null || grid.Columns.Count == 0) return;

            // Ẩn các cột kỹ thuật
            if (grid.Columns["MaPhong"] != null) grid.Columns["MaPhong"].Visible = false;
            if (grid.Columns["HinhAnh"] != null) grid.Columns["HinhAnh"].Visible = false;
            if (grid.Columns["MaLoai"] != null) grid.Columns["MaLoai"].Visible = false;
            if (grid.Columns["GhiChu"] != null) grid.Columns["GhiChu"].Visible = false;

            // Định dạng các cột hiển thị bằng FillWeight (chuẩn DataGridView Fill Mode an toàn 100%)
            if (grid.Columns["Số phòng"] != null)
            {
                var col = grid.Columns["Số phòng"];
                col.HeaderText = "SỐ PHÒNG";
                col.FillWeight = 85;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                col.DefaultCellStyle.ForeColor = UITheme.DarkNavy;
            }

            if (grid.Columns["Tầng"] != null)
            {
                var col = grid.Columns["Tầng"];
                col.HeaderText = "TẦNG";
                col.FillWeight = 55;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (grid.Columns["Loại phòng"] != null)
            {
                var col = grid.Columns["Loại phòng"];
                col.HeaderText = "LOẠI PHÒNG";
                col.FillWeight = 120;
                col.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }

            if (grid.Columns["Giá/đêm"] != null)
            {
                var col = grid.Columns["Giá/đêm"];
                col.HeaderText = "GIÁ / ĐÊM";
                col.FillWeight = 100;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.DefaultCellStyle.Format = "#,##0 đ";
                col.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                col.DefaultCellStyle.ForeColor = UITheme.PrimaryOrange;
            }

            if (grid.Columns["Sức chứa"] != null)
            {
                var col = grid.Columns["Sức chứa"];
                col.HeaderText = "SỨC CHỨA";
                col.FillWeight = 75;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (grid.Columns["Diện tích"] != null)
            {
                var col = grid.Columns["Diện tích"];
                col.HeaderText = "DIỆN TÍCH";
                col.FillWeight = 80;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.DefaultCellStyle.Format = "0.## m²";
            }

            if (grid.Columns["Trạng thái"] != null)
            {
                var col = grid.Columns["Trạng thái"];
                col.HeaderText = "TRẠNG THÁI";
                col.FillWeight = 95;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }

            if (grid.Columns["Quy cách giường"] != null)
            {
                var col = grid.Columns["Quy cách giường"];
                col.HeaderText = "QUY CÁCH GIƯỜNG";
                col.FillWeight = 130;
            }

            if (grid.Columns["Tiện nghi"] != null)
            {
                var col = grid.Columns["Tiện nghi"];
                col.HeaderText = "TIỆN NGHI BUỒNG PHÒNG";
                col.FillWeight = 200;
            }

            UITheme.DongBoCanLeTieuDe(grid); // tiêu đề canh lề giống số liệu bên dưới
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= grid.Rows.Count) return;
            if (e.ColumnIndex < 0 || e.ColumnIndex >= grid.Columns.Count) return;

            string colName = grid.Columns[e.ColumnIndex].Name;

            // AlternatingRowsDefaultCellStyle ưu tiên hơn style của cột, nên màu cột phải đặt tại đây
            // để các dòng chẵn/lẻ cùng một màu (trước đây giá cam/đen xen kẽ).
            if (colName == "Giá/đêm")
            {
                e.CellStyle.ForeColor = UITheme.PrimaryOrange;
            }
            else if (colName == "Số phòng")
            {
                e.CellStyle.ForeColor = UITheme.DarkNavy;
            }
            else if (colName == "Trạng thái" && e.Value != null)
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
            string kw = EscapeLikeValue(txtTim.Text.Trim());
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

            try
            {
                dvPhong.RowFilter = filter;
            }
            catch (Exception)
            {
                dvPhong.RowFilter = "1=0"; // từ khóa không hợp lệ: hiện danh sách rỗng thay vì báo lỗi
            }
        }

        // Escape theo quy tắc của DataView.RowFilter (LIKE): ' -> '', và [ ] * % đặt trong [ ].
        private static string EscapeLikeValue(string value)
        {
            var sb = new System.Text.StringBuilder(value.Length + 8);
            foreach (char c in value)
            {
                switch (c)
                {
                    case ']': case '[': case '%': case '*': sb.Append('[').Append(c).Append(']'); break;
                    case '\'': sb.Append("''"); break;
                    default: sb.Append(c); break;
                }
            }
            return sb.ToString();
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

        private static bool CanQuyenAdmin()
        {
            if (Session.LaAdmin) return true;
            MessageBox.Show("Chỉ Admin mới được thêm hoặc xóa phòng. Lễ tân chỉ được đổi trạng thái phòng.",
                "Không đủ quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void ThemPhong()
        {
            if (!CanQuyenAdmin()) return;
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

            using var modal = new frmPhongModal(id, !Session.LaAdmin);
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                LoadPhong();
            }
        }

        private void XoaPhong()
        {
            if (!CanQuyenAdmin()) return;
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
