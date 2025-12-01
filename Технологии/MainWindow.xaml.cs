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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Технологии
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        tecnoEntities entities = new tecnoEntities();

        /// <summary>
        /// Класс для переноса
        /// </summary>
        
        public class ViewPartners
        {
            public int id { get; set; }
            public int id_partner { get; set; }
            public string name { get; set; }
            public string partnerType { get; set; }
            public int id_partner_type { get; set; }
            public double cost_for_unit { get; set; }
            public string address { get; set; }
            public string phone { get; set; }
            public int rating { get; set; }
            public int countInRequest { get; set; }
            public double costAll { get; set; }
        }
        /// <summary>
        /// Инициализация и объединение таблиц
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            listPart.ItemsSource = entities.Partner_.ToList();
            var Parthners = entities.Partner_.ToList();
            var zayavka = entities.ParthAndProd_.ToList();
            var prod = entities.production_.ToList();
            var TypePart = entities.Partner_type_.ToList();
            var TablePart = from p in Parthners
                            join f in TypePart on p.Partner_type equals f.ID
                            join t in zayavka on p.ID equals t.Partner_name
                            join o in prod on t.Production equals o.Article
                            select new
                            {
                                id = p.ID,
                                name = p.Partner_name,
                                id_part = p.ID,
                                type = f.Partner_type,
                                id_part_type = f.ID,
                                adres = p.adress,
                                phone = p.phone,
                                rate = p.Rating,
                                price = o.Min_price * t.QuantityProd,
                                MinPrice = o.Min_price,
                                count = t.QuantityProd
                            };
            listPart.ItemsSource = TablePart.ToList();
            var PartnerList = TablePart.Select(m => new ViewPartners
            {
                id = m.id,
                name = m.name,
                id_partner = m.id_part,
                partnerType = m.type,
                id_partner_type = m.id_part_type,
                cost_for_unit = m.MinPrice,
                address = m.adres,
                phone = m.phone,
                rating = m.rate,
                countInRequest = m.count,
                costAll = m.count * m.MinPrice

            }

            ).ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewOrder order = new NewOrder();
            order.Show();
            this.Close();
        }

        private void listPart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Edit edit = new Edit((listPart.SelectedItem as ViewPartners).id);
            edit.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            product.Show();
            this.Close();
        }
    }
}
