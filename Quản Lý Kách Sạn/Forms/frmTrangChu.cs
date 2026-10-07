using System;
using System.Drawing;
using System.Windows.Forms;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmTrangChu : Form
    {
        private Form formHienTai = null;
        private Button nutDangChon = null;

        public frmTrangChu()
        {
            InitializeComponent();
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            // Mặc định khi mở chương trình sẽ nạp màn hình Quản lý Dịch vụ
            MoFormCon(new frmDichVu(), btnDichVu, "QUẢN LÝ DỊCH VỤ PHÒNG");
        }

        // Hàm tiện ích: Nhúng Form con trực tiếp vào panel giữa pnlContent
        private void MoFormCon(Form formCon, Button nutMenu, string tieuDe)
        {
            if (formHienTai != null)
            {
                formHienTai.Close();
                formHienTai.Dispose();
            }

            // Cập nhật trạng thái đổi màu nút đang chọn
            DoiTrangThaiNutMenu(nutMenu);

            formHienTai = formCon;
            lblTitleSection.Text = tieuDe;

            formCon.TopLevel = false;
            formCon.FormBorderStyle = FormBorderStyle.None;
            formCon.Dock = DockStyle.Fill;
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(formCon);
            pnlContent.Tag = formCon;
            formCon.BringToFront();
            formCon.Show();
        }

        private void DoiTrangThaiNutMenu(Button btn)
        {
            if (btn == null) return;

            // Trả màu các nút cũ về mặc định
            if (nutDangChon != null)
            {
                nutDangChon.BackColor = Color.FromArgb(15, 37, 55);
                nutDangChon.ForeColor = Color.FromArgb(226, 232, 240);
            }

            // Đổi màu nút vừa nhấn sang màu cam chủ đạo
            nutDangChon = btn;
            nutDangChon.BackColor = Color.FromArgb(232, 119, 26);
            nutDangChon.ForeColor = Color.White;
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {
            // Kẻ viền xám dưới header
            ControlPaint.DrawBorder(e.Graphics, pnlHeader.ClientRectangle,
                Color.Transparent, 0, ButtonBorderStyle.None,
                Color.Transparent, 0, ButtonBorderStyle.None,
                Color.Transparent, 0, ButtonBorderStyle.None,
                Color.FromArgb(226, 232, 240), 1, ButtonBorderStyle.Solid);
        }

        // ================= CÁC SỰ KIỆN CLICK MENU =================

        private void btnPhong_Click(object sender, EventArgs e)
        {
            try { MoFormCon(new frmDanhSachPhong(), btnPhong, "DANH SÁCH & TRẠNG THÁI PHÒNG"); }
            catch { MessageBox.Show("Form Danh sách phòng đang được đồng bộ.", "Thông báo"); }
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            try { MoFormCon(new frmDatPhong(), btnDatPhong, "ĐẶT PHÒNG & CHECK-IN / CHECK-OUT"); }
            catch { MessageBox.Show("Form Đặt phòng đang được đồng bộ.", "Thông báo"); }
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            try { MoFormCon(new frmKhachHang(), btnKhachHang, "HỒ SƠ KHÁCH HÀNG"); }
            catch { MessageBox.Show("Form Khách hàng đang được đồng bộ.", "Thông báo"); }
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmDichVu(), btnDichVu, "DANH MỤC DỊCH VỤ PHÒNG");
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmHoaDon(), btnHoaDon, "HÓA ĐƠN & THANH TOÁN TRẢ PHÒNG");
        }

        private void btnVoucher_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmVoucher(), btnVoucher, "QUẢN LÝ MÃ GIẢM GIÁ (VOUCHER)");
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmBaoCao(), btnBaoCao, "BÁO CÁO DOANH THU & THỐNG KÊ");
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất khỏi hệ thống?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}