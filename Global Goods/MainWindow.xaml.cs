// MainWindow.xaml.cs
using System.Windows;
using Global_Goods.ViewModels;

namespace Global_Goods
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Orders Tab Event Handlers
        private void ViewOrders_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.LoadOrders();
        }


        private void ViewOrderProducts_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.LoadOrderProducts();
        }

        // Products Tab Event Handlers
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.AddProduct();
        }

        private void ViewProducts_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.LoadProducts();
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.UpdateProduct();
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.DeleteProduct();
        }

        // Categories Tab Event Handlers
        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.AddCategory();
        }

        private void ViewCategories_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.LoadCategories();
        }

        private void UpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.UpdateCategory();
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.DeleteCategory();
        }

        // Customers Tab Event Handlers
        private void RegisterCustomer_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.RegisterCustomer();
        }

        private void ViewCustomers_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.LoadCustomers();
        }

        private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.UpdateCustomer();
        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.DeleteCustomer();
        }

        // Suppliers Tab Event Handlers
        private void RegisterSupplier_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.RegisterSupplier();
        }

        private void ViewSuppliers_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.LoadSuppliers();
        }

        private void UpdateSupplier_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.UpdateSupplier();
        }

        private void DeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.DeleteSupplier();
        }

        // Employees Tab Event Handlers
        private void RegisterEmployee_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.RegisterEmployee();
        }

        private void ViewEmployees_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.LoadEmployees();
        }

        private void UpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.UpdateEmployee();
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.DeleteEmployee();
        }

        // Shippers Tab Event Handlers
        private void RegisterShipper_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.RegisterShipper();
        }

        private void ViewShippers_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.LoadShippers();
        }

        private void UpdateShipper_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.UpdateShipper();
        }

        private void DeleteShipper_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.DeleteShipper();
        }
    }
}
