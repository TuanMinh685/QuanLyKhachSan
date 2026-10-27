using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public class frmChiTietPhongModal : Form
    {
        private int _maPhong;
        private DataRow _phongData;

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlDivider;

        private PictureBox picHinhAnh;
        private Label lblLoadingHinhAnh;

        private Label lblTenPhong;
        private Label lblGiaPhong;
        private Panel pnlBadges;

        private Label lblTienNghiTieuDe;
        private FlowLayoutPanel pnlTienNghiChips;

        private Label lblMoTaTieuDe;
        private Label lblMoTaGiuong;
        private Label lblGhiChu;

        private Panel pnlFooter;
        private Button btnSua;
        private Button btnDong;

        private static readonly HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };

        public frmChiTietPhongModal(int maPhong)
        {
            _maPhong = maPhong;
            InitializeComponentCustom();
            UITheme.ApplyTheme(this);
            TaiThongTinPhong();
        }

        private void InitializeComponentCustom()
        {
            Text = "Thông tin chi tiết buồng phòng";
            Size = new Size(860, 640);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;
            Font = UITheme.MainFont;

            // 1. Header
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                BackColor = Color.White,
                Padding = new Padding(24, 14, 24, 8)
            };

            lblTitle = new Label
            {
                Text = "🏨 CHI TIẾT BUỒNG PHÒNG & HÌNH ẢNH TIỆN NGHI",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(24, 14),
                AutoSize = true
            };

            lblSubtitle = new Label
            {
                Text = "Hình ảnh thực tế, tiện nghi buồng phòng, thông số diện tích và bảng giá niêm yết",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = UITheme.TextMuted,
                Location = new Point(24, 40),
                AutoSize = true
            };

            pnlDivider = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = UITheme.BorderColor
            };

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(pnlDivider);
            Controls.Add(pnlHeader);

            // 2. Footer
            pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 65,
                BackColor = Color.FromArgb(248, 250, 252),
                Padding = new Padding(24, 12, 24, 12)
            };
            pnlFooter.Paint += (s, e) =>
            {
                using var p = new Pen(UITheme.BorderColor, 1F);
                e.Graphics.DrawLine(p, 0, 0, pnlFooter.Width, 0);
            };

            btnSua = new Button
            {
                Text = "✏️ Chỉnh sửa phòng",
                Size = new Size(160, 40),
                Location = new Point(pnlFooter.Width - 280, 12),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.PrimaryBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.Click += (_, __) =>
            {
                using var modal = new frmPhongModal(_maPhong);
                if (modal.ShowDialog(this) == DialogResult.OK)
                {
                    TaiThongTinPhong();
                    DialogResult = DialogResult.OK; // Báo để form cha làm mới lưới
                }
            };

            btnDong = new Button
            {
                Text = "✕ Đóng",
                Size = new Size(95, 40),
                Location = new Point(pnlFooter.Width - 110, 12),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = UITheme.TextMuted,
                Font = new Font("Segoe UI", 9.5F),
                Cursor = Cursors.Hand
            };
            btnDong.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnDong.Click += (_, __) => Close();

            pnlFooter.Controls.Add(btnSua);
            pnlFooter.Controls.Add(btnDong);
            Controls.Add(pnlFooter);

            // 3. Body: Chia 2 cột (Trái: Hình ảnh 420px, Phải: Thông tin chi tiết)
            Panel pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(24, 18, 24, 18),
                BackColor = Color.White
            };

            // Khung hình ảnh bên trái
            Panel pnlImageCard = new Panel
            {
                Location = new Point(24, 18),
                Size = new Size(400, 380),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            pnlImageCard.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, pnlImageCard.Width - 1, pnlImageCard.Height - 1);
                using var path = UITheme.GetRoundedRectPath(rect, 10);
                using var pen = new Pen(UITheme.BorderColor, 1.2F);
                e.Graphics.DrawPath(pen, path);
            };

            picHinhAnh = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            lblLoadingHinhAnh = new Label
            {
                Text = "🖼️ Đang tải hình ảnh phòng...",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = UITheme.TextMuted
            };

            pnlImageCard.Controls.Add(picHinhAnh);
            pnlImageCard.Controls.Add(lblLoadingHinhAnh);
            pnlBody.Controls.Add(pnlImageCard);

            // Cột thông tin chi tiết bên phải
            int infoX = 444;
            int infoW = 370;

            lblTenPhong = new Label
            {
                Text = "PHÒNG P---",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(infoX, 18),
                AutoSize = true
            };
            pnlBody.Controls.Add(lblTenPhong);

            lblGiaPhong = new Label
            {
                Text = "0 đ / đêm",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = UITheme.PrimaryOrange,
                Location = new Point(infoX, 52),
                AutoSize = true
            };
            pnlBody.Controls.Add(lblGiaPhong);

            // Badges thông số
            pnlBadges = new Panel
            {
                Location = new Point(infoX, 90),
                Size = new Size(infoW, 110),
                BackColor = Color.Transparent
            };
            pnlBody.Controls.Add(pnlBadges);

            // Mục Tiện nghi phòng
            lblTienNghiTieuDe = new Label
            {
                Text = "🛎️ TIỆN NGHI BUỒNG PHÒNG:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(infoX, 210),
                AutoSize = true
            };
            pnlBody.Controls.Add(lblTienNghiTieuDe);

            pnlTienNghiChips = new FlowLayoutPanel
            {
                Location = new Point(infoX, 235),
                Size = new Size(infoW, 100),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                BackColor = Color.Transparent
            };
            pnlBody.Controls.Add(pnlTienNghiChips);

            // Mục Mô tả giường & Ghi chú
            lblMoTaTieuDe = new Label
            {
                Text = "🛏️ QUY CÁCH GIƯỜNG & DIỆN TÍCH:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(infoX, 345),
                AutoSize = true
            };
            pnlBody.Controls.Add(lblMoTaTieuDe);

            lblMoTaGiuong = new Label
            {
                Text = "• Giường: Đang cập nhật",
                Font = new Font("Segoe UI", 9F),
                ForeColor = UITheme.TextMain,
                Location = new Point(infoX, 370),
                AutoSize = true
            };
            pnlBody.Controls.Add(lblMoTaGiuong);

            lblGhiChu = new Label
            {
                Text = "• Ghi chú: Không có",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                ForeColor = UITheme.TextMuted,
                Location = new Point(infoX, 395),
                Size = new Size(infoW, 35)
            };
            pnlBody.Controls.Add(lblGhiChu);

            Controls.Add(pnlBody);
        }

        private void TaiThongTinPhong()
        {
            try
            {
                string sql = @"
                    SELECT 
                        p.MaPhong,
                        p.SoPhong,
                        p.Tang,
                        p.MoTaGiuong,
                        p.DienTich,
                        p.TinhTrang,
                        p.HinhAnh,
                        p.TienNghi,
                        p.GhiChu,
                        l.TenLoai,
                        l.GiaMotDem,
                        l.SoNguoiToiDa
                    FROM Phong p
                    JOIN LoaiPhong l ON p.MaLoai = l.MaLoai
                    WHERE p.MaPhong = @id";

                DataTable dt = DatabaseHelper.ExecuteQuery(sql, new[] { new SqlParameter("@id", _maPhong) });
                if (dt.Rows.Count == 0) return;

                _phongData = dt.Rows[0];

                string soPhong = _phongData["SoPhong"].ToString();
                string tenLoai = _phongData["TenLoai"].ToString();
                decimal gia = Convert.ToDecimal(_phongData["GiaMotDem"]);
                int tang = Convert.ToInt32(_phongData["Tang"]);
                int soNguoi = Convert.ToInt32(_phongData["SoNguoiToiDa"]);
                string dienTich = _phongData["DienTich"] == DBNull.Value ? "—" : _phongData["DienTich"].ToString();
                string moTaGiuong = _phongData["MoTaGiuong"] == DBNull.Value ? "Chưa có thông tin" : _phongData["MoTaGiuong"].ToString();
                string tinhTrang = _phongData["TinhTrang"].ToString();
                string hinhAnh = _phongData["HinhAnh"] == DBNull.Value ? null : _phongData["HinhAnh"].ToString();
                string tienNghi = _phongData["TienNghi"] == DBNull.Value ? null : _phongData["TienNghi"].ToString();
                string ghiChu = _phongData["GhiChu"] == DBNull.Value ? null : _phongData["GhiChu"].ToString();

                lblTenPhong.Text = $"PHÒNG {soPhong} • {tenLoai.ToUpper()}";
                lblGiaPhong.Text = $"{gia:N0} đ / đêm";

                // Đổ badges thông số
                pnlBadges.Controls.Clear();
                int by = 0;
                TaoThongSoRow(pnlBadges, "🏢 Tầng:", $"{tang}F", "📐 Diện tích:", $"{dienTich} m²", 0, by);
                by += 32;
                TaoThongSoRow(pnlBadges, "👥 Sức chứa:", $"Tối đa {soNguoi} khách", "🟢 Trạng thái:", tinhTrang, 0, by);

                lblMoTaGiuong.Text = $"• Quy cách giường: {moTaGiuong}";
                lblGhiChu.Text = string.IsNullOrWhiteSpace(ghiChu) ? "• Ghi chú: Phòng luôn được khử khuẩn sạch sẽ trước khi đón khách." : $"• Ghi chú: {ghiChu}";

                // Tiện nghi chips
                pnlTienNghiChips.Controls.Clear();
                string defaultTienNghi = "Wifi 6 tốc độ cao, Điều hòa 2 chiều, Smart TV 4K, Bồn tắm nằm, Ban công thoáng mát, Minibar, Bình đun siêu tốc, Két sắt an toàn";
                string finalTienNghi = string.IsNullOrWhiteSpace(tienNghi) ? defaultTienNghi : tienNghi;

                string[] chips = finalTienNghi.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string chip in chips)
                {
                    pnlTienNghiChips.Controls.Add(TaoChipTiTienNghi(chip.Trim()));
                }

                // Tải hình ảnh
                LoadHinhAnhPhong(hinhAnh, tenLoai);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void TaoThongSoRow(Control parent, string t1, string v1, string t2, string v2, int x, int y)
        {
            var pnl = new Panel { Location = new Point(x, y), Size = new Size(parent.Width, 28) };
            var lbl1 = new Label { Text = t1, Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), ForeColor = UITheme.TextMuted, Location = new Point(0, 4), AutoSize = true };
            var val1 = new Label { Text = v1, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = UITheme.DarkNavy, Location = new Point(70, 4), AutoSize = true };
            var lbl2 = new Label { Text = t2, Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), ForeColor = UITheme.TextMuted, Location = new Point(190, 4), AutoSize = true };
            var val2 = new Label { Text = v2, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = UITheme.DarkNavy, Location = new Point(275, 4), AutoSize = true };

            pnl.Controls.AddRange(new Control[] { lbl1, val1, lbl2, val2 });
            parent.Controls.Add(pnl);
        }

        private static Label TaoChipTiTienNghi(string text)
        {
            var lbl = new Label
            {
                Text = "✓ " + text,
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                ForeColor = Color.FromArgb(15, 81, 50),
                BackColor = Color.FromArgb(209, 231, 221),
                AutoSize = true,
                Padding = new Padding(8, 4, 8, 4),
                Margin = new Padding(0, 0, 6, 6)
            };
            return lbl;
        }

        private async void LoadHinhAnhPhong(string urlOrPath, string tenLoai)
        {
            lblLoadingHinhAnh.Visible = true;
            picHinhAnh.Image = null;

            // Ảnh mặc định theo loại phòng sang trọng
            string fallbackUrl = "https://images.unsplash.com/photo-1611892440504-42a792e24d32?auto=format&fit=crop&w=800&q=80";
            if (tenLoai.IndexOf("deluxe", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                fallbackUrl = "https://images.unsplash.com/photo-1590490360182-c33d57733427?auto=format&fit=crop&w=800&q=80";
            }
            else if (tenLoai.IndexOf("suite", StringComparison.OrdinalIgnoreCase) >= 0 || tenLoai.IndexOf("vip", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                fallbackUrl = "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?auto=format&fit=crop&w=800&q=80";
            }

            string source = string.IsNullOrWhiteSpace(urlOrPath) ? fallbackUrl : urlOrPath.Trim();

            try
            {
                if (source.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || source.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                {
                    byte[] bytes = await httpClient.GetByteArrayAsync(source);
                    using var ms = new MemoryStream(bytes);
                    picHinhAnh.Image = Image.FromStream(ms);
                }
                else if (File.Exists(source))
                {
                    picHinhAnh.Image = Image.FromFile(source);
                }
                else
                {
                    byte[] bytes = await httpClient.GetByteArrayAsync(fallbackUrl);
                    using var ms = new MemoryStream(bytes);
                    picHinhAnh.Image = Image.FromStream(ms);
                }
                lblLoadingHinhAnh.Visible = false;
            }
            catch
            {
                lblLoadingHinhAnh.Text = "🏨 Khách sạn An Nhiên\n(Ảnh phòng đang được cập nhật)";
                lblLoadingHinhAnh.Visible = true;
            }
        }
    }
}
