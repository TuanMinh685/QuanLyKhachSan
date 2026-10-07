using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmHoaDon : Form
    {
        private string maHDDangChon = null;
        private HoaDonDAL hoaDonDAL = new HoaDonDAL();
        private List<HoaDon> danhSachGoc = new List<HoaDon>();

        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            ThietKeBangChuanFigma();
            TaiDanhSachPhuongThucTT();
            TaiDanhSachDatPhong();
            TaiDanhSachHoaDon();
        }

        #region Styling Figma

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

        private void DrawFigmaInputBox(object sender, PaintEventArgs e)
        {
            if (sender is Panel pnl)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 6;
                Rectangle rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                using (GraphicsPath path = GetRoundedRectPath(rect, radius))
                using (Pen pen = new Pen(ColorTranslator.FromHtml("#CBD5E1"), 1f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private void DrawOrangeAccentBox(object sender, PaintEventArgs e)
        {
            if (sender is Panel pnl)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 8;
                Rectangle rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                using (GraphicsPath path = GetRoundedRectPath(rect, radius))
                using (Pen pen = new Pen(ColorTranslator.FromHtml("#FDBA74"), 1.2f))
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

        private void ThietKeBangChuanFigma()
        {
            dgvHoaDon.EnableHeadersVisualStyles = false;
            dgvHoaDon.BorderStyle = BorderStyle.None;
            dgvHoaDon.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvHoaDon.GridColor = ColorTranslator.FromHtml("#F1F5F9");
            dgvHoaDon.BackgroundColor = Color.White;
            dgvHoaDon.RowHeadersVisible = false;
            dgvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHoaDon.MultiSelect = false;
            dgvHoaDon.AllowUserToAddRows = false;
            dgvHoaDon.AllowUserToResizeRows = false;
            dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvHoaDon.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F8FAFC");
            dgvHoaDon.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#475569");
            dgvHoaDon.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.25f, FontStyle.Bold);
            dgvHoaDon.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 10, 12, 10);
            dgvHoaDon.ColumnHeadersHeight = 44;
            dgvHoaDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvHoaDon.RowTemplate.Height = 42;
            dgvHoaDon.DefaultCellStyle.BackColor = Color.White;
            dgvHoaDon.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#1E293B");
            dgvHoaDon.DefaultCellStyle.Font = new Font("Segoe UI", 9.25f);
            dgvHoaDon.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#EFF6FF");
            dgvHoaDon.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#1D4ED8");
            dgvHoaDon.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
        }

        #endregion

        #region Data & Calc

        private void TaiDanhSachPhuongThucTT()
        {
            cboPTTT.Items.Clear();
            cboPTTT.Items.Add(new { Text = "Tiền mặt", Value = "TM" });
            cboPTTT.Items.Add(new { Text = "Chuyển khoản QR", Value = "CK" });
            cboPTTT.Items.Add(new { Text = "Thẻ POS / Tín dụng", Value = "POS" });
            cboPTTT.DisplayMember = "Text";
            cboPTTT.ValueMember = "Value";
            if (cboPTTT.Items.Count > 0) cboPTTT.SelectedIndex = 0;
        }

        private void TaiDanhSachDatPhong()
        {
            cboDatPhong.Items.Clear();
            cboDatPhong.Items.Add("DP01 - Phòng 206 (Đỗ Thu Trang)");
            cboDatPhong.Items.Add("DP02 - Phòng 301 (Nguyễn Văn Hùng)");
            if (cboDatPhong.Items.Count > 0) cboDatPhong.SelectedIndex = 0;
        }

        private void TaiDanhSachHoaDon()
        {
            try
            {
                danhSachGoc = hoaDonDAL.GetAll();
                HienThiLenBang(danhSachGoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiLenBang(List<HoaDon> danhSach)
        {
            dgvHoaDon.DataSource = null;
            dgvHoaDon.DataSource = danhSach;

            if (dgvHoaDon.Columns["MaHoaDon"] != null)
            {
                dgvHoaDon.Columns["MaHoaDon"].HeaderText = "MÃ HÓA ĐƠN";
                dgvHoaDon.Columns["MaHoaDon"].FillWeight = 90;
                dgvHoaDon.Columns["MaHoaDon"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                dgvHoaDon.Columns["MaHoaDon"].DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#0F2537");
            }
            if (dgvHoaDon.Columns["MaDatPhong"] != null)
            {
                dgvHoaDon.Columns["MaDatPhong"].HeaderText = "ĐẶT PHÒNG";
                dgvHoaDon.Columns["MaDatPhong"].FillWeight = 85;
                dgvHoaDon.Columns["MaDatPhong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvHoaDon.Columns["NgayThanhToan"] != null)
            {
                dgvHoaDon.Columns["NgayThanhToan"].HeaderText = "NGÀY LẬP";
                dgvHoaDon.Columns["NgayThanhToan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvHoaDon.Columns["NgayThanhToan"].FillWeight = 110;
                dgvHoaDon.Columns["NgayThanhToan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvHoaDon.Columns["TongTien"] != null)
            {
                dgvHoaDon.Columns["TongTien"].HeaderText = "TỔNG THU";
                dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                dgvHoaDon.Columns["TongTien"].DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#EA580C");
                dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvHoaDon.Columns["TongTien"].FillWeight = 100;
            }
            if (dgvHoaDon.Columns["TrangThai"] != null)
            {
                dgvHoaDon.Columns["TrangThai"].HeaderText = "TRẠNG THÁI";
                dgvHoaDon.Columns["TrangThai"].FillWeight = 95;
                dgvHoaDon.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
                var loc = danhSachGoc.Where(h =>
                    (h.MaHoaDon != null && h.MaHoaDon.ToLower().Contains(k)) ||
                    (h.MaDatPhong != null && h.MaDatPhong.ToLower().Contains(k))).ToList();
                HienThiLenBang(loc);
            }
        }

        private void dgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow == null) return;
            var h = dgvHoaDon.CurrentRow.DataBoundItem as HoaDon;
            if (h == null) return;

            maHDDangChon = h.MaHoaDon;
            txtMaHD.Text = h.MaHoaDon;
            txtTienPhong.Text = h.TienPhong.ToString("N0");
            txtTienDV.Text = h.TienDichVu.ToString("N0");
            txtTienGiamGia.Text = h.TienGiamGia.ToString("N0");
            txtTienDatCoc.Text = h.TienDatCoc.ToString("N0");
            lblTongTienValue.Text = h.TongTien.ToString("N0") + " đ";
        }

        private void TinhTongTienTuDong(object sender, EventArgs e)
        {
            decimal tp = ParseMoney(txtTienPhong.Text);
            decimal dv = ParseMoney(txtTienDV.Text);
            decimal gg = ParseMoney(txtTienGiamGia.Text);
            decimal coc = ParseMoney(txtTienDatCoc.Text);

            decimal canThu = (tp + dv) - gg - coc;
            if (canThu < 0) canThu = 0;

            lblTongTienValue.Text = canThu.ToString("N0") + " đ";
        }

        private decimal ParseMoney(string txt)
        {
            if (decimal.TryParse(txt.Replace(",", "").Replace(".", "").Trim(), out decimal val))
                return val;
            return 0;
        }

        private void cboDatPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDatPhong.SelectedIndex == 0)
            {
                txtTienPhong.Text = "2,900,000";
                txtTienDV.Text = "210,000";
                txtTienGiamGia.Text = "0";
                txtTienDatCoc.Text = "1,000,000";
            }
            else
            {
                txtTienPhong.Text = "1,450,000";
                txtTienDV.Text = "120,000";
                txtTienGiamGia.Text = "100,000";
                txtTienDatCoc.Text = "500,000";
            }
            TinhTongTienTuDong(null, null);
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaHD.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn thanh toán.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal canThu = ParseMoney(lblTongTienValue.Text.Replace(" đ", ""));
            var hd = new HoaDon
            {
                MaHoaDon = txtMaHD.Text.Trim().ToUpper(),
                MaDatPhong = cboDatPhong.Text.Split(' ')[0],
                TienPhong = ParseMoney(txtTienPhong.Text),
                TienDichVu = ParseMoney(txtTienDV.Text),
                TienGiamGia = ParseMoney(txtTienGiamGia.Text),
                TienDatCoc = ParseMoney(txtTienDatCoc.Text),
                TongTien = canThu,
                NgayThanhToan = DateTime.Now,
                TrangThai = "Đã thanh toán"
            };

            if (hoaDonDAL.Insert(hd))
            {
                MessageBox.Show($"Thanh toán hóa đơn {hd.MaHoaDon} thành công! Số tiền đã thu: {canThu:N0} đ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachHoaDon();
                LamMoiForm();
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHD.Text))
            {
                MessageBox.Show("Vui lòng chọn hoặc nhập hóa đơn trước khi in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show($"Đang xuất lệnh in hóa đơn {txtMaHD.Text} ra máy in bill nhiệt...", "In hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLamMoi_Click(object sender, EventArgs e) => LamMoiForm();

        private void LamMoiForm()
        {
            maHDDangChon = null;
            txtMaHD.Text = "HD" + DateTime.Now.ToString("yyMMddHHmm");
            txtTienPhong.Text = "0";
            txtTienDV.Text = "0";
            txtTienGiamGia.Text = "0";
            txtTienDatCoc.Text = "0";
            lblTongTienValue.Text = "0 đ";
            txtTimKiem.Clear();
            dgvHoaDon.ClearSelection();
        }

        #endregion
    }
}