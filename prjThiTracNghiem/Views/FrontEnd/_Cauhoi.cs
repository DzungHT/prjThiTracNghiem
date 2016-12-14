using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjThiTracNghiem.Views.FrontEnd
{
    public partial class _Cauhoi : UserControl
    {
        public _Cauhoi()
        {
            InitializeComponent();
        }
        private void Bat_TatRadio(int radio)
        {
            if (radio == 1)
                rad1.Checked = true;
            else if (radio == 2)
                rad2.Checked = true;
            else if (radio == 3)
                rad3.Checked = true;
            else if (radio == 4)
                rad4.Checked = true;
            else
            {
                rad1.Checked = false;
                rad2.Checked = false;
                rad3.Checked = false;
                rad4.Checked = false;
            }
        }
        private int Kiemtratraloi()
        {
            if (rad1.Checked == true || rad2.Checked == true || rad3.Checked == true || rad4.Checked == true)
                return 1;
            return 0;
        }
        private void Luutraloi()
        {
            if (Kiemtratraloi() == 1)
            {
                //if (rad1.Checked == true)
                //    arrCauHoi[cauHienTai].CauTraLoi = 1;
                //if (rad2.Checked == true)
                //    arrCauHoi[cauHienTai].CauTraLoi = 2;
                //if (rad3.Checked == true)
                //    arrCauHoi[cauHienTai].CauTraLoi = 3;
                //if (rad4.Checked == true)
                //    arrCauHoi[cauHienTai].CauTraLoi = 4;
            }
        }
        private void Thaydoiloaitraloi(bool x)
        {
            if (x)
            {
                rad1.Visible = true;
                rad2.Visible = true;
                rad3.Visible = true;
                rad4.Visible = true;
                check1.Visible = false;
                check2.Visible = false;
                check3.Visible = false;
                check4.Visible = false;
            }
            else
            {
                rad1.Visible = false;
                rad2.Visible = false;
                rad3.Visible = false;
                rad4.Visible = false;
                check1.Visible = true;
                check2.Visible = true;
                check3.Visible = true;
                check4.Visible = true;
            }
        }
        
    }
}
