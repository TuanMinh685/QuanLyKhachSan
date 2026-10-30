using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    /// <summary>
    /// Quản lý loại phòng & giá (chỉ Admin): xem, thêm, sửa, xóa loại phòng.
    /// Giá một đêm của phòng lấy từ loại phòng, nên đổi giá phòng = sửa giá ở đây.
    /// </summary>
    public partial class frmLoaiPhong : Form
    {
        private static readonly CultureInfo Vi = new CultureInfo("vi-VN");

        private DataTable dtLoai;
        private DataView dvLoai;

        private Label lblPillTong;
        private TextBox txtTim;
        private DataGridView grid;

        public frmLoaiPhong()
        {
            InitializeComponent();
            KhoiTaoGiaoDien();
            UITheme.ApplyTheme(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadLoaiPhong();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Quản lý loại phòng";
            Dock = DockStyle.Fill;
            BackColor = UITheme.BackgroundGray;
            Font = UITheme.MainFont;

            // ===== 1. HEADER =====
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 84,
                BackColor = Color.White,
                Padding = new Padding(24, 12, 24, 10)
            };

            var lblTitle = new Label
            {
                Text = "🏷️ QUẢN LÝ LOẠI PHÒNG & GIÁ",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                AutoSize = true,
                UseMnemonic = false,
                Margin = new Padding(0, 0, 0, 2)
            };
            var lblSubtitle = new Label
            {
                Text = "Khai báo các hạng phòng, giá một đêm và số khách tối đa. Giá của từng phòng lấy theo loại phòng",
                Font = new Font("Segoe UI", 8.8F),
                ForeColor = UITheme.TextMuted,
                AutoSize = true,
                UseMnemonic = false,
                Margin = new Padding(0)
            };
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

            lblPillTong = new Label
            {
                Text = "🏷️ Tổng: 0 loại phòng",
                Font = new Font("Segoe UI", 8.8F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                BackColor = Color.FromArgb(241, 245, 249),
                AutoSize = true,
                Padding = new Padding(10, 5, 10, 5),
                Margin = new Padding(0)
            };
            var pnlStats = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Padding = new Padding(0, 8, 0, 0),
                BackColor = Color.Transparent
            };
            pnlStats.Controls.Add(lblPillTong);

            var pnlDivider = new Panel { Dock = DockStyle.Bottom, Height = 1, BackColor = UITheme.BorderColor };
            // Control Add SAU được xếp bố cục TRƯỚC: divider (Bottom) thêm cuối để chiếm đáy trước.
            pnlHeader.Controls.Add(pnlStats);
            pnlHeader.Controls.Add(pnlTieuDe);
            pnlHeader.Controls.Add(pnlDivider);

            // ===== 2. TOOLBAR =====
            var pnlToolbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 56,
                BackColor = Color.White,
                Padding = new Padding(24, 0, 24, 0)
            };

            txtTim = new TextBox
            {
                Size = new Size(280, 29),
                Font = new Font("Segoe UI", 9.5F),
                PlaceholderText = "🔍 Tìm theo tên loại phòng...",
                Margin = new Padding(0)
            };
            txtTim.TextChanged += (_, __) => ApDungBoLoc();

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
            flowLoc.Controls.Add(txtTim);

            var btnThem = TaoNutThaoTac("➕ Thêm loại", UITheme.PrimaryOrange, Color.White, 110);
            btnThem.Click += (_, __) => ThemLoai();
            var btnSua = TaoNutThaoTac("✏️ Sửa", UITheme.DarkNavy, Color.White, 80);
            btnSua.Click += (_, __) => SuaLoai();
            var btnXoa = TaoNutThaoTac("🗑️ Xóa", UITheme.DangerRed, Color.White, 80);
            btnXoa.Click += (_, __) => XoaLoai();
            var btnLamMoi = TaoNutThaoTac("🔄 Làm mới", Color.FromArgb(71, 85, 105), Color.White, 100);
            btnLamMoi.Click += (_, __) => LoadLoaiPhong();

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
            // RightToLeft: control thêm đầu tiên nằm sát mép phải
            flowActions.Controls.AddRange(new Control[] { btnLamMoi, btnXoa, btnSua, btnThem });

            var pnlDivider2 = new Panel { Dock = DockStyle.Bottom, Height = 1, BackColor = UITheme.BorderColor };
            pnlToolbar.Controls.Add(flowActions);
            pnlToolbar.Controls.Add(flowLoc);
            pnlToolbar.Controls.Add(pnlDivider2);

            // ===== 3. LƯỚI DỮ LIỆU =====
            var pnlGridContainer = new Panel
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
            grid.CellFormatting += Grid_CellFormatting;
            grid.CellDoubleClick += (_, e) => { if (e.RowIndex >= 0) SuaLoai(); };

            var menu = new ContextMenuStrip();
            var itemSua = new ToolStripMenuItem("✏️ Chỉnh sửa loại phòng");
            itemSua.Click += (_, __) => SuaLoai();
            var itemXoa = new ToolStripMenuItem("🗑️ Xóa loại phòng này");
            itemXoa.Click += (_, __) => XoaLoai();
            menu.Items.AddRange(new ToolStripItem[] { itemSua, new ToolStripSeparator(), itemXoa });
            grid.ContextMenuStrip = menu;

            pnlGridContainer.Controls.Add(grid);

            // Fill thêm đầu tiên (index 0) để được xếp bố cục SAU header/toolbar
            Controls.Add(pnlGridContainer);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeader);
        }

        private static Button TaoNutThaoTac(string text, Color bg, Color fg, int width)
        {
            var btn = new Button
            {
                Text = text,
                AutoSize = true,
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

        // ================= DỮ LIỆU =================

        private void LoadLoaiPhong()
        {
            try
            {
                string sql = @"
SELECT lp.MaLoai,
       lp.TenLoai      AS ""Loại phòng"",
       lp.GiaMotDem    AS ""Giá/đêm"",
       lp.SoNguoiToiDa AS ""Sức chứa"",
       COUNT(p.MaPhong) AS ""Số phòng""
FROM LoaiPhong lp
LEFT JOIN Phong p ON p.MaLoai = lp.MaLoai
GROUP BY lp.MaLoai, lp.TenLoai, lp.GiaMotDem, lp.SoNguoiToiDa
ORDER BY lp.GiaMotDem, lp.TenLoai";

                dtLoai = DatabaseHelper.ExecuteQuery(sql);
                dvLoai = new DataView(dtLoai);
                grid.DataSource = dvLoai;
                DinhDangCot();
                ApDungBoLoc();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được danh sách loại phòng:\n" + ex.Message,
                    "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DinhDangCot()
        {
            if (grid.Columns["MaLoai"] != null) grid.Columns["MaLoai"].Visible = false;

            ThietLapCot("Loại phòng", "LOẠI PHÒNG", 40, DataGridViewContentAlignment.MiddleLeft, true);
            ThietLapCot("Giá/đêm", "GIÁ / ĐÊM", 25, DataGridViewContentAlignment.MiddleRight, true);
            ThietLapCot("Sức chứa", "SỨC CHỨA", 18, DataGridViewContentAlignment.MiddleCenter, false);
            ThietLapCot("Số phòng", "SỐ PHÒNG THUỘC LOẠI", 22, DataGridViewContentAlignment.MiddleCenter, false);
        }

        private void ThietLapCot(string ten, string tieuDe, float fillWeight, DataGridViewContentAlignment canLe, bool dam)
        {
            var col = grid.Columns[ten];
            if (col == null) return;
            col.HeaderText = tieuDe;
            col.FillWeight = fillWeight;
            col.DefaultCellStyle.Alignment = canLe;
            if (dam) col.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null || e.Value == DBNull.Value) return;
            string col = grid.Columns[e.ColumnIndex].Name;

            // Màu đặt tại đây vì style dòng chẵn/lẻ ghi đè màu của cột
            if (col == "Giá/đêm")
            {
                e.Value = string.Format(Vi, "{0:N0} đ", Convert.ToDecimal(e.Value));
                e.CellStyle.ForeColor = UITheme.PrimaryOrange;
                e.FormattingApplied = true;
            }
            else if (col == "Sức chứa")
            {
                e.Value = $"{e.Value} người";
                e.FormattingApplied = true;
            }
            else if (col == "Số phòng")
            {
                e.Value = $"{e.Value} phòng";
                e.FormattingApplied = true;
            }
            else if (col == "Loại phòng")
            {
                e.CellStyle.ForeColor = UITheme.DarkNavy;
            }
        }

        private void ApDungBoLoc()
        {
            if (dvLoai == null) return;
            string kw = EscapeLikeValue(txtTim.Text.Trim());
            try
            {
                dvLoai.RowFilter = string.IsNullOrWhiteSpace(kw) ? "" : $"[Loại phòng] LIKE '%{kw}%'";
            }
            catch (Exception)
            {
                dvLoai.RowFilter = "1=0";
            }
            lblPillTong.Text = $"🏷️ Tổng: {dvLoai.Count} loại phòng";
        }

        // Escape theo quy tắc LIKE của DataView.RowFilter
        private static string EscapeLikeValue(string value)
        {
            var sb = new StringBuilder(value.Length + 8);
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

        // ================= THAO TÁC =================

        private static bool CanQuyenAdmin()
        {
            if (Session.LaAdmin) return true;
            MessageBox.Show("Chỉ Admin mới được quản lý loại phòng và giá.",
                "Không đủ quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private int LayMaLoaiDangChon(out string tenLoai)
        {
            tenLoai = "";
            if (grid.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một loại phòng trong danh sách.", "Chưa chọn dòng",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            tenLoai = grid.CurrentRow.Cells["Loại phòng"].Value?.ToString() ?? "";
            return Convert.ToInt32(grid.CurrentRow.Cells["MaLoai"].Value);
        }

        private void ThemLoai()
        {
            if (!CanQuyenAdmin()) return;
            using var modal = new frmLoaiPhongModal(0);
            if (modal.ShowDialog(this) == DialogResult.OK) LoadLoaiPhong();
        }

        private void SuaLoai()
        {
            if (!CanQuyenAdmin()) return;
            int id = LayMaLoaiDangChon(out _);
            if (id <= 0) return;
            using var modal = new frmLoaiPhongModal(id);
            if (modal.ShowDialog(this) == DialogResult.OK) LoadLoaiPhong();
        }

        private void XoaLoai()
        {
            if (!CanQuyenAdmin()) return;
            int id = LayMaLoaiDangChon(out string ten);
            if (id <= 0) return;

            try
            {
                int soPhong = Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM Phong WHERE MaLoai = @id",
                    new[] { new SqlParameter("@id", id) }));

                if (soPhong > 0)
                {
                    MessageBox.Show(
                        $"Không thể xóa loại [{ten}] vì còn {soPhong} phòng thuộc loại này.\n\n" +
                        "Hãy chuyển các phòng đó sang loại khác (Quản lý phòng → Sửa) rồi xóa lại.",
                        "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Bạn có chắc muốn xóa loại phòng [{ten}] không?",
                        "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                DatabaseHelper.ExecuteNonQuery("DELETE FROM LoaiPhong WHERE MaLoai = @id",
                    new[] { new SqlParameter("@id", id) });
                MessageBox.Show($"Đã xóa loại phòng [{ten}].", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLoaiPhong();
            }
            catch (PostgresException pex) when (pex.SqlState == PostgresErrorCodes.ForeignKeyViolation)
            {
                MessageBox.Show($"Loại [{ten}] đang được dữ liệu khác tham chiếu nên không thể xóa.",
                    "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa loại phòng:\n" + ex.Message, "Lỗi CSDL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
