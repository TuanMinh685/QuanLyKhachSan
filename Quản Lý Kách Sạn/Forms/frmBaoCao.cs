using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            FormatGrid(dgvDoanhThu);
            FormatGrid(dgvTopDichVu);

            // Mặc định lọc từ đầu tháng đến hiện tại
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            NapDuLieuBaoCao();
        }

        #region Paint Styling Figma

        private void DrawFigmaCard(object sender, PaintEventArgs e)
        {
            if (sender is Panel pnl)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 10;
                Rectangle rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                using (GraphicsPath path = GetRoundedRectPath(rect, radius))
                using (Pen pen = new Pen(ColorTranslator.FromHtml("#E2E8F0"), 1.2f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private void DrawPillBorder(object sender, PaintEventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Region = new Region(GetRoundedRectPath(new Rectangle(0, 0, btn.Width, btn.Height), 6));
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2F;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void FormatGrid(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = ColorTranslator.FromHtml("#F1F5F9");
            dgv.BackgroundColor = Color.White;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F8FAFC");
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#475569");
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.25f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            dgv.ColumnHeadersHeight = 40;

            dgv.RowTemplate.Height = 38;
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#1E293B");
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.25f);
            dgv.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#EFF6FF");
            dgv.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#1D4ED8");
        }

        #endregion

        #region Logic & Export

        private void NapDuLieuBaoCao()
        {
            try
            {
                DateTime tu = dtpTuNgay.Value.Date;
                DateTime den = dtpDenNgay.Value.Date;

                // 1. Thống kê KPI
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

                // 2. Danh sách hóa đơn chi tiết
                DataTable dtHD = baoCaoDAL.LayDanhSachHoaDonTheoNgay(tu, den);
                dgvDoanhThu.DataSource = dtHD;
                if (dgvDoanhThu.Columns["Thực Thu"] != null)
                {
                    dgvDoanhThu.Columns["Thực Thu"].DefaultCellStyle.Format = "N0";
                    dgvDoanhThu.Columns["Thực Thu"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    dgvDoanhThu.Columns["Thực Thu"].DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#EA580C");
                    dgvDoanhThu.Columns["Thực Thu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvDoanhThu.Columns["Tiền Phòng"] != null)
                {
                    dgvDoanhThu.Columns["Tiền Phòng"].DefaultCellStyle.Format = "N0";
                    dgvDoanhThu.Columns["Tiền Phòng"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvDoanhThu.Columns["Tiền Dịch Vụ"] != null)
                {
                    dgvDoanhThu.Columns["Tiền Dịch Vụ"].DefaultCellStyle.Format = "N0";
                    dgvDoanhThu.Columns["Tiền Dịch Vụ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // 3. Top dịch vụ
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
                MessageBox.Show("Khoảng ngày lọc không hợp lệ (Từ ngày phải nhỏ hơn hoặc bằng Đến ngày).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    // Header
                    for (int i = 0; i < dgvDoanhThu.Columns.Count; i++)
                    {
                        sb.Append(dgvDoanhThu.Columns[i].HeaderText + (i == dgvDoanhThu.Columns.Count - 1 ? "" : ","));
                    }
                    sb.AppendLine();

                    // Rows
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

        #endregion
    }
}