using Global_Goods.Data;
using Global_Goods.Models;
using System.Windows;

namespace Global_Goods.Views
{
    public partial class AddSupplierWindow : Window
    {
        private readonly ApplicationDbContext _context;
        private readonly Supplier _supplier;

        internal AddSupplierWindow(ApplicationDbContext context, Supplier supplier = null)
        {
            InitializeComponent();
            _context = context;

            if (supplier != null)  // Edit existing supplier
            {
                _supplier = supplier;

                // Populate fields with existing supplier data
                SupplierNameTextBox.Text = _supplier.SupplierName;
                ContactNameTextBox.Text = _supplier.ContactName;
                AddressTextBox.Text = _supplier.Address;
                CityTextBox.Text = _supplier.City;
                PostalCodeTextBox.Text = _supplier.PostalCode;
                CountryTextBox.Text = _supplier.Country;
                PhoneTextBox.Text = _supplier.Phone.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_supplier != null)  // Editing an existing supplier
            {
                var supplier = _context.Suppliers.Find(_supplier.SupplierID);
                if (supplier != null)
                {
                    supplier.SupplierName = SupplierNameTextBox.Text;
                    supplier.ContactName = ContactNameTextBox.Text;
                    supplier.Address = AddressTextBox.Text;
                    supplier.City = CityTextBox.Text;
                    supplier.PostalCode = PostalCodeTextBox.Text;
                    supplier.Country = CountryTextBox.Text;
                    supplier.Phone = int.Parse(PhoneTextBox.Text);

                    _context.Entry(supplier).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }
            else  // Adding a new supplier
            {
                var newSupplier = new Supplier
                {
                    SupplierName = SupplierNameTextBox.Text,
                    ContactName = ContactNameTextBox.Text,
                    Address = AddressTextBox.Text,
                    City = CityTextBox.Text,
                    PostalCode = PostalCodeTextBox.Text,
                    Country = CountryTextBox.Text,
                    Phone = int.Parse(PhoneTextBox.Text)
                };

                _context.Suppliers.Add(newSupplier);
            }

            _context.SaveChanges();
            this.DialogResult = true;  // Close the dialog and return success
            this.Close();
        }
    }
}
