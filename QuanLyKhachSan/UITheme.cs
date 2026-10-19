using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Quản_Lý_Khách_Sạn
{
    public static class UITheme
    {
        // --- 1. BẢNG MÀU CHUYÊN NGHIỆP (ĐỒNG BỘ VỚI MENU TRÁI CỦA BẠN) ---
        public static Color DarkNavy = Color.FromArgb(24, 30, 54);       // Màu Xanh Đen Menu
        public static Color PrimaryOrange = Color.FromArgb(253, 126, 20); // Màu Cam nổi bật

        public static Color PrimaryBlue = Color.FromArgb(41, 128, 185);  // Xanh dương nhạt (Nút Cập nhật/Mặc định)
        public static Color SuccessGreen = Color.FromArgb(46, 204, 113); // Xanh lá mượt (Nút Thêm/Lưu)
        public static Color DangerRed = Color.FromArgb(231, 76, 60);     // Đỏ mềm mại (Nút Xóa/Hủy)

        public static Color BackgroundLight = Color.FromArgb(245, 246, 250); // Xám cực nhạt làm nền Form
        public static Color TextDark = Color.FromArgb(44, 62, 80);           // Chữ xám đen không gây mỏi mắt

        public static Font MainFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static Font BoldFont = new Font("Segoe UI", 10.5F, FontStyle.Bold);

        // --- 2. HÀM ÁP DỤNG HÀNG LOẠT ---
        public static void ApplyTheme(Form form)
        {
            form.BackColor = BackgroundLight;
            ApplyThemeToControls(form.Controls);
        }

        private static void ApplyThemeToControls(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl is Button btn)
                {
                    FormatButton(btn);
                }
                else if (ctrl is DataGridView dgv)
                {
                    FormatDataGridView(dgv);
                }
                else if (ctrl is TextBox txt)
                {
                    txt.Font = MainFont;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (ctrl is ComboBox cbx)
                {
                    cbx.Font = MainFont;
                    cbx.FlatStyle = FlatStyle.Flat;
                }

                // Không đổi BackColor của Panel nếu nó đang là Menu (màu tối)
                if (ctrl.HasChildren && ctrl.BackColor != DarkNavy)
                {
                    ApplyThemeToControls(ctrl.Controls);
                }
            }
        }

        // --- 3. FORMAT NÚT BẤM (BUTTON) ---
        public static void FormatButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = BoldFont;
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;
            btn.Padding = new Padding(5); // Tăng độ dày cho nút

            string name = btn.Name.ToLower();
            string text = btn.Text.ToLower();

            // Tự động gán màu dựa trên chức năng
            if (name.Contains("them") || text.Contains("thêm") || text.Contains("tạo") || name.Contains("luu") || text.Contains("lưu"))
                btn.BackColor = SuccessGreen;
            else if (name.Contains("xoa") || text.Contains("xóa") || name.Contains("huy") || text.Contains("hủy"))
                btn.BackColor = DangerRed;
            else if (name.Contains("lammoi") || text.Contains("mới"))
                btn.BackColor = Color.FromArgb(108, 117, 125); // Màu xám cho nút làm mới
            else
                btn.BackColor = PrimaryBlue;
        }

        // --- 4. LỘT XÁC BẢNG DỮ LIỆU (DATAGRIDVIEW) ---
        public static void StyleModernGrid(DataGridView dgv) => FormatDataGridView(dgv);

        public static void FormatDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Chỉ kẻ chỉ ngang
            dgv.GridColor = Color.FromArgb(236, 240, 241);

            // Bỏ cột mũi tên ngoài cùng bên trái (cực kỳ quan trọng để dgv đẹp hơn)
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Header (Tiêu đề cột) - Đổi sang màu Xanh Đen giống Menu
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = DarkNavy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = BoldFont;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 45; // Chiều cao header lớn hơn

            // Dòng dữ liệu (Rows)
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = TextDark;
            dgv.DefaultCellStyle.Font = MainFont;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 238, 255); // Xanh biển cực nhạt khi click chọn
            dgv.DefaultCellStyle.SelectionForeColor = DarkNavy;
            dgv.DefaultCellStyle.Padding = new Padding(5, 0, 5, 0); // Cách lề trong ô
            dgv.RowTemplate.Height = 40; // Chiều cao dòng lớn hơn để thoáng chữ

            // Xen kẽ màu nền cho các dòng (Dòng trắng, dòng xám nhạt)
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 252);
        }

        // --- 5. CÁC HÀM ĐỒ HỌA BỔ TRỢ (VẼ BO GÓC NẾU CẦN DÙNG BÊN NGOÀI) ---
        public static GraphicsPath GetRoundedRectPath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0) { path.AddRectangle(bounds); return path; }

            path.AddArc(arc, 180, 90); arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90); arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90); arc.X = bounds.Left;
            path.AddArc(arc, 90, 90); path.CloseFigure();
            return path;
        }

        public static void DrawCard(object sender, PaintEventArgs e, int radius = 8)
        {
            if (sender is Control ctrl)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, ctrl.Width - 1, ctrl.Height - 1);
                using (GraphicsPath path = GetRoundedRectPath(rect, radius))
                {
                    using (SolidBrush brush = new SolidBrush(Color.White)) { e.Graphics.FillPath(brush, path); }
                    using (Pen pen = new Pen(Color.FromArgb(222, 226, 230), 1)) { e.Graphics.DrawPath(pen, path); }
                }
            }
        }

        public static void DrawInputBox(object sender, PaintEventArgs e, int radius = 5)
        {
            if (sender is Control ctrl)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, ctrl.Width - 1, ctrl.Height - 1);
                using (GraphicsPath path = GetRoundedRectPath(rect, radius))
                {
                    using (SolidBrush brush = new SolidBrush(Color.White)) { e.Graphics.FillPath(brush, path); }
                    using (Pen pen = new Pen(Color.FromArgb(206, 212, 218), 1)) { e.Graphics.DrawPath(pen, path); }
                }
            }
        }
    }
}