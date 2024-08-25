using Global_Goods.Data;
using Global_Goods.Models;
using System.Linq;
using System.Windows;

namespace Global_Goods.Views
{
    public partial class AddOrderWindow : Window
    {
        private readonly ApplicationDbContext _context;
        private readonly Order _order;

        internal AddOrderWindow(ApplicationDbContext context, Order order = null)
        {
            InitializeComponent();
            _context = context;

            // Load customers, employees, and shippers into ComboBoxes
            LoadData();

            if (order != null)  // Edit existing order
            {
                _order = order;

                // Populate fields with existing order data
                CustomerComboBox.SelectedValue = _order.CustomerID;
                EmployeeComboBox.SelectedValue = _order.EmployeeID;
                ShipperComboBox.SelectedValue = _order.ShipperID;
                OrderDatePicker.SelectedDate = _order.OrderDate;
            }
        }

        // Method to load customers, employees, and shippers from the database
        private void LoadData()
        {
            // Load customer data and bind it to CustomerComboBox
            var customers = _context.Customers.Select(c => new
            {
                c.CustomerID,
                c.CustomerName
            }).ToList();
            CustomerComboBox.ItemsSource = customers;
            CustomerComboBox.DisplayMemberPath = "CustomerName";
            CustomerComboBox.SelectedValuePath = "CustomerID";

            // Load employee data and bind it to EmployeeComboBox
            var employees = _context.Employees.Select(e => new
            {
                e.EmployeeID,
                FullName = e.FirstName + " " + e.LastName
            }).ToList();
            EmployeeComboBox.ItemsSource = employees;
            EmployeeComboBox.DisplayMemberPath = "FullName";
            EmployeeComboBox.SelectedValuePath = "EmployeeID";

            // Load shipper data and bind it to ShipperComboBox
            var shippers = _context.Shippers.Select(s => new
            {
                s.ShipperID,
                s.ShipperName
            }).ToList();
            ShipperComboBox.ItemsSource = shippers;
            ShipperComboBox.DisplayMemberPath = "ShipperName";
            ShipperComboBox.SelectedValuePath = "ShipperID";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_order != null)  // Editing an existing order
            {
                var order = _context.Orders.Find(_order.OrderID);
                if (order != null)
                {
                    order.CustomerID = (int)CustomerComboBox.SelectedValue;
                    order.EmployeeID = (int)EmployeeComboBox.SelectedValue;
                    order.ShipperID = (int)ShipperComboBox.SelectedValue;
                    order.OrderDate = OrderDatePicker.SelectedDate ?? order.OrderDate;

                    _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }
            else  // Adding a new order
            {
                var newOrder = new Order
                {
                    CustomerID = (int)CustomerComboBox.SelectedValue,
                    EmployeeID = (int)EmployeeComboBox.SelectedValue,
                    ShipperID = (int)ShipperComboBox.SelectedValue,
                    OrderDate = OrderDatePicker.SelectedDate ?? System.DateTime.Now
                };

                _context.Orders.Add(newOrder);
            }

            _context.SaveChanges();
            this.DialogResult = true;  // Close the dialog and return success
            this.Close();
        }
    }
}
