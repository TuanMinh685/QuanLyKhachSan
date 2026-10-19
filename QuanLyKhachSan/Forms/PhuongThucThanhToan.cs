using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class PhuongThucThanhToan : Form
    {
        private PhuongThucThanhToanDAL dal = new PhuongThucThanhToanDAL();
        private List<PhuongThucThanhToanModel> danhSachGoc = new List<PhuongThucThanhToanModel>();
        private int? maDangChon = null;

        public PhuongThucThanhToan()
        {
            InitializeComponent();
            UITheme.ApplyTheme(this);
        }

        private void PhuongThucThanhToan_Load(object sender, EventArgs e)
        {
            UITheme.StyleModernGrid(dgvPTTT);
            TaiDanhSach();
            LamMoi();
        }

        private void DrawFigmaCard(object sender, PaintEventArgs e) => UITheme.DrawCard(sender, e);
        private void DrawFigmaInputBox(object sender, PaintEventArgs e) => UITheme.DrawInputBox(sender, e);

        private void DrawPillBorder(object sender, PaintEventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Region = new Region(UITheme.GetRoundedRectPath(new Rectangle(0, 0, btn.Width, btn.Height), 6));
            }
        }

        private void TaiDanhSach()
        {
            try
            {
                danhSachGoc = dal.GetAll();
                HienThiLenBang(danhSachGoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải phương thức thanh toán: " + ex.Message);
            }
        }

        private void HienThiLenBang(List<PhuongThucThanhToanModel> ds)
        {
            dgvPTTT.DataSource = null;
            dgvPTTT.DataSource = ds;

            if (dgvPTTT.Columns["MaPhuongThuc"] != null)
            {
                dgvPTTT.Columns["MaPhuongThuc"].HeaderText = "MÃ";
                dgvPTTT.Columns["MaPhuongThuc"].FillWeight = 45;
                dgvPTTT.Columns["MaPhuongThuc"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                dgvPTTT.Columns["MaPhuongThuc"].DefaultCellStyle.ForeColor = UITheme.DarkNavy;
            }
            if (dgvPTTT.Columns["TenPhuongThuc"] != null)
            {
                dgvPTTT.Columns["TenPhuongThuc"].HeaderText = "TÊN PHƯƠNG THỨC";
                dgvPTTT.Columns["TenPhuongThuc"].FillWeight = 140;
                dgvPTTT.Columns["TenPhuongThuc"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            }
            if (dgvPTTT.Columns["MoTa"] != null)
            {
                dgvPTTT.Columns["MoTa"].HeaderText = "GHI CHÚ / MÔ TẢ";
                dgvPTTT.Columns["MoTa"].FillWeight = 180;
            }
            if (dgvPTTT.Columns["KichHoat"] != null)
            {
                dgvPTTT.Columns["KichHoat"].HeaderText = "KÍCH HOẠT";
                dgvPTTT.Columns["KichHoat"].FillWeight = 65;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string k = txtTimKiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(k))
            {
                HienThiLenBang(danhSachGoc);
            }
            else
            {
                var loc = danhSachGoc.Where(x =>
                    (x.TenPhuongThuc != null && x.TenPhuongThuc.ToLower().Contains(k)) ||
                    (x.MoTa != null && x.MoTa.ToLower().Contains(k))).ToList();
                HienThiLenBang(loc);
            }
        }

        private void dgvPTTT_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPTTT.CurrentRow == null) return;
            var item = dgvPTTT.CurrentRow.DataBoundItem as PhuongThucThanhToanModel;
            if (item == null) return;

            maDangChon = item.MaPhuongThuc;
            txtMaPTTT.Text = item.MaPhuongThuc.ToString();
            txtTenPTTT.Text = item.TenPhuongThuc;
            txtMoTa.Text = item.MoTa;
            chkKichHoat.Checked = item.KichHoat;

            btnLuu.Text = "Cập nhật";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string ten = txtTenPTTT.Text.Trim();
            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập tên phương thức thanh toán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenPTTT.Focus();
                return;
            }

            if (maDangChon.HasValue)
            {
                if (dal.Update(maDangChon.Value, ten, txtMoTa.Text.Trim(), chkKichHoat.Checked))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSach();
                    LamMoi();
                }
            }
            else
            {
                if (dal.Insert(ten, txtMoTa.Text.Trim(), chkKichHoat.Checked))
                {
                    MessageBox.Show("Thêm mới phương thức thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSach();
                    LamMoi();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!maDangChon.HasValue)
            {
                MessageBox.Show("Vui lòng chọn phương thức cần xóa.", "Thông báo");
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa phương thức thanh toán này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dal.Delete(maDangChon.Value))
                {
                    MessageBox.Show("Đã xóa thành công!");
                    TaiDanhSach();
                    LamMoi();
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e) => LamMoi();

        private void LamMoi()
        {
            maDangChon = null;
            txtMaPTTT.Text = "(Tự động tăng)";
            txtTenPTTT.Clear();
            txtMoTa.Clear();
            chkKichHoat.Checked = true;
            btnLuu.Text = "Lưu phương thức";
            txtTimKiem.Clear();
            dgvPTTT.ClearSelection();
        }
    }
}