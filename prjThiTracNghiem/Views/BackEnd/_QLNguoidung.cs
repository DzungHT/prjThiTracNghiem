﻿using System;
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
        SinhVien _sv = new SinhVien();
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

            //cboPageSize_DSSinhVien
            cboPageSize_DSSinhVien.DataSource = dtPageSize.Copy();
            cboPageSize_DSSinhVien.DisplayMember = "Display";
            cboPageSize_DSSinhVien.ValueMember = "Value";
            this.cboPageSize_DSSinhVien.SelectedIndexChanged += new System.EventHandler(this.cboPageSize_DSSinhVien_SelectedIndexChanged);

            //cboPageSize_DSBaiThi
            cboPageSize_DSBaiThi.DataSource = dtPageSize.Copy();
            cboPageSize_DSBaiThi.DisplayMember = "Display";
            cboPageSize_DSBaiThi.ValueMember = "Value";
            this.cboPageSize_DSBaiThi.SelectedIndexChanged += new System.EventHandler(this.cboPageSize_DSBaiThi_SelectedIndexChanged);
        }

        private void LoadDSGiaoVien(string textSearch, int pageNumber, int pageSize)
        {
            var ds = DataAccess.Instance.ExecuteQuery("sp_Lay_DS_GiaoVien", CommandType.StoredProcedure, new SqlParameter("@p_TextSearch", textSearch), new SqlParameter("@p_pageNumber", pageNumber), new SqlParameter("@p_pageSize", pageSize));

            dgv_DSGiaovien.DataSource = ds.Tables[0];
            int PageCount = int.Parse(ds.Tables[1].Rows[0]["PageCount"].ToString());
            numPageNumber_DSGiaoVien.Maximum = PageCount > 1 ? PageCount : 1;
            numPageNumber_DSGiaoVien.Value = int.Parse(ds.Tables[1].Rows[0]["PageNumber"].ToString());
        }

        private void LoadDSCauHoi(int GiaoVienID, int pageNumber, int pageSize)
        {
            DataSet ds = DataAccess.Instance.ExecuteQuery("sp_Lay_DS_GiaoVien_CauHoi", CommandType.StoredProcedure, new SqlParameter("@p_GiaoVienID", GiaoVienID), new SqlParameter("@p_pageNumber", pageNumber), new SqlParameter("@p_pageSize", pageSize));

            dgv_DSCauhoi.DataSource = ds.Tables[0];
            int PageCount = int.Parse(ds.Tables[1].Rows[0]["PageCount"].ToString());
            numPageNumber_DSCauHoi.Maximum = PageCount > 1 ? PageCount : 1;
            numPageNumber_DSCauHoi.Value = int.Parse(ds.Tables[1].Rows[0]["PageNumber"].ToString());
        }

        private void LoadDSSinhVien(string textSearch, int pageNumber, int pageSize)
        {
            var ds = DataAccess.Instance.ExecuteQuery("sp_Lay_DS_SinhVien", CommandType.StoredProcedure, new SqlParameter("@p_TextSearch", textSearch), new SqlParameter("@p_pageNumber", pageNumber), new SqlParameter("@p_pageSize", pageSize));

            dgv_DSSinhVien.DataSource = ds.Tables[0];
            int PageCount = int.Parse(ds.Tables[1].Rows[0]["PageCount"].ToString());
            numPageNumber_DSSinhVien.Maximum = PageCount > 1 ? PageCount : 1;
            numPageNumber_DSSinhVien.Value = int.Parse(ds.Tables[1].Rows[0]["PageNumber"].ToString());
        }

        private void LoadDSBaiThi(int SinhVienID, int pageNumber, int pageSize)
        {
            DataSet ds = DataAccess.Instance.ExecuteQuery("sp_Lay_DS_SinhVien_BaiThi", CommandType.StoredProcedure, new SqlParameter("@p_SinhVienID", SinhVienID), new SqlParameter("@p_pageNumber", pageNumber), new SqlParameter("@p_pageSize", pageSize));

            dgv_DSBaiThi.DataSource = ds.Tables[0];
            int PageCount = int.Parse(ds.Tables[1].Rows[0]["PageCount"].ToString());
            numPageNumber_DSBaiThi.Maximum = PageCount > 1 ? PageCount : 1;
            numPageNumber_DSBaiThi.Value = int.Parse(ds.Tables[1].Rows[0]["PageNumber"].ToString());
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

        private void dgv_DSSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ID = int.Parse(dgv_DSSinhVien.SelectedRows[0].Cells[0].Value.ToString());
            _sv = dbc.SinhViens.Where(x => x.SinhVienID == ID).FirstOrDefault();
            lblHoTenSinhVien.Text = _sv.HoTen;
            lblNgaySinhSinhVien.Text = _sv.NgaySinh;
            lblDiaChi.Text = _sv.DiaChi;
            lblTaiKhoanSinhVien.Text = _sv.TaiKhoan == null ? null : _sv.TaiKhoan.Username;
            lblIDSinhVien.Text = _sv.SinhVienID.ToString();

            LoadDSBaiThi(_sv.SinhVienID, 1, 10);
        }

        private void cboPageSize_DSSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            numPageNumber_DSSinhVien.Value = 1;
            LoadDSSinhVien(string.IsNullOrEmpty(txtTextSearch_DSSinhVien.Text) ? "%" : txtTextSearch_DSSinhVien.Text, (int)numPageNumber_DSSinhVien.Value, int.Parse(cboPageSize_DSSinhVien.SelectedValue.ToString()));
        }

        private void cboPageSize_DSBaiThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            numPageNumber_DSBaiThi.Value = 1;
            LoadDSBaiThi(_sv.SinhVienID, (int)numPageNumber_DSBaiThi.Value, int.Parse(cboPageSize_DSBaiThi.SelectedValue.ToString()));
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ctl = sender as TabControl;
            switch (ctl.SelectedTab.Name)
            {
                case "tabGiaoVien":
                    LoadDSGiaoVien("%", 1, 10);
                    break;
                case "tabSinhVien":
                    LoadDSSinhVien("%", 1, 10);
                    break;
            }
        }

        private void numPageNumber_DSSinhVien_ValueChanged(object sender, EventArgs e)
        {
            LoadDSSinhVien(string.IsNullOrEmpty(txtTextSearch_DSSinhVien.Text) ? "%" : txtTextSearch_DSSinhVien.Text, (int)numPageNumber_DSSinhVien.Value, int.Parse(cboPageSize_DSSinhVien.SelectedValue.ToString()));
        }

        private void btnGVThem_Click(object sender, EventArgs e)
        {
            _FormGiaovien frmGiaoVien = new _FormGiaovien(null);
            frmGiaoVien.CallBack += FrmGiaoVien_CallBack;
            frmGiaoVien.ShowDialog();
        }

        private void FrmGiaoVien_CallBack(object sender, EventArgs e)
        {
            LoadDSGiaoVien(string.IsNullOrEmpty(txtTextSearch_DSGiaoVien.Text) ? "%" : txtTextSearch_DSGiaoVien.Text, (int)numPageNumber_DSGiaoVien.Value, int.Parse(cboPageSize_DSGiaoVien.SelectedValue.ToString()));
        }

        private void btnGVXoa_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(dgv_DSGiaovien.SelectedRows[0].Cells[0].Value.ToString());
            if (ID == 1)
            {
                MessageBox.Show("Không thể xóa tài khoản Administrator!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                GiaoVien gv = dbc.GiaoViens.Where(x => x.GiaoVienID == ID).FirstOrDefault();
                var resDlg = MessageBox.Show("Xóa giáo viên có:\nID: " + ID.ToString() + " \nTên giáo viên: " + gv.HoTen, "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resDlg == DialogResult.Yes)
                {
                    var ds = DataAccess.Instance.ExecuteQuery("sp_Xoa_GiaoVien", CommandType.StoredProcedure, new SqlParameter("@GiaoVienID", gv.GiaoVienID), new SqlParameter("@TaiKhoanID", gv.TaiKhoanID));

                    if (ds.Tables.Count == 0)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo");
                        LoadDSGiaoVien(string.IsNullOrEmpty(txtTextSearch_DSGiaoVien.Text) ? "%" : txtTextSearch_DSGiaoVien.Text, (int)numPageNumber_DSGiaoVien.Value, int.Parse(cboPageSize_DSGiaoVien.SelectedValue.ToString()));
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnGVSua_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(dgv_DSGiaovien.SelectedRows[0].Cells[0].Value.ToString());
            var gv = dbc.GiaoViens.Where(x => x.GiaoVienID == ID).FirstOrDefault();
            _FormGiaovien frmGiaoVien = new _FormGiaovien(gv);
            frmGiaoVien.CallBack += FrmGiaoVien_CallBack;
            frmGiaoVien.ShowDialog();
        }

        private void btnSVXoa_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(dgv_DSSinhVien.SelectedRows[0].Cells[0].Value.ToString());
            SinhVien sv = dbc.SinhViens.Where(x => x.SinhVienID == ID).FirstOrDefault();
            var resDlg = MessageBox.Show("Xóa sinh viên\n\nID: " + ID.ToString() + " \nTên sinh viên: " + sv.HoTen, "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (resDlg == DialogResult.Yes)
            {
                var ds = DataAccess.Instance.ExecuteQuery("sp_Xoa_SinhVien", CommandType.StoredProcedure, new SqlParameter("@SinhVienID", sv.SinhVienID), new SqlParameter("@TaiKhoanID", sv.TaiKhoanID));

                if (ds.Tables.Count == 0)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    LoadDSSinhVien(string.IsNullOrEmpty(txtTextSearch_DSSinhVien.Text) ? "%" : txtTextSearch_DSSinhVien.Text, (int)numPageNumber_DSSinhVien.Value, int.Parse(cboPageSize_DSSinhVien.SelectedValue.ToString()));
                }
                else
                {
                    MessageBox.Show("Xóa không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSVThem_Click(object sender, EventArgs e)
        {
            _FormSinhvien frmSinhVien = new _FormSinhvien(null);
            frmSinhVien.CallBack += FrmSinhVien_CallBack;
            frmSinhVien.ShowDialog();
        }

        private void FrmSinhVien_CallBack(object sender, EventArgs e)
        {
            LoadDSSinhVien(string.IsNullOrEmpty(txtTextSearch_DSSinhVien.Text) ? "%" : txtTextSearch_DSSinhVien.Text, (int)numPageNumber_DSSinhVien.Value, int.Parse(cboPageSize_DSSinhVien.SelectedValue.ToString()));
        }

        private void btnSVSua_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(dgv_DSSinhVien.SelectedRows[0].Cells[0].Value.ToString());
            var sv = dbc.SinhViens.Where(x => x.SinhVienID == ID).FirstOrDefault();
            _FormSinhvien frmSinhVien = new _FormSinhvien(sv);
            frmSinhVien.CallBack += FrmSinhVien_CallBack;
            frmSinhVien.ShowDialog();
        }
    }
}
