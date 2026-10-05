using System.Drawing;
using System.Windows.Forms;

namespace Quản_Lý_Khách_Sạn
{
    public static class UITheme
    {
        public static readonly Color HeaderSidebarNavy = ColorTranslator.FromHtml("#0F2537");
        public static readonly Color AccentOrange = ColorTranslator.FromHtml("#E8771A");
        public static readonly Color AccentOrangeHover = ColorTranslator.FromHtml("#CF6610");
        public static readonly Color AccentBlue = ColorTranslator.FromHtml("#1A73E8");
        public static readonly Color BgContent = ColorTranslator.FromHtml("#F4F6F9");
        public static readonly Color BorderColor = ColorTranslator.FromHtml("#E2E8F0");
        public static readonly Color CardBg = Color.White;
        public static readonly Color TextDark = ColorTranslator.FromHtml("#1E293B");
        public static readonly Color TextMuted = ColorTranslator.FromHtml("#64748B");

        public static void StylePrimaryButton(Button btn)
        {
            btn.BackColor = AccentOrange;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Height = 36;
        }

        public static void StyleSecondaryButton(Button btn)
        {
            btn.BackColor = Color.White;
            btn.ForeColor = TextDark;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = BorderColor;
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btn.Cursor = Cursors.Hand;
            btn.Height = 36;
        }

        public static void StyleModernGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.GridColor = BorderColor;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToResizeRows = false;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 40;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F8FAFC");
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextMuted;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.RowTemplate.Height = 40;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgv.DefaultCellStyle.ForeColor = TextDark;
            dgv.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#EFF6FF");
            dgv.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#1D4ED8");
        }
    }
}