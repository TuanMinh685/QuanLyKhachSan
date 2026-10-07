using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Quản_Lý_Khách_Sạn
{
    public static class UITheme
    {
        public static readonly Color PrimaryOrange = ColorTranslator.FromHtml("#EA580C");
        public static readonly Color DarkNavy = ColorTranslator.FromHtml("#0F2537");
        public static readonly Color BackgroundGray = ColorTranslator.FromHtml("#F8FAFC");
        public static readonly Color BorderColor = ColorTranslator.FromHtml("#E2E8F0");
        public static readonly Color TextMain = ColorTranslator.FromHtml("#1E293B");
        public static readonly Color TextMuted = ColorTranslator.FromHtml("#64748B");
        public static readonly Color DangerRed = ColorTranslator.FromHtml("#EF4444");

        public static void StyleModernGrid(DataGridView dgv)
        {
            if (dgv == null) return;

            dgv.EnableHeadersVisualStyles = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = ColorTranslator.FromHtml("#F1F5F9");
            dgv.BackgroundColor = Color.White;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F8FAFC");
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#475569");
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.25f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            dgv.ColumnHeadersHeight = 42;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgv.RowTemplate.Height = 40;
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = TextMain;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.25f);
            dgv.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#EFF6FF");
            dgv.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#1D4ED8");
            dgv.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
        }

        public static void DrawCard(object sender, PaintEventArgs e)
        {
            if (sender is Panel pnl)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 10;
                Rectangle rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                using (GraphicsPath path = GetRoundedRectPath(rect, radius))
                using (Pen pen = new Pen(BorderColor, 1.2f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        public static void DrawInputBox(object sender, PaintEventArgs e)
        {
            if (sender is Panel pnl)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 6;
                Rectangle rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                using (GraphicsPath path = GetRoundedRectPath(rect, radius))
                using (Pen pen = new Pen(ColorTranslator.FromHtml("#CBD5E1"), 1f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        public static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
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