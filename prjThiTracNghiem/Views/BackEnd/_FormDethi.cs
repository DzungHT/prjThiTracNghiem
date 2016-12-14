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
    public partial class _FormDethi : Form
    {
        public event EventHandler CallBack;
        MyDbContext db2 = new MyDbContext();
        DeThi deThi;
        int status;
        public _FormDethi()
        {
            InitializeComponent();
            status = 1;
            deThi = null;
            Init();
        }

        public _FormDethi(DeThi dt, int TrangThai)
        {
            InitializeComponent();
            status = TrangThai;
            deThi = dt;
            Init();

        }

        private void Init()
        {
            
            txtDotThi.Text = deThi.DotThi.TenDotThi;
            txtTenHocPhan.Text = deThi.HocPhan.TenHocPhan;
            switch (status)
            {
                case 0:
                    {
                        txtMaDeThi.Text = deThi.MaDeThi;
                        txtThoiGian.Text = deThi.ThoiGian.ToString();
                        List<CauHoi> lst = deThi.CauHois.ToList();
                        lvDeThi.Items.Clear();
                        foreach (CauHoi ch in lst)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Name = ch.CauHoiID.ToString();
                            item.SubItems.Add(ch.NoiDung);
                            lvDeThi.Items.Add(item);
                        }
                        break;
                    }
                case 1:
                    {
                        txtMaDeThi.Text = txtThoiGian.Text = string.Empty;
                        cboChuong.Items.Clear();
                        List<Chuong> lstChuong = db2.Chuongs.Where(x=>x.HocPhanID == deThi.HocPhanID).ToList();
                        foreach (Chuong c in lstChuong)
                        {
                            cboChuong.Items.Add(c.TenChuong);
                        }
                        cboChuong.SelectedItem = lstChuong[0].TenChuong;
                        List<CauHoi> lstCH = db2.CauHois.Where(x => x.ChuongID == lstChuong[0].ChuongID).ToList();
                        lvChuong.Items.Clear();
                        foreach (CauHoi ch in lstCH)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Name = ch.CauHoiID.ToString();
                            item.SubItems.Add(ch.NoiDung);
                            lvChuong.Items.Add(item);
                        }
                        break;
                    }
            }

        }
    }
}
