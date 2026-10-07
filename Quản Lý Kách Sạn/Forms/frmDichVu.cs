using System;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;
using System.Drawing;
namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmDichVu : Form
    {
        private DichVuDAL dal = new DichVuDAL();
        private string currentMaDV = "";
        private void DrawCardBorder(object sender, PaintEventArgs e)
        {
            if (sender is Panel pnl)
            {
                ControlPaint.DrawBorder(e.Graphics, pnl.ClientRectangle,
                    ColorTranslator.FromHtml("#E2E8F0"), ButtonBorderStyle.Solid);
            }
        }
        public frmDichVu()
        {
            InitializeComponent();
        }

        private void frmDichVu_Load(object sender, EventArgs e)
        {
            UITheme.StyleModernGrid(dgvDichVu);
            cboLoai.SelectedIndex = 0;
            cboLoaiDVForm.SelectedIndex = 0;
            cboDVT.SelectedIndex = 0;
            LoadData();
        }

        private void LoadData()
        {
            string loai = cboLoai.SelectedItem != null ? cboLoai.SelectedItem.ToString() : "";
            var list = dal.GetAll(txtTimKiem.Text.Trim(), loai);
            if (list != null && list.Count > 0)
            {
                dgvDichVu.DataSource = list;
            }
            else
            {
                dgvDichVu.DataSource = new[]
                {
                    new { MaDichVu = "DV01", TenDichVu = "Nước ngọt Cocacola", LoaiDichVu = "Ăn uống", DonGia = 20000, DonViTinh = "Lon" },
                    new { MaDichVu = "DV02", TenDichVu = "Giặt sấy lấy liền", LoaiDichVu = "Giặt ủi", DonGia = 45000, DonViTinh = "Kg" },
                    new { MaDichVu = "DV03", TenDichVu = "Xe đón sân bay", LoaiDichVu = "Đưa đón", DonGia = 250000, DonViTinh = "Chuyến" }
                };
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDichVu.Rows[e.RowIndex].Cells["TenDichVu"] != null)
            {
                var row = dgvDichVu.Rows[e.RowIndex];
                currentMaDV = row.Cells["MaDichVu"]?.Value?.ToString() ?? "";
                txtTen.Text = row.Cells["TenDichVu"]?.Value?.ToString() ?? "";
                cboLoaiDVForm.Text = row.Cells["LoaiDichVu"]?.Value?.ToString() ?? "Ăn uống";
                if (decimal.TryParse(row.Cells["DonGia"]?.Value?.ToString(), out decimal gia))
                    numGia.Value = gia;
                cboDVT.Text = row.Cells["DonViTinh"]?.Value?.ToString() ?? "Lần";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập tên dịch vụ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dv = new DichVu
            {
                MaDichVu = string.IsNullOrEmpty(currentMaDV) ? "DV" + DateTime.Now.ToString("HHmmss") : currentMaDV,
                TenDichVu = txtTen.Text.Trim(),
                LoaiDichVu = cboLoaiDVForm.SelectedItem?.ToString() ?? "Ăn uống",
                DonGia = numGia.Value,
                DonViTinh = cboDVT.Text.Trim(),
                TrangThai = true
            };

            dal.Insert(dv);
            MessageBox.Show("Lưu dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnLamMoi_Click(null, null);
            LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaDV))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ muốn xóa trong bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dal.Delete(currentMaDV);
                MessageBox.Show("Đã xóa dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLamMoi_Click(null, null);
                LoadData();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            currentMaDV = "";
            txtTen.Clear();
            numGia.Value = 0;
            cboLoaiDVForm.SelectedIndex = 0;
            cboDVT.SelectedIndex = 0;
        }
    }
}