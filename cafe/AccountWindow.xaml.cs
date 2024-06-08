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
    /// Interaction logic for AccountWindow.xaml
    /// </summary>
    public partial class AccountWindow : Window
    {
        public AccountWindow()
        {
            InitializeComponent();
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAccount() == -1)
            {
                NotificationWindow notice = new NotificationWindow("Mật khẩu hiện tại không chính xác", "Vui lòng nhập lại");
                notice.ShowDialog();
            }
            else if (CheckAccount() == -2)
            {
                NotificationWindow notice = new NotificationWindow("Mật khẩu mới không khớp", "Vui lòng nhập lại");
                notice.ShowDialog();
            }
            else if (CheckAccount() == -3)
            {
                NotificationWindow notice = new NotificationWindow("Vui lòng nhập đầy đủ thông tin", "");
                notice.ShowDialog();
            }
            else
            {
                NotificationWindow notice = new NotificationWindow("Thay đổi mật khẩu thành công", "");
                notice.ShowDialog();
                CreateNewPassWord();
            }
        }

        private int CheckAccount()
        {
            var ac = DataProvider.Ins.DB.ACCOUNTs;
            foreach (var item in ac)
            {
                if (pbOld.Password.ToString() != item.MK)
                    return -1;
                if (pbNewAgain.Password.ToString() != pbNew.Password.ToString())
                    return -2;
                if (pbOld.Password.ToString() == "" || pbNew.Password.ToString() == "" || pbNewAgain.Password.ToString() == "")
                    return -3;
            }
            return 0;
        }
        private void CreateNewPassWord()
        {
            using (QLCFEntities1 db = new QLCFEntities1())
            {
                var newpw = db.ACCOUNTs.SingleOrDefault(p => p.TK == "admin");
                newpw.MK = pbNew.Password.ToString();
                db.SaveChanges();
            }
        }

        private void Terminate_Click(object sender, RoutedEventArgs e)
        {
            StaffWindow staffWindow = new StaffWindow();
            staffWindow.Show();
            this.Close();
        }

    }
}
