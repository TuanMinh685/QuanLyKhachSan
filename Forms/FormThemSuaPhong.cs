using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyKhachSan_Phan1.Helpers;

namespace QuanLyKhachSan_Phan1.Forms
{
    public class FormThemSuaPhong : Form
    {
        private readonly int? _id;
        private TextBox txtSoPhong, txtTang, txtGhiChu;
        private ComboBox cboLoaiPhong;
        private Button btnLuu;
        private Label lblLoi;

        public FormThemSuaPhong(int? id)
        {
            _id = id;
            KhoiTaoGiaoDien();
            TaiDanhSachLoaiPhong();
            if (_id.HasValue) TaiDuLieuPhong(_id.Value);
        }

        private void KhoiTaoGiaoDien()
        {
            Text = _id.HasValue ? "Sửa phòng" : "Thêm phòng mới";
            Size = new Size(460, 420);
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
                Location = new Point(24, 20),
                AutoSize = true
            };
            Controls.Add(lblTieuDe);

            lblLoi = new Label
            {
                ForeColor = GiaoDien.MauNguyHiem,
                Location = new Point(24, 56),
                Size = new Size(400, 40),
                Font = new Font("Segoe UI", 9F)
            };
            Controls.Add(lblLoi);

            int y = 100;

            Controls.Add(new Label { Text = "Số phòng *", Font = GiaoDien.FontNhan, Location = new Point(24, y), AutoSize = true });
            txtSoPhong = new TextBox { Location = new Point(24, y + 22), Size = new Size(190, 28), Font = GiaoDien.FontChuan, MaxLength = 10 };
            txtSoPhong.PlaceholderText = "Ví dụ: 101";
            Controls.Add(txtSoPhong);

            Controls.Add(new Label { Text = "Tầng", Font = GiaoDien.FontNhan, Location = new Point(234, y), AutoSize = true });
            txtTang = new TextBox { Location = new Point(234, y + 22), Size = new Size(190, 28), Font = GiaoDien.FontChuan, MaxLength = 3 };
            Controls.Add(txtTang);
            y += 62;

            Controls.Add(new Label { Text = "Loại phòng *", Font = GiaoDien.FontNhan, Location = new Point(24, y), AutoSize = true });
            cboLoaiPhong = new ComboBox
            {
                Location = new Point(24, y + 22),
                Size = new Size(400, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = GiaoDien.FontChuan,
                DisplayMember = "ten_loai_phong",
                ValueMember = "id"
            };
            Controls.Add(cboLoaiPhong);
            y += 62;

            Controls.Add(new Label { Text = "Ghi chú", Font = GiaoDien.FontNhan, Location = new Point(24, y), AutoSize = true });
            txtGhiChu = new TextBox { Location = new Point(24, y + 22), Size = new Size(400, 56), Font = GiaoDien.FontChuan, Multiline = true, MaxLength = 500 };
            Controls.Add(txtGhiChu);
            y += 90;

            btnLuu = new Button { Text = "Lưu", Location = new Point(24, y + 10), Size = new Size(190, 36) };
            GiaoDien.ApDungNutChinh(btnLuu);
            btnLuu.Click += BtnLuu_Click;
            Controls.Add(btnLuu);

            var btnHuy = new Button { Text = "Hủy", Location = new Point(234, y + 10), Size = new Size(190, 36) };
            GiaoDien.ApDungNutPhu(btnHuy);
            btnHuy.Click += (s, e) => Close();
            Controls.Add(btnHuy);

            ClientSize = new Size(448, y + 70);
        }

        private void TaiDanhSachLoaiPhong()
        {
            try
            {
                using var conn = DatabaseHelper.LayKetNoi();
                using var cmd = new SqlCommand("SELECT id, ten_loai_phong FROM loai_phong ORDER BY ten_loai_phong", conn);
                using var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                cboLoaiPhong.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    lblLoi.Text = "Chưa có loại phòng nào. Hãy đóng cửa sổ này, vào menu \"Loại phòng\" để tạo loại phòng trước.";
                    btnLuu.Enabled = false;
                    cboLoaiPhong.Enabled = false;
                }
            }
            catch (SqlException ex)
            {
                lblLoi.Text = "Lỗi tải loại phòng: " + ex.Message;
                btnLuu.Enabled = false;
            }
        }

        private void TaiDuLieuPhong(int id)
        {
            try
            {
                using var conn = DatabaseHelper.LayKetNoi();
                using var cmd = new SqlCommand("SELECT * FROM phong WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtSoPhong.Text = (string)reader["so_phong"];
                    txtTang.Text = reader["tang"] == DBNull.Value ? "" : Convert.ToInt32(reader["tang"]).ToString();
                    txtGhiChu.Text = reader["ghi_chu"] == DBNull.Value ? "" : (string)reader["ghi_chu"];
                    cboLoaiPhong.SelectedValue = Convert.ToInt32(reader["loai_phong_id"]);
                }
            }
            catch (SqlException ex)
            {
                lblLoi.Text = "Lỗi tải dữ liệu: " + ex.Message;
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            string soPhong = txtSoPhong.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();

            if (string.IsNullOrEmpty(soPhong))
            {
                lblLoi.Text = "Vui lòng nhập số phòng.";
                return;
            }
            if (cboLoaiPhong.SelectedValue == null)
            {
                lblLoi.Text = "Vui lòng chọn loại phòng.";
                return;
            }

            int? tang = null;
            if (!string.IsNullOrEmpty(txtTang.Text.Trim()))
            {
                if (!int.TryParse(txtTang.Text.Trim(), out int t) || t < 0 || t > 200)
                {
                    lblLoi.Text = "Tầng phải là số nguyên từ 0 đến 200 (hoặc để trống).";
                    return;
                }
                tang = t;
            }

            int loaiPhongId = Convert.ToInt32(cboLoaiPhong.SelectedValue);

            try
            {
                using var conn = DatabaseHelper.LayKetNoi();

                using (var cmdKiemTra = new SqlCommand(
                    "SELECT id FROM phong WHERE so_phong = @sp AND id <> @id", conn))
                {
                    cmdKiemTra.Parameters.AddWithValue("@sp", soPhong);
                    cmdKiemTra.Parameters.AddWithValue("@id", _id ?? 0);
                    if (cmdKiemTra.ExecuteScalar() != null)
                    {
                        lblLoi.Text = "Số phòng này đã tồn tại.";
                        return;
                    }
                }

                SqlCommand cmd;
                if (_id.HasValue)
                {
                    cmd = new SqlCommand(
                        @"UPDATE phong SET so_phong=@sp, loai_phong_id=@lp, tang=@tang, ghi_chu=@gc WHERE id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", _id.Value);
                }
                else
                {
                    cmd = new SqlCommand(
                        @"INSERT INTO phong (so_phong, loai_phong_id, tang, ghi_chu) VALUES (@sp, @lp, @tang, @gc)", conn);
                }

                cmd.Parameters.AddWithValue("@sp", soPhong);
                cmd.Parameters.AddWithValue("@lp", loaiPhongId);
                cmd.Parameters.AddWithValue("@tang", tang.HasValue ? tang.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@gc", string.IsNullOrEmpty(ghiChu) ? DBNull.Value : ghiChu);

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
