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
    public partial class _QLDethi : UserControl
    {
        private MyDbContext db = new MyDbContext();
        private int DotThiID = 0;
        private int HocPhanID = 0;
        public _QLDethi()
        {
            InitializeComponent();
            Init();
        }  
        private void Init()
        {
            List<DotThi> lstDotThi = db.DotThis.ToList();
            dgvDSDotthi.DataSource = lstDotThi;
            List<HocPhan> lstHocPhan = db.HocPhans.ToList();
            dgvHocPhan.DataSource = lstHocPhan;
        }

        private void dgvDSDotthi_SelectionChanged(object sender, EventArgs e)
        {
            DotThiID = int.Parse(dgvDSDotthi.CurrentRow.Cells[0].Value.ToString());
            ShowListDeThi();
        }

        private void dgvHocPhan_SelectionChanged(object sender, EventArgs e)
        {
            HocPhanID = int.Parse(dgvHocPhan.CurrentRow.Cells[0].Value.ToString());
            ShowListDeThi();
        }
        private void ShowListDeThi()
        {
            if (DotThiID == 0 || HocPhanID == 0)
                return;
            List<DeThi> lstDeThi = db.DeThis.Where(x => x.DotThiID == DotThiID && x.HocPhanID == HocPhanID).ToList() ;
            dgvDeThi.DataSource = lstDeThi;

        }

        private void btnDotThiThem_Click(object sender, EventArgs e)
        {
            _FormDotThi frmDotThi = new _FormDotThi();
            frmDotThi.CallBack += frmDotThiCallBack;
            frmDotThi.Show();
        }

        private void frmDotThiCallBack(object sender, EventArgs e)
        {
            if ((bool)sender)
            {
                Init();
            }
        }

        private void btnDotThiSua_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(dgvDSDotthi.CurrentRow.Cells[0].Value.ToString());
            DotThi dt = db.DotThis.Find(ID);
            _FormDotThi frmDotThi = new _FormDotThi(dt,2);
            frmDotThi.CallBack += frmDotThiCallBack;
            frmDotThi.Show();
        }

        private void btnDotThiXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(dgvDSDotthi.CurrentRow.Cells[0].Value.ToString());
                DotThi dt = db.DotThis.Find(id);
                db.Entry(dt).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                MessageBox.Show("Xoá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void dgvDSDotthi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int ID = int.Parse(dgvDSDotthi.CurrentRow.Cells[0].Value.ToString());
            DotThi dt = db.DotThis.Find(ID);
            _FormDotThi frmDotThi = new _FormDotThi(dt, 0);
            frmDotThi.CallBack += frmDotThiCallBack;
            frmDotThi.Show();
        }

        private void dgvDeThi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int ID = int.Parse(dgvDeThi.CurrentRow.Cells[0].Value.ToString());
            DeThi dt = db.DeThis.Find(ID);
            _FormDethi frmDeThi = new _FormDethi(dt, 0);
            frmDeThi.CallBack += frmDeThiCallBack;
            frmDeThi.Show();
        }

        private void frmDeThiCallBack(object sender, EventArgs e)
        {
            if ((bool)sender)
            {
                Init();
            }
        }

        private void btnDeTChitiet_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(dgvDeThi.CurrentRow.Cells[0].Value.ToString());
            DeThi dt = db.DeThis.Find(ID);
            _FormDethi frmDeThi = new _FormDethi(dt, 0);
            frmDeThi.CallBack += frmDeThiCallBack;
            frmDeThi.Show();
        }

        private void btnDeThiThem_Click(object sender, EventArgs e)
        {
            DeThi dt = new DeThi();
            dt.DotThiID = int.Parse(dgvDSDotthi.CurrentRow.Cells[0].Value.ToString());
            dt.HocPhanID = int.Parse(dgvHocPhan.CurrentRow.Cells[0].Value.ToString());
            _FormDethi frmDeThi = new _FormDethi(dt, 1);
            frmDeThi.CallBack += frmDeThiCallBack;
            frmDeThi.Show();
        }

        private void btnDeThiSua_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(dgvDeThi.CurrentRow.Cells[0].Value.ToString());
            DeThi dt = db.DeThis.Find(ID);
            _FormDethi frmDeThi = new _FormDethi(dt, 2);
            frmDeThi.CallBack += frmDeThiCallBack;
            frmDeThi.Show();
        }

        private void btnDeThiXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(dgvDeThi.CurrentRow.Cells[0].Value.ToString());
                DeThi c = db.DeThis.Find(id);
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

        private void button1_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(dgvDSDotthi.CurrentRow.Cells[0].Value.ToString());
            DotThi dt = db.DotThis.Find(ID);
            _FormDotThi frmDotThi = new _FormDotThi(dt, 0);
            frmDotThi.CallBack += frmDotThiCallBack;
            frmDotThi.Show();
        }

    }
}
