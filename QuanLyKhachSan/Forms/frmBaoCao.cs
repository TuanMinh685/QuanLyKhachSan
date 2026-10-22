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
        private int currentTabIndex = 0; // 0: Doanh thu, 1: Cong suat, 2: Khach hang

        public frmBaoCao()
        {
            InitializeComponent();
            UITheme.ApplyTheme(this);
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            UITheme.StyleModernGrid(dgvDoanhThu);
            UITheme.StyleModernGrid(dgvTopDichVu);
            UITheme.StyleModernGrid(dgvCongSuat);
            UITheme.StyleModernGrid(dgvLuotDat);
            UITheme.StyleModernGrid(dgvTopKhachHang);

            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            SwitchTab(0);
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

        private void SwitchTab(int tabIndex)
        {
            currentTabIndex = tabIndex;

            // Reset tab button colors
            Color activeBg = UITheme.PrimaryOrange;
            Color inactiveBg = Color.White;
            Color activeFg = Color.White;
            Color inactiveFg = UITheme.DarkNavy;

            btnTabDoanhThu.BackColor = (tabIndex == 0) ? activeBg : inactiveBg;
            btnTabDoanhThu.ForeColor = (tabIndex == 0) ? activeFg : inactiveFg;

            btnTabCongSuat.BackColor = (tabIndex == 1) ? activeBg : inactiveBg;
            btnTabCongSuat.ForeColor = (tabIndex == 1) ? activeFg : inactiveFg;

            btnTabKhachHang.BackColor = (tabIndex == 2) ? activeBg : inactiveBg;
            btnTabKhachHang.ForeColor = (tabIndex == 2) ? activeFg : inactiveFg;

            if (tabIndex == 0)
            {
                // Tab 1: Doanh thu & Dịch vụ
                lblGridTitle.Text = "Hóa Đơn Theo Khoảng Thời Gian";
                dgvDoanhThu.Visible = true;
                dgvCongSuat.Visible = false;
                dgvTopKhachHang.Visible = false;

                lblTopDvTitle.Text = "Top 5 Dịch Vụ Doanh Thu Cao";
                dgvTopDichVu.Visible = true;
                dgvLuotDat.Visible = false;
                pnlVipInfo.Visible = false;
            }
            else if (tabIndex == 1)
            {
                // Tab 2: Công suất & Lượt đặt
                lblGridTitle.Text = "Công Suất Sử Dụng Phòng (%) Theo Loại";
                dgvDoanhThu.Visible = false;
                dgvCongSuat.Visible = true;
                dgvTopKhachHang.Visible = false;

                lblTopDvTitle.Text = "Thống Kê Lượt Đặt & Hủy Phòng";
                dgvTopDichVu.Visible = false;
                dgvLuotDat.Visible = true;
                pnlVipInfo.Visible = false;
            }
            else
            {
                // Tab 3: Khách hàng thân thiết VIP
                lblGridTitle.Text = "Top Khách Hàng Chi Tiêu Cao Nhất";
                dgvDoanhThu.Visible = false;
                dgvCongSuat.Visible = false;
                dgvTopKhachHang.Visible = true;

                lblTopDvTitle.Text = "Chính Sách Khách Hàng VIP & Thân Thiết";
                dgvTopDichVu.Visible = false;
                dgvLuotDat.Visible = false;
                pnlVipInfo.Visible = true;
            }
        }

        private void btnTabDoanhThu_Click(object sender, EventArgs e) => SwitchTab(0);

        private void btnTabCongSuat_Click(object sender, EventArgs e) => SwitchTab(1);

        private void btnTabKhachHang_Click(object sender, EventArgs e) => SwitchTab(2);

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

                // 4. Công suất sử dụng phòng
                DataTable dtCongSuat = baoCaoDAL.LayCongSuatPhong();
                dgvCongSuat.DataSource = dtCongSuat;
                if (dgvCongSuat.Columns["Công Suất"] != null)
                {
                    dgvCongSuat.Columns["Công Suất"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvCongSuat.Columns["Công Suất"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    dgvCongSuat.Columns["Công Suất"].DefaultCellStyle.ForeColor = Color.FromArgb(16, 185, 129);
                }

                // 5. Thống kê lượt đặt & hủy
                DataTable dtLuotDat = baoCaoDAL.LayLuotDatPhong(tu, den);
                dgvLuotDat.DataSource = dtLuotDat;
                if (dgvLuotDat.Columns["Lượt Đặt"] != null)
                {
                    dgvLuotDat.Columns["Lượt Đặt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvLuotDat.Columns["Lượt Đặt"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                }
                if (dgvLuotDat.Columns["Lượt Hủy"] != null)
                {
                    dgvLuotDat.Columns["Lượt Hủy"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvLuotDat.Columns["Lượt Hủy"].DefaultCellStyle.ForeColor = Color.Crimson;
                }

                // 6. Top khách hàng VIP
                DataTable dtTopKH = baoCaoDAL.LayThongKeKhachHang();
                dgvTopKhachHang.DataSource = dtTopKH;
                if (dgvTopKhachHang.Columns["Tổng Chi Tiêu"] != null)
                {
                    dgvTopKhachHang.Columns["Tổng Chi Tiêu"].DefaultCellStyle.Format = "N0";
                    dgvTopKhachHang.Columns["Tổng Chi Tiêu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvTopKhachHang.Columns["Tổng Chi Tiêu"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    dgvTopKhachHang.Columns["Tổng Chi Tiêu"].DefaultCellStyle.ForeColor = UITheme.PrimaryOrange;
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
                DataGridView activeGrid;
                string tabName;

                if (currentTabIndex == 0)
                {
                    activeGrid = dgvDoanhThu;
                    tabName = "DoanhThu";
                }
                else if (currentTabIndex == 1)
                {
                    activeGrid = dgvCongSuat;
                    tabName = "CongSuatPhong";
                }
                else
                {
                    activeGrid = dgvTopKhachHang;
                    tabName = "TopKhachHang";
                }

                if (activeGrid.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu trong bảng hiện tại để xuất báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV File (*.csv)|*.csv";
                sfd.FileName = $"BaoCao_{tabName}_{DateTime.Now:yyyyMMdd_HHmm}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < activeGrid.Columns.Count; i++)
                    {
                        sb.Append(activeGrid.Columns[i].HeaderText + (i == activeGrid.Columns.Count - 1 ? "" : ","));
                    }
                    sb.AppendLine();

                    foreach (DataGridViewRow row in activeGrid.Rows)
                    {
                        for (int i = 0; i < activeGrid.Columns.Count; i++)
                        {
                            string val = row.Cells[i].Value?.ToString() ?? "";
                            sb.Append($"\"{val.Replace("\"", "\"\"")}\"" + (i == activeGrid.Columns.Count - 1 ? "" : ","));
                        }
                        sb.AppendLine();
                    }

                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show($"Xuất báo cáo [{tabName}] thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
