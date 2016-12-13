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

            if(sender.GetType() is GiaoVien)
            {

            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
