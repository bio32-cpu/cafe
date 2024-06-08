using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using cafe_management.Model;
using System.Collections.ObjectModel;

namespace cafe_management.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand StaffCommand { get; set; }
        public ICommand MenuCommand { get; set; }
        public ICommand StatisticCommand { get; set; }
        public ICommand SpendingCommand { get; set; }

        private LoginWindow loginWindow;
        private StaffWindow staffWindow;
        private MenuWindow menuWindow;
        private StatisticWindow statisticWindow;
        private SpendingWindow spendingWindow;

        public MainViewModel()
        {
            loginWindow = (LoginWindow)App.Current.MainWindow;

            StaffCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (staffWindow == null)
                {
                    staffWindow = new StaffWindow();
                }

                statisticWindow?.Close();
                loginWindow?.Close();
                menuWindow?.Close();
                spendingWindow?.Close();
                staffWindow.ShowDialog();
            });

            MenuCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                menuWindow = new MenuWindow();
                staffWindow?.Hide();
                menuWindow.ShowDialog();
            });

            StatisticCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                statisticWindow = new StatisticWindow();
                staffWindow?.Hide();
                statisticWindow.ShowDialog();
            });

            SpendingCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                spendingWindow = new SpendingWindow();
                staffWindow?.Hide();
                spendingWindow.ShowDialog();
            });
        }
    }
}
