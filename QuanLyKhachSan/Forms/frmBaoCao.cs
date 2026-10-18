using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmBaoCao : Form
    {
        private BaoCaoDAL baoCaoDAL = new BaoCaoDAL();

        public frmBaoCao()
        {
            InitializeComponent();
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            UITheme.StyleModernGrid(dgvDoanhThu);
            UITheme.StyleModernGrid(dgvTopDichVu);

            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            NapDuLieuBaoCao();
        }

        private void DrawFigmaCard(object sender, PaintEventArgs e) => UITheme.DrawCard(sender, e);

        private void DrawPillBorder(object sender, PaintEventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Region = new Region(UITheme.GetRoundedRectPath(new Rectangle(0, 0, btn.Width, btn.Height), 6));
            }
        }

        private void NapDuLieuBaoCao()
        {
            try
            {
                DateTime tu = dtpTuNgay.Value.Date;
                DateTime den = dtpDenNgay.Value.Date;

                // 1. KPI Doanh Thu Thực Tế
                DataTable dtKpi = baoCaoDAL.LayTongQuanDoanhThu(tu, den);
                if (dtKpi.Rows.Count > 0)
                {
                    DataRow r = dtKpi.Rows[0];
                    decimal tong = Convert.ToDecimal(r["TongDoanhThu"]);
                    decimal phong = Convert.ToDecimal(r["DoanhThuPhong"]);
                    decimal dv = Convert.ToDecimal(r["DoanhThuDichVu"]);
                    int soHD = Convert.ToInt32(r["SoLuongHoaDon"]);

                    lblKpi1Value.Text = tong.ToString("N0") + " đ";
                    lblKpi2Value.Text = phong.ToString("N0") + " đ";
                    lblKpi3Value.Text = dv.ToString("N0") + " đ";
                    lblKpi4Value.Text = soHD.ToString();
                }

                // 2. Danh sách Hóa đơn
                DataTable dtHD = baoCaoDAL.LayDanhSachHoaDonTheoNgay(tu, den);
                dgvDoanhThu.DataSource = dtHD;
                if (dgvDoanhThu.Columns["Thực Thu"] != null)
                {
                    dgvDoanhThu.Columns["Thực Thu"].DefaultCellStyle.Format = "N0";
                    dgvDoanhThu.Columns["Thực Thu"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    dgvDoanhThu.Columns["Thực Thu"].DefaultCellStyle.ForeColor = UITheme.PrimaryOrange;
                    dgvDoanhThu.Columns["Thực Thu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // 3. Top Dịch vụ Doanh thu cao
                DataTable dtTop = baoCaoDAL.LayTopDichVuSuDungNhieu(tu, den);
                dgvTopDichVu.DataSource = dtTop;
                if (dgvTopDichVu.Columns["Doanh Thu"] != null)
                {
                    dgvTopDichVu.Columns["Doanh Thu"].DefaultCellStyle.Format = "N0";
                    dgvTopDichVu.Columns["Doanh Thu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvTopDichVu.Columns["Doanh Thu"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy báo cáo: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
            {
                MessageBox.Show("Khoảng ngày lọc không hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NapDuLieuBaoCao();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDoanhThu.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu hóa đơn nào để xuất báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV File (*.csv)|*.csv";
                sfd.FileName = $"BaoCao_DoanhThu_{DateTime.Now:yyyyMMdd_HHmm}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < dgvDoanhThu.Columns.Count; i++)
                    {
                        sb.Append(dgvDoanhThu.Columns[i].HeaderText + (i == dgvDoanhThu.Columns.Count - 1 ? "" : ","));
                    }
                    sb.AppendLine();

                    foreach (DataGridViewRow row in dgvDoanhThu.Rows)
                    {
                        for (int i = 0; i < dgvDoanhThu.Columns.Count; i++)
                        {
                            string val = row.Cells[i].Value?.ToString() ?? "";
                            sb.Append($"\"{val.Replace("\"", "\"\"")}\"" + (i == dgvDoanhThu.Columns.Count - 1 ? "" : ","));
                        }
                        sb.AppendLine();
                    }

                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Xuất file báo cáo doanh thu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}