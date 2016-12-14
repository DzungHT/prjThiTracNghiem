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
using System.Data.SqlClient;

namespace prjThiTracNghiem.Views.FrontEnd
{
    public partial class _Dethi : UserControl
    {
        MyDbContext mydb = new MyDbContext();
        DeThi _objDethi;
        public event EventHandler activeUCCauhoi;
        public _Dethi()
        {
            InitializeComponent();
        }
        public void BindData()
        {
            // dổ dữ liệu vào datagridview
            DataSet ds= DataAccess.Instance.ExecuteQuery("sp_LayDanhSachDeThi", CommandType.Text);
            dgvDSDethi.DataSource= ds.Tables[0];
            
        }
        public void SetThongtindethi(DeThi _dethi)
        {
            if (_objDethi != null)
            {
                lblSoCau.Text = _dethi.CauHois.Count.ToString();
                lblTenDeT.Text = _dethi.TenDeThi;
                lblTenDT.Text = _dethi.DotThi.TenDotThi;
                lblThoigian.Text = _dethi.ThoiGian.ToString() + " phút";
                lblTenHp.Text = _dethi.HocPhan.TenHocPhan;
            }
        }
        private void GetDethi(int DethiID, out DeThi dethi)
        {
            dethi = mydb.DeThis.Single(x => x.DeThiID == DethiID);
        }
        private void dgvDSDethi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ID = int.Parse(dgvDSDethi.SelectedRows[0].Cells["ID"].Value.ToString());
            if (ID != 0)
                this.GetDethi(ID,out _objDethi);
            SetThongtindethi(_objDethi);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            activeUCCauhoi(_objDethi as object, null);
            if (_objDethi != null)
            {
                
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đề thi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
