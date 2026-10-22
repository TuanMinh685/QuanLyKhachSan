using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Quản_Lý_Khách_Sạn.DAL;
using Quản_Lý_Khách_Sạn.Models;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmDichVu : Form
    {
        private string maDVDangChon = null;
        private DichVuDAL dichVuDAL = new DichVuDAL();
        private List<DichVu> danhSachGoc = new List<DichVu>();

        // Controls giao diện nâng cấp hiện đại
        private Panel pnlHeaderLeft;
        private Label lblTitleLeft;
        private Label lblSubtitleLeft;
        private FlowLayoutPanel pnlStatsPills;
        private Label lblPillTotal;
        private Label lblPillActive;
        private Label lblPillInactive;

        public frmDichVu()
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
                Text = "📦 DANH MỤC DỊCH VỤ & BẢNG GIÁ",
                Font = new Font("Segoe UI", 12.5f, FontStyle.Bold),
                ForeColor = UITheme.DarkNavy,
                Location = new Point(0, 0),
                AutoSize = true
            };

            lblSubtitleLeft = new Label
            {
                Text = "Quản lý bảng giá, nhóm dịch vụ ăn uống, giặt ủi và tiện ích khách sạn",
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

            lblPillTotal = TaoTheThongKe("Tổng: 0", Color.FromArgb(241, 245, 249), UITheme.DarkNavy);
            lblPillActive = TaoTheThongKe("🟢 Đang phục vụ: 0", Color.FromArgb(220, 252, 231), Color.FromArgb(22, 163, 74));
            lblPillInactive = TaoTheThongKe("🔴 Tạm ngưng: 0", Color.FromArgb(254, 226, 226), Color.FromArgb(220, 38, 38));

            pnlStatsPills.Controls.Add(lblPillTotal);
            pnlStatsPills.Controls.Add(lblPillActive);
            pnlStatsPills.Controls.Add(lblPillInactive);

            pnlHeaderLeft.Controls.Add(lblTitleLeft);
            pnlHeaderLeft.Controls.Add(lblSubtitleLeft);
            pnlHeaderLeft.Controls.Add(pnlStatsPills);

            cardLeft.Controls.Add(pnlHeaderLeft);

            // Đặt lại vị trí search bar và grid để cân đối với Header
            pnlSearch.Location = new Point(20, 108);
            pnlSearch.Height = 40;

            pnlGridWrapper.Location = new Point(20, 160);
            pnlGridWrapper.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            cardLeft.Resize += (s, e) =>
            {
                pnlGridWrapper.Size = new Size(cardLeft.ClientSize.Width - 40, cardLeft.ClientSize.Height - 180);
            };

            // 2. Nâng cấp nút bấm bên phải với icon và màu chuẩn SaaS
            btnLuu.Text = "💾 Lưu dịch vụ";
            btnLamMoi.Text = "🔄 Làm mới";
            btnXoa.Text = "🗑 Xóa dịch vụ";

            btnLuu.BackColor = UITheme.PrimaryOrange;
            btnLuu.ForeColor = Color.White;

            btnLamMoi.BackColor = Color.FromArgb(248, 250, 252);
            btnLamMoi.ForeColor = UITheme.TextMain;

            btnXoa.BackColor = Color.FromArgb(254, 242, 242);
            btnXoa.ForeColor = Color.FromArgb(220, 38, 38);

            // 3. Sự kiện định dạng đơn giá trực tiếp khi người dùng gõ
            txtDonGia.TextChanged += TxtDonGia_TextChanged;

            // 4. Sự kiện định dạng cell trong DataGridView
            dgvDichVu.CellFormatting += DgvDichVu_CellFormatting;
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

        private void TxtDonGia_TextChanged(object sender, EventArgs e)
        {
            if (txtDonGia.Focused)
            {
                string raw = Regex.Replace(txtDonGia.Text, @"[^\d]", "");
                if (decimal.TryParse(raw, out decimal val))
                {
                    int pos = txtDonGia.SelectionStart;
                    txtDonGia.Text = val.ToString("N0");
                    txtDonGia.SelectionStart = txtDonGia.Text.Length;
                }
            }
        }

        private void DgvDichVu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var col = dgvDichVu.Columns[e.ColumnIndex];

            if (col.Name == "TrangThai" && !(col is DataGridViewCheckBoxColumn) && e.Value != null)
            {
                bool active = Convert.ToBoolean(e.Value);
                e.Value = active ? "● Đang phục vụ" : "● Tạm ngưng";
                e.CellStyle.ForeColor = active ? Color.FromArgb(22, 163, 74) : Color.FromArgb(220, 38, 38);
                e.CellStyle.Font = new Font("Segoe UI", 9.25f, active ? FontStyle.Bold : FontStyle.Regular);
                e.FormattingApplied = true;
            }
            else if (col.Name == "DonGia" && e.Value != null)
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal gia))
                {
                    e.Value = gia.ToString("N0") + " đ";
                    e.CellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    e.CellStyle.ForeColor = UITheme.PrimaryOrange;
                    e.FormattingApplied = true;
                }
            }
        }

        private void frmDichVu_Load(object sender, EventArgs e)
        {
            UITheme.StyleModernGrid(dgvDichVu);
            if (cboLoaiDV.Items.Count > 0) cboLoaiDV.SelectedIndex = 0;
            if (cboDonViTinh.Items.Count > 0) cboDonViTinh.SelectedIndex = 0;
            TaiDanhSach();
            LamMoiForm();
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
                danhSachGoc = dichVuDAL.GetAll();
                HienThiLenBang(danhSachGoc);
                CapNhatThongKe();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatThongKe()
        {
            int total = danhSachGoc.Count;
            int active = danhSachGoc.Count(d => d.TrangThai);
            int inactive = total - active;

            lblPillTotal.Text = $"Tổng số: {total}";
            lblPillActive.Text = $"🟢 Đang phục vụ: {active}";
            lblPillInactive.Text = $"🔴 Tạm ngưng: {inactive}";
        }

        private void HienThiLenBang(List<DichVu> danhSach)
        {
            dgvDichVu.DataSource = null;
            dgvDichVu.DataSource = danhSach;

            if (dgvDichVu.Columns["MaDichVu"] != null)
            {
                dgvDichVu.Columns["MaDichVu"].HeaderText = "MÃ DV";
                dgvDichVu.Columns["MaDichVu"].FillWeight = 75;
                dgvDichVu.Columns["MaDichVu"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                dgvDichVu.Columns["MaDichVu"].DefaultCellStyle.ForeColor = UITheme.DarkNavy;
            }
            if (dgvDichVu.Columns["TenDichVu"] != null)
            {
                dgvDichVu.Columns["TenDichVu"].HeaderText = "TÊN DỊCH VỤ";
                dgvDichVu.Columns["TenDichVu"].FillWeight = 160;
            }
            if (dgvDichVu.Columns["LoaiDichVu"] != null)
            {
                dgvDichVu.Columns["LoaiDichVu"].HeaderText = "NHÓM LOẠI";
                dgvDichVu.Columns["LoaiDichVu"].FillWeight = 95;
            }
            if (dgvDichVu.Columns["DonGia"] != null)
            {
                dgvDichVu.Columns["DonGia"].HeaderText = "ĐƠN GIÁ";
                dgvDichVu.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDichVu.Columns["DonGia"].FillWeight = 95;
            }
            if (dgvDichVu.Columns["DonViTinh"] != null)
            {
                dgvDichVu.Columns["DonViTinh"].HeaderText = "ĐVT";
                dgvDichVu.Columns["DonViTinh"].FillWeight = 55;
                dgvDichVu.Columns["DonViTinh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvDichVu.Columns["TrangThai"] is DataGridViewCheckBoxColumn)
            {
                int idx = dgvDichVu.Columns["TrangThai"].Index;
                dgvDichVu.Columns.Remove("TrangThai");
                var col = new DataGridViewTextBoxColumn
                {
                    Name = "TrangThai",
                    DataPropertyName = "TrangThai",
                    HeaderText = "TRẠNG THÁI",
                    FillWeight = 85
                };
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDichVu.Columns.Insert(idx, col);
            }
            else if (dgvDichVu.Columns["TrangThai"] != null)
            {
                dgvDichVu.Columns["TrangThai"].HeaderText = "TRẠNG THÁI";
                dgvDichVu.Columns["TrangThai"].FillWeight = 85;
                dgvDichVu.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
                var loc = danhSachGoc.Where(dv =>
                    (dv.MaDichVu != null && dv.MaDichVu.ToLower().Contains(k)) ||
                    (dv.TenDichVu != null && dv.TenDichVu.ToLower().Contains(k)) ||
                    (dv.LoaiDichVu != null && dv.LoaiDichVu.ToLower().Contains(k))).ToList();
                HienThiLenBang(loc);
            }
        }

        private void dgvDichVu_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDichVu.CurrentRow == null) return;
            var dv = dgvDichVu.CurrentRow.DataBoundItem as DichVu;
            if (dv == null) return;

            maDVDangChon = dv.MaDichVu;
            txtMaDV.Text = dv.MaDichVu;
            txtMaDV.ReadOnly = true;
            txtTenDV.Text = dv.TenDichVu;
            cboLoaiDV.Text = dv.LoaiDichVu;
            txtDonGia.Text = dv.DonGia.ToString("N0");
            cboDonViTinh.Text = dv.DonViTinh;
            chkTrangThai.Checked = dv.TrangThai;

            lblCardTitle.Text = "✏️ Cập nhật dịch vụ: " + dv.MaDichVu;
            lblCardSubtitle.Text = "Đang chỉnh sửa dịch vụ đã chọn";
            btnLuu.Text = "💾 Cập nhật";
            btnXoa.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDV.Text) || string.IsNullOrWhiteSpace(txtTenDV.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mã và tên dịch vụ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string rawGia = Regex.Replace(txtDonGia.Text, @"[^\d]", "");
            if (!decimal.TryParse(rawGia, out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá phải là số hợp lệ lớn hơn hoặc bằng 0.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return;
            }

            var dv = new DichVu
            {
                MaDichVu = txtMaDV.Text.Trim().ToUpper(),
                TenDichVu = txtTenDV.Text.Trim(),
                LoaiDichVu = cboLoaiDV.Text,
                DonGia = donGia,
                DonViTinh = cboDonViTinh.Text,
                TrangThai = chkTrangThai.Checked
            };

            if (!string.IsNullOrEmpty(maDVDangChon))
            {
                if (dichVuDAL.Update(dv))
                {
                    MessageBox.Show("Cập nhật thông tin dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSach();
                    LamMoiForm();
                }
            }
            else
            {
                // Kiểm tra trùng mã
                if (danhSachGoc.Any(x => string.Equals(x.MaDichVu, dv.MaDichVu, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show($"Mã dịch vụ [{dv.MaDichVu}] đã tồn tại trong danh mục!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaDV.Focus();
                    return;
                }

                if (dichVuDAL.Insert(dv))
                {
                    MessageBox.Show("Thêm mới dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSach();
                    LamMoiForm();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maDVDangChon))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa dịch vụ [{maDVDangChon}]?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            if (dichVuDAL.Delete(maDVDangChon))
            {
                MessageBox.Show("Đã xóa dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSach();
                LamMoiForm();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e) => LamMoiForm();

        private void LamMoiForm()
        {
            maDVDangChon = null;
            txtMaDV.ReadOnly = false;

            // Gợi ý mã tự động: DV01, DV02...
            string nextMa = GợiÝMãDịchVụ();
            txtMaDV.Text = nextMa;

            txtTenDV.Clear();
            txtDonGia.Clear();
            if (cboLoaiDV.Items.Count > 0) cboLoaiDV.SelectedIndex = 0;
            if (cboDonViTinh.Items.Count > 0) cboDonViTinh.SelectedIndex = 0;
            chkTrangThai.Checked = true;

            lblCardTitle.Text = "✨ Thêm mới dịch vụ";
            lblCardSubtitle.Text = "Nhập thông tin dịch vụ để thêm vào thực đơn & bảng giá";
            btnLuu.Text = "💾 Lưu dịch vụ";
            btnXoa.Enabled = false;

            txtTimKiem.Clear();
            dgvDichVu.ClearSelection();
        }

        private string GợiÝMãDịchVụ()
        {
            int maxId = 0;
            foreach (var d in danhSachGoc)
            {
                if (d.MaDichVu != null && d.MaDichVu.StartsWith("DV", StringComparison.OrdinalIgnoreCase))
                {
                    string numPart = d.MaDichVu.Substring(2);
                    if (int.TryParse(numPart, out int num) && num > maxId)
                    {
                        maxId = num;
                    }
                }
            }
            return $"DV{(maxId + 1):D2}";
        }
    }
}
