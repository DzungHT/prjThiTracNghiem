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

namespace prjThiTracNghiem.Views.BackEnd
{
    public partial class _FormDotThi : Form
    {
        public event EventHandler CallBack;
        MyDbContext db2 = new MyDbContext();
        DotThi dotThi;
        int status;
        public _FormDotThi()
        {
            InitializeComponent();
            status = 1;
            dotThi = null;
            Init();
        }
        public _FormDotThi(DotThi dt, int TrangThai)
        {
            InitializeComponent();
            status = TrangThai;
            dotThi = dt;
            Init();
        }
        private void Init()
        {
            if (status == 0)
            {
                btnLammoi.Enabled = btnLuu.Enabled = textBox1.Enabled = textBox2.Enabled= false;
            }
            if (status == 1)
            {
                textBox1.Text = textBox2.Text = string.Empty;
                checkBox1.Checked = false;
            }
            else
            {
                textBox1.Text = dotThi.TenDotThi;
                textBox2.Text = dotThi.NamHoc;
                checkBox1.Checked = (dotThi.HienThi == null || dotThi.HienThi == false ? false : true);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            switch (status)
            {
                case 1:
                    try
                    {
                        DotThi dt = new DotThi();
                        dt.TenDotThi = textBox1.Text;
                        dt.NamHoc = textBox2.Text;
                        dt.HienThi = checkBox1.Checked;
                        db2.DotThis.Add(dt);
                        db2.SaveChanges();
                        CallBack(true, e);
                        MessageBox.Show("Insert thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        CallBack(false, e);
                        MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 2:
                    try
                    {
                        dotThi.TenDotThi = textBox1.Text;
                        dotThi.NamHoc = textBox2.Text;
                        dotThi.HienThi = checkBox1.Checked;
                        db2.SaveChanges();
                        CallBack(true, e);
                        MessageBox.Show("Update thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        CallBack(false, e);
                        MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                default:
                    Init();
                    break;
            }
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            Init();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
