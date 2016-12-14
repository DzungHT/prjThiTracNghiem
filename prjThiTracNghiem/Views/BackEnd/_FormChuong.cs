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
    public partial class _FormChuong : Form
    {
        public event EventHandler CallBack;
        Chuong chuong;
        int status;
        MyDbContext db2 = new MyDbContext();
        public _FormChuong()
        {
            InitializeComponent();
            chuong = null;
            status = 1;
            Init();
        }
        public _FormChuong(Chuong chuong,int TrangThai)
        {
            InitializeComponent();
            this.chuong = chuong;
            status = TrangThai;
            Init();

        }
        private void Init()
        {
            comboBox1.Items.Clear();
            foreach(HocPhan hp in db2.HocPhans.ToList()){
                comboBox1.Items.Add(hp.TenHocPhan);
            }
            if (chuong == null)
            {
                textBox1.Text = string.Empty;
            }
            else
            {
                textBox1.Text =  chuong.TenChuong;
                foreach (var x in comboBox1.Items) 
                {
                    if(chuong.HocPhan.TenHocPhan.Equals(x.ToString())){
                        comboBox1.SelectedItem = x;
                        break;
                    }
                } 
            }
            if (status == 0)
            {
                btnLuu.Enabled = false;
                btnLammoi.Enabled = false;
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            switch (status)
            {
                case 1:
                    try
                    {
                        Chuong c = new Chuong();
                        c.TenChuong = textBox1.Text;
                        string TenHocPhan = comboBox1.SelectedItem.ToString();
                        List<HocPhan> hp = db2.HocPhans.Where(x=>x.TenHocPhan.Equals(TenHocPhan)).ToList();
                        if(hp.Count >0){
                            c.HocPhanID = hp[0].HocPhanID;
                        }
                        db2.Chuongs.Add(c);
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
                        chuong.TenChuong = textBox1.Text;
                        string TenHocPhan = comboBox1.SelectedItem.ToString();
                        List<HocPhan> hp = db2.HocPhans.Where(x => x.TenHocPhan.Equals(TenHocPhan)).ToList();
                        if (hp.Count > 0)
                        {
                            chuong.HocPhanID = hp[0].HocPhanID;
                        }
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
    }
}
