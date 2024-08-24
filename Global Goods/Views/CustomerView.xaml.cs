using System.Windows;
using Global_Goods.ViewModels;

namespace Global_Goods.Views
{
    public partial class CustomerView : Window
    {
        public CustomerView()
        {
            InitializeComponent();
        }

        private void SaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (CustomerViewModel)DataContext;
            viewModel.SaveCustomer();
        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (CustomerViewModel)DataContext;
            viewModel.DeleteCustomer();
        }
    }
}
