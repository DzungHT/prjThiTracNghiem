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
        public List<int> lstTraloi;
        int Cauhientai;
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
            Thongbaocauhientai();
            Lammoithongtin();
            Thongtincauhientai();
        }
        private void Bat_TatRadio(int radio)
        {
            //switch (radio)
            //{
            //    case 0:
            //        if (check2.Checked) check2.Checked = false;
            //        if (check3.Checked) check3.Checked = false;
            //        if (check4.Checked) check4.Checked = false;
            //        if (check1.Checked) check1.Checked = false;
            //        break;
            //    case 1:
            //        if(check2.Checked) check2.Checked = false;
            //        if (check3.Checked) check3.Checked = false;
            //        if (check4.Checked) check4.Checked = false;
            //        check1.Checked = true;
            //        break;
            //    case 2:
            //        if (check1.Checked) check1.Checked = false;
            //        if (check3.Checked) check3.Checked = false;
            //        if (check4.Checked) check4.Checked = false;
            //        check2.Checked = true;
            //        break;
            //    case 3:
            //        if (check2.Checked) check2.Checked = false;
            //        if (check1.Checked) check1.Checked = false;
            //        if (check4.Checked) check4.Checked = false;
            //        check3.Checked = true;
            //        break;
            //    case 4:
            //        if (check2.Checked) check2.Checked = false;
            //        if (check3.Checked) check3.Checked = false;
            //        if (check1.Checked) check1.Checked = false;
            //        check4.Checked = true;
            //        break;
            //}
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
            if (Kiemtratraloi() == 1 && IDTraloi1.Text!="" && IDTraloi2.Text != "" && IDTraloi4.Text != "" && IDTraloi3.Text != "")
            {
                if (rad1.Checked == true)
                    lstTraloi.Add(int.Parse(IDTraloi1.Text));
                if (rad2.Checked == true)
                    lstTraloi.Add(int.Parse(IDTraloi2.Text));
                if (rad3.Checked == true)
                    lstTraloi.Add(int.Parse(IDTraloi3.Text));
                if (rad4.Checked == true)
                    lstTraloi.Add(int.Parse(IDTraloi4.Text));
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
        private void Thongbaocauhientai()
        {
            lblCauhientai.Text = Cauhientai.ToString();
            lblDatraloi.Text = lstTraloi.Count.ToString();
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
            CauHoi objCauhoi = lstCauhoi[Cauhientai-1];
            List<DapAnCauHoi> lstDapan = (from s in objCauhoi.DapAnCauHois orderby s.CauHoiID descending select s).ToList();
            if (lstDapan.Count > 0)
            {
                if (thutudapan[Cauhientai-1, 0] == 0)
                {
                    List<int> arr = new List<int>();
                    arr.Add(1);
                    arr.Add(2);
                    arr.Add(3);
                    arr.Add(4);
                    Random r = new Random();
                    int i = 0;
                    while (arr.Count() > 0)
                    {
                        int index = r.Next(0, arr.Count() - 1);
                        thutudapan[Cauhientai-1, i] = arr[index];
                        arr.RemoveAt(index);
                        i++;
                    }
                }
                txtCauhoi.Text = objCauhoi.NoiDung;
                for (int i = 0; i < 4; i++)
                {
                    int index = thutudapan[Cauhientai-1, i];
                    Thongtindapan(index, lstDapan[index - 1]);
                }
            }
            else
            {
                return;
            }
        }
        private int Kiemtratraloi(int id)
        {
            return lstTraloi.Find(x => x == id);
        }
        private void Thongtindapan(int index, DapAnCauHoi da)
        {
            switch (index)
            {
                case 1:
                    if (Kiemtratraloi(da.DapAnCauHoiID) != 0)
                    {
                        Bat_TatRadio(1);
                    }
                    IDTraloi1.Text = da.DapAnCauHoiID.ToString();
                    txtDA1.Text = da.NoiDungDapAn;
                    break;
                case 2:
                    if (Kiemtratraloi(da.DapAnCauHoiID) != 0)
                    {
                        Bat_TatRadio(2);
                    }
                    IDTraloi2.Text = da.DapAnCauHoiID.ToString();
                    txtDA2.Text = da.NoiDungDapAn;
                    break;
                case 3:
                    if (Kiemtratraloi(da.DapAnCauHoiID) != 0)
                    {
                        Bat_TatRadio(3);
                    }
                    IDTraloi3.Text = da.DapAnCauHoiID.ToString();
                    txtDA3.Text = da.NoiDungDapAn;
                    break;
                case 4:
                    if (Kiemtratraloi(da.DapAnCauHoiID) != 0)
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
                if(Cauhientai==1)    
                    Bat_TatButton(0);
                else
                    Bat_TatButton(3);
            }
            else
            {
                Bat_TatButton(0);
            }
            Thongbaocauhientai();
            Lammoithongtin();
            Thongtincauhientai();
        }

        private void btnQuaylai1_Click(object sender, EventArgs e)
        {
            if (Cauhientai > 1)
            {
                Cauhientai--;
                if (Cauhientai == 1)
                    Bat_TatButton(0);
                else
                    Bat_TatButton(3);
            }
            else
            {
                Bat_TatButton(0);
            }
            Thongbaocauhientai();
            Lammoithongtin();
            Thongtincauhientai();
        }

        private void btnTiep_Click(object sender, EventArgs e)
        {
            if (Cauhientai < lstCauhoi.Count)
            {
                Cauhientai++;
                if (Cauhientai == 5)
                    Bat_TatButton(1);
                else
                    Bat_TatButton(3);
            }
            else
            {
                Bat_TatButton(1);
            }
            Thongbaocauhientai();
            Lammoithongtin();
            Thongtincauhientai();
        }

        private void btnTiep1_Click(object sender, EventArgs e)
        {
            if (Cauhientai < lstCauhoi.Count)
            {
                Cauhientai++;
                if (Cauhientai == 5)
                    Bat_TatButton(1);
                else
                    Bat_TatButton(3);
            }
            else
            {
                Bat_TatButton(1);
            }
            Thongbaocauhientai();
            Lammoithongtin();
            Thongtincauhientai();
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
            }
            else
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

        private void rad1_CheckedChanged(object sender, EventArgs e)
        {
            Luutraloi();
        }

        private void rad2_CheckedChanged(object sender, EventArgs e)
        {
            Luutraloi();
        }

        private void rad3_CheckedChanged(object sender, EventArgs e)
        {
            Luutraloi();
        }

        private void rad4_CheckedChanged(object sender, EventArgs e)
        {
            Luutraloi();
        }

        private void check1_CheckedChanged(object sender, EventArgs e)
        {
            if(check1.Checked==true)
                Bat_TatRadio(1);
            Luutraloi();
        }
        private void check2_CheckedChanged(object sender, EventArgs e)
        {
            if (check2.Checked == true)
                Bat_TatRadio(2);
            Luutraloi();
        }
        private void check3_CheckedChanged(object sender, EventArgs e)
        {
            if (check3.Checked == true)
                Bat_TatRadio(2);
            Luutraloi();
        }
        private void check4_CheckedChanged(object sender, EventArgs e)
        {
            if (check4.Checked == true)
                Bat_TatRadio(2);
            Luutraloi();
        }
    }
}
