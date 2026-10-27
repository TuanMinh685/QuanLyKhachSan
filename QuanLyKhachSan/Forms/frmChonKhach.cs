using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.BLL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmChonKhach : Form
    {
        // Kết quả trả về cho màn hình đặt phòng.
        public int MaKH { get; private set; }
        public string HoTen { get; private set; }
        public string SoDienThoai { get; private set; }
        public string MaKHHienThi { get; private set; }

        public frmChonKhach()
        {
            InitializeComponent();

            AcceptButton = btnTimKhach;
            CancelButton = btnDong;
            dgvKhach.CellDoubleClick += dgvKhach_CellDoubleClick;

            Button btnThemKhach = new Button
            {
                Text = "➕ Thêm khách mới",
                Location = new Point(12, 364),
                Size = new Size(160, 30),
                BackColor = UITheme.PrimaryOrange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnThemKhach.FlatAppearance.BorderSize = 0;
            btnThemKhach.Click += (_, __) =>
            {
                using var f = new frmThemKhachNhanhModal();
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    MaKH = f.MaKH;
                    HoTen = f.HoTen;
                    SoDienThoai = f.SoDienThoai;
                    MaKHHienThi = f.MaKHHienThi;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };
            Controls.Add(btnThemKhach);
            ThietKeChonKhach(btnThemKhach);
        }

        private void frmChonKhach_Load(object sender, EventArgs e)
        {
            CauHinhLuoi();
            TimKhach();
        }

        private void CauHinhLuoi()
        {
            dgvKhach.AutoGenerateColumns = false;
            dgvKhach.ReadOnly = true;
            dgvKhach.AllowUserToAddRows = false;
            dgvKhach.AllowUserToDeleteRows = false;
            dgvKhach.RowHeadersVisible = false;
            dgvKhach.MultiSelect = false;
            dgvKhach.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvKhach.BackgroundColor = Color.White;
            dgvKhach.BorderStyle = BorderStyle.None;

            dgvKhach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaKHHienThi",
                HeaderText = "Mã KH",
                Width = 100
            });

            dgvKhach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HoTen",
                HeaderText = "Họ tên",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvKhach.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoDienThoai",
                HeaderText = "Số điện thoại",
                Width = 130
            });
            LeTanUi.Grid(dgvKhach);
            dgvKhach.Columns[0].MinimumWidth = 110;
            dgvKhach.Columns[1].MinimumWidth = 220;
            dgvKhach.Columns[1].FillWeight = 200;
            dgvKhach.Columns[2].MinimumWidth = 160;
        }

        private DataTable LayDanhSachKhach(string tuKhoa)
        {
            return LeTanDatPhongBLL.TimKhach(tuKhoa);
        }

        private void TimKhach()
        {
            try
            {
                dgvKhach.DataSource =
                    LayDanhSachKhach(txtTimKhach.Text.Trim());
                lblDemKhach.Text = $"{dgvKhach.Rows.Count} khách hàng · Chọn một dòng để tiếp tục";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không tải được danh sách khách: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnTimKhach_Click(object sender, EventArgs e)
        {
            TimKhach();
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            ChonKhachDangChon();
        }

        private void dgvKhach_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ChonKhachDangChon();
            }
        }

        private void ChonKhachDangChon()
        {
            if (dgvKhach.CurrentRow == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn một khách trong danh sách.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var r = (DataRowView)dgvKhach.CurrentRow.DataBoundItem;

            MaKH = (int)r["MaKH"];
            HoTen = r["HoTen"].ToString();
            SoDienThoai = r["SoDienThoai"].ToString();
            MaKHHienThi = r["MaKHHienThi"].ToString();

            DialogResult = DialogResult.OK;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void dgvKhach_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
        }
    }
}
