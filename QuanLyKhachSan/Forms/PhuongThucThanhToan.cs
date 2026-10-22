using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class PhuongThucThanhToan : Form
    {
        private PhuongThucThanhToanDAL dal = new PhuongThucThanhToanDAL();
        private List<PhuongThucThanhToanModel> danhSachGoc = new List<PhuongThucThanhToanModel>();
        private int? maDangChon = null;

        // Controls giao diện nâng cấp hiện đại
        private Panel pnlHeaderLeft;
        private Label lblTitleLeft;
        private Label lblSubtitleLeft;
        private FlowLayoutPanel pnlStatsPills;
        private Label lblPillTotal;
        private Label lblPillActive;
        private Label lblPillInactive;

        public PhuongThucThanhToan()
        {
            InitializeComponent();
            UITheme.ApplyTheme(this);
            KhoiTaoGiaoDienNangCao();
        }

        private void KhoiTaoGiaoDienNangCao()
        {
            // 1. Tạo Header và Thẻ thống kê (Stats Pills) ở bên trái
            pnlHeaderLeft = new Panel
            {
                Dock = DockStyle.Top,
                Height = 82,
                BackColor = Color.Transparent
            };

            lblTitleLeft = new Label
            {
                Text = "💳 PHƯƠNG THỨC THANH TOÁN",
                Font = new Font("Segoe UI", 12.5f, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(0, 0),
                AutoSize = true
            };

            lblSubtitleLeft = new Label
            {
                Text = "Cấu hình cổng thu ngân: Tiền mặt, Chuyển khoản VietQR, Thẻ POS ngân hàng...",
                Font = new Font("Segoe UI", 9f, FontStyle.Regular),
                ForeColor = UITheme.TextMuted,
                Location = new Point(0, 26),
                AutoSize = true
            };

            pnlStatsPills = new FlowLayoutPanel
            {
                Location = new Point(0, 52),
                Height = 30,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent
            };

            lblPillTotal = TaoTheThongKe("Tổng: 0 kênh", Color.FromArgb(241, 245, 249), UITheme.DarkNavy);
            lblPillActive = TaoTheThongKe("🟢 Hoạt động: 0", Color.FromArgb(220, 252, 231), Color.FromArgb(22, 163, 74));
            lblPillInactive = TaoTheThongKe("🔴 Tạm khóa: 0", Color.FromArgb(254, 226, 226), Color.FromArgb(220, 38, 38));

            pnlStatsPills.Controls.Add(lblPillTotal);
            pnlStatsPills.Controls.Add(lblPillActive);
            pnlStatsPills.Controls.Add(lblPillInactive);

            pnlHeaderLeft.Controls.Add(lblTitleLeft);
            pnlHeaderLeft.Controls.Add(lblSubtitleLeft);
            pnlHeaderLeft.Controls.Add(pnlStatsPills);

            cardLeft.Controls.Add(pnlHeaderLeft);

            // Cân đối vị trí tìm kiếm và bảng
            pnlSearch.Location = new Point(20, 108);
            pnlSearch.Height = 40;

            pnlGridWrapper.Location = new Point(20, 160);
            pnlGridWrapper.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            cardLeft.Resize += (s, e) =>
            {
                pnlGridWrapper.Size = new Size(cardLeft.ClientSize.Width - 40, cardLeft.ClientSize.Height - 180);
            };

            // 2. Nâng cấp nút bấm bên phải với icon và màu chuẩn SaaS
            btnLuu.Text = "💾 Lưu phương thức";
            btnLamMoi.Text = "🔄 Làm mới";
            btnXoa.Text = "🗑 Xóa phương thức";

            btnLuu.BackColor = UITheme.PrimaryOrange;
            btnLuu.ForeColor = Color.White;

            btnLamMoi.BackColor = Color.FromArgb(248, 250, 252);
            btnLamMoi.ForeColor = UITheme.TextMain;

            btnXoa.BackColor = Color.FromArgb(254, 242, 242);
            btnXoa.ForeColor = Color.FromArgb(220, 38, 38);

            // 3. Sự kiện định dạng trạng thái bảng
            dgvPTTT.CellFormatting += DgvPTTT_CellFormatting;
        }

        private Label TaoTheThongKe(string text, Color bg, Color fg)
        {
            var lbl = new Label
            {
                Text = text,
                BackColor = bg,
                ForeColor = fg,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(10, 4, 10, 4),
                Margin = new Padding(0, 0, 8, 0),
                Cursor = Cursors.Default
            };
            lbl.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(0, 0, lbl.Width - 1, lbl.Height - 1);
                using (GraphicsPath path = UITheme.GetRoundedRectPath(rect, 6))
                using (Pen pen = new Pen(Color.FromArgb(226, 232, 240), 1f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            };
            return lbl;
        }

        private void DgvPTTT_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPTTT.Columns[e.ColumnIndex].Name == "KichHoat" && e.Value != null)
            {
                bool active = Convert.ToBoolean(e.Value);
                if (active)
                {
                    e.Value = "● Đang hoạt động";
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                    e.CellStyle.Font = new Font("Segoe UI", 9.25f, FontStyle.Bold);
                }
                else
                {
                    e.Value = "● Tạm ngưng";
                    e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                    e.CellStyle.Font = new Font("Segoe UI", 9.25f, FontStyle.Regular);
                }
                e.FormattingApplied = true;
            }
        }

        private void PhuongThucThanhToan_Load(object sender, EventArgs e)
        {
            UITheme.StyleModernGrid(dgvPTTT);
            TaiDanhSach();
            LamMoi();
        }

        private void DrawFigmaCard(object sender, PaintEventArgs e) => UITheme.DrawCard(sender, e);
        private void DrawFigmaInputBox(object sender, PaintEventArgs e) => UITheme.DrawInputBox(sender, e);

        private void DrawPillBorder(object sender, PaintEventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Region = new Region(UITheme.GetRoundedRectPath(new Rectangle(0, 0, btn.Width, btn.Height), 6));
            }
        }

        private void TaiDanhSach()
        {
            try
            {
                danhSachGoc = dal.GetAll();
                HienThiLenBang(danhSachGoc);
                CapNhatThongKe();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải phương thức thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatThongKe()
        {
            int total = danhSachGoc.Count;
            int active = danhSachGoc.Count(x => x.KichHoat);
            int inactive = total - active;

            lblPillTotal.Text = $"Tổng số: {total} kênh";
            lblPillActive.Text = $"🟢 Hoạt động: {active}";
            lblPillInactive.Text = $"🔴 Tạm khóa: {inactive}";
        }

        private void HienThiLenBang(List<PhuongThucThanhToanModel> ds)
        {
            dgvPTTT.DataSource = null;
            dgvPTTT.DataSource = ds;

            if (dgvPTTT.Columns["MaPhuongThuc"] != null)
            {
                dgvPTTT.Columns["MaPhuongThuc"].HeaderText = "MÃ";
                dgvPTTT.Columns["MaPhuongThuc"].FillWeight = 40;
                dgvPTTT.Columns["MaPhuongThuc"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                dgvPTTT.Columns["MaPhuongThuc"].DefaultCellStyle.ForeColor = UITheme.DarkNavy;
                dgvPTTT.Columns["MaPhuongThuc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvPTTT.Columns["TenPhuongThuc"] != null)
            {
                dgvPTTT.Columns["TenPhuongThuc"].HeaderText = "TÊN PHƯƠNG THỨC";
                dgvPTTT.Columns["TenPhuongThuc"].FillWeight = 140;
                dgvPTTT.Columns["TenPhuongThuc"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            }
            if (dgvPTTT.Columns["MoTa"] != null)
            {
                dgvPTTT.Columns["MoTa"].HeaderText = "GHI CHÚ / MÔ TẢ";
                dgvPTTT.Columns["MoTa"].FillWeight = 180;
            }
            if (dgvPTTT.Columns["KichHoat"] is DataGridViewCheckBoxColumn)
            {
                int idx = dgvPTTT.Columns["KichHoat"].Index;
                dgvPTTT.Columns.Remove("KichHoat");
                var col = new DataGridViewTextBoxColumn
                {
                    Name = "KichHoat",
                    DataPropertyName = "KichHoat",
                    HeaderText = "TRẠNG THÁI",
                    FillWeight = 75
                };
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvPTTT.Columns.Insert(idx, col);
            }
            else if (dgvPTTT.Columns["KichHoat"] != null)
            {
                dgvPTTT.Columns["KichHoat"].HeaderText = "TRẠNG THÁI";
                dgvPTTT.Columns["KichHoat"].FillWeight = 75;
                dgvPTTT.Columns["KichHoat"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
                var loc = danhSachGoc.Where(x =>
                    (x.TenPhuongThuc != null && x.TenPhuongThuc.ToLower().Contains(k)) ||
                    (x.MoTa != null && x.MoTa.ToLower().Contains(k))).ToList();
                HienThiLenBang(loc);
            }
        }

        private void dgvPTTT_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPTTT.CurrentRow == null) return;
            var item = dgvPTTT.CurrentRow.DataBoundItem as PhuongThucThanhToanModel;
            if (item == null) return;

            maDangChon = item.MaPhuongThuc;
            txtMaPTTT.Text = item.MaPhuongThuc.ToString();
            txtTenPTTT.Text = item.TenPhuongThuc;
            txtMoTa.Text = item.MoTa;
            chkKichHoat.Checked = item.KichHoat;

            lblRightTitle.Text = "✏️ Cập nhật: " + item.TenPhuongThuc;
            lblRightSubtitle.Text = "Đang chỉnh sửa kênh thanh toán đã chọn";
            btnLuu.Text = "💾 Cập nhật";
            btnXoa.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string ten = txtTenPTTT.Text.Trim();
            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập tên phương thức thanh toán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenPTTT.Focus();
                return;
            }

            if (maDangChon.HasValue)
            {
                if (dal.Update(maDangChon.Value, ten, txtMoTa.Text.Trim(), chkKichHoat.Checked))
                {
                    MessageBox.Show($"Cập nhật phương thức [{ten}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSach();
                    LamMoi();
                }
            }
            else
            {
                if (dal.Insert(ten, txtMoTa.Text.Trim(), chkKichHoat.Checked))
                {
                    MessageBox.Show($"Thêm mới phương thức [{ten}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSach();
                    LamMoi();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!maDangChon.HasValue)
            {
                MessageBox.Show("Vui lòng chọn phương thức cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa phương thức [{txtTenPTTT.Text}]?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dal.Delete(maDangChon.Value))
                {
                    MessageBox.Show("Đã xóa phương thức thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSach();
                    LamMoi();
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e) => LamMoi();

        private void LamMoi()
        {
            maDangChon = null;
            txtMaPTTT.Text = "(Tự động tăng)";
            txtTenPTTT.Clear();
            txtMoTa.Clear();
            chkKichHoat.Checked = true;

            lblRightTitle.Text = "✨ Thêm mới phương thức";
            lblRightSubtitle.Text = "Cấu hình kênh thanh toán tiền mặt, chuyển khoản, POS...";
            btnLuu.Text = "💾 Lưu phương thức";
            btnXoa.Enabled = false;

            txtTimKiem.Clear();
            dgvPTTT.ClearSelection();
        }
    }
}
