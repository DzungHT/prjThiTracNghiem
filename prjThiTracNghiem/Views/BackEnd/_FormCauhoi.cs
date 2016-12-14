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
    public partial class _FormCauhoi : Form
    {
        public event EventHandler CallBack;
        MyDbContext db2 = new MyDbContext();
        int status;
        CauHoi cauHoi;
        public _FormCauhoi()
        {
            InitializeComponent();
            cauHoi = null;
            status = 1;
            Init();
        }
        public _FormCauhoi(CauHoi ch, int TrangThai)
        {
            InitializeComponent();
            cauHoi = ch;
            status = TrangThai;
            Init();
        }
        private void Init()
        {
            comboBox1.Items.Clear();
            foreach(DoKho dk in db2.DoKhoes.ToList()){
                comboBox1.Items.Add(dk.TenDoKho);
            }
            if (cauHoi == null || status == 1)
            {
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = string.Empty;
                checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = checkBox4.Checked = false;
            }
            else
            {
                textBox1.Text = cauHoi.NoiDung;
                try{
                    List<DapAnCauHoi> lstDA = db2.DapAnCauHois.Where(x=>x.CauHoiID == cauHoi.CauHoiID).ToList();
                    textBox2.Text = lstDA[0].NoiDungDapAn;
                    checkBox1.Checked = (lstDA[0].LaDapAnDung==true?true:false);
                    textBox3.Text = lstDA[1].NoiDungDapAn;
                    checkBox2.Checked = (lstDA[1].LaDapAnDung == true ? true : false);
                    textBox4.Text = lstDA[2].NoiDungDapAn;
                    checkBox3.Checked = (lstDA[2].LaDapAnDung == true ? true : false);
                    textBox5.Text = lstDA[3].NoiDungDapAn;
                    checkBox4.Checked = (lstDA[3].LaDapAnDung == true ? true : false);
                    foreach (var x in comboBox1.Items)
                    {
                        if (cauHoi.DoKho.TenDoKho.Equals(x.ToString()))
                        {
                            comboBox1.SelectedItem = x;
                            break;
                        }
                    } 
                }
                catch(Exception ex){
                    MessageBox.Show(ex.ToString());
                }
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
                        AddCauHoi();
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
                        UpdateCauHoi();
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
        private void AddCauHoi()
        {
            try
            {
                //add CauHoi
                CauHoi cauHoi = new CauHoi();
                cauHoi.NoiDung = textBox1.Text;
                foreach (DoKho dk in db2.DoKhoes.ToList())
                {
                    if (dk.TenDoKho.Equals(comboBox1.SelectedItem.ToString()))
                    {
                        cauHoi.DoKhoID = dk.DoKhoID;
                        break;
                    }
                }
                cauHoi.ChuongID = this.cauHoi.ChuongID;
                int NumRightAns = 0;
                NumRightAns += (checkBox1.Checked ? 1 : 0);
                NumRightAns += (checkBox2.Checked ? 1 : 0);
                NumRightAns += (checkBox3.Checked ? 1 : 0);
                NumRightAns += (checkBox4.Checked ? 1 : 0);
                cauHoi.CoNhieuDapAnDung = (NumRightAns > 1 ? true : false);
                cauHoi.GiaoVienID = this.cauHoi.GiaoVienID;
                db2.CauHois.Add(cauHoi);
                db2.SaveChanges();
                //Add DapAnCauHoi
                DapAnCauHoi da1 = new DapAnCauHoi();
                da1.LaDapAnDung = checkBox1.Checked;
                da1.NoiDungDapAn = textBox2.Text;
                da1.CauHoiID = cauHoi.CauHoiID;
                db2.DapAnCauHois.Add(da1);
                //Add DapAnCauHoi
                DapAnCauHoi da2 = new DapAnCauHoi();
                da2.LaDapAnDung = checkBox2.Checked;
                da2.NoiDungDapAn = textBox3.Text;
                da2.CauHoiID = cauHoi.CauHoiID;
                db2.DapAnCauHois.Add(da2);
                //Add DapAnCauHoi
                DapAnCauHoi da3 = new DapAnCauHoi();
                da3.LaDapAnDung = checkBox3.Checked;
                da3.NoiDungDapAn = textBox4.Text;
                da3.CauHoiID = cauHoi.CauHoiID;
                db2.DapAnCauHois.Add(da3);
                //Add DapAnCauHoi
                DapAnCauHoi da4 = new DapAnCauHoi();
                da4.LaDapAnDung = checkBox4.Checked;
                da4.NoiDungDapAn = textBox5.Text;
                da4.CauHoiID = cauHoi.CauHoiID;
                db2.DapAnCauHois.Add(da4);
                //
                db2.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void UpdateCauHoi()
        {
            try
            {
                //add CauHoi
                cauHoi.NoiDung = textBox1.Text;
                foreach (DoKho dk in db2.DoKhoes.ToList())
                {
                    if (dk.TenDoKho.Equals(comboBox1.SelectedItem.ToString()))
                    {
                        cauHoi.DoKhoID = dk.DoKhoID;
                        break;
                    }
                }
                int NumRightAns = 0;
                NumRightAns += (checkBox1.Checked ? 1 : 0);
                NumRightAns += (checkBox2.Checked ? 1 : 0);
                NumRightAns += (checkBox3.Checked ? 1 : 0);
                NumRightAns += (checkBox4.Checked ? 1 : 0);
                cauHoi.CoNhieuDapAnDung = (NumRightAns > 1 ? true : false);
                cauHoi.GiaoVienID = this.cauHoi.GiaoVienID;
                foreach (DoKho dk in db2.DoKhoes.ToList())
                {
                    if (dk.TenDoKho.Equals(comboBox1.SelectedItem.ToString()))
                    {
                        cauHoi.DoKhoID = dk.DoKhoID;
                        break;
                    }
                } 
                List<DapAnCauHoi> lstDA = db2.DapAnCauHois.Where(x => x.CauHoiID == cauHoi.CauHoiID).ToList();
                lstDA[0].NoiDungDapAn = textBox2.Text ;
                lstDA[0].LaDapAnDung = (checkBox1.Checked == true ? true : false);
                lstDA[1].NoiDungDapAn =textBox3.Text ;
                lstDA[1].LaDapAnDung = (checkBox2.Checked == true ? true : false);
                lstDA[2].NoiDungDapAn = textBox4.Text ;
                lstDA[2].LaDapAnDung = (checkBox3.Checked == true ? true : false);
                lstDA[3].NoiDungDapAn= textBox5.Text ;
                lstDA[3].LaDapAnDung = (checkBox4.Checked == true ? true : false);
                db2.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
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
