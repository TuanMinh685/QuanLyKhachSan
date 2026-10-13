using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyKhachSan_Phan1.Helpers;

namespace QuanLyKhachSan_Phan1.Forms
{
    public class FormCheckIn : Form
    {
        // Thứ tự khớp với cboLoaiGiayTo: 0 = CCCD, 1 = CMND, 2 = Hộ chiếu
        private static readonly string[] MaLoaiGiayTo = { "CCCD", "CMND", "HoChieu" };

        private ComboBox cboPhong, cboLoaiGiayTo;
        private TextBox txtHoTen, txtSdt, txtEmail, txtDiaChi, txtQuocTich, txtSoGiayTo;
        private ComboBox cboGioiTinh;
        private DateTimePicker dtpNgaySinh;
        private CheckBox chkCoNgaySinh;
        private Button btnXacNhan;
        private Label lblLoi, lblThanhCong;

        public FormCheckIn()
        {
            KhoiTaoGiaoDien();
            TaiDanhSachPhongChoNhan();
        }

        private void KhoiTaoGiaoDien()
        {
            AutoScaleMode = AutoScaleMode.None; // Tat tu dong phong to theo DPI, tranh giao dien bi lech/de nhau

            BackColor = GiaoDien.MauNen;
            Font = GiaoDien.FontChuan;
            AutoScroll = true;

            var lblTieuDe = new Label
            {
                Text = "Khai báo giấy tờ khách check-in",
                Font = GiaoDien.FontTieuDe,
                ForeColor = GiaoDien.MauChinhDam,
                Location = new Point(0, 0),
                AutoSize = true
            };
            Controls.Add(lblTieuDe);

            var lblGhiChu = new Label
            {
                Text = "Lưu ý: đây là bản rút gọn. Hệ thống đầy đủ sẽ gắn bước này với 1 đơn đặt\n" +
                       "phòng có sẵn (thuộc Phần II - Khách hàng && Đặt phòng).",
                ForeColor = GiaoDien.MauChinh,
                Location = new Point(0, 34),
                Size = new Size(620, 40),
                Font = new Font("Segoe UI", 9F)
            };
            Controls.Add(lblGhiChu);

            lblLoi = new Label { ForeColor = GiaoDien.MauNguyHiem, Location = new Point(0, 80), Size = new Size(620, 36), Font = new Font("Segoe UI", 9F) };
            Controls.Add(lblLoi);

            lblThanhCong = new Label { ForeColor = ColorTranslator.FromHtml("#1f7a3d"), Location = new Point(0, 80), Size = new Size(620, 36), Font = new Font("Segoe UI", 9F) };
            Controls.Add(lblThanhCong);

            int y = 120;

            Controls.Add(new Label { Text = "Chọn phòng nhận (đang ở trạng thái Đã đặt) *", Font = GiaoDien.FontNhan, Location = new Point(0, y), AutoSize = true });
            cboPhong = new ComboBox
            {
                Location = new Point(0, y + 22),
                Size = new Size(420, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = GiaoDien.FontChuan,
                DisplayMember = "hien_thi",
                ValueMember = "id"
            };
            Controls.Add(cboPhong);
            y += 62;

            Controls.Add(new Label { Text = "Họ tên khách *", Font = GiaoDien.FontNhan, Location = new Point(0, y), AutoSize = true });
            txtHoTen = new TextBox { Location = new Point(0, y + 22), Size = new Size(420, 28), Font = GiaoDien.FontChuan, MaxLength = 100 };
            Controls.Add(txtHoTen);
            y += 62;

            // Hàng: Giới tính | Ngày sinh
            Controls.Add(new Label { Text = "Giới tính", Font = GiaoDien.FontNhan, Location = new Point(0, y), AutoSize = true });
            cboGioiTinh = new ComboBox
            {
                Location = new Point(0, y + 22),
                Size = new Size(200, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = GiaoDien.FontChuan
            };
            // Thứ tự: 0 = không chọn, 1 = Nam, 2 = Nữ, 3 = Khác
            cboGioiTinh.Items.AddRange(new object[] { "-- Không chọn --", "Nam", "Nữ", "Khác" });
            cboGioiTinh.SelectedIndex = 0;
            Controls.Add(cboGioiTinh);

            chkCoNgaySinh = new CheckBox { Text = "Nhập ngày sinh", Font = GiaoDien.FontNhan, Location = new Point(220, y - 1), AutoSize = true };
            chkCoNgaySinh.CheckedChanged += (s, e) => dtpNgaySinh.Enabled = chkCoNgaySinh.Checked;
            Controls.Add(chkCoNgaySinh);
            dtpNgaySinh = new DateTimePicker
            {
                Location = new Point(220, y + 22),
                Size = new Size(200, 28),
                Format = DateTimePickerFormat.Short,
                Enabled = false,
                MinDate = new DateTime(1900, 1, 1),
                MaxDate = DateTime.Today,
                Value = new DateTime(1990, 1, 1)
            };
            Controls.Add(dtpNgaySinh);
            y += 62;

            // Hàng: Số điện thoại | Email
            Controls.Add(new Label { Text = "Số điện thoại (9-11 số)", Font = GiaoDien.FontNhan, Location = new Point(0, y), AutoSize = true });
            txtSdt = new TextBox { Location = new Point(0, y + 22), Size = new Size(200, 28), Font = GiaoDien.FontChuan, MaxLength = 11 };
            Controls.Add(txtSdt);

            Controls.Add(new Label { Text = "Email", Font = GiaoDien.FontNhan, Location = new Point(220, y), AutoSize = true });
            txtEmail = new TextBox { Location = new Point(220, y + 22), Size = new Size(200, 28), Font = GiaoDien.FontChuan, MaxLength = 100 };
            Controls.Add(txtEmail);
            y += 62;

            Controls.Add(new Label { Text = "Địa chỉ", Font = GiaoDien.FontNhan, Location = new Point(0, y), AutoSize = true });
            txtDiaChi = new TextBox { Location = new Point(0, y + 22), Size = new Size(420, 28), Font = GiaoDien.FontChuan, MaxLength = 255 };
            Controls.Add(txtDiaChi);
            y += 62;

            Controls.Add(new Label { Text = "Quốc tịch", Font = GiaoDien.FontNhan, Location = new Point(0, y), AutoSize = true });
            txtQuocTich = new TextBox { Location = new Point(0, y + 22), Size = new Size(420, 28), Font = GiaoDien.FontChuan, Text = "Việt Nam", MaxLength = 50 };
            Controls.Add(txtQuocTich);
            y += 62;

            Controls.Add(new Label { Text = "Loại giấy tờ *", Font = GiaoDien.FontNhan, Location = new Point(0, y), AutoSize = true });
            cboLoaiGiayTo = new ComboBox
            {
                Location = new Point(0, y + 22),
                Size = new Size(200, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = GiaoDien.FontChuan
            };
            cboLoaiGiayTo.Items.AddRange(new object[] { "CCCD", "CMND", "Hộ chiếu" });
            cboLoaiGiayTo.SelectedIndex = 0;
            Controls.Add(cboLoaiGiayTo);

            Controls.Add(new Label { Text = "Số giấy tờ *", Font = GiaoDien.FontNhan, Location = new Point(220, y), AutoSize = true });
            txtSoGiayTo = new TextBox { Location = new Point(220, y + 22), Size = new Size(200, 28), Font = GiaoDien.FontChuan, MaxLength = 30 };
            Controls.Add(txtSoGiayTo);
            y += 62;

            btnXacNhan = new Button { Text = "Xác nhận check-in", Location = new Point(0, y + 10), Size = new Size(420, 38) };
            GiaoDien.ApDungNutChinh(btnXacNhan);
            btnXacNhan.Click += BtnXacNhan_Click;
            Controls.Add(btnXacNhan);
        }

        private void TaiDanhSachPhongChoNhan()
        {
            try
            {
                using var conn = DatabaseHelper.LayKetNoi();
                using var cmd = new SqlCommand(
                    @"SELECT p.id, p.so_phong, lp.ten_loai_phong
                      FROM phong p JOIN loai_phong lp ON lp.id = p.loai_phong_id
                      WHERE p.trang_thai = 'DaDat' ORDER BY p.so_phong", conn);
                using var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);

                dt.Columns.Add("hien_thi", typeof(string));
                foreach (DataRow row in dt.Rows)
                    row["hien_thi"] = $"Phòng {row["so_phong"]} ({row["ten_loai_phong"]})";

                cboPhong.DataSource = dt;

                bool coPhong = dt.Rows.Count > 0;
                btnXacNhan.Enabled = coPhong;
                if (!coPhong && string.IsNullOrEmpty(lblThanhCong.Text))
                {
                    lblLoi.Text = "Hiện không có phòng nào ở trạng thái \"Đã đặt\" để nhận phòng. " +
                                  "Hãy vào menu Phòng, đổi trạng thái 1 phòng sang \"Đã đặt\" trước.";
                }
            }
            catch (SqlException ex)
            {
                lblLoi.Text = "Lỗi tải danh sách phòng: " + ex.Message;
                btnXacNhan.Enabled = false;
            }
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            lblThanhCong.Text = "";
            lblLoi.Text = "";

            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSdt.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string[] maGioiTinh = { null, "Nam", "Nu", "Khac" };
            string gioiTinh = maGioiTinh[cboGioiTinh.SelectedIndex];
            string quocTich = txtQuocTich.Text.Trim();
            string loaiGiayTo = MaLoaiGiayTo[cboLoaiGiayTo.SelectedIndex];
            string soGiayTo = txtSoGiayTo.Text.Trim();

            if (cboPhong.SelectedValue == null)
            {
                lblLoi.Text = "Vui lòng chọn phòng nhận.";
                return;
            }

            string loi = KiemTraHelper.KiemTraHoTen(hoTen)
                      ?? KiemTraHelper.KiemTraSdt(sdt)
                      ?? KiemTraHelper.KiemTraEmail(email)
                      ?? KiemTraHelper.KiemTraSoGiayTo(loaiGiayTo, soGiayTo);
            if (loi != null)
            {
                lblLoi.Text = loi;
                return;
            }

            int phongId = Convert.ToInt32(cboPhong.SelectedValue);

            try
            {
                using var conn = DatabaseHelper.LayKetNoi();
                using var giaoDich = conn.BeginTransaction();
                try
                {
                    using (var cmd = new SqlCommand(
                        @"INSERT INTO khach_hang (ho_ten, ngay_sinh, gioi_tinh, sdt, email, dia_chi, quoc_tich, loai_giay_to, so_giay_to)
                          VALUES (@ht, @ns, @gt, @sdt, @email, @dc, @qt, @lgt, @sgt)", conn, giaoDich))
                    {
                        cmd.Parameters.AddWithValue("@ht", hoTen);
                        cmd.Parameters.AddWithValue("@ns", chkCoNgaySinh.Checked ? dtpNgaySinh.Value.Date : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@gt", gioiTinh == null ? DBNull.Value : gioiTinh);
                        cmd.Parameters.AddWithValue("@sdt", string.IsNullOrEmpty(sdt) ? DBNull.Value : sdt);
                        cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(email) ? DBNull.Value : email);
                        cmd.Parameters.AddWithValue("@dc", string.IsNullOrEmpty(diaChi) ? DBNull.Value : diaChi);
                        cmd.Parameters.AddWithValue("@qt", string.IsNullOrEmpty(quocTich) ? "Việt Nam" : quocTich);
                        cmd.Parameters.AddWithValue("@lgt", loaiGiayTo);
                        cmd.Parameters.AddWithValue("@sgt", soGiayTo);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand(
                        "UPDATE phong SET trang_thai = 'DangSuDung' WHERE id = @id", conn, giaoDich))
                    {
                        cmd.Parameters.AddWithValue("@id", phongId);
                        cmd.ExecuteNonQuery();
                    }

                    giaoDich.Commit();
                }
                catch
                {
                    giaoDich.Rollback();
                    throw;
                }

                lblThanhCong.Text = "Đã lưu thông tin khách và cập nhật trạng thái phòng thành công.";
                txtHoTen.Clear();
                txtSdt.Clear();
                txtEmail.Clear();
                txtDiaChi.Clear();
                cboGioiTinh.SelectedIndex = 0;
                txtSoGiayTo.Clear();
                chkCoNgaySinh.Checked = false;
                TaiDanhSachPhongChoNhan();
            }
            catch (SqlException ex)
            {
                lblLoi.Text = "Có lỗi xảy ra: " + ex.Message;
            }
        }
    }
}