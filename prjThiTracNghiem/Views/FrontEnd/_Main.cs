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
    public partial class _Main : UserControl
    {

        DeThi _Dethi;
        SinhVien _objSinhvien;
        List<CauHoi> _lstCauhoi;
        MyDbContext mydb = new MyDbContext();
        #region Biến hỗ trợ
        private int phut;
        TimeSpan ts;
        #endregion


        #region Hàm hỗ trợ
        public void setThongtinbaithi(bool IsActive)
        {
            if (IsActive)
            {
                if (_Dethi != null)
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
            _objSinhvien = sv as SinhVien;
            lblMaSV.Text = _objSinhvien.SinhVienID.ToString();
            lblHoTen.Text = _objSinhvien.HoTen;
            lblNgaysinh.Text = _objSinhvien.NgaySinh;
        }
        public void GetDethi(DeThi dethi)
        {
            _Dethi = dethi;
        }
        public void SetActiveUCCauhoi(object sender, EventArgs e)
        {
            _Dethi = sender as DeThi;
            //Hiển thị thông tin bài thi và tính giờ
            setThongtinbaithi(true);
            phut = (_Dethi.ThoiGian == null ? 0 : int.Parse(_Dethi.ThoiGian.ToString()));
            ts = new TimeSpan(0, phut, 0);
            Time.Start();
            ////
            var ucCauhoi = new _Cauhoi();
            PanelContent.Controls.Clear();
            PanelContent.Controls.Add(ucCauhoi);
            ucCauhoi.Dock = DockStyle.Fill;
            // Tạo bài thi
            this.Taobaithi();
            // Đổ dữ liệu câu hỏi vào view câu hỏi
            ucCauhoi.Laydanhsachcauhoi(_lstCauhoi);

        }
        public List<CauHoi> Thaydoithutucauhoi()
        {
            List<CauHoi> lstCauhoi = _Dethi.CauHois.ToList();
            List<CauHoi> lstNew = new List<CauHoi>();
            Random r = new Random();
            while (lstCauhoi.Count > 0)
            {
                int index = r.Next(0, lstCauhoi.Count - 1);
                lstNew.Add(lstCauhoi[index]);
                lstCauhoi.RemoveAt(index);
            }
            return lstNew;
        }
        public void Taobaithi()
        {
            _lstCauhoi = Thaydoithutucauhoi();
            int idbt = DataAccess.Instance.InsertBaithi(_Dethi.DeThiID, _objSinhvien.SinhVienID, DateTime.Now.ToShortDateString());
            DataAccess.Instance.InsertCauhoiBaithi(_lstCauhoi, idbt);
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
                MessageBox.Show("Bài thi của bạn đã kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Time.Stop();
            DialogResult dr = MessageBox.Show("Xác nhận kết thúc bài thi!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {

            }
        }
    }
}
