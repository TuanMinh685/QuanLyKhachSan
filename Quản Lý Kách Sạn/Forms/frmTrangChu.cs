using System;
using System.Drawing;
using System.Windows.Forms;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmTrangChu : Form
    {
        private Control manHinhHienTai;
        private ucDatPhong manHinhDatPhong;
        private frmKhachHang manHinhKhachHang;
        private Button nutDangChon = null;

        public frmTrangChu()
        {
            InitializeComponent();
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            MoDatPhong();
        }

        private void MoDatPhong()
        {
            if (manHinhDatPhong == null || manHinhDatPhong.IsDisposed)
                manHinhDatPhong = new ucDatPhong();

            HienThiManHinh(manHinhDatPhong, btnDatPhong, "ĐẶT PHÒNG & LƯU TRÚ");
        }

        private void MoKhachHang()
        {
            if (manHinhKhachHang == null || manHinhKhachHang.IsDisposed)
                manHinhKhachHang = new frmKhachHang();

            HienThiManHinh(manHinhKhachHang, btnKhachHang, "HỒ SƠ KHÁCH HÀNG");
        }

        // Các form khác vẫn gọi qua hàm cũ của trang chủ.
        private void MoFormCon(Form formCon, Button nutMenu, string tieuDe)
        {
            HienThiManHinh(formCon, nutMenu, tieuDe);
        }

        private void HienThiManHinh(Control manHinh, Button nutMenu, string tieuDe)
        {
            if (ReferenceEquals(manHinhHienTai, manHinh))
                return;

            Control manHinhCu = manHinhHienTai;
            try
            {
                if (manHinh.Parent != pnlContent)
                {
                    if (manHinh is Form formCon)
                    {
                        formCon.TopLevel = false;
                        formCon.FormBorderStyle = FormBorderStyle.None;
                        formCon.ShowInTaskbar = false;
                        formCon.WindowState = FormWindowState.Normal;
                    }

                    manHinh.Dock = DockStyle.Fill;
                    manHinh.Visible = false;
                    pnlContent.Controls.Add(manHinh);
                }

                manHinh.Show();
                manHinh.BringToFront();

                if (manHinhCu != null && !manHinhCu.IsDisposed)
                {
                    // Giữ dữ liệu chưa lưu của hai chức năng được phân công.
                    if (ReferenceEquals(manHinhCu, manHinhDatPhong)
                        || ReferenceEquals(manHinhCu, manHinhKhachHang))
                    {
                        manHinhCu.Hide();
                    }
                    else
                    {
                        if (manHinhCu is Form formCu) formCu.Close();
                        manHinhCu.Dispose();
                    }
                }

                manHinhHienTai = manHinh;
                pnlContent.Tag = manHinh;
                lblTitleSection.Text = tieuDe;
                DoiTrangThaiNutMenu(nutMenu);
                manHinh.Select();
            }
            catch (Exception ex)
            {
                pnlContent.Controls.Remove(manHinh);
                manHinh.Dispose();

                if (manHinhCu != null && !manHinhCu.IsDisposed)
                {
                    manHinhCu.Show();
                    manHinhCu.BringToFront();
                }

                MessageBox.Show(this,
                    "Không mở được màn hình: " + tieuDe
                    + Environment.NewLine + ex.Message,
                    "Lỗi điều hướng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DoiTrangThaiNutMenu(Button btn)
        {
            if (btn == null) return;

            if (nutDangChon != null)
            {
                nutDangChon.BackColor = Color.FromArgb(15, 37, 55);
                nutDangChon.ForeColor = Color.FromArgb(226, 232, 240);
            }

            nutDangChon = btn;
            nutDangChon.BackColor = Color.FromArgb(232, 119, 26);
            nutDangChon.ForeColor = Color.White;
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, pnlHeader.ClientRectangle,
                Color.Transparent, 0, ButtonBorderStyle.None,
                Color.Transparent, 0, ButtonBorderStyle.None,
                Color.Transparent, 0, ButtonBorderStyle.None,
                Color.FromArgb(226, 232, 240), 1, ButtonBorderStyle.Solid);
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            try { MoFormCon(new frmDanhSachPhong(), btnPhong, "DANH SÁCH & TRẠNG THÁI PHÒNG"); }
            catch { MessageBox.Show("Form Danh sách phòng đang được đồng bộ.", "Thông báo"); }
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            MoDatPhong();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            MoKhachHang();
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
            // Mở trực tiếp frmBaoCao sẵn có
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