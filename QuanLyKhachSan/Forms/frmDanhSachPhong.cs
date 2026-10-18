using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Quản_Lý_Khách_Sạn.DAL;

namespace Quản_Lý_Khách_Sạn.Forms
{
    public partial class frmDanhSachPhong : Form
    {
        private readonly DataGridView grid = new();
        private readonly TextBox txtSoPhong = new(), txtTang = new(), txtMoTa = new(), txtDienTich = new(), txtTim = new();
        private readonly ComboBox cboLoai = new(), cboTinhTrang = new();
        private readonly Button btnThem = new(), btnLuu = new(), btnXoa = new(), btnMoi = new();
        private int maPhong = 0;

        public frmDanhSachPhong()
        {
            Text = "Quản lý phòng"; Dock = DockStyle.Fill; BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 9F); BuildUI(); LoadLoaiPhong(); LoadPhong();
        }

        private void BuildUI()
        {
            var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, Padding = new Padding(18) };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 58)); root.RowStyles.Add(new RowStyle(SizeType.Absolute, 205)); root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Controls.Add(root);
            var heading = new Label { Text = "QUẢN LÝ PHÒNG", Font = new Font("Segoe UI", 19, FontStyle.Bold), ForeColor = Color.FromArgb(25, 48, 68), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft };
            root.Controls.Add(heading, 0, 0);
            var editor = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 3, BackColor = Color.White, Padding = new Padding(14) };
            for (int i=0;i<4;i++) editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,25));
            editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
            editor.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.Controls.Add(editor,0,1);
            AddField(editor,"Số phòng",txtSoPhong,0,0); AddField(editor,"Tầng",txtTang,1,0); AddField(editor,"Loại phòng",cboLoai,2,0); AddField(editor,"Trạng thái",cboTinhTrang,3,0);
            AddField(editor,"Mô tả giường",txtMoTa,0,1); AddField(editor,"Diện tích (m²)",txtDienTich,1,1);
            cboTinhTrang.DropDownStyle = ComboBoxStyle.DropDownList; cboTinhTrang.Items.AddRange(new object[]{"Hoạt động","Bảo trì"}); cboTinhTrang.SelectedIndex=0;
            var actions = new FlowLayoutPanel { Dock=DockStyle.Fill, FlowDirection=FlowDirection.LeftToRight, WrapContents=false };
            StyleButton(btnThem,"＋ Thêm mới",Color.FromArgb(21,128,105)); StyleButton(btnLuu,"Lưu thay đổi",Color.FromArgb(37,99,235)); StyleButton(btnXoa,"Xóa phòng",Color.FromArgb(220,38,38)); StyleButton(btnMoi,"Làm mới",Color.FromArgb(71,85,105));
            actions.Controls.AddRange(new Control[]{btnThem,btnLuu,btnXoa,btnMoi}); editor.Controls.Add(actions,2,2); editor.SetColumnSpan(actions,2);
            var listPanel = new TableLayoutPanel { Dock=DockStyle.Fill, RowCount=2, ColumnCount=1, Padding=new Padding(0,12,0,0) }; listPanel.RowStyles.Add(new RowStyle(SizeType.Absolute,42)); listPanel.RowStyles.Add(new RowStyle(SizeType.Percent,100)); root.Controls.Add(listPanel,0,2);
            var search = new FlowLayoutPanel { Dock=DockStyle.Fill, FlowDirection=FlowDirection.LeftToRight };
            search.Controls.Add(new Label{Text="Tìm phòng:",AutoSize=true,Padding=new Padding(0,8,8,0)}); txtTim.Width=280; txtTim.PlaceholderText="Nhập số phòng hoặc loại phòng"; search.Controls.Add(txtTim);
            var btnTim=new Button{Text="Tìm kiếm",Width=100,Height=30,FlatStyle=FlatStyle.Flat,BackColor=Color.FromArgb(25,48,68),ForeColor=Color.White}; search.Controls.Add(btnTim); listPanel.Controls.Add(search,0,0);
            grid.Dock=DockStyle.Fill; grid.ReadOnly=true; grid.AllowUserToAddRows=false; grid.AllowUserToDeleteRows=false; grid.SelectionMode=DataGridViewSelectionMode.FullRowSelect; grid.MultiSelect=false; grid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill; grid.BackgroundColor=Color.White; grid.BorderStyle=BorderStyle.None; grid.RowHeadersVisible=false; grid.EnableHeadersVisualStyles=false; grid.ColumnHeadersDefaultCellStyle.BackColor=Color.FromArgb(25,48,68); grid.ColumnHeadersDefaultCellStyle.ForeColor=Color.White; grid.ColumnHeadersHeight=38; grid.RowTemplate.Height=32; listPanel.Controls.Add(grid,0,1);
            btnThem.Click += (_,__)=>ThemPhong(); btnLuu.Click += (_,__)=>LuuPhong(); btnXoa.Click += (_,__)=>XoaPhong(); btnMoi.Click += (_,__)=>ResetForm(); btnTim.Click += (_,__)=>LoadPhong(txtTim.Text.Trim()); txtTim.KeyDown += (_,e)=>{if(e.KeyCode==Keys.Enter){LoadPhong(txtTim.Text.Trim());e.Handled=true;}}; grid.SelectionChanged += (_,__)=>{ if (grid.SelectedRows.Count > 0 && grid.CurrentRow != null) ChonPhong(); };
        }
        private static void AddField(TableLayoutPanel panel,string title,Control input,int col,int row){var box=new Panel{Dock=DockStyle.Fill,Padding=new Padding(4,0,8,2),Margin=new Padding(0)}; var label=new Label{Text=title,Dock=DockStyle.Top,Height=22,ForeColor=Color.FromArgb(71,85,105),TextAlign=ContentAlignment.MiddleLeft}; input.Dock=DockStyle.Top; input.Height=28; input.Margin=new Padding(0); if(input is ComboBox cb)cb.DropDownStyle=ComboBoxStyle.DropDownList; box.Controls.Add(input); box.Controls.Add(label); panel.Controls.Add(box,col,row);}
        private static void StyleButton(Button b,string text,Color color){b.Text=text;b.Width=125;b.Height=34;b.Margin=new Padding(4,8,4,0);b.FlatStyle=FlatStyle.Flat;b.FlatAppearance.BorderSize=0;b.BackColor=color;b.ForeColor=Color.White;}
        private void LoadLoaiPhong(){try{var dt=DatabaseHelper.ExecuteQuery("SELECT MaLoai, TenLoai FROM LoaiPhong ORDER BY TenLoai");cboLoai.DataSource=dt;cboLoai.DisplayMember="TenLoai";cboLoai.ValueMember="MaLoai";}catch(Exception ex){MessageBox.Show("Không tải được loại phòng. Kiểm tra kết nối CSDL.\n"+ex.Message,"Lỗi CSDL",MessageBoxButtons.OK,MessageBoxIcon.Error);}}
        private void LoadPhong(string keyword="") {try{grid.DataSource=DatabaseHelper.ExecuteQuery(@"SELECT p.MaPhong,p.SoPhong AS [Số phòng],p.Tang AS [Tầng],l.TenLoai AS [Loại phòng],l.GiaMotDem AS [Giá/đêm],l.SoNguoiToiDa AS [Số người tối đa],p.MoTaGiuong AS [Mô tả giường],p.DienTich AS [Diện tích],p.TinhTrang AS [Trạng thái] FROM Phong p JOIN LoaiPhong l ON p.MaLoai=l.MaLoai WHERE p.SoPhong LIKE @kw OR l.TenLoai LIKE @kw ORDER BY p.SoPhong",new[]{new SqlParameter("@kw","%"+keyword+"%")});if(grid.Columns.Contains("MaPhong"))grid.Columns["MaPhong"].Visible=false;}catch(Exception ex){MessageBox.Show("Không tải được danh sách phòng.\n"+ex.Message,"Lỗi CSDL",MessageBoxButtons.OK,MessageBoxIcon.Error);}}
        private bool ValidateInput(){if(string.IsNullOrWhiteSpace(txtSoPhong.Text)){MessageBox.Show("Vui lòng nhập số phòng.");return false;}if(!int.TryParse(txtTang.Text,out _)){MessageBox.Show("Tầng phải là số nguyên.");return false;}if(!decimal.TryParse(txtDienTich.Text,out _)&&!string.IsNullOrWhiteSpace(txtDienTich.Text)){MessageBox.Show("Diện tích không hợp lệ.");return false;}if(cboLoai.SelectedValue==null){MessageBox.Show("Vui lòng khai báo loại phòng trước.");return false;}return true;}
        private SqlParameter[] Params()=>new[]{new SqlParameter("@so",txtSoPhong.Text.Trim()),new SqlParameter("@tang",int.Parse(txtTang.Text)),new SqlParameter("@loai",Convert.ToInt32(cboLoai.SelectedValue)),new SqlParameter("@mota",string.IsNullOrWhiteSpace(txtMoTa.Text)?DBNull.Value:txtMoTa.Text.Trim()),new SqlParameter("@dt",string.IsNullOrWhiteSpace(txtDienTich.Text)?DBNull.Value:decimal.Parse(txtDienTich.Text)),new SqlParameter("@tt",cboTinhTrang.Text)};
        private void ThemPhong(){if(!ValidateInput())return;try{DatabaseHelper.ExecuteNonQuery("INSERT INTO Phong(SoPhong,Tang,MaLoai,MoTaGiuong,DienTich,TinhTrang) VALUES(@so,@tang,@loai,@mota,@dt,@tt)",Params());LoadPhong(txtTim.Text.Trim());ResetForm();MessageBox.Show("Đã thêm phòng.");}catch(Exception ex){MessageBox.Show("Không thể thêm phòng. Có thể số phòng đã tồn tại.\n"+ex.Message);}}
        private void LuuPhong(){if(maPhong==0){MessageBox.Show("Chọn phòng cần cập nhật hoặc bấm Thêm mới.");return;}if(!ValidateInput())return;try{var p=Params();Array.Resize(ref p,p.Length+1);p[^1]=new SqlParameter("@id",maPhong);DatabaseHelper.ExecuteNonQuery("UPDATE Phong SET SoPhong=@so,Tang=@tang,MaLoai=@loai,MoTaGiuong=@mota,DienTich=@dt,TinhTrang=@tt WHERE MaPhong=@id",p);LoadPhong(txtTim.Text.Trim());MessageBox.Show("Đã cập nhật phòng.");}catch(Exception ex){MessageBox.Show("Không thể cập nhật phòng.\n"+ex.Message);}}
        private void XoaPhong(){if(maPhong==0){MessageBox.Show("Vui lòng chọn phòng cần xóa.");return;}if(MessageBox.Show("Bạn có chắc muốn xóa phòng này? Phòng đã phát sinh đặt phòng có thể không xóa được.","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)!=DialogResult.Yes)return;try{DatabaseHelper.ExecuteNonQuery("DELETE FROM Phong WHERE MaPhong=@id",new[]{new SqlParameter("@id",maPhong)});LoadPhong();ResetForm();}catch(Exception ex){MessageBox.Show("Không thể xóa phòng đã có dữ liệu liên quan.\n"+ex.Message);}}
        private void ChonPhong(){if(grid.SelectedRows.Count==0||grid.CurrentRow==null||!grid.Columns.Contains("MaPhong")||grid.CurrentRow.Cells["MaPhong"].Value==null)return;try{maPhong=Convert.ToInt32(grid.CurrentRow.Cells["MaPhong"].Value);var dt=DatabaseHelper.ExecuteQuery("SELECT * FROM Phong WHERE MaPhong=@id",new[]{new SqlParameter("@id",maPhong)});if(dt.Rows.Count==0)return;var r=dt.Rows[0];txtSoPhong.Text=r["SoPhong"].ToString();txtTang.Text=r["Tang"].ToString();cboLoai.SelectedValue=Convert.ToInt32(r["MaLoai"]);txtMoTa.Text=r["MoTaGiuong"]==DBNull.Value?"":r["MoTaGiuong"].ToString();txtDienTich.Text=r["DienTich"]==DBNull.Value?"":r["DienTich"].ToString();cboTinhTrang.Text=r["TinhTrang"].ToString();}catch{}}
        private void ResetForm(){maPhong=0;grid.CurrentCell=null;grid.ClearSelection();txtSoPhong.Clear();txtTang.Clear();txtMoTa.Clear();txtDienTich.Clear();if(cboLoai.Items.Count>0)cboLoai.SelectedIndex=0;cboTinhTrang.SelectedIndex=0;txtSoPhong.Focus();}
    }
}
