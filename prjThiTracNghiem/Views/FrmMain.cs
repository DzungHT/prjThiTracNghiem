using prjThiTracNghiem.Models;
using prjThiTracNghiem.Views.BackEnd;
using prjThiTracNghiem.Views.FrontEnd;
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
        GiaoVien _GiaoVien;
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

        private void ActiveBackEnd()
        {
            quảnLýCâuHỏiToolStripMenuItem.Visible = quảnLýNgườiDùngToolStripMenuItem.Visible =  quảnLýĐềThiToolStripMenuItem.Visible = true;
           
        }

        private void ActiveFrontEnd()
        {
            quảnLýCâuHỏiToolStripMenuItem.Visible = quảnLýNgườiDùngToolStripMenuItem.Visible = quảnLýĐềThiToolStripMenuItem.Visible = false;

            
        }

        private void Login_CallBack(object sender, EventArgs e)
        {
            đăngXuấtToolStripMenuItem.Visible = true;
            đăngNhậpToolStripMenuItem.Visible = false;
            _NguoiDung = sender as INguoiDung;
            tenNguoiDungToolStripStatusLabel.Text = _NguoiDung.HoTen;
            tenTaiKhoanToolStripStatusLabel.Text = _NguoiDung.TaiKhoan.Username;


            if (_NguoiDung.TaiKhoan.LoaiTaiKhoan == 1)
            {
                // Giao diện frontend
                ActiveFrontEnd();
                var ucFontEnd = new _Main();
                ucFontEnd.setThongtinsinhvien(sender);
                pnlMain.Controls.Add(ucFontEnd);
                ucFontEnd.Dock = DockStyle.Fill;
            }
            else
            {
                // Giao diện của backend
                ActiveBackEnd();
                _GiaoVien = sender as GiaoVien;

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

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var resDlg = MessageBox.Show("Đăng xuất", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if( resDlg == DialogResult.Yes)
            {
                pnlMain.Controls.Clear();
                _NguoiDung = null;
                ActiveFrontEnd();
                tenNguoiDungToolStripStatusLabel.Text = tenNguoiDungToolStripStatusLabel.Tag.ToString();
                tenTaiKhoanToolStripStatusLabel.Text = tenTaiKhoanToolStripStatusLabel.Tag.ToString();
                đăngXuấtToolStripMenuItem.Visible = false;
            }
        }

        private void đăngXuấtToolStripMenuItem_VisibleChanged(object sender, EventArgs e)
        {
            đăngNhậpToolStripMenuItem.Visible = !đăngXuấtToolStripMenuItem.Visible;
        }

        private void quảnLýCâuHỏiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var uc = new _QLCauhoi(_GiaoVien.GiaoVienID);
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
        }

        private void quảnLýĐềThiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var uc = new _QLDethi();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
        }

        private void quảnLýNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var uc = new _QLNguoidung();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
        }
    }
}
