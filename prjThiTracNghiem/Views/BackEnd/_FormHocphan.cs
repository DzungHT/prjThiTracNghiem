using prjThiTracNghiem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjThiTracNghiem.Views.BackEnd
{
    public partial class _FormHocphan : Form
    {
        public event EventHandler CallBack;
        MyDbContext db2 = new MyDbContext();
        HocPhan hp;
        int status;
        //status: 0-View, 1 - Add, 2 - update
        public _FormHocphan()
        {
            InitializeComponent();
            status = 1;
            hp = null;
            Init();
        }
        public _FormHocphan(HocPhan hocPhan,int TrangThai)
        {
            InitializeComponent();
            hp = hocPhan;
            status = TrangThai;
            Init(); 
        }
        private void Init()
        {
            if (hp == null)
            {
                textBox1.Text = textBox2.Text = string.Empty;
            }
            else
            {
                textBox1.Text = hp.TenHocPhan;
                textBox2.Text = hp.SoTinChi.ToString();
            }
            if (status == 0)
            {
                btnLuu.Enabled = false;
                btnLammoi.Enabled = false;
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            switch (status)
            {
                case 1:
                    try
                    {
                        HocPhan newHP = new HocPhan();
                        newHP.TenHocPhan = textBox1.Text;
                        newHP.SoTinChi = int.Parse(textBox2.Text);
                        db2.HocPhans.Add(newHP);
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
                        hp.TenHocPhan = textBox1.Text;
                        hp.SoTinChi = int.Parse(textBox2.Text);
                        //db2.Entry(hp).State = EntityState.Modified;
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            Init();
        }

    }
}
