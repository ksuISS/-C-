using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для Product.xaml
    /// </summary>
    
    public partial class Product : Window
    {
       static tecnoEntities entities = new tecnoEntities();
       private List<production_> products;
        /// <summary>
        /// Вывод списка материалов для продукции
        /// </summary>
        public Product()
        {
            InitializeComponent();
            products = entities.production_.ToList();
            product.ItemsSource = products;
        }
        /// <summary>
        /// Расчёт и вывод в таблицу по кнопке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (product == null)
                {
                    MessageBox.Show("Выберите продукт!");
                    return;
                }

                var selectProd = entities.production_.FirstOrDefault(z => z.Production_name == product.Text);
                int id_prodtype = entities.production_type_.FirstOrDefault(z => z.ID == selectProd.Production_Type).ID;
                var materialInProduct = entities.MaterialInProducts.FirstOrDefault(z => z.id_prodiction == selectProd.Article);
                if (materialInProduct == null)
                {
                    MessageBox.Show("У этого продукта не списка материалов");
                    return;
                }
                int matType = entities.material_type_.FirstOrDefault(z => z.ID == materialInProduct.id_material).ID;
                int kolichestvo = Convert.ToInt32(kolvo.Text);
                int kolvoskald = entities.production_.FirstOrDefault(z => z.Production_name == product.Text).QuantitySklad;
                int parametr1 = Convert.ToInt32(param1.Text);
                int parametr2 = Convert.ToInt32(param2.Text);

                var materiall = entities.MaterialInProducts.Where(z => z.id_prodiction == selectProd.Article).Include(z => z.Material_).Include(z => z.Material_.material_type_).ToList();



                var CalcMaterial = materiall.Select(z => new
                {
                    MaterialName = z.Material_.Material,
                    Quantity = calc(id_prodtype, matType,
                    kolichestvo, kolvoskald, parametr1, parametr2)
                }


                ).ToList();
                material.ItemsSource = CalcMaterial;
                double min = selectProd.Min_price * Convert.ToInt32(kolvo.Text);
                mincost.Content = $"Минимальная стоимость: {min}";
            }
            catch
            {
                MessageBox.Show("Ошибка...");
            }
           

        }
        /// <summary>
        /// метод расчёта
        /// </summary>
        /// <param name="id_prodType"></param>
        /// <param name="id_mattype"></param>
        /// <param name="kolvo"></param>
        /// <param name="kolvoSklad"></param>
        /// <param name="par1"></param>
        /// <param name="par2"></param>
        /// <returns></returns>
        private int calc(int id_prodType, int id_mattype, int kolvo, int kolvoSklad, int par1, int par2) 
        {
            double NeedMaterail;
            double coef = entities.production_type_.FirstOrDefault(z =>z.ID == id_mattype).Coef_type;
            double coefDefect = entities.material_type_.FirstOrDefault(z => z.ID == id_mattype).PerDefect;
            double MatUnit = Math.Floor( par1 * par2 * coef)*coefDefect;
            int kolvoProd;
            if (kolvoSklad < kolvo)
            {
                kolvoProd = kolvo-kolvoSklad;
                NeedMaterail = Math.Floor(kolvoProd * MatUnit);
                
                
            }
            else
            {
                NeedMaterail = 0;
            }
            


            return (int)NeedMaterail;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void param1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void param2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
