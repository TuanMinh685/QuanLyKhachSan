using System;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmBaoCao : Form
    {
        public frmBaoCao()
        {
            InitializeComponent();
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            dtpTu.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpDen.Value = DateTime.Now;
            XemBaoCao();
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            if (dtpTu.Value.Date > dtpDen.Value.Date)
            {
                MessageBox.Show("Ngày 'Từ' phải trước ngày 'Đến'.");
                return;
            }
            XemBaoCao();
        }

        private void XemBaoCao()
        {
            try
            {
                DateTime tu = dtpTu.Value.Date;
                DateTime den = dtpDen.Value.Date.AddDays(1).AddSeconds(-1); // lấy trọn ngày cuối

                dgvDoanhThu.DataSource = null;
                dgvDoanhThu.DataSource = BaoCaoDAL.GetDoanhThuTheoThang(tu, den);

                dgvTopDichVu.DataSource = null;
                dgvTopDichVu.DataSource = BaoCaoDAL.GetTopDichVu(tu, den);

                dgvCongSuat.DataSource = null;
                dgvCongSuat.DataSource = BaoCaoDAL.GetCongSuatPhong(tu, den);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo: " + ex.Message);
            }
        }
    }
}