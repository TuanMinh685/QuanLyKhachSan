using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    /// <summary>Form thêm / sửa một loại phòng (tên, giá một đêm, số khách tối đa).</summary>
    public class frmLoaiPhongModal : Form
    {
        private static readonly CultureInfo Vi = new CultureInfo("vi-VN");

        private readonly int _maLoai;

        private Label lblTitle;
        private Label lblSubtitle;
        private TextBox txtTen;
        private TextBox txtGia;
        private NumericUpDown nudSucChua;
        private Button btnLuu;
        private Button btnHuy;

        public frmLoaiPhongModal(int maLoai = 0)
        {
            _maLoai = maLoai;
            KhoiTaoGiaoDien();
            UITheme.ApplyTheme(this);

            if (_maLoai > 0)
            {
                lblTitle.Text = "✏️ CẬP NHẬT LOẠI PHÒNG";
                btnLuu.Text = "💾 Cập nhật";
                btnLuu.BackColor = UITheme.PrimaryBlue;
                TaiThongTin();
            }
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Thông tin loại phòng";
            Size = new Size(540, 430);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            BackColor = Color.White;
            Font = UITheme.MainFont;

            // ===== Header =====
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                BackColor = Color.White,
                Padding = new Padding(24, 12, 24, 8)
            };
            lblTitle = new Label
            {
                Text = "➕ THÊM LOẠI PHÒNG MỚI",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                AutoSize = true,
                UseMnemonic = false,
                Margin = new Padding(0, 0, 0, 2)
            };
            lblSubtitle = new Label
            {
                Text = "Khai báo tên loại, giá một đêm và số khách tối đa",
                Font = new Font("Segoe UI", 8.5F),
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
            var pnlDivider = new Panel { Dock = DockStyle.Bottom, Height = 1, BackColor = UITheme.BorderColor };
            pnlHeader.Controls.Add(pnlTieuDe);
            pnlHeader.Controls.Add(pnlDivider);

            // ===== Footer: nút tự giãn theo chữ, bám mép phải =====
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 65,
                BackColor = Color.FromArgb(248, 250, 252),
                Padding = new Padding(24, 0, 24, 0)
            };
            pnlFooter.Paint += (s, e) =>
            {
                using var p = new Pen(UITheme.BorderColor, 1F);
                e.Graphics.DrawLine(p, 0, 0, pnlFooter.Width, 0);
            };

            btnLuu = new Button
            {
                Text = "💾 Lưu loại phòng",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(150, 40),
                Padding = new Padding(10, 0, 10, 0),
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.PrimaryOrange,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseMnemonic = false,
                Margin = new Padding(10, 0, 0, 0)
            };
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Click += (_, __) => LuuDuLieu();

            btnHuy = new Button
            {
                Text = "✕ Đóng",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(95, 40),
                Padding = new Padding(10, 0, 10, 0),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = UITheme.TextMuted,
                Font = new Font("Segoe UI", 9.5F),
                Cursor = Cursors.Hand,
                UseMnemonic = false,
                Margin = new Padding(0)
            };
            btnHuy.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnHuy.Click += (_, __) => Close();

            var flowNut = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Padding = new Padding(0, 12, 0, 0),
                BackColor = Color.Transparent
            };
            flowNut.Controls.Add(btnHuy);   // RightToLeft: thêm trước = sát mép phải
            flowNut.Controls.Add(btnLuu);
            pnlFooter.Controls.Add(flowNut);

            // ===== Body =====
            var pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(28, 16, 28, 16),
                BackColor = Color.White
            };

            int y = 14;
            int gap = 58;

            TaoNhan(pnlBody, "Tên loại phòng *", 28, y);
            var boTen = TaoBox(pnlBody, 28, y + 20, 468, 36);
            txtTen = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F),
                Dock = DockStyle.Fill,
                MaxLength = 50,
                PlaceholderText = "Ví dụ: Standard, Deluxe, Suite"
            };
            boTen.Controls.Add(txtTen);

            y += gap;
            TaoNhan(pnlBody, "Giá một đêm (VNĐ) *", 28, y);
            var boGia = TaoBox(pnlBody, 28, y + 20, 270, 36);
            txtGia = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F),
                Dock = DockStyle.Fill,
                TextAlign = HorizontalAlignment.Right,
                PlaceholderText = "Ví dụ: 500.000"
            };
            // Rời ô thì tự định dạng 500000 -> 500.000
            txtGia.Leave += (_, __) =>
            {
                if (TryParseGia(txtGia.Text, out decimal g)) txtGia.Text = g.ToString("N0", Vi);
            };
            boGia.Controls.Add(txtGia);

            TaoNhan(pnlBody, "Số khách tối đa *", 316, y);
            var boSuc = TaoBox(pnlBody, 316, y + 20, 180, 36);
            nudSucChua = new NumericUpDown
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9.5F),
                Dock = DockStyle.Fill,
                Minimum = 1,
                Maximum = 20,
                Value = 2
            };
            boSuc.Controls.Add(nudSucChua);

            y += gap;
            var lblGoiY = new Label
            {
                Text = "💡 Giá mới chỉ áp dụng cho các lượt đặt phòng tạo SAU này. " +
                       "Các đơn đã đặt vẫn giữ nguyên giá lúc đặt.",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                ForeColor = UITheme.TextMuted,
                AutoSize = true,
                MaximumSize = new Size(468, 0),
                Location = new Point(28, y + 4)
            };
            pnlBody.Controls.Add(lblGoiY);

            // Thứ tự Add: header, footer, rồi body. Body (Fill) phải được đưa lên TRÊN CÙNG
            // để WinForms xếp bố cục nó SAU header/footer (xem ghi chú ở các modal khác).
            Controls.Add(pnlHeader);
            Controls.Add(pnlFooter);
            Controls.Add(pnlBody);
            pnlBody.BringToFront();

            AcceptButton = btnLuu;
            CancelButton = btnHuy;
        }

        private static Label TaoNhan(Control parent, string text, int x, int y)
        {
            var lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                Location = new Point(x, y),
                AutoSize = true,
                UseMnemonic = false
            };
            parent.Controls.Add(lbl);
            return lbl;
        }

        private static Panel TaoBox(Control parent, int x, int y, int w, int h)
        {
            var pnl = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(w, h),
                BackColor = Color.White,
                Padding = new Padding(8, 7, 8, 4)
            };
            pnl.Paint += (s, e) => UITheme.DrawInputBox(s, e, 6);
            parent.Controls.Add(pnl);
            return pnl;
        }

        // ================= DỮ LIỆU =================

        private void TaiThongTin()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(
                    "SELECT TenLoai, GiaMotDem, SoNguoiToiDa FROM LoaiPhong WHERE MaLoai = @id",
                    new[] { new SqlParameter("@id", _maLoai) });

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy loại phòng này (có thể đã bị xóa).", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Load += (_, __) => Close();
                    return;
                }

                DataRow r = dt.Rows[0];
                txtTen.Text = r["TenLoai"].ToString();
                txtGia.Text = Convert.ToDecimal(r["GiaMotDem"]).ToString("N0", Vi);
                int suc = Convert.ToInt32(r["SoNguoiToiDa"]);
                nudSucChua.Value = Math.Max(nudSucChua.Minimum, Math.Min(nudSucChua.Maximum, suc));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được thông tin loại phòng:\n" + ex.Message, "Lỗi CSDL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>Đọc giá VNĐ: chấp nhận "500000", "500.000", "500,000", "500.000 đ".</summary>
        private static bool TryParseGia(string text, out decimal gia)
        {
            gia = 0;
            var digits = new StringBuilder();
            foreach (char c in text ?? "")
            {
                if (char.IsDigit(c)) digits.Append(c);
            }
            if (digits.Length == 0 || digits.Length > 12) return false;
            if (!decimal.TryParse(digits.ToString(), NumberStyles.None, CultureInfo.InvariantCulture, out gia))
                return false;
            return gia > 0;
        }

        private void LuuDuLieu()
        {
            if (!Session.LaAdmin)
            {
                MessageBox.Show("Chỉ Admin mới được thêm hoặc sửa loại phòng.", "Không đủ quyền",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ten = txtTen.Text.Trim();
            if (ten.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập tên loại phòng.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            if (!TryParseGia(txtGia.Text, out decimal gia))
            {
                MessageBox.Show("Giá một đêm phải là số dương, ví dụ 500000 hoặc 500.000.", "Giá không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGia.Focus();
                return;
            }

            int sucChua = (int)nudSucChua.Value;

            try
            {
                int trung = Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM LoaiPhong WHERE LOWER(TenLoai) = LOWER(@ten) AND MaLoai <> @id",
                    new[] { new SqlParameter("@ten", ten), new SqlParameter("@id", _maLoai) }));
                if (trung > 0)
                {
                    MessageBox.Show($"Đã có loại phòng tên [{ten}]. Vui lòng chọn tên khác.", "Trùng tên loại phòng",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTen.Focus();
                    return;
                }

                if (_maLoai == 0)
                {
                    DatabaseHelper.ExecuteNonQuery(
                        "INSERT INTO LoaiPhong (TenLoai, GiaMotDem, SoNguoiToiDa) VALUES (@ten, @gia, @suc)",
                        new[] { new SqlParameter("@ten", ten), new SqlParameter("@gia", gia), new SqlParameter("@suc", sucChua) });
                }
                else
                {
                    DatabaseHelper.ExecuteNonQuery(
                        "UPDATE LoaiPhong SET TenLoai = @ten, GiaMotDem = @gia, SoNguoiToiDa = @suc WHERE MaLoai = @id",
                        new[]
                        {
                            new SqlParameter("@ten", ten), new SqlParameter("@gia", gia),
                            new SqlParameter("@suc", sucChua), new SqlParameter("@id", _maLoai)
                        });
                }

                MessageBox.Show(_maLoai == 0 ? $"Đã thêm loại phòng [{ten}]." : $"Đã cập nhật loại phòng [{ten}].",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu loại phòng:\n" + ex.Message, "Lỗi CSDL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
