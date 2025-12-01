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
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        tecnoEntities entities = new tecnoEntities();
        private List<Partner_> partners;
        private List<production_> prodi;
        ParthAndProd_ select;

        public Edit(int selectedMaterial)
        {
            InitializeComponent();
            partners = entities.Partner_.ToList();
            part.ItemsSource = partners;
            prodi = entities.production_.ToList();
            prod.ItemsSource = prodi;
            select = new ParthAndProd_();
            select = entities.ParthAndProd_.FirstOrDefault(z => z.ID == selectedMaterial);
            part.Text = select.Partner_.Partner_name;
            prod.Text = select.production_.Production_name;
            kolvo.Text = select.QuantityProd.ToString();


        }
        /// <summary>
        /// Редактирование заявки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var editProd = entities.ParthAndProd_.FirstOrDefault(z => z.ID == select.ID);
            editProd.Partner_name = partners.FirstOrDefault(z => z.Partner_name == part.Text).ID;
            editProd.Production = prodi.FirstOrDefault(z => z.Production_name == prod.Text).Article;
            editProd.QuantityProd = Convert.ToInt32(kolvo.Text);
            entities.SaveChanges();
            MessageBox.Show("Заявка обновлена");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewPartner partner = new NewPartner();
            partner.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
