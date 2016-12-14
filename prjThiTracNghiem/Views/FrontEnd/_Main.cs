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
    public partial class _Main : UserControl
    {
        
        DeThi _Dethi;
        MyDbContext mydb = new MyDbContext();
        #region Biến hỗ trợ
        int cauHienTai = 0;
        int flag = 0;
        bool flaga = false;
        bool flagb = false;
        bool flagc = false;
        bool flagd = false;
        int tongThoiGianThi;

        private int phut=1;
        TimeSpan ts;
        #endregion


        #region Hàm hỗ trợ
        public void setThongtinbaithi(bool IsActive)
        {
            if (IsActive)
            {
                if(_Dethi != null)
                {
                    lblTenHP.Text = _Dethi.HocPhan.TenHocPhan;
                    lblTenDT.Text = _Dethi.DotThi.TenDotThi;
                    lblMaDT.Text = _Dethi.MaDeThi;
                    lblThoigian.Text = _Dethi.ThoiGian.ToString() + " phút";
                }
                panel2.Visible = true;
            }
            else
            {
                panel2.Visible = false;
            }
        }
        public void setThongtinsinhvien(object sv)
        {
            SinhVien _sv = sv as SinhVien;
            lblMaSV.Text = _sv.SinhVienID.ToString();
            lblHoTen.Text = _sv.HoTen;
            lblNgaysinh.Text = _sv.NgaySinh;
        }
        public void GetDethi(DeThi dethi)
        {
            _Dethi = dethi;
        }
        public void SetActiveUCCauhoi(object sender, EventArgs e)
        {
            _Dethi = sender as DeThi;
            setThongtinbaithi(true);
            phut = (_Dethi.ThoiGian == null ? 0 : int.Parse(_Dethi.ThoiGian.ToString()));
            ts = new TimeSpan(0,phut,0);
            Time.Start();
            var ucCauhoi = new _Cauhoi();
            PanelContent.Controls.Clear();
            PanelContent.Controls.Add(ucCauhoi);
            ucCauhoi.Dock = DockStyle.Fill;
        }
        #endregion
        public _Main()
        {
            InitializeComponent();
            var ucDethi = new _Dethi();
            ucDethi.activeUCCauhoi += SetActiveUCCauhoi;
            ucDethi.BindData();
            PanelContent.Controls.Add(ucDethi);
            ucDethi.Dock = DockStyle.Fill;
            this.setThongtinbaithi(false);
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            TimeSpan dt = new TimeSpan(0, 0, 1);
            ts = ts.Subtract(dt);
            lblThoigianconlai.Text = (ts.Minutes < 10 ? "0" + ts.Minutes.ToString() : ts.Minutes.ToString()) + " : " + (ts.Seconds < 10 ? "0" + ts.Seconds.ToString() : ts.Seconds.ToString());
            if (ts.Minutes == 0 && ts.Seconds == 0)
            {
                Time.Stop();
                MessageBox.Show("Bài thi của bạn đã kết thúc!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Time.Stop();
            DialogResult dr = MessageBox.Show("Xác nhận kết thúc bài thi!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr== DialogResult.Yes)
            {

            }
        }
    }
}
