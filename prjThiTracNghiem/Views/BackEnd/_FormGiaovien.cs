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
    public partial class _FormGiaovien : Form
    {
        public GiaoVien ObjGiaoVien { get; set; }
        private bool IsUpdate { get; set; }
        public _FormGiaovien(GiaoVien obj)
        {
            InitializeComponent();
            if(obj == null)
            {
                IsUpdate = false;
            }
            else
            {
                IsUpdate = true;
                ObjGiaoVien = obj;
            }
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            txtTenGiaoVien.Text = txtSoDienThoai.Text = txtMatKhau.Text = string.Empty;
        }
    }
}
