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

namespace prjThiTracNghiem.Views.BackEnd
{
    public partial class _QLNguoidung : UserControl
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbContext"].ConnectionString);
        public _QLNguoidung()
        {
            InitializeComponent();
        }

        private void LoadTabGiaoVien()
        {
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
