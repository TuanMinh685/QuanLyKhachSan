using Microsoft.Data.SqlClient;
using QuanLyKhachSan_Phan1.Helpers;

namespace QuanLyKhachSan_Phan1.Forms
{
    public class FormThemSuaLoaiPhong : Form
    {
        private readonly int? _id;
        private TextBox txtTen, txtGia, txtSucChua, txtDienTich, txtMoTa;
        private Label lblLoi;

        public FormThemSuaLoaiPhong(int? id)
        {
            _id = id;
            KhoiTaoGiaoDien();
            if (_id.HasValue) TaiDuLieu(_id.Value);
        }

        private void KhoiTaoGiaoDien()
        {
            Text = _id.HasValue ? "Sửa loại phòng" : "Thêm loại phòng";
            Size = new Size(460, 480);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;
            Font = GiaoDien.FontChuan;
            AutoScroll = true;

            var lblTieuDe = new Label
            {
                Text = Text,
                Font = GiaoDien.FontTieuDe,
                ForeColor = GiaoDien.MauChinhDam,
                Location = new Point(24, 16),
                AutoSize = true
            };
            Controls.Add(lblTieuDe);

            lblLoi = new Label
            {
                ForeColor = GiaoDien.MauNguyHiem,
                Location = new Point(24, 52),
                Size = new Size(400, 36),
                Font = new Font("Segoe UI", 9F)
            };
            Controls.Add(lblLoi);

            int y = 92;

            // 1. Tên loại phòng
            Controls.Add(new Label { Text = "Tên loại phòng *", Font = GiaoDien.FontNhan, Location = new Point(24, y), AutoSize = true });
            txtTen = new TextBox { Location = new Point(24, y + 22), Size = new Size(400, 28), Font = GiaoDien.FontChuan, MaxLength = 50 };
            txtTen.PlaceholderText = "Ví dụ: Standard, Deluxe, Suite";
            Controls.Add(txtTen);
            y += 62;

            // 2. Giá cơ bản + Sức chứa (cùng một hàng, đặt ngay phía trên để luôn nhìn thấy)
            Controls.Add(new Label { Text = "Giá cơ bản / đêm (VND) *", Font = GiaoDien.FontNhan, Location = new Point(24, y), AutoSize = true });
            txtGia = new TextBox { Location = new Point(24, y + 22), Size = new Size(190, 28), Font = GiaoDien.FontChuan, MaxLength = 12 };
            txtGia.PlaceholderText = "Ví dụ: 500000";
            Controls.Add(txtGia);

            Controls.Add(new Label { Text = "Sức chứa (người)", Font = GiaoDien.FontNhan, Location = new Point(234, y), AutoSize = true });
            txtSucChua = new TextBox { Location = new Point(234, y + 22), Size = new Size(190, 28), Font = GiaoDien.FontChuan, Text = "2", MaxLength = 2 };
            Controls.Add(txtSucChua);
            y += 62;

            // 3. Diện tích
            Controls.Add(new Label { Text = "Diện tích (m²)", Font = GiaoDien.FontNhan, Location = new Point(24, y), AutoSize = true });
            txtDienTich = new TextBox { Location = new Point(24, y + 22), Size = new Size(400, 28), Font = GiaoDien.FontChuan, MaxLength = 7 };
            txtDienTich.PlaceholderText = "Không bắt buộc";
            Controls.Add(txtDienTich);
            y += 62;

            // 4. Mô tả
            Controls.Add(new Label { Text = "Mô tả", Font = GiaoDien.FontNhan, Location = new Point(24, y), AutoSize = true });
            txtMoTa = new TextBox { Location = new Point(24, y + 22), Size = new Size(400, 56), Font = GiaoDien.FontChuan, Multiline = true, MaxLength = 500 };
            Controls.Add(txtMoTa);
            y += 90;

            var btnLuu = new Button { Text = "Lưu", Location = new Point(24, y + 6), Size = new Size(190, 36) };
            GiaoDien.ApDungNutChinh(btnLuu);
            btnLuu.Click += BtnLuu_Click;
            Controls.Add(btnLuu);

            var btnHuy = new Button { Text = "Hủy", Location = new Point(234, y + 6), Size = new Size(190, 36) };
            GiaoDien.ApDungNutPhu(btnHuy);
            btnHuy.Click += (s, e) => Close();
            Controls.Add(btnHuy);

            ClientSize = new Size(448, y + 60);
        }

        private void TaiDuLieu(int id)
        {
            try
            {
                using var conn = DatabaseHelper.LayKetNoi();
                using var cmd = new SqlCommand("SELECT * FROM loai_phong WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtTen.Text = (string)reader["ten_loai_phong"];
                    txtMoTa.Text = reader["mo_ta"] == DBNull.Value ? "" : (string)reader["mo_ta"];
                    txtGia.Text = Convert.ToDecimal(reader["gia_co_ban"]).ToString("0");
                    txtSucChua.Text = Convert.ToInt32(reader["suc_chua"]).ToString();
                    txtDienTich.Text = reader["dien_tich"] == DBNull.Value ? "" : Convert.ToDecimal(reader["dien_tich"]).ToString("0.##");
                }
            }
            catch (SqlException ex)
            {
                lblLoi.Text = "Lỗi tải dữ liệu: " + ex.Message;
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            string ten = txtTen.Text.Trim();
            string moTa = txtMoTa.Text.Trim();

            if (string.IsNullOrEmpty(ten))
            {
                lblLoi.Text = "Vui lòng nhập tên loại phòng.";
                return;
            }
            if (!decimal.TryParse(txtGia.Text.Trim(), out decimal gia) || gia <= 0)
            {
                lblLoi.Text = "Giá cơ bản phải là một số lớn hơn 0 (ví dụ: 500000).";
                return;
            }
            if (gia > 9999999999m)
            {
                lblLoi.Text = "Giá cơ bản quá lớn.";
                return;
            }
            if (!int.TryParse(txtSucChua.Text.Trim(), out int sucChua) || sucChua < 1 || sucChua > 20)
            {
                lblLoi.Text = "Sức chứa phải là số nguyên từ 1 đến 20.";
                return;
            }

            decimal? dienTich = null;
            if (!string.IsNullOrEmpty(txtDienTich.Text.Trim()))
            {
                if (!decimal.TryParse(txtDienTich.Text.Trim(), out decimal dt) || dt <= 0 || dt >= 10000)
                {
                    lblLoi.Text = "Diện tích phải là số lớn hơn 0 và nhỏ hơn 10000 (hoặc để trống).";
                    return;
                }
                dienTich = dt;
            }

            try
            {
                using var conn = DatabaseHelper.LayKetNoi();
                SqlCommand cmd;

                if (_id.HasValue)
                {
                    cmd = new SqlCommand(
                        @"UPDATE loai_phong SET ten_loai_phong=@ten, mo_ta=@mota, gia_co_ban=@gia,
                          suc_chua=@sc, dien_tich=@dt WHERE id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", _id.Value);
                }
                else
                {
                    cmd = new SqlCommand(
                        @"INSERT INTO loai_phong (ten_loai_phong, mo_ta, gia_co_ban, suc_chua, dien_tich)
                          VALUES (@ten, @mota, @gia, @sc, @dt)", conn);
                }

                cmd.Parameters.AddWithValue("@ten", ten);
                cmd.Parameters.AddWithValue("@mota", string.IsNullOrEmpty(moTa) ? DBNull.Value : moTa);
                cmd.Parameters.AddWithValue("@gia", gia);
                cmd.Parameters.AddWithValue("@sc", sucChua);
                cmd.Parameters.AddWithValue("@dt", dienTich.HasValue ? dienTich.Value : DBNull.Value);

                cmd.ExecuteNonQuery();
                cmd.Dispose();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (SqlException ex)
            {
                lblLoi.Text = "Lỗi lưu dữ liệu: " + ex.Message;
            }
        }
    }
}
