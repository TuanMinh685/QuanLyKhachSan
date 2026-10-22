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
        private int currentTabIndex = 0; // 0: Doanh thu, 1: Cong suat, 2: Khach hang

        private DataTable _dtCongSuat;
        private DataTable _dtLuotDat;

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
                pnlChartCongSuat.Visible = false;
                dgvLuotDat.Visible = false;
                pnlVipInfo.Visible = false;
            }
            else if (tabIndex == 1)
            {
                // Tab 2: Công suất & Lượt đặt (Bảng bên trái + Biểu đồ trực quan bên phải)
                lblGridTitle.Text = "Báo Cáo Công Suất Sử Dụng Phòng (%)";
                dgvDoanhThu.Visible = false;
                dgvCongSuat.Visible = true;
                dgvTopKhachHang.Visible = false;

                lblTopDvTitle.Text = "Biểu Đồ Trực Quan Tỷ Lệ Lấp Đầy & Đặt Phòng";
                dgvTopDichVu.Visible = false;
                pnlChartCongSuat.Visible = true;
                dgvLuotDat.Visible = false;
                pnlVipInfo.Visible = false;

                pnlChartCongSuat.Invalidate();
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
                pnlChartCongSuat.Visible = false;
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
                _dtCongSuat = baoCaoDAL.LayCongSuatPhong();
                dgvCongSuat.DataSource = _dtCongSuat;
                if (dgvCongSuat.Columns["Công Suất"] != null)
                {
                    dgvCongSuat.Columns["Công Suất"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvCongSuat.Columns["Công Suất"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    dgvCongSuat.Columns["Công Suất"].DefaultCellStyle.ForeColor = Color.FromArgb(16, 185, 129);
                }

                // 5. Thống kê lượt đặt & hủy
                _dtLuotDat = baoCaoDAL.LayLuotDatPhong(tu, den);
                dgvLuotDat.DataSource = _dtLuotDat;
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

                if (pnlChartCongSuat.Visible)
                {
                    pnlChartCongSuat.Invalidate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy báo cáo: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pnlChartCongSuat_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int w = pnlChartCongSuat.Width;
            int h = pnlChartCongSuat.Height;

            if (w < 100 || h < 100) return;

            g.Clear(Color.White);

            using (Font fTitle = new Font("Segoe UI", 10f, FontStyle.Bold))
            using (Font fNormal = new Font("Segoe UI", 9f, FontStyle.Regular))
            using (Font fBold = new Font("Segoe UI", 9f, FontStyle.Bold))
            using (Font fSmall = new Font("Segoe UI", 8.5f, FontStyle.Regular))
            using (Brush bNavy = new SolidBrush(UITheme.DarkNavy))
            using (Brush bGray = new SolidBrush(Color.FromArgb(100, 116, 139)))
            using (Brush bTrack = new SolidBrush(Color.FromArgb(241, 245, 249)))
            {
                int y = 8;

                // --- PHẦN 1: TỶ LỆ LẤP ĐẦY PHÒNG ---
                g.DrawString("📊 TỶ LỆ LẤP ĐẦY THEO HẠNG PHÒNG", fTitle, bNavy, 5, y);
                y += 26;

                if (_dtCongSuat != null && _dtCongSuat.Rows.Count > 0)
                {
                    Color[] barColors = {
                        Color.FromArgb(2, 132, 199),   // Sky blue
                        Color.FromArgb(234, 88, 12),   // Orange
                        Color.FromArgb(16, 185, 129),  // Emerald Green
                        Color.FromArgb(147, 51, 234)   // Purple
                    };

                    int colorIdx = 0;
                    foreach (DataRow r in _dtCongSuat.Rows)
                    {
                        string tenLoai = r["Loại Phòng"].ToString();
                        int tongPhong = Convert.ToInt32(r["Tổng Số Phòng"]);
                        int dangO = Convert.ToInt32(r["Đang Ở"]);
                        float percent = (tongPhong > 0) ? (float)dangO * 100f / tongPhong : 0f;

                        g.DrawString(tenLoai, fBold, bNavy, 5, y);
                        string statStr = $"{dangO}/{tongPhong} phòng ({percent:0.#}%)";
                        float statW = g.MeasureString(statStr, fBold).Width;
                        g.DrawString(statStr, fBold, bGray, w - statW - 10, y);
                        y += 20;

                        // Progress Bar
                        int barW = w - 15;
                        int barH = 16;
                        var trackRect = new Rectangle(5, y, barW, barH);
                        using (GraphicsPath trackPath = UITheme.GetRoundedRectPath(trackRect, 4))
                        {
                            g.FillPath(bTrack, trackPath);
                        }

                        int fillW = Math.Max(0, (int)(barW * (percent / 100f)));
                        if (fillW > 4)
                        {
                            var fillRect = new Rectangle(5, y, fillW, barH);
                            using (GraphicsPath fillPath = UITheme.GetRoundedRectPath(fillRect, 4))
                            using (Brush bFill = new SolidBrush(barColors[colorIdx % barColors.Length]))
                            {
                                g.FillPath(bFill, fillPath);
                            }
                        }
                        colorIdx++;
                        y += 28;
                    }
                }
                else
                {
                    g.DrawString("Chưa có số liệu phòng.", fNormal, bGray, 10, y);
                    y += 30;
                }

                y += 6;
                using (Pen pLine = new Pen(Color.FromArgb(226, 232, 240), 1))
                {
                    g.DrawLine(pLine, 5, y, w - 10, y);
                }
                y += 14;

                // --- PHẦN 2: BIỂU ĐỒ CỘT LƯỢT ĐẶT & HỦY PHÒNG ---
                g.DrawString("📈 LƯỢT ĐẶT VÀ HỦY GẦN ĐÂY", fTitle, bNavy, 5, y);
                y += 22;

                // Legend
                using (Brush bDat = new SolidBrush(Color.FromArgb(16, 185, 129)))
                using (Brush bHuy = new SolidBrush(Color.FromArgb(239, 68, 68)))
                {
                    g.FillRectangle(bDat, 5, y + 3, 10, 10);
                    g.DrawString("Đặt phòng", fSmall, bNavy, 20, y + 1);

                    g.FillRectangle(bHuy, 110, y + 3, 10, 10);
                    g.DrawString("Hủy phòng", fSmall, bNavy, 125, y + 1);
                }
                y += 22;

                if (_dtLuotDat != null && _dtLuotDat.Rows.Count > 0)
                {
                    int maxDays = Math.Min(6, _dtLuotDat.Rows.Count);
                    int chartAreaH = Math.Max(70, h - y - 35);
                    int chartAreaW = w - 20;
                    int colWidth = chartAreaW / maxDays;

                    int maxVal = 1;
                    for (int i = 0; i < maxDays; i++)
                    {
                        int dat = Convert.ToInt32(_dtLuotDat.Rows[i]["Lượt Đặt"]);
                        int huy = Convert.ToInt32(_dtLuotDat.Rows[i]["Lượt Hủy"]);
                        if (dat > maxVal) maxVal = dat;
                        if (huy > maxVal) maxVal = huy;
                    }

                    int baselineY = y + chartAreaH;
                    using (Pen pAxis = new Pen(Color.FromArgb(203, 213, 225), 1))
                    {
                        g.DrawLine(pAxis, 5, baselineY, w - 10, baselineY);
                    }

                    for (int i = 0; i < maxDays; i++)
                    {
                        DataRow row = _dtLuotDat.Rows[maxDays - 1 - i];
                        string ngay = row["Ngày Đặt"].ToString();
                        if (ngay.Length >= 5) ngay = ngay.Substring(0, 5); // dd/MM

                        int dat = Convert.ToInt32(row["Lượt Đặt"]);
                        int huy = Convert.ToInt32(row["Lượt Hủy"]);

                        int colX = 10 + i * colWidth;
                        int barSubW = Math.Max(8, (colWidth - 14) / 2);

                        int hDat = (int)((float)dat / maxVal * (chartAreaH - 18));
                        int hHuy = (int)((float)huy / maxVal * (chartAreaH - 18));

                        if (dat > 0)
                        {
                            var rectDat = new Rectangle(colX, baselineY - hDat, barSubW, hDat);
                            using (Brush bDat = new SolidBrush(Color.FromArgb(16, 185, 129)))
                            {
                                g.FillRectangle(bDat, rectDat);
                            }
                            g.DrawString(dat.ToString(), fSmall, Brushes.Black, colX + (barSubW / 2) - 4, baselineY - hDat - 14);
                        }

                        if (huy > 0)
                        {
                            var rectHuy = new Rectangle(colX + barSubW + 2, baselineY - hHuy, barSubW, hHuy);
                            using (Brush bHuy = new SolidBrush(Color.FromArgb(239, 68, 68)))
                            {
                                g.FillRectangle(bHuy, rectHuy);
                            }
                            g.DrawString(huy.ToString(), fSmall, Brushes.Red, colX + barSubW + 2 + (barSubW / 2) - 4, baselineY - hHuy - 14);
                        }

                        g.DrawString(ngay, fSmall, bGray, colX, baselineY + 5);
                    }
                }
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
