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

namespace Технологии
{
    /// <summary>
    /// Логика взаимодействия для NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Window
    {
        tecnoEntities entities = new tecnoEntities();
        private List<Partner_> partners;
        private List<production_> product;
        private List<ParthAndProd_> spisok;

        public NewOrder()
        {
            InitializeComponent();
            partners = entities.Partner_.ToList();
            product = entities.production_.ToList();
            part.ItemsSource = partners;
            prod.ItemsSource = product;
            
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        /// <summary>
        /// Добавление нового заказа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try {
                if (Convert.ToInt32(kolvo.Text) > 0)
                {
                    var parth = new ParthAndProd_();
                    parth.Partner_name = partners.FirstOrDefault(z => z.Partner_name == part.Text).ID;
                    parth.QuantityProd = Convert.ToInt32(kolvo.Text);
                    parth.Production = product.FirstOrDefault(z => z.Production_name == prod.Text).Article;
                    entities.ParthAndProd_.Add(parth);
                    entities.SaveChanges();
                    spisok.Add(parth);
                    entities = new tecnoEntities();
                    spisokProd.ItemsSource = spisok;
                    MessageBox.Show("продукт добавлен!");
                }
                else MessageBox.Show("Количество не может быть отрицательным");
           
            }
            catch {
                MessageBox.Show("Проверьте правильность и заполненность всех полей");
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
