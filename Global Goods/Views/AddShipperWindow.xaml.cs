using Global_Goods.Data;
using Global_Goods.Models;
using System.Windows;

namespace Global_Goods.Views
{
    public partial class AddShipperWindow : Window
    {
        private readonly ApplicationDbContext _context;
        private readonly Shipper _shipper;

        internal AddShipperWindow(ApplicationDbContext context, Shipper shipper = null)
        {
            InitializeComponent();
            _context = context;

            if (shipper != null)  // Edit existing shipper
            {
                _shipper = shipper;
                ShipperNameTextBox.Text = _shipper.ShipperName;
                PhoneTextBox.Text = _shipper.Phone;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_shipper != null)  // Editing an existing shipper
            {
                var shipper = _context.Shippers.Find(_shipper.ShipperID);
                if (shipper != null)
                {
                    shipper.ShipperName = ShipperNameTextBox.Text;
                    shipper.Phone = PhoneTextBox.Text;
                    _context.Entry(shipper).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }
            else  // Adding a new shipper
            {
                var newShipper = new Shipper
                {
                    ShipperName = ShipperNameTextBox.Text,
                    Phone = PhoneTextBox.Text
                };

                _context.Shippers.Add(newShipper);
            }

            _context.SaveChanges();
            this.DialogResult = true;  // Close the dialog and return success
            this.Close();
        }
    }
}
