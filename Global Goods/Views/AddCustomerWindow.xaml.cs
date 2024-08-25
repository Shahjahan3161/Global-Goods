using Global_Goods.Data;
using Global_Goods.Models;
using System.Windows;

namespace Global_Goods
{
    public partial class AddCustomerWindow : Window
    {
        private readonly ApplicationDbContext _context;
        private readonly Customer _customer;

        internal AddCustomerWindow(ApplicationDbContext context, Customer customer = null)
        {
            InitializeComponent();
            _context = context;

            if (customer != null)
            {
                _customer = customer;
                CustomerNameTextBox.Text = _customer.CustomerName;
                ContactNameTextBox.Text = _customer.ContactName;
                AddressTextBox.Text = _customer.Address;
                CityTextBox.Text = _customer.City;
                PostalCodeTextBox.Text = _customer.PostalCode;
                CountryTextBox.Text = _customer.Country;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_customer != null)
            {
                // Editing an existing customer
                var customer = _context.Customers.Find(_customer.CustomerID);
                if (customer != null)
                {
                    customer.CustomerName = CustomerNameTextBox.Text;
                    customer.ContactName = ContactNameTextBox.Text;
                    customer.Address = AddressTextBox.Text;
                    customer.City = CityTextBox.Text;
                    customer.PostalCode = PostalCodeTextBox.Text;
                    customer.Country = CountryTextBox.Text;
                    _context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }
            else
            {
                // Adding a new customer
                var newCustomer = new Customer
                {
                    CustomerName = CustomerNameTextBox.Text,
                    ContactName = ContactNameTextBox.Text,
                    Address = AddressTextBox.Text,
                    City = CityTextBox.Text,
                    PostalCode = PostalCodeTextBox.Text,
                    Country = CountryTextBox.Text
                };

                _context.Customers.Add(newCustomer);
            }

            _context.SaveChanges();
            this.DialogResult = true; // Close the dialog and return a success result
            this.Close();
        }
    }
}
