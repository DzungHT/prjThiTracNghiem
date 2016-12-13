using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjThiTracNghiem.Views
{
    public partial class FrmLogin : Form
    {
        public event EventHandler CallBack;
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void ShowError(string mess)
        {
            MessageBox.Show(mess, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private bool Login(string username, string password)
        {
            return false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text.Trim()) && !string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                if (Login(txtUsername.Text, txtPassword.Text))
                {
                    // Đăng nhập thành công
                    CallBack(sender, e);
                    SendKeys.Send("{ESC}");
                }
                else
                {
                    // Đăng nhập thất bại
                    ShowError("Tên đăng nhập hoặc mật khẩu không chính xác!");
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    txtUsername.Focus();
                }
            }
            else
            {
                ShowError("Chưa nhập đầy đủ thông tin đăng nhập!");
                if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
                {
                    txtUsername.Focus();
                }else
                {
                    txtPassword.Focus();
                }
            }

        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            AcceptButton = btnLogin;
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    SendKeys.Send("{TAB}");
                    break;
            }
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            AcceptButton = null;
        }
    }
}
