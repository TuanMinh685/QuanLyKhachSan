using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;
using QuanLyKhachSan.DAL;
using QuanLyKhachSan.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmDichVu : Form
    {
        private DichVuDAL dal = new DichVuDAL();
        private DataGridView dgvDichVu = new DataGridView();
        private TextBox txtTimKiem = new TextBox();
        private ComboBox cboLoai = new ComboBox();

        private TextBox txtTen = new TextBox();
        private NumericUpDown numGia = new NumericUpDown();
        private ComboBox cboDVT = new ComboBox();
        private ComboBox cboLoaiDVForm = new ComboBox();

        public frmDichVu()
        {
            InitializeComponent();
            SetupCustomUI();
            LoadData();
        }

        private void SetupCustomUI()
        {
            this.Text = "Quản Lý Dịch Vụ";
            this.Size = new Size(1180, 720);
            this.BackColor = UITheme.BgContent;

            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 55, BackColor = UITheme.HeaderSidebarNavy };
            Label lblHeader = new Label { Text = "QUẢN LÝ DỊCH VỤ KHÁCH SẠN", ForeColor = Color.White, Font = new Font("Segoe UI", 12F, FontStyle.Bold), Location = new Point(20, 16), AutoSize = true };
            pnlHeader.Controls.Add(lblHeader);
            this.Controls.Add(pnlHeader);

            Panel pnlLeft = new Panel { Location = new Point(25, 75), Size = new Size(720, 580), BackColor = Color.White };
            pnlLeft.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, pnlLeft.ClientRectangle, UITheme.BorderColor, ButtonBorderStyle.Solid);

            txtTimKiem.Location = new Point(15, 15);
            txtTimKiem.Size = new Size(250, 26);
            txtTimKiem.PlaceholderText = "Tìm theo tên dịch vụ...";
            txtTimKiem.TextChanged += (s, e) => LoadData();

            cboLoai.Location = new Point(280, 15);
            cboLoai.Size = new Size(160, 26);
            cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoai.Items.AddRange(new object[] { "Tất cả", "Ăn uống", "Giặt ủi", "Đưa đón", "Khác" });
            cboLoai.SelectedIndex = 0;
            cboLoai.SelectedIndexChanged += (s, e) => LoadData();

            dgvDichVu.Location = new Point(15, 55);
            dgvDichVu.Size = new Size(690, 500);
            UITheme.StyleModernGrid(dgvDichVu);
            pnlLeft.Controls.AddRange(new Control[] { txtTimKiem, cboLoai, dgvDichVu });
            this.Controls.Add(pnlLeft);

            Panel pnlRight = new Panel { Location = new Point(765, 75), Size = new Size(370, 580), BackColor = Color.White };
            pnlRight.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, pnlRight.ClientRectangle, UITheme.BorderColor, ButtonBorderStyle.Solid);

            Label lblRightTitle = new Label { Text = "Thông tin dịch vụ", Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = UITheme.TextDark, Location = new Point(20, 15), AutoSize = true };
            pnlRight.Controls.Add(lblRightTitle);

            int y = 60;
            AddInputField(pnlRight, "Tên dịch vụ:", txtTen, ref y);

            Label lblLoai = new Label { Text = "Loại dịch vụ:", Location = new Point(20, y), AutoSize = true, ForeColor = UITheme.TextMuted };
            cboLoaiDVForm.Location = new Point(20, y + 22);
            cboLoaiDVForm.Size = new Size(325, 26);
            cboLoaiDVForm.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiDVForm.Items.AddRange(new object[] { "Ăn uống", "Giặt ủi", "Đưa đón", "Khác" });
            cboLoaiDVForm.SelectedIndex = 0;
            pnlRight.Controls.AddRange(new Control[] { lblLoai, cboLoaiDVForm });
            y += 65;

            Label lblGia = new Label { Text = "Đơn giá (VNĐ):", Location = new Point(20, y), AutoSize = true, ForeColor = UITheme.TextMuted };
            numGia.Location = new Point(20, y + 22);
            numGia.Size = new Size(325, 26);
            numGia.Maximum = 100000000;
            numGia.Increment = 10000;
            pnlRight.Controls.AddRange(new Control[] { lblGia, numGia });
            y += 65;

            Label lblDVT = new Label { Text = "Đơn vị tính:", Location = new Point(20, y), AutoSize = true, ForeColor = UITheme.TextMuted };
            cboDVT.Location = new Point(20, y + 22);
            cboDVT.Size = new Size(325, 26);
            cboDVT.Items.AddRange(new object[] { "Lần", "Phần", "Chai/Lon", "Kg", "Chuyến" });
            cboDVT.SelectedIndex = 0;
            pnlRight.Controls.AddRange(new Control[] { lblDVT, cboDVT });
            y += 80;

            Button btnSave = new Button { Text = "LƯU DỊCH VỤ", Location = new Point(20, y), Size = new Size(325, 40) };
            UITheme.StylePrimaryButton(btnSave);
            btnSave.Click += BtnSave_Click;
            pnlRight.Controls.Add(btnSave);

            this.Controls.Add(pnlRight);
        }

        private void AddInputField(Panel pnl, string label, Control ctrl, ref int y)
        {
            Label lbl = new Label { Text = label, Location = new Point(20, y), AutoSize = true, ForeColor = UITheme.TextMuted, Font = new Font("Segoe UI", 9.5F) };
            ctrl.Location = new Point(20, y + 22);
            ctrl.Size = new Size(325, 26);
            pnl.Controls.AddRange(new Control[] { lbl, ctrl });
            y += 65;
        }

        private void LoadData()
        {
            try
            {
                string loai = cboLoai.SelectedItem != null ? cboLoai.SelectedItem.ToString() : "";
                dgvDichVu.DataSource = dal.GetAll(txtTimKiem.Text.Trim(), loai);
            }
            catch
            {
                dgvDichVu.DataSource = new[]
                {
                    new { MaDichVu = "DV01", TenDichVu = "Nước ngọt Cocacola", LoaiDichVu = "Ăn uống", DonGia = 20000, DonViTinh = "Lon" },
                    new { MaDichVu = "DV02", TenDichVu = "Giặt sấy lấy liền", LoaiDichVu = "Giặt ủi", DonGia = 45000, DonViTinh = "Kg" },
                    new { MaDichVu = "DV03", TenDichVu = "Xe đón sân bay", LoaiDichVu = "Đưa đón", DonGia = 250000, DonViTinh = "Chuyến" }
                };
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập tên dịch vụ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dv = new DichVu
            {
                MaDichVu = "DV" + DateTime.Now.ToString("HHmmss"),
                TenDichVu = txtTen.Text.Trim(),
                LoaiDichVu = cboLoaiDVForm.SelectedItem != null ? cboLoaiDVForm.SelectedItem.ToString() : "Ăn uống",
                DonGia = numGia.Value,
                DonViTinh = cboDVT.Text.Trim(),
                TrangThai = true
            };

            try
            {
                dal.Insert(dv);
                MessageBox.Show("Thêm mới dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                txtTen.Clear();
                numGia.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}