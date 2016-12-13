using prjThiTracNghiem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjThiTracNghiem.Views
{
    public partial class FrmMain : Form
    {
        INguoiDung _NguoiDung;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLogin frm = new FrmLogin();
            frm.CallBack += Login_CallBack;
            frm.ShowDialog();
        }

        private void Login_CallBack(object sender, EventArgs e)
        {
            đăngXuấtToolStripMenuItem.Visible = true;
            đăngNhậpToolStripMenuItem.Visible = false;
            _NguoiDung = sender as INguoiDung;
            tenNguoiDungToolStripStatusLabel.Text = _NguoiDung.HoTen;
            tenTaiKhoanToolStripStatusLabel.Text = _NguoiDung.TaiKhoan.Username;


            if (sender.GetType().Name.Equals("GiaoVien"))
            {
                // Giao diện của backend
                MessageBox.Show("GiaoVien");
            }
            else
            {
                // Giao diện frontend
                MessageBox.Show("SinhVien");
            }

           
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Thoát chương trình?", "Xác nhận thoát chương trình", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
