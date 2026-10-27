using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmPhongModal : Form
    {
        private int _maPhong;

        public frmPhongModal(int maPhong = 0)
        {
            _maPhong = maPhong;
            InitializeComponent();
            InitializeComponentCustom();
            UITheme.ApplyTheme(this);
            LoadLoaiPhong();

            if (_maPhong > 0)
            {
                lblTitle.Text = "✏️ CHỈNH SỬA THÔNG TIN PHÒNG";
                lblSubtitle.Text = "Cập nhật số phòng, giá thuê, hình ảnh thực tế và tiện nghi";
                btnLuu.Text = "💾 Cập nhật";
                btnLuu.BackColor = UITheme.PrimaryBlue;
                TaiThongTinPhong();
            }
            else
            {
                lblTitle.Text = "➕ THÊM MỚI BUỒNG PHÒNG";
                lblSubtitle.Text = "Khai báo buồng phòng mới vào hệ thống khách sạn";
                btnLuu.Text = "💾 Lưu phòng";
                btnLuu.BackColor = UITheme.PrimaryOrange;
            }
        }

        private void InitializeComponentCustom()
        {
            Text = "Quản lý phòng";
            Size = new Size(620, 640);
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
                Text = "➕ THÊM MỚI BUỒNG PHÒNG",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(24, 14),
                AutoSize = true
            };

            lblSubtitle = new Label
            {
                Text = "Khai báo buồng phòng mới vào hệ thống khách sạn",
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

            btnLuu = new Button
            {
                Size = new Size(150, 40),
                Location = new Point(pnlFooter.Width - 270, 12),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.PrimaryOrange,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Click += (_, __) => LuuDuLieu();

            btnHuy = new Button
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
            btnHuy.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnHuy.Click += (_, __) => Close();

            pnlFooter.Controls.Add(btnLuu);
            pnlFooter.Controls.Add(btnHuy);
            Controls.Add(pnlFooter);

            // 3. Body Content
            Panel pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(28, 16, 28, 16),
                BackColor = Color.White
            };

            int y = 14;
            int gap = 58;

            // Số phòng & Tầng
            TaoNhan(pnlBody, "Số phòng *", 28, y);
            var pnlSo = TaoBox(pnlBody, 28, y + 20, 260, 36);
            txtSoPhong = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "Ví dụ: P101, P202..." };
            pnlSo.Controls.Add(txtSoPhong);

            TaoNhan(pnlBody, "Tầng *", 308, y);
            var pnlTang = TaoBox(pnlBody, 308, y + 20, 260, 36);
            txtTang = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "1, 2, 3..." };
            pnlTang.Controls.Add(txtTang);

            // Loại phòng & Trạng thái
            y += gap;
            TaoNhan(pnlBody, "Loại phòng *", 28, y);
            var pnlLoai = TaoBox(pnlBody, 28, y + 20, 260, 36);
            cboLoai = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F) };
            pnlLoai.Controls.Add(cboLoai);

            TaoNhan(pnlBody, "Trạng thái phòng *", 308, y);
            var pnlTT = TaoBox(pnlBody, 308, y + 20, 260, 36);
            cboTinhTrang = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9.5F) };
            cboTinhTrang.Items.AddRange(new object[] { "Hoạt động", "Bảo trì" });
            cboTinhTrang.SelectedIndex = 0;
            pnlTT.Controls.Add(cboTinhTrang);

            // Mô tả giường & Diện tích
            y += gap;
            TaoNhan(pnlBody, "Quy cách giường", 28, y);
            var pnlGiuong = TaoBox(pnlBody, 28, y + 20, 260, 36);
            txtMoTa = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "1 giường đôi King, 2 giường đơn..." };
            pnlGiuong.Controls.Add(txtMoTa);

            TaoNhan(pnlBody, "Diện tích phòng (m²)", 308, y);
            var pnlDT = TaoBox(pnlBody, 308, y + 20, 260, 36);
            txtDienTich = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "25.5" };
            pnlDT.Controls.Add(txtDienTich);

            // Hình ảnh phòng
            y += gap;
            TaoNhan(pnlBody, "Hình ảnh phòng (Đường dẫn file hoặc URL ảnh)", 28, y);
            var pnlAnh = TaoBox(pnlBody, 28, y + 20, 420, 36);
            txtHinhAnh = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "https://... hoặc C:\\hotel\\room.jpg" };
            pnlAnh.Controls.Add(txtHinhAnh);

            btnChonAnh = new Button
            {
                Text = "📂 Chọn file",
                Location = new Point(458, y + 20),
                Size = new Size(110, 36),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = UITheme.DarkNavy,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnChonAnh.FlatAppearance.BorderColor = UITheme.BorderColor;
            btnChonAnh.Click += (_, __) =>
            {
                using OpenFileDialog ofd = new OpenFileDialog { Filter = "Hình ảnh|*.jpg;*.jpeg;*.png;*.webp;*.bmp" };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtHinhAnh.Text = ofd.FileName;
                }
            };
            pnlBody.Controls.Add(btnChonAnh);

            // Tiện nghi
            y += gap;
            TaoNhan(pnlBody, "Tiện nghi buồng phòng (cách nhau bởi dấu phẩy)", 28, y);
            var pnlTienNghi = TaoBox(pnlBody, 28, y + 20, 540, 36);
            txtTienNghi = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "Wifi 6, Điều hòa, Smart TV 4K, Bồn tắm, Ban công, Minibar..." };
            pnlTienNghi.Controls.Add(txtTienNghi);

            // Ghi chú
            y += gap;
            TaoNhan(pnlBody, "Ghi chú đặc biệt", 28, y);
            var pnlGhiChu = TaoBox(pnlBody, 28, y + 20, 540, 36);
            txtGhiChu = new TextBox { BorderStyle = BorderStyle.None, Font = new Font("Segoe UI", 9.5F), Dock = DockStyle.Fill, PlaceholderText = "View hướng biển, góc yên tĩnh..." };
            pnlGhiChu.Controls.Add(txtGhiChu);

            Controls.Add(pnlBody);
        }

        private static Label TaoNhan(Control parent, string text, int x, int y)
        {
            var lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                Location = new Point(x, y),
                AutoSize = true
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

        private void LoadLoaiPhong()
        {
            try
            {
                var dt = DatabaseHelper.ExecuteQuery("SELECT MaLoai, TenLoai FROM LoaiPhong ORDER BY TenLoai");
                cboLoai.DataSource = dt;
                cboLoai.DisplayMember = "TenLoai";
                cboLoai.ValueMember = "MaLoai";
            }
            catch { }
        }

        private void TaiThongTinPhong()
        {
            try
            {
                string sql = "SELECT * FROM Phong WHERE MaPhong = @id";
                var dt = DatabaseHelper.ExecuteQuery(sql, new[] { new SqlParameter("@id", _maPhong) });
                if (dt.Rows.Count == 0) return;

                var r = dt.Rows[0];
                txtSoPhong.Text = r["SoPhong"].ToString();
                txtTang.Text = r["Tang"].ToString();
                cboLoai.SelectedValue = Convert.ToInt32(r["MaLoai"]);
                cboTinhTrang.Text = r["TinhTrang"].ToString();
                txtMoTa.Text = r["MoTaGiuong"] == DBNull.Value ? "" : r["MoTaGiuong"].ToString();
                txtDienTich.Text = r["DienTich"] == DBNull.Value ? "" : r["DienTich"].ToString();
                txtHinhAnh.Text = r.Table.Columns.Contains("HinhAnh") && r["HinhAnh"] != DBNull.Value ? r["HinhAnh"].ToString() : "";
                txtTienNghi.Text = r.Table.Columns.Contains("TienNghi") && r["TienNghi"] != DBNull.Value ? r["TienNghi"].ToString() : "";
                txtGhiChu.Text = r.Table.Columns.Contains("GhiChu") && r["GhiChu"] != DBNull.Value ? r["GhiChu"].ToString() : "";
            }
            catch { }
        }

        private void LuuDuLieu()
        {
            string soPhong = txtSoPhong.Text.Trim();
            if (string.IsNullOrWhiteSpace(soPhong))
            {
                MessageBox.Show("Vui lòng nhập số phòng.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoPhong.Focus();
                return;
            }

            if (!int.TryParse(txtTang.Text.Trim(), out int tang))
            {
                MessageBox.Show("Tầng phải là số nguyên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTang.Focus();
                return;
            }

            decimal? dt = null;
            if (!string.IsNullOrWhiteSpace(txtDienTich.Text))
            {
                if (decimal.TryParse(txtDienTich.Text.Trim(), out decimal val)) dt = val;
            }

            int maLoai = Convert.ToInt32(cboLoai.SelectedValue);
            string tt = cboTinhTrang.Text;
            string moTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim();
            string hinhAnh = string.IsNullOrWhiteSpace(txtHinhAnh.Text) ? null : txtHinhAnh.Text.Trim();
            string tienNghi = string.IsNullOrWhiteSpace(txtTienNghi.Text) ? null : txtTienNghi.Text.Trim();
            string ghiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim();

            SqlParameter[] pars = {
                new SqlParameter("@so", soPhong),
                new SqlParameter("@tang", tang),
                new SqlParameter("@loai", maLoai),
                new SqlParameter("@mota", (object)moTa ?? DBNull.Value),
                new SqlParameter("@dt", (object)dt ?? DBNull.Value),
                new SqlParameter("@tt", tt),
                new SqlParameter("@anh", (object)hinhAnh ?? DBNull.Value),
                new SqlParameter("@tn", (object)tienNghi ?? DBNull.Value),
                new SqlParameter("@gc", (object)ghiChu ?? DBNull.Value)
            };

            try
            {
                if (_maPhong == 0)
                {
                    string sql = @"
                        INSERT INTO Phong (SoPhong, Tang, MaLoai, MoTaGiuong, DienTich, TinhTrang, HinhAnh, TienNghi, GhiChu)
                        VALUES (@so, @tang, @loai, @mota, @dt, @tt, @anh, @tn, @gc)";
                    DatabaseHelper.ExecuteNonQuery(sql, pars);
                    MessageBox.Show($"Thêm mới phòng [{soPhong}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string sql = @"
                        UPDATE Phong
                        SET SoPhong = @so, Tang = @tang, MaLoai = @loai, MoTaGiuong = @mota,
                            DienTich = @dt, TinhTrang = @tt, HinhAnh = @anh, TienNghi = @tn, GhiChu = @gc
                        WHERE MaPhong = @id";

                    Array.Resize(ref pars, pars.Length + 1);
                    pars[^1] = new SqlParameter("@id", _maPhong);
                    DatabaseHelper.ExecuteNonQuery(sql, pars);
                    MessageBox.Show($"Cập nhật phòng [{soPhong}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu thông tin phòng: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
