using cafe_management.ViewModel;
using System.Windows.Controls;

namespace cafe_management.UserControlCF
{
    /// <summary>
    /// Interaction logic for UserControlUC.xaml
    /// </summary>
    public partial class ControlBarUC : UserControl
    {
        public ControlBarViewModel Viewmodel { get; set; }
        public ControlBarUC()
        {
            InitializeComponent();
            this.DataContext = Viewmodel = new ControlBarViewModel();
        }
    }
}
