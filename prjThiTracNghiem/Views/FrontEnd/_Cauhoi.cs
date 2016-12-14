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

namespace prjThiTracNghiem.Views.FrontEnd
{
    public partial class _Cauhoi : UserControl
    {
        List<CauHoi> lstCauhoi;
        List<int> lstTraloi;
        int Cauhientai;
        int Socaudatraloi;
        int[,] thutudapan; 
        public _Cauhoi()
        {
            InitializeComponent();
        }
        public void Laydanhsachcauhoi(List<CauHoi> lst)
        {
            lstCauhoi = lst;
            // Tạo dữ liệu cho câu đầu tiên
            lstTraloi = new List<int>();
            thutudapan = new int[4, lstCauhoi.Count + 1];
            Cauhientai = 1;
            Socaudatraloi = 0;
            Thongbaocauhientai();
            Lammoithongtin();
            Thongtincauhientai();
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
            //if (Kiemtratraloi() == 1)
            //{
            //    if (rad1.Checked == true)
            //    if (rad2.Checked == true)
            //    if (rad3.Checked == true)
            //    if (rad4.Checked == true)
            //}
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
        private void Thongbaocauhientai()
        {
            lblCauhientai.Text = Cauhientai.ToString();
            lblDatraloi.Text = Socaudatraloi.ToString();
            lblTongsocau.Text = lstCauhoi.Count.ToString();
        }
        private void Lammoithongtin()
        {
            Bat_TatRadio(0);
            txtCauhoi.Text = txtDA1.Text = txtDA2.Text = txtDA3.Text = txtDA4.Text = "";
            IDTraloi1.Text = IDTraloi2.Text = IDTraloi3.Text = IDTraloi4.Text = "";
        }
        private void Thongtincauhientai()
        {
            CauHoi objCauhoi = lstCauhoi[Cauhientai];
            List<DapAnCauHoi> lstDapan = (from s in objCauhoi.DapAnCauHois orderby s.CauHoiID descending select s).ToList();
            if (thutudapan[Cauhientai, 0] == 0)
            {
                Random r = new Random();
                int i = 0;
                int k=4;
                while (k > 0)
                {
                    int index = r.Next(1,k);
                    thutudapan[Cauhientai, i] = index;
                    k--;i++;
                }
            }
            txtCauhoi.Text = objCauhoi.NoiDung;
            for(int i = 0; i < 4; i++)
            {
                int index = thutudapan[Cauhientai, i];
                Thongtindapan(index, lstDapan[index - 1]);
            }
        }
        private int Kiemtratraloi(int id)
        {
            return lstTraloi.Find(x => x == id);
        }
        private void Thongtindapan(int index,DapAnCauHoi da)
        {
            switch (index)
            {
                case 1:
                    if (Kiemtratraloi(da.DapAnCauHoiID) != -1)
                    {
                        Bat_TatRadio(1);
                    }
                    IDTraloi1.Text = da.DapAnCauHoiID.ToString();
                    txtDA1.Text = da.NoiDungDapAn;
                    break;
                case 2:
                    if (Kiemtratraloi(da.DapAnCauHoiID) != -1)
                    {
                        Bat_TatRadio(2);
                    }
                    IDTraloi2.Text = da.DapAnCauHoiID.ToString();
                    txtDA2.Text = da.NoiDungDapAn;
                    break;
                case 3:
                    if (Kiemtratraloi(da.DapAnCauHoiID) != -1)
                    {
                        Bat_TatRadio(3);
                    }
                    IDTraloi3.Text = da.DapAnCauHoiID.ToString();
                    txtDA3.Text = da.NoiDungDapAn;
                    break;
                case 4:
                    if (Kiemtratraloi(da.DapAnCauHoiID) != -1)
                    {
                        Bat_TatRadio(4);
                    }
                    IDTraloi4.Text = da.DapAnCauHoiID.ToString();
                    txtDA4.Text = da.NoiDungDapAn;
                    break;
            }
        }

        private void btnQuaylai_Click(object sender, EventArgs e)
        {
            if (Cauhientai > 1)
            {
                Cauhientai--;
                Bat_TatButton(3);
            }
            else
            {
                Bat_TatButton(0);
            }
        }

        private void btnQuaylai1_Click(object sender, EventArgs e)
        {
            if (Cauhientai > 1)
            {
                Cauhientai--;
                Bat_TatButton(3);
            }
            else
            {
                Bat_TatButton(0);
            }
        }

        private void btnTiep_Click(object sender, EventArgs e)
        {
            if (Cauhientai > 1)
            {
                Cauhientai--;
                Bat_TatButton(3);
            }
            else
            {
                Bat_TatButton(1);
            }
        }

        private void btnTiep1_Click(object sender, EventArgs e)
        {
            if (Cauhientai > 1)
            {
                Cauhientai--;
                Bat_TatButton(3);
            }
            else
            {
                Bat_TatButton(1);
            }
        }
        private void Bat_TatButton(int state)
        {
            btnQuaylai.Enabled = true;
            btnQuaylai1.Enabled = true;
            btnTiep.Enabled = true;
            btnTiep1.Enabled = true;
            if (state == 1)
            {
                btnTiep.Enabled = false;
                btnTiep1.Enabled = false;
            }else
            {
                if (state == 0)
                {
                    btnQuaylai.Enabled = false;
                    btnQuaylai1.Enabled = false;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
