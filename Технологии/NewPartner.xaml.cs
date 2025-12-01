using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для NewPartner.xaml
    /// </summary>
    public partial class NewPartner : Window
    {
        tecnoEntities entities = new tecnoEntities();
        private List<Partner_type_> types;
        public NewPartner()
        {
            InitializeComponent();
            types = entities.Partner_type_.ToList();
            parttype.ItemsSource = types;
        }
        /// <summary>
        /// добавление нового партнёра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Partner_ part = new Partner_();
                part.Partner_type = types.FirstOrDefault(z => z.Partner_type == parttype.Text).ID;
                part.INN = inn.Text;
                part.Direktor = dir.Text;
                part.Partner_name = name.Text;
                part.phone = phon.Text;
                part.email = email.Text;
                part.adress = uradres.Text;
                part.Rating = Convert.ToInt32(rate.Text);
                entities.Partner_.Add(part);
               
            }
            catch
            {
                entities.SaveChanges();
                MessageBox.Show("Проверте заполненность полей!");
            }
        }

        private void inn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void phon_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void rate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NewOrder order = new NewOrder();
            order.Show();
            this.Close(); 
        }
    }
}
