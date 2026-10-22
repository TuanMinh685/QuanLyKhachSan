using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Quản_Lý_Khách_Sạn
{
    public static class UITheme
    {
        // --- 1. BẢNG MÀU CHUYÊN NGHIỆP ĐỒNG BỘ TOÀN HỆ THỐNG ---
        public static readonly Color PrimaryOrange = ColorTranslator.FromHtml("#EA580C");
        public static readonly Color DarkNavy = ColorTranslator.FromHtml("#0F2537");
        public static readonly Color PrimaryBlue = ColorTranslator.FromHtml("#2563EB");
        public static readonly Color SuccessGreen = ColorTranslator.FromHtml("#16A34A");
        public static readonly Color DangerRed = ColorTranslator.FromHtml("#EF4444");

        public static readonly Color BackgroundGray = ColorTranslator.FromHtml("#F1F5F9");
        public static readonly Color BackgroundLight = ColorTranslator.FromHtml("#F8FAFC");
        public static readonly Color BorderColor = ColorTranslator.FromHtml("#E2E8F0");
        public static readonly Color TextMain = ColorTranslator.FromHtml("#1E293B");
        public static readonly Color TextDark = ColorTranslator.FromHtml("#0F172A");
        public static readonly Color TextMuted = ColorTranslator.FromHtml("#64748B");

        public static readonly Font MainFont = new Font("Segoe UI", 9.5F, FontStyle.Regular);
        public static readonly Font BoldFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);

        // --- 2. ÁP DỤNG THEME CHO FORM (GIỮ NGUYÊN BỐ CỤC FIGMA CARD & INPUT) ---
        public static void ApplyTheme(Form form)
        {
            if (form == null) return;
            form.BackColor = BackgroundGray;
            ApplyThemeToControls(form.Controls);
        }

        private static void ApplyThemeToControls(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl is DataGridView dgv)
                {
                    FormatDataGridView(dgv);
                }
                else if (ctrl is Button btn)
                {
                    btn.UseMnemonic = false;
                    btn.Cursor = Cursors.Hand;
                }

                if (ctrl.HasChildren)
                {
                    ApplyThemeToControls(ctrl.Controls);
                }
            }
        }

        public static void FormatButton(Button btn)
        {
            if (btn == null) return;
            btn.UseMnemonic = false;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Cursor = Cursors.Hand;
        }

        // --- 3. ĐỊNH DẠNG BẢNG DỮ LIỆU (DATAGRIDVIEW) ĐỀU, RỘNG 100% KHUNG ---
        public static void StyleModernGrid(DataGridView dgv) => FormatDataGridView(dgv);

        public static void FormatDataGridView(DataGridView dgv)
        {
            if (dgv == null) return;

            dgv.EnableHeadersVisualStyles = false;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = ColorTranslator.FromHtml("#E2E8F0");

            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Header đồng bộ màu DarkNavy, không bị đổi sang màu xanh dương khi chọn dòng
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = DarkNavy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = DarkNavy;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.25F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 0, 12, 0);
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgv.ColumnHeadersHeight = 44;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Dòng dữ liệu thoáng, rõ chữ
            dgv.RowTemplate.Height = 42;
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = TextMain;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#FFF7ED");
            dgv.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#9A3412");
            dgv.DefaultCellStyle.Padding = new Padding(12, 0, 12, 0);

            dgv.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F8FAFC");
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = TextMain;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#FFF7ED");
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#9A3412");

            // Ngăn chặn triệt để hộp thoại lỗi mặc định DataGridView Default Error Dialog
            dgv.DataError -= Dgv_DataError;
            dgv.DataError += Dgv_DataError;
        }

        private static void Dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        // --- 4. VẼ KHUNG CARD & Ô INPUT BO GÓC MƯỢT ---
        public static void DrawCard(object sender, PaintEventArgs e, int radius = 10)
        {
            if (sender is Control ctrl)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, ctrl.Width - 1, ctrl.Height - 1);
                using (GraphicsPath path = GetRoundedRectPath(rect, radius))
                using (Pen pen = new Pen(BorderColor, 1.2f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        public static void DrawInputBox(object sender, PaintEventArgs e, int radius = 6)
        {
            if (sender is Control ctrl)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, ctrl.Width - 1, ctrl.Height - 1);
                using (GraphicsPath path = GetRoundedRectPath(rect, radius))
                using (Pen pen = new Pen(ColorTranslator.FromHtml("#CBD5E1"), 1.1f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        public static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }
            float d = radius * 2F;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
