using cafe_management.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace cafe_management
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (CheckAccount() == true)
                {
                    StaffWindow staffWindow = new StaffWindow();
                    this.Close();
                    staffWindow.ShowDialog();
                }
                else
                {
                    NotificationWindow notice = new NotificationWindow("Sai tài khoản hoặc mật khẩu", "Vui lòng nhập lại");
                    notice.ShowDialog();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAccount() == true)
            {
                StaffWindow staffWindow = new StaffWindow();
                this.Close();
                staffWindow.ShowDialog();
            }
            else
            {
                NotificationWindow notice = new NotificationWindow("Sai tài khoản hoặc mật khẩu", "Vui lòng nhập lại");
                notice.ShowDialog();
            }
        }

        private bool CheckAccount()
        {
            var objectList = DataProvider.Ins.DB.ACCOUNTs;
            foreach (var item in objectList)
            {
                string id = IdTextBox.Text.ToString();
                string pw = Convert.ToString(passwordBox.Password.ToString());
                if (string.Compare(id, item.TK) == 0 && string.Compare(pw, item.MK) == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
