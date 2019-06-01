using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RMDesktopUI.Views
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {

        public ProductsView()
        {
            InitializeComponent();
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            
            //ProductModel productToDelete = (ProductModel)((Button)e.Source).DataContext;
            //return productToDelete.Id;

            ////MessageBoxResult result = MessageBox.Show(dataRowView.Id.ToString());
            //object ID = ((Button)sender).CommandParameter;
        }
    }
}
