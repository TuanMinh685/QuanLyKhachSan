using Quản_Lý_Khách_Sạn.BLL;
using Quản_Lý_Khách_Sạn.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Quản_Lý_Khách_Sạn.Forms
{
    /// <summary>
    /// Hộp thoại chọn một đơn đặt phòng, dùng chung cho 3 thao tác:
    /// Nhận phòng / Trả phòng / Hủy phòng (khác nhau ở danh sách đơn được hiện và nút xác nhận).
    /// Form chỉ CHỌN đơn rồi trả kết quả qua các property; việc ghi CSDL do form gọi (ucDatPhong) thực hiện.
    /// </summary>
    public partial class frmChonDatPhong : Form
    {
        private static readonly CultureInfo Vi = new CultureInfo("vi-VN");
        private readonly ThaoTacDatPhong _thaoTac;

        // Kết quả trả về cho form gọi (bên ngoài chỉ đọc)
        public int MaDatPhong { get; private set; }
        public string MaDat { get; private set; }
        public string TenKhach { get; private set; }
        public string SoPhong { get; private set; }
        public string LyDo { get; private set; }     // chỉ có ý nghĩa khi Hủy phòng
        public decimal TienCoc { get; private set; }

        // Constructor không tham số: dành cho Visual Studio Designer (Designer cần nó để dựng form)
        public frmChonDatPhong() : this(ThaoTacDatPhong.NhanPhong)
        {
        }

        public frmChonDatPhong(ThaoTacDatPhong thaoTac)
        {
            InitializeComponent();
            _thaoTac = thaoTac;
            ThietKeChonDatPhong();

            AcceptButton = btnTim;      // Enter = Tìm
            CancelButton = btnDong;     // Esc   = Đóng

            // Gắn sự kiện bằng code (gỡ rồi gắn lại để không bị gắn 2 lần)
            Load -= frmChonDatPhong_Load; Load += frmChonDatPhong_Load;
            btnTim.Click -= btnTim_Click; btnTim.Click += btnTim_Click;
            btnXacNhan.Click -= btnXacNhan_Click; btnXacNhan.Click += btnXacNhan_Click;
            btnDong.Click -= btnDong_Click; btnDong.Click += btnDong_Click;
            dgvDatPhong.CellDoubleClick -= dgvDatPhong_CellDoubleClick;
            dgvDatPhong.CellDoubleClick += dgvDatPhong_CellDoubleClick;
        }

        private void frmChonDatPhong_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            CauHinhGiaoDien();
            CauHinhLuoi();
            TaiDanhSach();
        }

        // Chữ trên form thay đổi theo thao tác được chọn
        private void CauHinhGiaoDien()
        {
            switch (_thaoTac)
            {
                case ThaoTacDatPhong.NhanPhong:
                    Text = "Nhận phòng";
                    lblTieuDe.Text = "Chọn đơn đặt phòng để nhận phòng";
                    btnXacNhan.Text = "Nhận phòng";
                    break;

                case ThaoTacDatPhong.TraPhong:
                    Text = "Trả phòng";
                    lblTieuDe.Text = "Chọn khách đang ở để trả phòng";
                    btnXacNhan.Text = "Trả phòng";
                    break;

                case ThaoTacDatPhong.HuyPhong:
                    Text = "Hủy phòng";
                    lblTieuDe.Text = "Chọn đơn đặt phòng cần hủy";
                    btnXacNhan.Text = "Hủy đơn";
                    btnXacNhan.BackColor = UITheme.DangerRed;
                    btnXacNhan.ForeColor = Color.White;
                    break;
            }

            // Ô lý do chỉ có trong bố cục hủy; các thao tác khác dùng toàn vùng bảng.
            bool huy = _thaoTac == ThaoTacDatPhong.HuyPhong;
            lblLyDo.Visible = huy;
            txtLyDo.Visible = huy;
        }

        private void CauHinhLuoi()
        {
            if (dgvDatPhong.Columns.Count > 0) return;      // đã cấu hình rồi

            dgvDatPhong.AutoGenerateColumns = false;        // chỉ hiện cột mình khai báo
            LeTanUi.Grid(dgvDatPhong);

            ThemCot("MaDat", "Mã đặt", 115, false, null);
            ThemCot("HoTen", "Khách", 0, true, null);
            ThemCot("SoPhong", "Phòng", 65, false, null);
            ThemCot("TenLoai", "Loại", 85, false, null);
            ThemCot("NgayNhan", "Nhận", 90, false, "dd/MM/yyyy");
            ThemCot("NgayTra", "Trả", 90, false, "dd/MM/yyyy");
            ThemCot("TongTien", "Tổng tiền (đ)", 110, false, "#,##0");
            ThemCot("TienCoc", "Đã cọc (đ)", 110, false, "#,##0");
            foreach (DataGridViewColumn column in dgvDatPhong.Columns)
            {
                column.MinimumWidth = column.DataPropertyName == "HoTen" ? 170 :
                    column.DataPropertyName == "SoPhong" ? 80 : 118;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void ThemCot(string prop, string tieuDe, int rong, bool fill, string dinhDang)
        {
            var c = new DataGridViewTextBoxColumn
            {
                DataPropertyName = prop,        // cột này lấy dữ liệu từ cột nào của DataTable
                HeaderText = tieuDe,
                Name = "col" + prop,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            if (fill) c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            else c.Width = rong;

            if (dinhDang != null)
            {
                c.DefaultCellStyle.Format = dinhDang;
                c.DefaultCellStyle.FormatProvider = Vi;
                if (dinhDang == "#,##0") c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            dgvDatPhong.Columns.Add(c);
        }

        private void TaiDanhSach()
        {
            try
            {
                DataTable dt = LeTanLuuTruBLL.TimDatPhong(_thaoTac, txtTim.Text);
                dgvDatPhong.DataSource = dt;
                lblKetQua.Text = dt.Rows.Count == 0 ? "Không có đơn nào" : dt.Rows.Count + " đơn";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được danh sách đơn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            TaiDanhSach();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            XacNhan();
        }

        private void dgvDatPhong_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) XacNhan();     // RowIndex = -1 là bấm đôi vào tiêu đề cột
        }

        private void XacNhan()
        {
            DataRowView r = dgvDatPhong.CurrentRow == null
                ? null
                : dgvDatPhong.CurrentRow.DataBoundItem as DataRowView;
            if (r == null)
            {
                MessageBox.Show("Vui lòng chọn một đơn trong danh sách.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string lyDo = txtLyDo.Text.Trim();
            if (_thaoTac == ThaoTacDatPhong.HuyPhong && lyDo.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập lý do hủy phòng.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLyDo.Focus();
                return;
            }

            MaDatPhong = Convert.ToInt32(r["MaDatPhong"]);
            MaDat = Convert.ToString(r["MaDat"]);
            TenKhach = Convert.ToString(r["HoTen"]);
            SoPhong = Convert.ToString(r["SoPhong"]);
            LyDo = lyDo;
            TienCoc = Convert.ToDecimal(r["TienCoc"]);

            DialogResult = DialogResult.OK;     // gán DialogResult sẽ tự đóng form
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
