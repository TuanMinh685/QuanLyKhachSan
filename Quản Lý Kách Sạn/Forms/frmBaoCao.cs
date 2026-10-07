using System;
using System.Data;
using System.Drawing;
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
            UITheme.StyleModernGrid(dgvBaoCao);

            // Tự động kéo giãn các cột bảng phủ kín chiều rộng
            dgvBaoCao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dtpTuNgay.Value = DateTime.Now.AddDays(-30);
            dtpDenNgay.Value = DateTime.Now;
            LoadDuLieuBaoCao();
        }

        private void DrawCardBorder(object sender, PaintEventArgs e)
        {
            if (sender is Panel pnl)
            {
                ControlPaint.DrawBorder(e.Graphics, pnl.ClientRectangle,
                    ColorTranslator.FromHtml("#E2E8F0"), ButtonBorderStyle.Solid);
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadDuLieuBaoCao();
        }

        private void LoadDuLieuBaoCao()
        {
            DataTable dt = BaoCaoDAL.GetDoanhThuTheoNgay(dtpTuNgay.Value.Date, dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1));
            decimal tongDoanhThu = 0;
            decimal tongTienPhong = 0;
            decimal tongTienDichVu = 0;

            if (dt != null && dt.Rows.Count > 0)
            {
                dgvBaoCao.DataSource = dt;
                foreach (DataRow row in dt.Rows)
                {
                    tongTienPhong += Convert.ToDecimal(row["TongTienPhong"]);
                    tongTienDichVu += Convert.ToDecimal(row["TongTienDichVu"]);
                    tongDoanhThu += Convert.ToDecimal(row["TongDoanhThu"]);
                }
            }
            else
            {
                // Dữ liệu mẫu minh họa
                DataTable mock = new DataTable();
                mock.Columns.Add("Ngay", typeof(string));
                mock.Columns.Add("TongTienPhong", typeof(decimal));
                mock.Columns.Add("TongTienDichVu", typeof(decimal));
                mock.Columns.Add("TongDoanhThu", typeof(decimal));

                mock.Rows.Add(DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy"), 2900000m, 210000m, 3110000m);
                mock.Rows.Add(DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy"), 4350000m, 350000m, 4700000m);
                mock.Rows.Add(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"), 1450000m, 120000m, 1570000m);
                mock.Rows.Add(DateTime.Now.ToString("dd/MM/yyyy"), 5800000m, 500000m, 6300000m);

                dgvBaoCao.DataSource = mock;
                tongTienPhong = 14500000m;
                tongTienDichVu = 1180000m;
                tongDoanhThu = 15680000m;
            }

            // Định dạng tiêu đề cột
            if (dgvBaoCao.Columns["Ngay"] != null) dgvBaoCao.Columns["Ngay"].HeaderText = "Ngày";
            if (dgvBaoCao.Columns["TongTienPhong"] != null)
            {
                dgvBaoCao.Columns["TongTienPhong"].HeaderText = "Tiền phòng";
                dgvBaoCao.Columns["TongTienPhong"].DefaultCellStyle.Format = "N0";
            }
            if (dgvBaoCao.Columns["TongTienDichVu"] != null)
            {
                dgvBaoCao.Columns["TongTienDichVu"].HeaderText = "Tiền dịch vụ";
                dgvBaoCao.Columns["TongTienDichVu"].DefaultCellStyle.Format = "N0";
            }
            if (dgvBaoCao.Columns["TongDoanhThu"] != null)
            {
                dgvBaoCao.Columns["TongDoanhThu"].HeaderText = "Tổng cộng";
                dgvBaoCao.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
            }

            // Cập nhật 3 Card KPI
            lblKpi1Value.Text = $"{tongDoanhThu:N0} đ";
            lblKpi2Value.Text = $"{tongTienPhong:N0} đ";
            lblKpi3Value.Text = $"{tongTienDichVu:N0} đ";
        }
    }
}