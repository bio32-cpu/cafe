using cafe_management.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Paragraph = iTextSharp.text.Paragraph;

namespace cafe_management
{
    /// <summary>
    /// Interaction logic for MenuWindow_1.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        private List<PurchaseItem> PurchaseList = new List<PurchaseItem>();
        public MenuWindow()
        {
            InitializeComponent();

            LoadMenuItems();
        }

        private void changeColorAnimation(Button btn)
        {
            ColorAnimation ca = new ColorAnimation((Color)ColorConverter.ConvertFromString("#EA8025"), new Duration(TimeSpan.FromSeconds(0.5)));
            btn.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, ca);
        }

        private void returnDefaultColor(Button[] btn)
        {
            foreach (Button button in btn)
            {
                button.Foreground = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#000000");
            }
        }

        private void CaPheVietNam_Click(object sender, RoutedEventArgs e)
        {
            changeColorAnimation(btnCaPheVietNam);
            Button[] btn = new Button[] { btnCaPheMay, btnDaXay, btnTraTraiCay, btnTraSuaMacchiato };
            returnDefaultColor(btn);

            double scrollTarget = 0;
            scrollViewer.ScrollToVerticalOffset(scrollTarget);
        }

        private void CaPheMay_Click(object sender, RoutedEventArgs e)
        {
            changeColorAnimation(btnCaPheMay);
            Button[] btn = new Button[] { btnCaPheVietNam, btnDaXay, btnTraTraiCay, btnTraSuaMacchiato };
            returnDefaultColor(btn);

            double scrollTarget = 423;            
            scrollViewer.ScrollToVerticalOffset(scrollTarget);          
        }

        private void DaXay_Click(object sender, RoutedEventArgs e)
        {
            changeColorAnimation(btnDaXay);
            Button[] btn = new Button[] { btnCaPheVietNam, btnCaPheMay, btnTraTraiCay, btnTraSuaMacchiato };
            returnDefaultColor(btn);

            double scrollTarget = 1191;
            scrollViewer.ScrollToVerticalOffset(scrollTarget);
        }

        private void TraTraiCay_Click(object sender, RoutedEventArgs e)
        {
            changeColorAnimation(btnTraTraiCay);
            Button[] btn = new Button[] { btnCaPheVietNam, btnCaPheMay, btnDaXay, btnTraSuaMacchiato };
            returnDefaultColor(btn);

            double scrollTarget = 1959;
            scrollViewer.ScrollToVerticalOffset(scrollTarget);
        }

        private void TraSuaMacchiato_Click(object sender, RoutedEventArgs e)
        {
            changeColorAnimation(btnTraSuaMacchiato);
            Button[] btn = new Button[] { btnCaPheVietNam, btnCaPheMay, btnDaXay, btnTraTraiCay };
            returnDefaultColor(btn);

            double scrollTarget = 2727;
            scrollViewer.ScrollToVerticalOffset(scrollTarget);
        }

        private List<MenuItem> GetMenu(int type)
        {
            List<MenuItem> list = new List<MenuItem>();
            var objectList = DataProvider.Ins.DB.MONs;
            foreach (var item in objectList)
            {
                if (item.MaLoai == type)
                {
                    string source = string.Format("/Resource/MenuWindow/{0}/{1}.jpg", item.MaLoai, item.MaM);
                    MenuItem menuItem = new MenuItem(item.MaM, source, item.TenMon, Convert.ToInt64(item.DonGia));
                    list.Add(menuItem);
                }
            }
            return list;
        }

        private void LoadMenuItems()
        {
            CaPheVietNamListView.ItemsSource = GetMenu(1);
            CaPheMayListView.ItemsSource = GetMenu(2);
            DaXayListView.ItemsSource = GetMenu(3);
            TraTraiCayListView.ItemsSource = GetMenu(4);
            MacchiatoListView.ItemsSource = GetMenu(5);
        }

        private void Add(object sender, MouseButtonEventArgs e)
        {
            StackPanel stackPanel = sender as StackPanel;
            var objectList = DataProvider.Ins.DB.MONs;

            foreach (var item in objectList)
            {
                if (stackPanel.Tag.ToString() == item.MaM)
                {
                    for (int i = 0; i < PurchaseList.Count; i++)
                    {
                        if (PurchaseList[i].Id == stackPanel.Tag.ToString())
                        {
                            PurchaseList[i].Quantity++;
                            PurchaseList[i].Price += Convert.ToInt32(item.DonGia);

                            dgPurchase.ItemsSource = null;
                            dgPurchase.ItemsSource = PurchaseList;
                            displayTotalPrice();

                            return;
                        }
                    }

                    PurchaseList.Add(new PurchaseItem(item.MaM, item.TenMon, 1, Convert.ToInt32(item.DonGia)));

                    dgPurchase.ItemsSource = null;
                    dgPurchase.ItemsSource = PurchaseList;
                    displayTotalPrice();
                }
            }
        }

        private void MinusQuantity_Click(object sender, RoutedEventArgs e)
        {
            var index = dgPurchase.SelectedIndex;
            if (PurchaseList[index].Quantity <= 1)
                return;
            PurchaseList[index].Quantity--;

            int price = 0;
            var objectList = DataProvider.Ins.DB.MONs;
            foreach (var item in objectList)
            {
                if (item.MaM == PurchaseList[index].Id)
                {
                    price = Convert.ToInt32(item.DonGia);
                }
            }

            PurchaseList[index].Price -= price;

            dgPurchase.ItemsSource = null;
            dgPurchase.ItemsSource = PurchaseList;
            displayTotalPrice();
        }

        private void AddQuantity_Click(object sender, RoutedEventArgs e)
        {
            var index = dgPurchase.SelectedIndex;
            PurchaseList[index].Quantity++;

            int price = 0;
            var objectList = DataProvider.Ins.DB.MONs;
            foreach (var item in objectList)
            {
                if (item.MaM == PurchaseList[index].Id)
                {
                    price = Convert.ToInt32(item.DonGia);
                }
            }

            PurchaseList[index].Price += price;

            dgPurchase.ItemsSource = null;
            dgPurchase.ItemsSource = PurchaseList;
            displayTotalPrice();
        }

        private void Garbage_Click(object sender, RoutedEventArgs e)
        {
            var index = dgPurchase.SelectedIndex;
            PurchaseList.RemoveAt(index);

            dgPurchase.ItemsSource = null;
            dgPurchase.ItemsSource = PurchaseList;
            displayTotalPrice();
        }

        private bool check_Discount()
        {
            if (!int.TryParse(Discount.Text, out int value) || int.Parse(Discount.Text) < 0 || int.Parse(Discount.Text) > 100)
            {
                return false;
            }
            return true;
        }

        private void displayTotalPrice()
        {
            int totalPrice = 0;
            for (int i = 0; i < PurchaseList.Count; i++)
            {
                totalPrice += PurchaseList[i].Price;
            }
            if (check_Discount())
            {                
                double discount = Convert.ToDouble(Discount.Text) / 100;
                totalPrice -= (int) (discount * totalPrice);
                TotalPrice.Text = totalPrice.ToString() + " đ";
            }
            else
            {
                NotificationWindow notice = new NotificationWindow("Dữ liệu giảm giá sai","Vui lòng nhập lại");
                Discount.Text = "0";
                TotalPrice.Text = totalPrice.ToString() + " đ";
                notice.ShowDialog();
            }            
        }

        private List<CTHD> ConvertToCTHD()
        {
            List<CTHD> list = new List<CTHD>();
            foreach (var item in PurchaseList)
            {
                CTHD cthd = new CTHD()
                {
                    MaM = item.Id,
                    SL = item.Quantity

                };
                list.Add(cthd);
            }
            return list;
        }

        private void CreateHOADON(List<CTHD> list)
        {
            HOADON hd = new HOADON();
            string x = TotalPrice.Text.Replace("đ", "");
            if (list.Count == 0)
            {
                return;
            }
            else
            {
                using (QLCFEntities1 db = new QLCFEntities1())
                {
                    var temp = db.HOADONs.Add(new HOADON()
                    {
                        NgXuat = DateTime.Now,
                        TongGia = Convert.ToDecimal(x),
                        CTHDs = list,

                    });
                    db.SaveChanges();
                    foreach (CTHD cthd in hd.CTHDs)
                    {

                        cthd.MaHD = temp.MaHD;
                        db.CTHDs.Add(cthd);

                    }
                    db.SaveChanges();
                }
            }
        }

        [Obsolete]
        private void PrintHoaDon()
        {
            var objectList = DataProvider.Ins.DB.HOADONs;
            int maHD = 0;
            foreach (var item in objectList)
            {
                if (maHD <= item.MaHD)
                    maHD = item.MaHD;
            }

            int STT = 1;
            int total_price = 0;
            Document doc = new Document(iTextSharp.text.PageSize.A5);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream( maHD+".pdf", FileMode.Create));
            doc.Open();

            PdfPTable table = new PdfPTable(5);

            PdfPCell cell = new PdfPCell(new Phrase("BILL DETAIL"));
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = 5;
            cell.HorizontalAlignment = 1;

            table.AddCell(cell);
            table.AddCell("Number order");
            table.AddCell("Name");
            table.AddCell("Quantity");
            table.AddCell("Unit price");
            table.AddCell("Price");

            for (int i = 0; i < PurchaseList.Count; i++)
            {
                table.AddCell("" + STT);
                table.AddCell("" + PurchaseList[i].Name);
                table.AddCell("" + PurchaseList[i].Quantity);
                table.AddCell("" + PurchaseList[i].Price / PurchaseList[i].Quantity);
                table.AddCell("" + PurchaseList[i].Price);
                total_price += PurchaseList[i].Price;
                STT++;
            }
            Paragraph discount = new Paragraph("\nDiscount: " + Discount.Text + "%");
            Paragraph total = new Paragraph("\nTotal: " + total_price );
            Paragraph final_total = new Paragraph("\nFinal total: " + (total_price - (int)(total_price * Convert.ToDouble(Discount.Text) / 100)));
            
            doc.Add(table);
            doc.Add(discount);
            doc.Add(total);
            doc.Add(final_total);

            doc.Close();
        }

        [Obsolete]
        private void btnThanhToan_Click(object sender, RoutedEventArgs e)
        {
            if(!check_Discount())
            {
                NotificationWindow notice = new NotificationWindow("Dữ liệu giảm giá sai", "Vui lòng nhập lại");
                notice.ShowDialog();
            }
            else
            {
                List<CTHD> cthd = ConvertToCTHD();
                CreateHOADON(cthd);

                PrintHoaDon();
                StaffWindow staffWindow = new StaffWindow();
                staffWindow.Show();
                this.Close();
            }            
        }      

        private void UpdateTotalPrice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            displayTotalPrice();
        }

        private void Terminate_Click(object sender, RoutedEventArgs e)
        {
            StaffWindow staffWindow = new StaffWindow();
            staffWindow.Show();
            this.Close();
        }
    }

    public class MenuItem
    {
        public string Id { get; set; }

        public string ImagePath { get; set; }

        public string Name { get; set; }

        public long Price { get; set; }
        public int PhanLoai { get; set; }
        public MenuItem(string id, string imagePath, string name, long price)
        {
            Id = id;
            ImagePath = imagePath;
            Name = name;
            Price = price;
        }
    }

    public class PurchaseItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public PurchaseItem(string id, string name, int quantity, int price)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
        }
    }
}

