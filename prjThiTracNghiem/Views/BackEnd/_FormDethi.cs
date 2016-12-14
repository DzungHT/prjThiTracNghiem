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
            
            switch (status)
            {
                case 0:
                    {
                        txtDotThi.Text = deThi.DotThi.TenDotThi;
                        txtTenHocPhan.Text = deThi.HocPhan.TenHocPhan;
                        txtMaDeThi.Text = deThi.MaDeThi;
                        txtThoiGian.Text = deThi.ThoiGian.ToString();
                        List<CauHoi> lst = deThi.CauHois.ToList();
                        lvDeThi.Items.Clear();
                        foreach (CauHoi ch in lst)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = ch.CauHoiID.ToString();
                            item.SubItems.Add(ch.NoiDung);
                            lvDeThi.Items.Add(item);
                        }
                        UpdateNum();
                        break;
                    }
                case 1:
                    {
                        txtDotThi.Text = db2.DotThis.Find(deThi.DotThiID).TenDotThi;
                        txtTenHocPhan.Text = db2.HocPhans.Find(deThi.HocPhanID).TenHocPhan;
                        txtMaDeThi.Text = txtThoiGian.Text = string.Empty;
                        cboChuong.Items.Clear();
                        List<Chuong> lstChuong = db2.Chuongs.Where(x=>x.HocPhanID == deThi.HocPhanID).ToList();
                        foreach (Chuong c in lstChuong)
                        {
                            cboChuong.Items.Add(c.TenChuong);
                        }
                        lvChuong.Items.Clear();
                        lvDeThi.Items.Clear();
                        //cboChuong.SelectedItem = lstChuong[0].TenChuong;
                        //List<CauHoi> lstCH = db2.CauHois.Where(x => x.ChuongID.Equals(lstChuong[0].ChuongID)).ToList();
                        //foreach (CauHoi ch in lstCH)
                        //{
                        //    ListViewItem item = new ListViewItem();
                        //    item.Name = ch.CauHoiID.ToString();
                        //    item.SubItems.Add(ch.NoiDung);
                        //    lvChuong.Items.Add(item);
                        //}
                        UpdateNum();
                        break;
                    }
                case 2:
                    {
                        txtDotThi.Text = deThi.DotThi.TenDotThi;
                        txtTenHocPhan.Text = deThi.HocPhan.TenHocPhan;
                        txtMaDeThi.Text = deThi.MaDeThi;
                        txtThoiGian.Text = deThi.ThoiGian.ToString();
                        lvDeThi.Items.Clear();
                        lvChuong.Items.Clear();
                        cboChuong.Items.Clear();
                        List<Chuong> lstChuong = db2.Chuongs.Where(x => x.HocPhanID == deThi.HocPhanID).ToList();
                        foreach (Chuong c in lstChuong)
                        {
                            cboChuong.Items.Add(c.TenChuong);
                        }
                        List<CauHoi> lstCHInDeThi = deThi.CauHois.ToList();
                        foreach (CauHoi ch in lstCHInDeThi)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Name = ch.CauHoiID.ToString();
                            item.SubItems.Add(ch.NoiDung);
                            lvDeThi.Items.Add(item);
                        }
                        UpdateNum();
                        break;
                    }
            }

        }

        private void cboChuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (status == 1)
                {
                    int ID = db2.Chuongs.Where(x => x.TenChuong.Equals(cboChuong.SelectedItem.ToString())).Single().ChuongID;
                    List<CauHoi> lstCH = db2.CauHois.Where(x => x.ChuongID == ID).ToList();
                    lvChuong.Items.Clear();
                    foreach (CauHoi ch in lstCH)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = ch.CauHoiID.ToString();
                        item.SubItems.Add(ch.NoiDung);
                        lvChuong.Items.Add(item);
                    }
                }
                else
                {
                    int ID = db2.Chuongs.Where(x => x.TenChuong.Equals(cboChuong.SelectedItem.ToString())).Single().ChuongID;
                    List<CauHoi> lstCH = db2.CauHois.Where(x => x.ChuongID == ID).ToList();
                    List<CauHoi> lstCHInDeThi = deThi.CauHois.ToList();
                    foreach (CauHoi ch in lstCH)
                    {
                        foreach (CauHoi kk in lstCHInDeThi)
                        {
                            if (ch.CauHoiID == kk.CauHoiID)
                            {
                                lstCH.Remove(ch);
                                break;
                            }
                        }
                    }
                    lvChuong.Items.Clear();
                    foreach (CauHoi ch in lstCH)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = ch.CauHoiID.ToString();
                        item.SubItems.Add(ch.NoiDung);
                        lvChuong.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void UpdateNum()
        {
            lblNumDeThi.Text = lvDeThi.Items.Count.ToString();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            ListViewItem item = lvChuong.SelectedItems[0];
            lvChuong.Items.Remove(item);
            lvDeThi.Items.Add(item);
            UpdateNum();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Init();
        }

        private void btnBo_Click(object sender, EventArgs e)
        {
            ListViewItem item = lvChuong.SelectedItems[0];
            lvDeThi.Items.Remove(item);
            UpdateNum();
            //lvDeThi.Items.Add(item);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            switch (status)
            {
                case 1:
                    try
                    {
                        DeThi dt = new DeThi();
                        dt.HocPhanID = deThi.HocPhanID;
                        dt.MaDeThi = txtMaDeThi.Text;
                        dt.HienThi = true;
                        dt.DotThiID = deThi.DotThiID;
                        dt.ThoiGian = int.Parse(txtThoiGian.Text);
                        db2.DeThis.Add(dt);
                        db2.SaveChanges();
                        foreach (ListViewItem item in lvDeThi.Items)
                        {
                            int CauHoiID = int.Parse(item.Text);
                            CauHoi ch = db2.CauHois.Find(CauHoiID);
                            dt.CauHois.Add(ch);
                            db2.SaveChanges();
                        }
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
                        deThi.MaDeThi = txtMaDeThi.Text;
                        deThi.ThoiGian = int.Parse(txtThoiGian.Text);
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
