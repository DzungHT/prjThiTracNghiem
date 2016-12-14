using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using prjThiTracNghiem.Models;
using PagedList;

namespace prjThiTracNghiem.Views.BackEnd
{
    public partial class _QLNguoidung : UserControl
    {
        MyDbContext dbc = new MyDbContext();
        GiaoVien _gv = new GiaoVien();
        public _QLNguoidung()
        {
            InitializeComponent();
            LoadDSGiaoVien("%", 1, 10);
            DataTable dtPageSize = new DataTable();
            dtPageSize.Columns.Add("Display");
            dtPageSize.Columns.Add("Value");
            dtPageSize.Rows.Add(10, 10);
            dtPageSize.Rows.Add(20, 20);
            dtPageSize.Rows.Add(50, 50);
            dtPageSize.Rows.Add(100, 100);

            //cboPageSize_DSGiaoVien
            cboPageSize_DSGiaoVien.DataSource = dtPageSize;
            cboPageSize_DSGiaoVien.DisplayMember = "Display";
            cboPageSize_DSGiaoVien.ValueMember = "Value";
            this.cboPageSize_DSGiaoVien.SelectedIndexChanged += new System.EventHandler(this.cboPageSize_DSGiaoVien_SelectedIndexChanged);

            //cboPageSize_DSCauhoi
            cboPageSize_DSCauhoi.DataSource = dtPageSize.Copy();
            cboPageSize_DSCauhoi.DisplayMember = "Display";
            cboPageSize_DSCauhoi.ValueMember = "Value";
            this.cboPageSize_DSCauhoi.SelectedIndexChanged += new System.EventHandler(this.cboPageSize_DSCauhoi_SelectedIndexChanged);
        }

        private void LoadDSGiaoVien(string textSearch, int pageNumber, int pageSize)
        {
            var ds = DataAccess.Instance.ExecuteQuery("sp_Lay_DS_GiaoVien", CommandType.StoredProcedure, new SqlParameter("@p_TextSearch", textSearch), new SqlParameter("@p_pageNumber", pageNumber), new SqlParameter("@p_pageSize", pageSize));
            
            dgv_DSGiaovien.DataSource = ds.Tables[0];
            numPageNumber_DSGiaoVien.Value = int.Parse(ds.Tables[1].Rows[0]["PageNumber"].ToString());
            numPageNumber_DSGiaoVien.Maximum = int.Parse(ds.Tables[1].Rows[0]["PageCount"].ToString());
        }
        
        private void LoadDSCauHoi(int GiaoVienID, int pageNumber, int pageSize)
        {
            DataSet ds = DataAccess.Instance.ExecuteQuery("sp_Lay_DS_GiaoVien_CauHoi", CommandType.StoredProcedure, new SqlParameter("@p_GiaoVienID", GiaoVienID), new SqlParameter("@p_pageNumber", pageNumber), new SqlParameter("@p_pageSize", pageSize));

            dgv_DSCauhoi.DataSource = ds.Tables[0];
            numPageNumber_DSCauHoi.Value = int.Parse(ds.Tables[1].Rows[0]["PageNumber"].ToString());
            numPageNumber_DSCauHoi.Maximum = int.Parse(ds.Tables[1].Rows[0]["PageCount"].ToString());
        }

        private void LoadDSSinhVien(string textSearch, int pageNumber, int pageSize)
        {
            var ds = DataAccess.Instance.ExecuteQuery("sp_Lay_DS_GiaoVien", CommandType.StoredProcedure, new SqlParameter("@p_TextSearch", textSearch), new SqlParameter("@p_pageNumber", pageNumber), new SqlParameter("@p_pageSize", pageSize));

            dgv_DSSinhVien.DataSource = ds.Tables[0];
            numPageNumber_DSSinhVien.Value = int.Parse(ds.Tables[1].Rows[0]["PageNumber"].ToString());
            numPageNumber_DSSinhVien.Maximum = int.Parse(ds.Tables[1].Rows[0]["PageCount"].ToString());
        }

        private void dgv_DSGiaovien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ID = int.Parse(dgv_DSGiaovien.SelectedRows[0].Cells[0].Value.ToString());
            _gv = dbc.GiaoViens.Where(x => x.GiaoVienID == ID).FirstOrDefault();
            lblTenGiaoVien.Text = _gv.HoTen;
            lblSoDienThoai.Text = _gv.SDT;
            lblTaiKhoanGiaoVien.Text = _gv.TaiKhoan == null ? null : _gv.TaiKhoan.Username;
            lblIDGiaovien.Text = _gv.GiaoVienID.ToString();

            LoadDSCauHoi(_gv.GiaoVienID, 1, 10);
        }


        private void numPageNumber_DSGiaoVien_ValueChanged(object sender, EventArgs e)
        {
            LoadDSGiaoVien(string.IsNullOrEmpty(txtTextSearch_DSGiaoVien.Text) ? "%" : txtTextSearch_DSGiaoVien.Text, (int)numPageNumber_DSGiaoVien.Value, int.Parse(cboPageSize_DSGiaoVien.SelectedValue.ToString()));
        }

        private void cboPageSize_DSGiaoVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            numPageNumber_DSGiaoVien.Value = 1;
            LoadDSGiaoVien(string.IsNullOrEmpty(txtTextSearch_DSGiaoVien.Text) ? "%" : txtTextSearch_DSGiaoVien.Text, (int)numPageNumber_DSGiaoVien.Value, int.Parse(cboPageSize_DSGiaoVien.SelectedValue.ToString()));
        }

        private void cboPageSize_DSCauhoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            numPageNumber_DSCauHoi.Value = 1;
            LoadDSCauHoi(_gv.GiaoVienID, (int)numPageNumber_DSCauHoi.Value, int.Parse(cboPageSize_DSCauhoi.SelectedValue.ToString()));
        }
    }
}
