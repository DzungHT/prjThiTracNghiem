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
            
        }
    }
}
