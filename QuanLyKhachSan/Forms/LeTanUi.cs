using System.Drawing.Drawing2D;

namespace Quản_Lý_Khách_Sạn.Forms
{
    // Thành phần trình bày riêng của màn hình khách hàng và đặt phòng.
    // Lấy bảng màu từ UITheme để đồng bộ với các module của nhóm.
    internal static class LeTanUi
    {
        internal static readonly Color OrangeLight = ColorTranslator.FromHtml("#FFF7ED");

        internal static Label Caption(string text, bool bold = false) => new Label
        {
            Text = text, AutoSize = false, Dock = DockStyle.Fill,
            Font = bold ? UITheme.BoldFont : UITheme.MainFont,
            ForeColor = bold ? UITheme.DarkNavy : UITheme.TextMuted,
            TextAlign = ContentAlignment.MiddleLeft, AutoEllipsis = true,
            Margin = Padding.Empty, UseMnemonic = false
        };

        internal static TableLayoutPanel Table(int columns = 1)
        {
            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill, ColumnCount = columns, RowCount = 0,
                Margin = Padding.Empty, Padding = Padding.Empty, BackColor = Color.Transparent
            };
            for (int i = 0; i < columns; i++)
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / columns));
            return table;
        }

        internal static void Row(TableLayoutPanel table, Control child, float height = 0)
        {
            int row = table.RowCount++;
            table.RowStyles.Add(height > 0 ? new RowStyle(SizeType.Absolute, height) : new RowStyle(SizeType.Percent, 100));
            child.Dock = DockStyle.Fill;
            child.Margin = Padding.Empty;
            table.Controls.Add(child, 0, row);
            if (table.ColumnCount > 1) table.SetColumnSpan(child, table.ColumnCount);
        }

        internal static Panel Header(Label title, Label subtitle, Control trailing = null)
        {
            var header = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(24, 14, 24, 12) };
            var layout = Table(2);
            layout.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
            layout.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, trailing == null ? 0 : 225);
            layout.RowCount = 2;
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            title.AutoSize = false; title.Dock = DockStyle.Fill; title.Margin = Padding.Empty;
            title.Font = new Font("Segoe UI", 13F, FontStyle.Bold); title.ForeColor = UITheme.DarkNavy;
            title.TextAlign = ContentAlignment.MiddleLeft; title.AutoEllipsis = true;
            subtitle.AutoSize = false; subtitle.Dock = DockStyle.Fill; subtitle.Margin = Padding.Empty;
            subtitle.Font = new Font("Segoe UI", 9F); subtitle.ForeColor = UITheme.TextMuted;
            subtitle.AutoEllipsis = true; subtitle.TextAlign = ContentAlignment.MiddleLeft;
            layout.Controls.Add(title, 0, 0); layout.Controls.Add(subtitle, 0, 1);
            if (trailing != null)
            {
                trailing.Dock = DockStyle.Fill; trailing.Margin = new Padding(16, 6, 0, 6);
                layout.Controls.Add(trailing, 1, 0); layout.SetRowSpan(trailing, 2);
            }
            header.Controls.Add(layout);
            header.Paint += (_, e) =>
            {
                using var pen = new Pen(UITheme.BorderColor);
                e.Graphics.DrawLine(pen, 0, header.Height - 1, header.Width, header.Height - 1);
            };
            return header;
        }

        internal static void Badge(Label label, Color? background = null, Color? foreground = null)
        {
            label.AutoSize = false; label.BorderStyle = BorderStyle.None;
            label.BackColor = background ?? UITheme.BackgroundLight;
            label.ForeColor = foreground ?? UITheme.TextMuted;
            label.Font = UITheme.BoldFont; label.TextAlign = ContentAlignment.MiddleCenter;
            label.AutoEllipsis = true; label.Padding = new Padding(8, 2, 8, 2);
        }

        internal static void Button(Button button, Color? accent = null, bool filled = false)
        {
            button.AutoSize = false; button.FlatStyle = FlatStyle.Flat; button.UseVisualStyleBackColor = false;
            button.Cursor = Cursors.Hand; button.UseMnemonic = false; button.Font = UITheme.BoldFont;
            button.ForeColor = filled ? Color.White : accent ?? UITheme.DarkNavy;
            button.BackColor = filled ? accent ?? UITheme.PrimaryOrange : Color.White;
            button.FlatAppearance.BorderSize = filled ? 0 : 1;
            button.FlatAppearance.BorderColor = UITheme.BorderColor;
            button.FlatAppearance.MouseOverBackColor = filled
                ? ControlPaint.Dark(button.BackColor, .08F) : UITheme.BackgroundLight;
            button.FlatAppearance.MouseDownBackColor = filled
                ? ControlPaint.Dark(button.BackColor, .15F) : OrangeLight;
            button.Padding = new Padding(8, 0, 8, 0);
            button.Height = 40;
        }

        internal static Control Input(Control input, int height = 40)
        {
            input.Font = UITheme.MainFont;
            input.ForeColor = UITheme.TextMain;
            input.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            input.Margin = Padding.Empty;
            var frame = new LeTanCard { Padding = new Padding(10, 8, 10, 6), Height = height, Margin = Padding.Empty };
            if (input is TextBox text) text.BorderStyle = BorderStyle.None;
            if (input is ComboBox combo) combo.FlatStyle = FlatStyle.Flat;
            input.Dock = DockStyle.Fill;
            frame.Controls.Add(input);
            input.Enter += (_, __) => { frame.BorderColor = UITheme.PrimaryOrange; frame.Invalidate(); };
            input.Leave += (_, __) => { frame.BorderColor = UITheme.BorderColor; frame.Invalidate(); };
            return frame;
        }

        internal static Control Field(Label label, Control input, int height = 72)
        {
            label.AutoSize = false; label.Font = UITheme.BoldFont; label.ForeColor = UITheme.TextMain;
            label.TextAlign = ContentAlignment.MiddleLeft; label.AutoEllipsis = true;
            var field = Table(); field.Height = height; field.Margin = new Padding(0, 0, 12, 8);
            Row(field, label, 26); Row(field, Input(input));
            return field;
        }

        internal static Control Field(string label, Control input, int height = 72) => Field(Caption(label), input, height);

        internal static FlowLayoutPanel Actions(params Button[] buttons)
        {
            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false, Padding = new Padding(0, 7, 0, 7), Margin = Padding.Empty
            };
            foreach (var button in buttons)
            {
                button.Dock = DockStyle.None; button.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                button.Margin = new Padding(8, 0, 0, 0); actions.Controls.Add(button);
            }
            return actions;
        }

        internal static void Grid(DataGridView grid)
        {
            UITheme.FormatDataGridView(grid);
            grid.Dock = DockStyle.Fill; grid.Margin = Padding.Empty;
            grid.DefaultCellStyle.Padding = new Padding(10, 0, 10, 0);
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 10, 0);
            grid.RowTemplate.Height = 46;
        }

        internal static void EmptyGrid(DataGridView grid, string title, string detail)
        {
            grid.Paint += (_, e) =>
            {
                if (grid.Rows.Count != 0 || grid.ClientSize.Height < 120) return;
                int top = grid.ColumnHeadersHeight + Math.Max(20, (grid.Height - grid.ColumnHeadersHeight - 70) / 2);
                var bounds = new Rectangle(16, top, Math.Max(1, grid.Width - 32), 28);
                TextRenderer.DrawText(e.Graphics, title, UITheme.BoldFont, bounds, UITheme.DarkNavy,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
                bounds.Y += 30; bounds.Height = 42;
                TextRenderer.DrawText(e.Graphics, detail, UITheme.MainFont, bounds, UITheme.TextMuted,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.WordBreak);
            };
        }
    }

    internal sealed class LeTanCard : Panel
    {
        internal Color BorderColor = UITheme.BorderColor;
        internal LeTanCard()
        {
            DoubleBuffered = true; ResizeRedraw = true;
            BackColor = Color.White; Margin = Padding.Empty;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Width < 20 || Height < 20) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var path = UITheme.GetRoundedRectPath(new Rectangle(0, 0, Width - 1, Height - 1), 8);
            using var pen = new Pen(BorderColor);
            e.Graphics.DrawPath(pen, path);
        }
    }

    // Hai cột trên cửa sổ rộng; xếp dọc và cuộn khi vùng làm việc hẹp.
    internal sealed class LeTanWorkspace : Panel
    {
        private readonly Control left;
        private readonly Control right;
        private bool arranging;
        internal LeTanWorkspace(Control left, Control right)
        {
            this.left = left; this.right = right;
            Dock = DockStyle.Fill; AutoScroll = true; BackColor = UITheme.BackgroundGray;
            Controls.Add(left); Controls.Add(right);
            left.Dock = right.Dock = DockStyle.None;
            left.Anchor = right.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        }
        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            if (arranging || left == null || right == null) return;
            arranging = true;
            try
            {
                float scale = DeviceDpi / 96F;
                int gap = (int)(16 * scale), pad = (int)(24 * scale);
                int width = Math.Max((int)(480 * scale), ClientSize.Width - pad * 2);
                bool wide = width >= 960 * scale;
                int height = Math.Max((int)(480 * scale), ClientSize.Height - gap * 2);
                int rightWidth = (int)(360 * scale);
                AutoScrollMinSize = new Size(width + pad * 2, wide ? height + gap * 2 : (int)(1190 * scale) + gap * 3);
                Point offset = AutoScrollPosition;
                if (wide)
                {
                    left.SetBounds(pad + offset.X, gap + offset.Y, width - rightWidth - gap, height);
                    right.SetBounds(pad + width - rightWidth + offset.X, gap + offset.Y, rightWidth, height);
                }
                else
                {
                    left.SetBounds(pad + offset.X, gap + offset.Y, width, (int)(560 * scale));
                    right.SetBounds(pad + offset.X, gap * 2 + (int)(560 * scale) + offset.Y, width, (int)(630 * scale));
                }
            }
            finally { arranging = false; }
        }
    }
}
