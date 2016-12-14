using prjThiTracNghiem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjThiTracNghiem.Views.BackEnd
{
    public partial class _FormSinhvien : Form
    {
        public event EventHandler CallBack;
        public SinhVien ObjSinhVien { get; set; }
        private bool IsUpdate { get; set; }
        public _FormSinhvien(SinhVien obj)
        {
            InitializeComponent();
            if (obj == null)
            {
                // Thêm mới
                IsUpdate = false;
                btnKiemTra.Visible = true;
            }
            else
            {
                // Sửa
                IsUpdate = true;
                ObjSinhVien = obj;
                txtTenTaiKhoan.Enabled = false;
                btnKiemTra.Visible = false;

                txtSinhVienID.Text = obj.SinhVienID.ToString();
                txtTenSinhVien.Text = obj.HoTen;
                txtNgaySinh.Text = obj.NgaySinh;
                txtDiaChi.Text = obj.DiaChi;
                txtKhoaHoc.Text = obj.KhoaHoc;

                txtTaiKhoanID.Text = obj.TaiKhoanID.ToString();
                txtTenTaiKhoan.Text = obj.TaiKhoan.Username;
            }
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            txtTenSinhVien.Text = txtNgaySinh.Text = txtDiaChi.Text = txtKhoaHoc.Text = txtMatKhau.Text = string.Empty;

            if (!IsUpdate)
            {
                txtTenTaiKhoan.Text = string.Empty;
            }
        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtTenTaiKhoan.Text.Trim()))
            {
                MessageBox.Show("Phải điền tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenTaiKhoan.Text = string.Empty;
                txtTenTaiKhoan.Focus();
            }
            else
            {
                MyDbContext dbc = new MyDbContext();
                var lstTK = dbc.TaiKhoans.Where(x => x.Username == txtTenTaiKhoan.Text).ToList();
                if (lstTK.Count == 0)
                {
                    MessageBox.Show("Tên đăng nhập có thể sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Tên tài khoản đã tồn tại
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenTaiKhoan.Text = string.Empty;
                    txtTenTaiKhoan.Focus();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenSinhVien.Text.Trim()))
            {
                MessageBox.Show("Phải điền tên giáo viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenSinhVien.Text = string.Empty;
                txtTenSinhVien.Focus();
            }
            else
            {

                if (IsUpdate)
                {
                    // Nếu là form chỉnh sửa
                    MyDbContext dbc = new MyDbContext();
                    var sv = dbc.SinhViens.Where(x => x.SinhVienID == ObjSinhVien.SinhVienID).FirstOrDefault();

                    sv.HoTen = txtTenSinhVien.Text;
                    sv.NgaySinh = txtNgaySinh.Text;
                    sv.DiaChi = txtDiaChi.Text;
                    sv.KhoaHoc = txtKhoaHoc.Text;

                    if (!string.IsNullOrEmpty(txtMatKhau.Text.Trim()))
                        sv.TaiKhoan.Password = txtMatKhau.Text;

                    dbc.SaveChanges();

                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CallBack(null, null);
                    this.Dispose();
                }
                else
                {
                    // Nếu là form thêm mới

                    if (string.IsNullOrEmpty(txtTenTaiKhoan.Text.Trim()))
                    {
                        MessageBox.Show("Phải điền tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtTenTaiKhoan.Text = string.Empty;
                        txtTenTaiKhoan.Focus();
                    }
                    else if (string.IsNullOrEmpty(txtMatKhau.Text.Trim()))
                    {
                        MessageBox.Show("Phải điền mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtMatKhau.Text = string.Empty;
                        txtMatKhau.Focus();
                    }
                    else
                    {
                        MyDbContext dbc = new MyDbContext();
                        var lstTK = dbc.TaiKhoans.Where(x => x.Username == txtTenTaiKhoan.Text).ToList();
                        if (lstTK.Count == 0)
                        {
                            // Thêm mới
                            var ds = DataAccess.Instance.ExecuteQuery("sp_Them_SinhVien", CommandType.StoredProcedure,
                                new SqlParameter("@Hoten", txtTenSinhVien.Text),
                                new SqlParameter("@NgaySinh", txtNgaySinh.Text),
                                new SqlParameter("@DiaChi", txtDiaChi.Text),
                                new SqlParameter("@KhoaHoc", txtKhoaHoc.Text),
                                new SqlParameter("@TenTaiKhoan", txtTenTaiKhoan.Text),
                                new SqlParameter("@MatKhau", txtMatKhau.Text));

                            if (ds.Tables.Count == 0)
                            {
                                MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnLammoi_Click(null, null);
                                CallBack(null, null);
                            }
                            else
                            {
                                MessageBox.Show("Có lỗi xảy ra, hãy thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtTenTaiKhoan.Text = string.Empty;
                                txtTenTaiKhoan.Focus();
                            }

                        }
                        else
                        {
                            // Tên tài khoản đã tồn tại
                            MessageBox.Show("Tên đăng nhập đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtTenTaiKhoan.Text = string.Empty;
                            txtTenTaiKhoan.Focus();
                        }
                    }
                }
            }
        }
    }
}
