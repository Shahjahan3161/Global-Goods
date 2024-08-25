using Global_Goods.Data;
using Global_Goods.Models;
using System.Linq;
using System.Windows;

namespace Global_Goods.Views
{
    public partial class AddOrderDetailWindow : Window
    {
        private readonly ApplicationDbContext _context;
        private readonly Order_Detail _orderDetail;
        private readonly Order _order;

        // Constructor for adding a new order detail
        internal AddOrderDetailWindow(ApplicationDbContext context, Order order)
        {
            InitializeComponent();
            _context = context;
            _order = order;

            ProductComboBox.ItemsSource = _context.Products.ToList();
        }

        // Constructor for editing an existing order detail
        internal AddOrderDetailWindow(ApplicationDbContext context, Order_Detail orderDetail)
        {
            InitializeComponent();
            _context = context;
            _orderDetail = orderDetail;

            ProductComboBox.ItemsSource = _context.Products.ToList();
            ProductComboBox.SelectedItem = _orderDetail.Product;
            QuantityTextBox.Text = _orderDetail.Quantity.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_orderDetail != null)
            {
                // Editing an existing order detail
                var orderDetail = _context.Order_Details.Find(_orderDetail.OrderID, _orderDetail.ProductID);
                if (orderDetail != null)
                {
                    orderDetail.ProductID = ((Product)ProductComboBox.SelectedItem).ProductID;
                    orderDetail.Quantity = int.Parse(QuantityTextBox.Text);
                    _context.Entry(orderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }
            else
            {
                // Adding a new order detail
                var newOrderDetail = new Order_Detail
                {
                    OrderID = _order.OrderID,
                    ProductID = ((Product)ProductComboBox.SelectedItem).ProductID,
                    Quantity = int.Parse(QuantityTextBox.Text)
                };

                _context.Order_Details.Add(newOrderDetail);
            }

            _context.SaveChanges();
            this.DialogResult = true; // Close the dialog and return a success result
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Close the dialog without saving
            this.Close();
        }
    }
}
