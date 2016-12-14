using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using prjThiTracNghiem.Models;

namespace prjThiTracNghiem.Views.BackEnd
{
    public partial class _QLCauhoi : UserControl
    {
        int GiaoVienID;
        public _QLCauhoi()
        {
            InitializeComponent();
            Init();
        }
        public _QLCauhoi(int GiaoVienID)
        {
            InitializeComponent();
            this.GiaoVienID = GiaoVienID;
            Init();
        }
        private MyDbContext db = new MyDbContext();
        private void Init()
        {
            List<HocPhan> lstHocPhan = db.HocPhans.ToList();
            dgv_DSHocphan.DataSource = lstHocPhan;
        }
        private void dgv_DSHocphan_SelectionChanged(object sender, EventArgs e)
        {
            int id = int.Parse(dgv_DSHocphan.CurrentRow.Cells[0].Value.ToString());
            List<Chuong> lstChuong = db.Chuongs.Where(x => x.HocPhanID == id)
                                               .ToList() ;
            dgv_DSChuong.DataSource = lstChuong;
        }

        private void dgv_DSHocphan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgv_DSChuong_SelectionChanged(object sender, EventArgs e)
        {
            int id = int.Parse(dgv_DSChuong.CurrentRow.Cells[0].Value.ToString());
            List<CauHoi> lstCauHoi = db.CauHois.Where(x => x.ChuongID == id)
                                               .ToList();
            dgv_DSCauhoi.DataSource = lstCauHoi;
        }

        private void btnHPThem_Click(object sender, EventArgs e)
        {
            _FormHocphan frmHocPhan = new _FormHocphan();
            frmHocPhan.CallBack += frmHocPhan_CallBack;
            frmHocPhan.Show();

        }
        private void btnHPSua_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dgv_DSHocphan.CurrentRow.Cells[0].Value.ToString());
            HocPhan hp = db.HocPhans.Find(id);
            _FormHocphan frmHocPhan = new _FormHocphan(hp,2);
            frmHocPhan.CallBack += frmHocPhan_CallBack;
            frmHocPhan.Show();
        }
        private void frmHocPhan_CallBack(object sender, EventArgs e)
        {
            bool ok = (bool)sender;
            if (ok)
            {
                Init();
            }
        }

        private void btnHPXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(dgv_DSHocphan.CurrentRow.Cells[0].Value.ToString());
                HocPhan hp = db.HocPhans.Find(id);
                db.Entry(hp).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                MessageBox.Show("Xoá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void dgv_DSHocphan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dgv_DSHocphan.CurrentRow.Cells[0].Value.ToString());
            HocPhan hp = db.HocPhans.Find(id);
            _FormHocphan frmHocPhan = new _FormHocphan(hp,0);
            frmHocPhan.CallBack += frmHocPhan_CallBack;
            frmHocPhan.Show();
        }

        private void btnCThem_Click(object sender, EventArgs e)
        {
            _FormChuong frmChuong = new _FormChuong();
            frmChuong.CallBack += frmChuong_CallBack;
            frmChuong.Show();
        }

        void frmChuong_CallBack(object sender, EventArgs e)
        {
            bool ok = (bool)sender;
            if (ok)
            {
                Init();
            }
        }

        private void btnCSua_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(dgv_DSChuong.CurrentRow.Cells[0].Value.ToString());
            Chuong c = db.Chuongs.Find(ID);
            _FormChuong frmChuong = new _FormChuong(c,2);
            frmChuong.CallBack += frmChuong_CallBack;
            frmChuong.Show();
        }

        private void btnCXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(dgv_DSChuong.CurrentRow.Cells[0].Value.ToString());
                Chuong c = db.Chuongs.Find(id);
                db.Entry(c).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                MessageBox.Show("Xoá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void dgv_DSChuong_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int ID = int.Parse(dgv_DSChuong.CurrentRow.Cells[0].Value.ToString());
            Chuong c = db.Chuongs.Find(ID);
            _FormChuong frmChuong = new _FormChuong(c, 0);
            frmChuong.CallBack += frmChuong_CallBack;
            frmChuong.Show();
        }
        private void frmCauHoi_CallBack(object sender, EventArgs e)
        {
            bool ok = (bool)sender;
            if (ok)
            {
                Init();
            }
        }
        private void btnCHThem_Click(object sender, EventArgs e)
        {
            CauHoi ch = new CauHoi();
            int ChuongID = int.Parse(dgv_DSChuong.CurrentRow.Cells[0].Value.ToString());
            ch.ChuongID = ChuongID;
            ch.GiaoVienID = GiaoVienID;
            _FormCauhoi frmCauHoi = new _FormCauhoi(ch,1);
            frmCauHoi.CallBack += frmCauHoi_CallBack;
            frmCauHoi.Show();
        }
        private void dgv_DSCauhoi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int ID = int.Parse(dgv_DSCauhoi.CurrentRow.Cells[0].Value.ToString());
            CauHoi ch = db.CauHois.Find(ID);
            _FormCauhoi frmCauHoi = new _FormCauhoi(ch,0);
            frmCauHoi.CallBack += frmChuong_CallBack;
            frmCauHoi.Show();
        }
        private void btnCHSua_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(dgv_DSCauhoi.CurrentRow.Cells[0].Value.ToString());
            CauHoi ch = db.CauHois.Find(ID);
            _FormCauhoi frmCauHoi = new _FormCauhoi(ch, 2);
            frmCauHoi.CallBack += frmChuong_CallBack;
            frmCauHoi.Show();
        }
        private void btnCHXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = int.Parse(dgv_DSCauhoi.CurrentRow.Cells[0].Value.ToString());
                CauHoi ch = db.CauHois.Find(ID);
                db.Entry(ch).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                MessageBox.Show("Xoá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
