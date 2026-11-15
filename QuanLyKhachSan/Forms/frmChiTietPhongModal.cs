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
    public partial class frmChiTietPhongModal : Form
    {
        private int _maPhong;
        private DataRow _phongData;
        private static readonly HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };

        public frmChiTietPhongModal(int maPhong)
        {
            _maPhong = maPhong;
            InitializeComponent();
            InitializeComponentCustom();
            UITheme.ApplyTheme(this);
            TaiThongTinPhong();
        }

        private void InitializeComponentCustom()
        {
            AutoScaleMode = AutoScaleMode.None;
            Text = "Thông tin chi tiết buồng phòng";
            ClientSize = new Size(920, 575);
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
                Height = 74,
                BackColor = Color.White,
                Padding = new Padding(24, 14, 24, 8)
            };
            pnlHeader.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var topBrush = new LinearGradientBrush(
                    new Rectangle(0, 0, Math.Max(1, pnlHeader.Width), 4),
                    UITheme.PrimaryOrange, UITheme.DarkNavy, LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(topBrush, 0, 0, pnlHeader.Width, 3);
                }

                using (var pillPath = UITheme.GetRoundedRectPath(new Rectangle(24, 16, 4, 42), 2))
                using (var pillBrush = new SolidBrush(UITheme.PrimaryOrange))
                {
                    e.Graphics.FillPath(pillBrush, pillPath);
                }
            };

            lblTitle = new Label
            {
                Text = "🏨 CHI TIẾT BUỒNG PHÒNG & HÌNH ẢNH TIỆN NGHI",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(38, 14),
                AutoSize = true
            };

            lblSubtitle = new Label
            {
                Text = "Hình ảnh thực tế, tiện nghi buồng phòng, thông số diện tích và bảng giá niêm yết",
                Font = new Font("Segoe UI", 9F),
                ForeColor = UITheme.TextMuted,
                Location = new Point(38, 42),
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
                Height = 64,
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
                Text = Session.LaAdmin ? "✏️ Chỉnh sửa phòng" : "🔄 Đổi trạng thái",
                Size = new Size(168, 40),
                Location = new Point(ClientSize.Width - 298, 12),
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
                using var modal = new frmPhongModal(_maPhong, !Session.LaAdmin);
                if (modal.ShowDialog(this) == DialogResult.OK)
                {
                    TaiThongTinPhong();
                    DialogResult = DialogResult.OK;
                }
            };

            btnDong = new Button
            {
                Text = "✕ Đóng",
                Size = new Size(98, 40),
                Location = new Point(ClientSize.Width - 120, 12),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = UITheme.TextMuted,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnDong.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnDong.Click += (_, __) => Close();

            pnlFooter.Controls.Add(btnSua);
            pnlFooter.Controls.Add(btnDong);
            Controls.Add(pnlFooter);

            // 3. Body: Chia 2 cột cân đối (Trái: Khung ảnh Cover + Thẻ Quy cách giường/Ghi chú; Phải: Thông số 2x2 Card + Tiện nghi)
            Panel pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = false,
                Padding = new Padding(24, 18, 24, 18),
                BackColor = Color.White
            };

            // Cột Trái 1: Khung hình ảnh tỷ lệ chuẩn 16:11 (không còn dải xám thừa trên/dưới)
            Panel pnlImageCard = new Panel
            {
                Location = new Point(24, 18),
                Size = new Size(415, 286),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            pnlImageCard.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, pnlImageCard.Width - 1, pnlImageCard.Height - 1);
                using var path = UITheme.GetRoundedRectPath(rect, 12);
                using var pen = new Pen(UITheme.BorderColor, 1.2F);
                e.Graphics.DrawPath(pen, path);
            };

            picHinhAnh = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Normal,
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

            // Cột Trái 2: Thẻ Quy cách giường & Ghi chú vận hành đặt ngay dưới ảnh (rộng 415px, hiển thị trọn vẹn nhiều dòng)
            Panel pnlNoteCard = new Panel
            {
                Location = new Point(24, 316),
                Size = new Size(415, 104),
                BackColor = Color.FromArgb(248, 250, 252)
            };
            pnlNoteCard.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, pnlNoteCard.Width - 1, pnlNoteCard.Height - 1);
                using var path = UITheme.GetRoundedRectPath(rect, 10);
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1F);
                e.Graphics.DrawPath(pen, path);
            };

            lblMoTaTieuDe = new Label
            {
                Text = "🛏️ QUY CÁCH GIƯỜNG & GHI CHÚ BUỒNG PHÒNG:",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(14, 10),
                AutoSize = true
            };
            pnlNoteCard.Controls.Add(lblMoTaTieuDe);

            lblMoTaGiuong = new Label
            {
                Text = "• Quy cách giường: Đang cập nhật",
                Font = new Font("Segoe UI", 9.25F, FontStyle.Bold),
                ForeColor = UITheme.TextMain,
                Location = new Point(14, 34),
                AutoSize = true
            };
            pnlNoteCard.Controls.Add(lblMoTaGiuong);

            lblGhiChu = new Label
            {
                Text = "• Ghi chú: Không có",
                Font = new Font("Segoe UI", 8.75F, FontStyle.Italic),
                ForeColor = UITheme.TextMuted,
                Location = new Point(14, 58),
                Size = new Size(387, 40),
                AutoEllipsis = true
            };
            pnlNoteCard.Controls.Add(lblGhiChu);
            pnlBody.Controls.Add(pnlNoteCard);

            // Cột Phải: Thông tin định danh, Bảng 4 Thẻ Thông số 2x2 & Tiện nghi
            int infoX = 462;
            int infoW = 434;

            lblTenPhong = new Label
            {
                Text = "PHÒNG P---",
                Font = new Font("Segoe UI", 15.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(infoX, 16),
                AutoSize = true
            };
            pnlBody.Controls.Add(lblTenPhong);

            lblGiaPhong = new Label
            {
                Text = "0 đ / đêm",
                Font = new Font("Segoe UI", 13.5F, FontStyle.Bold),
                ForeColor = UITheme.PrimaryOrange,
                Location = new Point(infoX, 48),
                AutoSize = true
            };
            pnlBody.Controls.Add(lblGiaPhong);

            // Khung 4 thẻ thông số 2x2 (Loại bỏ hoàn toàn lỗi đè chữ ở mọi mức DPI)
            pnlBadges = new Panel
            {
                Location = new Point(infoX, 86),
                Size = new Size(infoW, 132),
                BackColor = Color.Transparent
            };
            pnlBody.Controls.Add(pnlBadges);

            // Mục Tiện nghi phòng
            lblTienNghiTieuDe = new Label
            {
                Text = "🛎️ TIỆN NGHI BUỒNG PHÒNG TIÊU CHUẨN:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(infoX, 232),
                AutoSize = true
            };
            pnlBody.Controls.Add(lblTienNghiTieuDe);

            pnlTienNghiChips = new FlowLayoutPanel
            {
                Location = new Point(infoX, 258),
                Size = new Size(infoW, 162),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = false,
                BackColor = Color.Transparent
            };
            pnlBody.Controls.Add(pnlTienNghiChips);

            Controls.Add(pnlBody);
            pnlBody.BringToFront();
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
                string dienTich = _phongData["DienTich"] == DBNull.Value ? "—" : Convert.ToDecimal(_phongData["DienTich"]).ToString("0.##");
                string moTaGiuong = _phongData["MoTaGiuong"] == DBNull.Value ? "Chưa có thông tin" : _phongData["MoTaGiuong"].ToString();
                string tinhTrang = _phongData["TinhTrang"].ToString();
                string hinhAnh = _phongData["HinhAnh"] == DBNull.Value ? null : _phongData["HinhAnh"].ToString();
                string tienNghi = _phongData["TienNghi"] == DBNull.Value ? null : _phongData["TienNghi"].ToString();
                string ghiChu = _phongData["GhiChu"] == DBNull.Value ? null : _phongData["GhiChu"].ToString();

                lblTenPhong.Text = $"PHÒNG {soPhong} • {tenLoai.ToUpper()}";
                lblGiaPhong.Text = $"{gia:N0} đ / đêm";

                // Đổ 4 thẻ thông số 2x2 chuẩn mực, không bao giờ đè chữ
                pnlBadges.Controls.Clear();
                var gridSpecs = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 2,
                    Margin = Padding.Empty,
                    Padding = Padding.Empty,
                    BackColor = Color.Transparent
                };
                gridSpecs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                gridSpecs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                gridSpecs.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                gridSpecs.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

                bool hoatDong = string.Equals(tinhTrang, "Hoạt động", StringComparison.OrdinalIgnoreCase);
                Color statusColor = hoatDong ? Color.FromArgb(22, 163, 74) : Color.FromArgb(220, 38, 38);
                string statusIcon = hoatDong ? "🟢 TRẠNG THÁI HIỆN TẠI" : "🔴 TRẠNG THÁI HIỆN TẠI";

                gridSpecs.Controls.Add(TaoTheThongSo("🏢 TẦNG VỊ TRÍ", $"Tầng {tang} ({tang}F)", UITheme.DarkNavy, new Padding(0, 0, 6, 6)), 0, 0);
                gridSpecs.Controls.Add(TaoTheThongSo("📐 DIỆN TÍCH PHÒNG", $"{dienTich} m²", UITheme.DarkNavy, new Padding(6, 0, 0, 6)), 1, 0);
                gridSpecs.Controls.Add(TaoTheThongSo("👥 SỨC CHỨA TỐI ĐA", $"Tối đa {soNguoi} khách", UITheme.DarkNavy, new Padding(0, 6, 6, 0)), 0, 1);
                gridSpecs.Controls.Add(TaoTheThongSo(statusIcon, tinhTrang, statusColor, new Padding(6, 6, 0, 0)), 1, 1);
                pnlBadges.Controls.Add(gridSpecs);

                lblMoTaGiuong.Text = $"• Quy cách giường: {moTaGiuong}";
                lblGhiChu.Text = string.IsNullOrWhiteSpace(ghiChu)
                    ? "• Ghi chú: Phòng luôn được khử khuẩn sạch sẽ trước khi đón khách."
                    : $"• Ghi chú: {ghiChu}";

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

        private static Panel TaoTheThongSo(string caption, string value, Color valueColor, Padding margin)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = margin,
                BackColor = Color.FromArgb(248, 250, 252)
            };
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                using var path = UITheme.GetRoundedRectPath(rect, 8);
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1F);
                e.Graphics.DrawPath(pen, path);
            };

            var lblCaption = new Label
            {
                Text = caption,
                Font = new Font("Segoe UI", 7.8F, FontStyle.Bold),
                ForeColor = UITheme.TextMuted,
                Location = new Point(12, 8),
                AutoSize = true
            };

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = valueColor,
                Location = new Point(12, 28),
                AutoSize = true
            };

            card.Controls.Add(lblCaption);
            card.Controls.Add(lblValue);
            return card;
        }

        private static Label TaoChipTiTienNghi(string text)
        {
            var lbl = new Label
            {
                Text = "✓ " + text,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(21, 128, 61),
                BackColor = Color.FromArgb(220, 252, 231),
                AutoSize = true,
                Padding = new Padding(10, 6, 10, 6),
                Margin = new Padding(0, 0, 8, 8)
            };
            lbl.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, lbl.Width - 1, lbl.Height - 1);
                using var path = UITheme.GetRoundedRectPath(rect, 6);
                using var pen = new Pen(Color.FromArgb(187, 247, 208), 1F);
                e.Graphics.DrawPath(pen, path);
            };
            return lbl;
        }

        private static Bitmap TaoAnhCoverBoGoc(Image src, int targetW, int targetH, int radius)
        {
            var bmp = new Bitmap(targetW, targetH);
            using var g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.Clear(Color.White);

            var rect = new Rectangle(0, 0, targetW - 1, targetH - 1);
            using var clipPath = UITheme.GetRoundedRectPath(rect, radius);
            g.SetClip(clipPath);

            double scaleX = (double)targetW / src.Width;
            double scaleY = (double)targetH / src.Height;
            double scale = Math.Max(scaleX, scaleY); // Cover (Aspect-Fill)

            int drawW = (int)Math.Ceiling(src.Width * scale);
            int drawH = (int)Math.Ceiling(src.Height * scale);
            int drawX = (targetW - drawW) / 2;
            int drawY = (targetH - drawH) / 2;

            g.DrawImage(src, new Rectangle(drawX, drawY, drawW, drawH));
            g.ResetClip();

            using var borderPen = new Pen(UITheme.BorderColor, 1.2F);
            g.DrawPath(borderPen, clipPath);
            return bmp;
        }

        private async void LoadHinhAnhPhong(string urlOrPath, string tenLoai)
        {
            lblLoadingHinhAnh.Visible = true;
            picHinhAnh.Image?.Dispose();
            picHinhAnh.Image = null;

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
                Image rawImg = null;
                if (source.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || source.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                {
                    byte[] bytes = await httpClient.GetByteArrayAsync(source);
                    using var ms = new MemoryStream(bytes);
                    rawImg = Image.FromStream(ms);
                }
                else if (File.Exists(source))
                {
                    rawImg = Image.FromFile(source);
                }
                else
                {
                    byte[] bytes = await httpClient.GetByteArrayAsync(fallbackUrl);
                    using var ms = new MemoryStream(bytes);
                    rawImg = Image.FromStream(ms);
                }

                if (rawImg != null)
                {
                    using (rawImg)
                    {
                        int w = Math.Max(100, picHinhAnh.Width > 0 ? picHinhAnh.Width : 415);
                        int h = Math.Max(100, picHinhAnh.Height > 0 ? picHinhAnh.Height : 286);
                        picHinhAnh.Image = TaoAnhCoverBoGoc(rawImg, w, h, 12);
                    }
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
