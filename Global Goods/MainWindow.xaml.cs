using Global_Goods.Data;
using Global_Goods.Models;
using Global_Goods.Views;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace Global_Goods
{
    public partial class MainWindow : Window
    {
        private readonly ApplicationDbContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();

            LoadData();
        }

        private void LoadData()
        {


            // Load Orders
            //OrdersListView.ItemsSource = _context.Orders
            //.Include(o => o.Customer)        // Eager loading Customer data
            //.Include(o => o.Shipper)         // Eager loading Shipper data
            //.Select(o => new                 // Projecting data to a new anonymous type
            // {
            //     o.OrderID,                   // Order ID
            //     o.OrderDate,                 // Order Date
            //     CustomerName = o.Customer.CustomerName,    // Customer's name
            //     ShipperName = o.Shipper.ShipperName       // Shipper's name
            // })
            // .ToList();

     var ordersWithDetails = _context.Orders
    .Include(o => o.Customer)  // Eager loading Customer data
    .Include(o => o.Shipper)   // Eager loading Shipper data
    .Select(o => new OrderViewModel
    {
        OrderID = o.OrderID,
        OrderDate = o.OrderDate,
        CustomerName = o.Customer != null ? o.Customer.CustomerName : "No Customer",
        ShipperName = o.Shipper != null ? o.Shipper.ShipperName : "No Shipper",
        OriginalOrder = o  // Preserve the original Order object
    })
    .ToList();

OrdersListView.ItemsSource = ordersWithDetails;




            // Load Products
            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Select(p => new
                {
                    p.ProductID,
                    p.ProductName,
                    CategoryName = p.Category.CategoryName,
                    SupplierName = p.Supplier.SupplierName,
                    p.Price
                })
                .ToList();

            ProductsListView.ItemsSource = products;

            // Load Categories
            CategoriesListView.ItemsSource = _context.Categories
                .Select(c => new
                {
                    c.CategoryID,
                    c.CategoryName,
                    c.Description
                })
                .ToList();

            // Load Customers
            CustomersListView.ItemsSource = _context.Customers
                .Select(c => new
                {
                    CustomerID = c.CustomerID,
                    CustomerName = c.CustomerName ?? "Unknown",    // Replace null with "Unknown"
                    ContactName = c.ContactName ?? "No Contact",   // Replace null with "No Contact"
                    Address = c.Address ?? "No Address",           // Replace null with "No Address"
                    Country = c.Country ?? "No Country",           // Replace null with "No Country"
                    City = c.City ?? "No City",                    // Replace null with "No City"
                })
                .ToList();

            // Load Suppliers
            SuppliersListView.ItemsSource = _context.Suppliers
                .Select(s => new
                {
                    s.SupplierID,
                    s.SupplierName,
                    s.ContactName,
                    s.Address,
                    s.City,
                    s.PostalCode,
                    s.Country,
                    Phone = s.Phone.ToString()
                })
                .ToList();

            // Load Employees
            EmployeesListView.ItemsSource = _context.Employees
                .Select(e => new
                {
                    e.EmployeeID,
                    e.LastName,
                    e.FirstName,
                    e.BirthDate,
                    e.Photo,
                    e.Notes,
                  

                })
                .ToList();

            // Load Shippers
            ShippersListView.ItemsSource = _context.Shippers
                .Select(s => new
                {
                    s.ShipperID,
                    s.ShipperName,
                    s.Phone
                })
                .ToList();

            // Load Order Details
            //OrderDetailsListView.ItemsSource = _context.Order_Details
            //    .Include(od => od.Product)
            //    .Select(od => new
            //    {
            //        od.OrderID,
            //        ProductName = od.Product.ProductName,
            //        od.Quantity,
            //        Price = od.Quantity * od.Product.Price
            //    })
            //    .ToList();
        }

        #region Order CRUD
        private void AddOrders_Click(object sender, RoutedEventArgs e)
        {
            var addOrderWindow = new AddOrderWindow(_context);
            if (addOrderWindow.ShowDialog() == true)
            {
                LoadData();  // Reload Orders after adding a new one
            }
        }

        private void EditOrders_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersListView.SelectedItem is OrderViewModel selectedOrderViewModel)  // Use OrderViewModel instead of Order
            {
                var originalOrder = selectedOrderViewModel.OriginalOrder;  // Get the original Order object
                var editOrderWindow = new AddOrderWindow(_context, originalOrder);

                if (editOrderWindow.ShowDialog() == true)
                {
                    LoadData();  // Reload Orders after editing
                }
            }
            else
            {
                MessageBox.Show("Please select an order to edit.", "Edit Order", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }




        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersListView.SelectedItem is Order selectedOrder)
            {
                var order = _context.Orders.First(o => o.OrderID == selectedOrder.OrderID);
                _context.Orders.Remove(order);
                _context.SaveChanges();
                LoadData();  // Refresh the list after deletion
            }
            else
            {
                MessageBox.Show("Please select an order to delete.", "Delete Order", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region Product CRUD
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProductWindow(_context);
            if (addProductWindow.ShowDialog() == true) // If Save is successful
            {
                LoadData();  // Refresh the data
            }
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsListView.SelectedItem is Product selectedProduct)
            {
                var editProductWindow = new AddProductWindow(_context, selectedProduct);

                if (editProductWindow.ShowDialog() == true)
                {
                    LoadData();  // Refresh the product data list
                }
            }
            else
            {
                MessageBox.Show("Please select a product to edit.", "Edit Product", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsListView.SelectedItem is Product selectedProduct)
            {
                var product = _context.Products.First(p => p.ProductID == selectedProduct.ProductID);
                _context.Products.Remove(product);
                _context.SaveChanges();
                LoadData();  // Refresh UI
            }
        }
        #endregion

        #region Category CRUD
        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var addCategoryWindow = new AddCategoryWindow(_context);
            if (addCategoryWindow.ShowDialog() == true)
            {
                LoadData(); // Refresh the category list after adding a new category
            }
        }

        private void EditCategory_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesListView.SelectedItem is Category selectedCategory)
            {
                var editCategoryWindow = new AddCategoryWindow(_context, selectedCategory);
                if (editCategoryWindow.ShowDialog() == true)
                {
                    LoadData(); // Refresh the category list after editing
                }
            }
            else
            {
                MessageBox.Show("Please select a category to edit.");
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesListView.SelectedItem is Category selectedCategory)
            {
                var category = _context.Categories.First(c => c.CategoryID == selectedCategory.CategoryID);
                _context.Categories.Remove(category);
                _context.SaveChanges();
                LoadData();  // Refresh UI
            }
        }
        #endregion

        #region Customer CRUD
        private void RegisterCustomer_Click(object sender, RoutedEventArgs e)
        {
            var addCustomerWindow = new AddCustomerWindow(_context);
            if (addCustomerWindow.ShowDialog() == true)
            {
                LoadData();  // Refresh the data
            }
        }

        private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersListView.SelectedItem is Customer selectedCustomer)
            {
                var editCustomerWindow = new AddCustomerWindow(_context, selectedCustomer);
                if (editCustomerWindow.ShowDialog() == true)
                {
                    LoadData();  // Refresh the data
                }
            }
        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersListView.SelectedItem is Customer selectedCustomer)
            {
                var customer = _context.Customers.First(c => c.CustomerID == selectedCustomer.CustomerID);
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                LoadData();  // Refresh UI
            }
        }
        #endregion

        #region Supplier CRUD
        //private void RegisterSupplier_Click(object sender, RoutedEventArgs e)
        //{
        //    var addSupplierWindow = new AddSupplierWindow(_context);
        //    if (addSupplierWindow.ShowDialog() == true)
        //    {
        //        LoadData();  // Refresh the data
        //    }
        //}

        //private void UpdateSupplier_Click(object sender, RoutedEventArgs e)
        //{
        //    if (SuppliersListView.SelectedItem is Supplier selectedSupplier)
        //    {
        //        var editSupplierWindow = new AddSupplierWindow(_context, selectedSupplier);
        //        if (editSupplierWindow.ShowDialog() == true)
        //        {
        //            LoadData();  // Refresh the data
        //        }
        //    }
        //}

    
        #endregion

        #region Supplier CRUD
        private void RegisterSupplier_Click(object sender, RoutedEventArgs e)
        {
            var addSupplierWindow = new AddSupplierWindow(_context);
            if (addSupplierWindow.ShowDialog() == true) // If Save is successful
            {
                LoadData();  // Refresh the data
            }
        }

        private void UpdateSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (SuppliersListView.SelectedItem is Supplier selectedSupplier)
            {
                var editSupplierWindow = new AddSupplierWindow(_context, selectedSupplier);
                if (editSupplierWindow.ShowDialog() == true) // If Save is successful
                {
                    LoadData();  // Refresh the data
                }
            }
            else
            {
                MessageBox.Show("Please select a supplier to update.", "Update Supplier", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (SuppliersListView.SelectedItem is Supplier selectedSupplier)
            {
                var supplier = _context.Suppliers.First(s => s.SupplierID == selectedSupplier.SupplierID);
                _context.Suppliers.Remove(supplier);
                _context.SaveChanges();
                LoadData();  // Refresh UI
            }
            else
            {
                MessageBox.Show("Please select a supplier to delete.", "Delete Supplier", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion


        #region Employee CRUD
        private void RegisterEmployee_Click(object sender, RoutedEventArgs e)
        {
            var addEmployeeWindow = new AddEmployeeWindow(_context);
            if (addEmployeeWindow.ShowDialog() == true) // If Save is successful
            {
                LoadData();  // Refresh the data
            }
        }

        private void UpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesListView.SelectedItem is Employee selectedEmployee)
            {
                var editEmployeeWindow = new AddEmployeeWindow(_context, selectedEmployee);
                if (editEmployeeWindow.ShowDialog() == true) // If Save is successful
                {
                    LoadData();  // Refresh the data
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to update.", "Update Employee", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesListView.SelectedItem is Employee selectedEmployee)
            {
                var employee = _context.Employees.First(e => e.EmployeeID == selectedEmployee.EmployeeID);
                _context.Employees.Remove(employee);
                _context.SaveChanges();
                LoadData();  // Refresh the data
            }
            else
            {
                MessageBox.Show("Please select an employee to delete.", "Delete Employee", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion

        #region Shipper CRUD
        private void RegisterShipper_Click(object sender, RoutedEventArgs e)
        {
            var addShipperWindow = new AddShipperWindow(_context);
            if (addShipperWindow.ShowDialog() == true) // If Save is successful
            {
                LoadData();  // Refresh the data
            }
        }

        private void UpdateShipper_Click(object sender, RoutedEventArgs e)
        {
            if (ShippersListView.SelectedItem is Shipper selectedShipper)
            {
                var editShipperWindow = new AddShipperWindow(_context, selectedShipper);
                if (editShipperWindow.ShowDialog() == true) // If Save is successful
                {
                    LoadData();  // Refresh the data
                }
            }
            else
            {
                MessageBox.Show("Please select a shipper to update.", "Update Shipper", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteShipper_Click(object sender, RoutedEventArgs e)
        {
            if (ShippersListView.SelectedItem is Shipper selectedShipper)
            {
                var shipper = _context.Shippers.First(s => s.ShipperID == selectedShipper.ShipperID);
                _context.Shippers.Remove(shipper);
                _context.SaveChanges();
                LoadData();  // Refresh the data
            }
            else
            {
                MessageBox.Show("Please select a shipper to delete.", "Delete Shipper", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion

        #region OrderDetail CRUD
        private void AddOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersListView.SelectedItem is Order selectedOrder)
            {
                var addOrderDetailWindow = new AddOrderDetailWindow(_context, selectedOrder);
                if (addOrderDetailWindow.ShowDialog() == true)
                {
                    LoadData();  // Refresh the data
                }
            }
        }

        //private void EditOrderDetail_Click(object sender, RoutedEventArgs e)
        //{
        //    if (OrderDetailsListView.SelectedItem is Order_Detail selectedOrderDetail)
        //    {
        //        var editOrderDetailWindow = new AddOrderDetailWindow(_context, selectedOrderDetail);
        //        if (editOrderDetailWindow.ShowDialog() == true)
        //        {
        //            LoadData();  // Refresh the data
        //        }
        //    }
        //}

        //private void DeleteOrderDetail_Click(object sender, RoutedEventArgs e)
        //{
        //    if (OrderDetailsListView.SelectedItem is Order_Detail selectedOrderDetail)
        //    {
        //        var orderDetail = _context.Order_Details.First(od => od.OrderID == selectedOrderDetail.OrderID && od.ProductID == selectedOrderDetail.ProductID);
        //        _context.Order_Details.Remove(orderDetail);
        //        _context.SaveChanges();
        //        LoadData();  // Refresh UI
        //    }
        //}
        #endregion
    }
}


//using Global_Goods.Data;
//using Global_Goods.Models;
//using Global_Goods.Views;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;
//using System.Windows;

//namespace Global_Goods
//{
//    public partial class MainWindow : Window
//    {
//        private readonly ApplicationDbContext _context;

//        public MainWindow()
//        {
//            InitializeComponent();
//            _context = new ApplicationDbContext();

//            LoadData();
//        }

//        private void LoadData()
//        {
//            // Load Products
//            ProductList.ItemsSource = _context.Products
//                .Include(p => p.Category)
//                .Include(p => p.Supplier)
//                .Select(p => new
//                {
//                    p.ProductID,
//                    p.ProductName,
//                    CategoryName = p.Category.CategoryName,
//                    SupplierName = p.Supplier.SupplierName,
//                    p.Price
//                })
//                .ToList();

//            // Load Categories
//            CategoryList.ItemsSource = _context.Categories
//                .Select(c => new
//                {
//                    c.CategoryID,
//                    c.CategoryName
//                })
//                .ToList();

//            // Load Customers

//            CustomerList.ItemsSource = _context.Customers
//                .Select(c => new Customer
//                {
//                    CustomerID = c.CustomerID ?? 0,
//                    CustomerName = c.CustomerName ?? "No Name",
//                    ContactName = c.ContactName ?? "No Contact"
//                })
//                .ToList();
//            //CustomerList.ItemsSource = _context.Customers
//            //    .Select(c => new Customer
//            //    {
//            //        CustomerID = c.CustomerID,
//            //        CustomerName = c.CustomerName ?? "No Name",
//            //        ContactName = c.ContactName ?? "No Contact"
//            //    })
//            //    .ToList();

//            // Load Orders
//            OrderGrid.ItemsSource = _context.Orders
//                .Include(o => o.Customer)
//                .Include(o => o.Employee)
//                .Include(o => o.Shipper)
//                .Select(o => new
//                {
//                    o.OrderID,
//                    o.OrderDate,
//                    CustomerName = o.Customer.CustomerName,
//                    EmployeeName = o.Employee.FirstName + " " + o.Employee.LastName,
//                    ShipperName = o.Shipper.ShipperName
//                })
//                .ToList();

//            // Load Order Details
//            OrderDetailGrid.ItemsSource = _context.Order_Details
//                .Include(od => od.Product)
//                .Select(od => new
//                {
//                    od.OrderID,
//                    ProductName = od.Product.ProductName,
//                    od.Quantity,
//                    Price = od.Quantity * od.Product.Price
//                })
//                .ToList();
//        }

//        #region Product CRUD
//        private void AddProduct_Click(object sender, RoutedEventArgs e)
//        {
//            var addProductWindow = new AddProductWindow(_context);
//            if (addProductWindow.ShowDialog() == true) // If Save is successful
//            {
//                LoadData();  // Refresh the data
//            }
//        }

//        //private void EditProduct_Click(object sender, RoutedEventArgs e)
//        //{
//        //    if (ProductList.SelectedItem is Product selectedProduct)
//        //    {
//        //        var editProductWindow = new AddProductWindow(_context, selectedProduct);
//        //        if (editProductWindow.ShowDialog() == true) // If Save is successful
//        //        {
//        //            LoadData();  // Refresh the data
//        //        }
//        //    }
//        //}

//        private void EditProduct_Click(object sender, RoutedEventArgs e)
//        {
//            if (ProductList.SelectedItem is Product selectedProduct)
//            {
//                var editProductWindow = new AddProductWindow(_context, selectedProduct);

//                // Show the dialog and check if it was closed successfully (i.e., Save was clicked)
//                if (editProductWindow.ShowDialog() == true)
//                {
//                    // Refresh the product data list to reflect changes
//                    LoadData();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please select a product to edit.", "Edit Product", MessageBoxButton.OK, MessageBoxImage.Warning);
//            }
//        }

//        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
//        {
//            if (ProductList.SelectedItem is Product selectedProduct)
//            {
//                var product = _context.Products.First(p => p.ProductID == selectedProduct.ProductID);
//                _context.Products.Remove(product);
//                _context.SaveChanges();
//                LoadData();  // Refresh UI
//            }
//        }
//        #endregion

//        #region Category CRUD
//        private void AddCategory_Click(object sender, RoutedEventArgs e)
//        {
//            var addCategoryWindow = new AddCategoryWindow(_context);
//            if (addCategoryWindow.ShowDialog() == true)
//            {
//                LoadData(); // Refresh the category list after adding a new category
//            }
//        }

//        private void EditCategory_Click(object sender, RoutedEventArgs e)
//        {
//            if (CategoryList.SelectedItem is Category selectedCategory)
//            {
//                var editCategoryWindow = new AddCategoryWindow(_context, selectedCategory);
//                if (editCategoryWindow.ShowDialog() == true)
//                {
//                    LoadData(); // Refresh the category list after editing
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please select a category to edit.");
//            }
//        }

//        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
//        {
//            if (CategoryList.SelectedItem is Category selectedCategory)
//            {
//                var category = _context.Categories.First(c => c.CategoryID == selectedCategory.CategoryID);
//                _context.Categories.Remove(category);
//                _context.SaveChanges();
//                LoadData();  // Refresh UI
//            }
//        }
//        #endregion

//        #region Customer CRUD
//        private void AddCustomer_Click(object sender, RoutedEventArgs e)
//        {
//            var addCustomerWindow = new AddCustomerWindow(_context);
//            if (addCustomerWindow.ShowDialog() == true)
//            {
//                LoadData();  // Refresh the data
//            }
//        }

//        private void EditCustomer_Click(object sender, RoutedEventArgs e)
//        {
//            if (CustomerList.SelectedItem is Customer selectedCustomer)
//            {
//                var editCustomerWindow = new AddCustomerWindow(_context, selectedCustomer);
//                if (editCustomerWindow.ShowDialog() == true)
//                {
//                    LoadData();  // Refresh the data
//                }
//            }
//        }

//        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
//        {
//            if (CustomerList.SelectedItem is Customer selectedCustomer)
//            {
//                var customer = _context.Customers.First(c => c.CustomerID == selectedCustomer.CustomerID);
//                _context.Customers.Remove(customer);
//                _context.SaveChanges();
//                LoadData();  // Refresh UI
//            }
//        }
//        #endregion

//        #region Order CRUD
//        private void AddOrder_Click(object sender, RoutedEventArgs e)
//        {
//            var addOrderWindow = new AddOrderWindow(_context);
//            if (addOrderWindow.ShowDialog() == true)
//            {
//                LoadData();  // Refresh the data
//            }
//        }

//        private void EditOrder_Click(object sender, RoutedEventArgs e)
//        {
//            if (OrderGrid.SelectedItem is Order selectedOrder)
//            {
//                var editOrderWindow = new AddOrderWindow(_context, selectedOrder);
//                if (editOrderWindow.ShowDialog() == true)
//                {
//                    LoadData();  // Refresh the data
//                }
//            }
//        }

//        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
//        {
//            if (OrderGrid.SelectedItem is Order selectedOrder)
//            {
//                var order = _context.Orders.First(o => o.OrderID == selectedOrder.OrderID);
//                _context.Orders.Remove(order);
//                _context.SaveChanges();
//                LoadData();  // Refresh UI
//            }
//        }
//        #endregion

//        //#region OrderDetail CRUD
//        private void AddOrderDetail_Click(object sender, RoutedEventArgs e)
//        {
//            if (OrderGrid.SelectedItem is Order selectedOrder)
//            {
//                var addOrderDetailWindow = new AddOrderDetailWindow(_context, selectedOrder);
//                if (addOrderDetailWindow.ShowDialog() == true)
//                {
//                    LoadData();  // Refresh the data
//                }
//            }
//        }

//        private void EditOrderDetail_Click(object sender, RoutedEventArgs e)
//        {
//            if (OrderDetailGrid.SelectedItem is Order_Detail selectedOrderDetail)
//            {
//                var editOrderDetailWindow = new AddOrderDetailWindow(_context, selectedOrderDetail);
//                if (editOrderDetailWindow.ShowDialog() == true)
//                {
//                    LoadData();  // Refresh the data
//                }
//            }
//        }

//        private void DeleteOrderDetail_Click(object sender, RoutedEventArgs e)
//        {
//            if (OrderDetailGrid.SelectedItem is Order_Detail selectedOrderDetail)
//            {
//                var orderDetail = _context.Order_Details.First(od => od.OrderID == selectedOrderDetail.OrderID && od.ProductID == selectedOrderDetail.ProductID);
//                _context.Order_Details.Remove(orderDetail);
//                _context.SaveChanges();
//                LoadData();  // Refresh UI
//            }
//        }

//    }
//}


//using Global_Goods.Data;
//using Global_Goods.Models;
//using Global_Goods.ViewModels;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;
//using System.Windows;

//namespace Global_Goods
//{
//    public partial class MainWindow : Window
//    {
//        private readonly ApplicationDbContext _context;

//        public MainWindow()
//        {
//            InitializeComponent();
//            _context = new ApplicationDbContext();

//            LoadData();
//        }

//        private void LoadData()
//        {
//            // Load Products
//            ProductList.ItemsSource = _context.Products
//                .Include(p => p.Category)
//                .Include(p => p.Supplier)
//                .Select(p => new
//                {
//                    p.ProductID,
//                    p.ProductName,
//                    CategoryName = p.Category.CategoryName,
//                    SupplierName = p.Supplier.SupplierName,
//                    p.Price
//                })
//                .ToList();

//            // Load Categories
//            CategoryList.ItemsSource = _context.Categories
//                .Select(c => new
//                {
//                    c.CategoryID,
//                    c.CategoryName
//                })
//                .ToList();

//            // Load Customers
//            CustomerList.ItemsSource = _context.Customers
//                .Select(c => new Customer
//                {
//                    CustomerID = c.CustomerID,
//                    CustomerName = c.CustomerName ?? "No Name",
//                    ContactName = c.ContactName ?? "No Contact"
//                })
//                .ToList();


//            // Load Orders
//            OrderGrid.ItemsSource = _context.Orders
//                .Include(o => o.Customer)
//                .Include(o => o.Employee)
//                .Include(o => o.Shipper)
//                .Select(o => new
//                {
//                    o.OrderID,
//                    o.OrderDate,
//                    CustomerName = o.Customer.CustomerName,
//                    EmployeeName = o.Employee.FirstName + " " + o.Employee.LastName,
//                    ShipperName = o.Shipper.ShipperName
//                })
//                .ToList();

//            // Load Order Details
//            OrderDetailGrid.ItemsSource = _context.Order_Details
//                .Include(od => od.Product)
//                .Select(od => new
//                {
//                    od.OrderID,
//                    ProductName = od.Product.ProductName,
//                    od.Quantity,
//                    Price = od.Quantity * od.Product.Price
//                })
//                .ToList();
//        }

//        private void AddProduct_Click(object sender, RoutedEventArgs e)
//        {
//            var addProductWindow = new AddProductWindow(_context);
//            if (addProductWindow.ShowDialog() == true) // If Save is successful
//            {
//                LoadData();  // Refresh the data
//            }
//        }

//        private void EditProduct_Click(object sender, RoutedEventArgs e)
//        {
//            if (ProductList.SelectedItem is Product selectedProduct)
//            {
//                var editProductWindow = new AddProductWindow(_context, selectedProduct);
//                if (editProductWindow.ShowDialog() == true) // If Save is successful
//                {
//                    LoadData();  // Refresh the data
//                }
//            }
//        }

//        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
//        {
//            var selectedProduct = ProductList.SelectedItem as Product;
//            if (selectedProduct != null)
//            {
//                var product = _context.Products.First(p => p.ProductID == selectedProduct.ProductID);
//                _context.Products.Remove(product);
//                _context.SaveChanges();
//                LoadData();  // Refresh UI
//            }
//        }




//        private void AddCategory_Click(object sender, RoutedEventArgs e)
//        {
//            var addCategoryWindow = new AddCategoryWindow(_context);
//            if (addCategoryWindow.ShowDialog() == true)
//            {
//                LoadData(); // Refresh the category list after adding a new category
//            }
//        }

//        // Method to open AddCategoryWindow for editing an existing category
//        private void EditCategory_Click(object sender, RoutedEventArgs e)
//        {
//            var selectedCategory = CategoryList.SelectedItem as Category;
//            if (selectedCategory != null)
//            {
//                var editCategoryWindow = new AddCategoryWindow(_context, selectedCategory);
//                if (editCategoryWindow.ShowDialog() == true)
//                {
//                    LoadData(); // Refresh the category list after editing
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please select a category to edit.");
//            }

//    }

//        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
//        {
//            var selectedCategory = CategoryList.SelectedItem as Category;
//            if (selectedCategory != null)
//            {
//                var category = _context.Categories.First(c => c.CategoryID == selectedCategory.CategoryID);
//                _context.Categories.Remove(category);
//                _context.SaveChanges();
//                LoadData();  // Refresh UI
//            }
//        }




//        private void AddCustomer_Click(object sender, RoutedEventArgs e)
//        {
//            var newCustomer = new Customer { CustomerName = "New Customer" };
//            _context.Customers.Add(newCustomer);
//            _context.SaveChanges();
//            LoadData();  // Refresh UI
//        }

//        private void EditCustomer_Click(object sender, RoutedEventArgs e)
//        {
//            var selectedCustomer = CustomerList.SelectedItem as Customer;
//            if (selectedCustomer != null)
//            {
//                var customer = _context.Customers.First(c => c.CustomerID == selectedCustomer.CustomerID);
//                customer.CustomerName = "Updated Customer";  // Replace with real input
//                _context.SaveChanges();
//                LoadData();  // Refresh UI
//            }
//        }

//        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
//        {
//            var selectedCustomer = CustomerList.SelectedItem as Customer;
//            if (selectedCustomer != null)
//            {
//                var customer = _context.Customers.First(c => c.CustomerID == selectedCustomer.CustomerID);
//                _context.Customers.Remove(customer);
//                _context.SaveChanges();
//                LoadData();  // Refresh UI
//            }
//        }



//        #region Order CRUD

//        private void AddOrder_Click(object sender, RoutedEventArgs e)
//        {
//            var newOrder = new Order { CustomerID = 1, OrderDate = DateTime.Now };
//            _context.Orders.Add(newOrder);
//            _context.SaveChanges();
//            LoadData();  // Refresh UI
//        }

//        private void EditOrder_Click(object sender, RoutedEventArgs e)
//        {
//            var selectedOrder = OrderGrid.SelectedItem as Order;
//            if (selectedOrder != null)
//            {
//                var order = _context.Orders.First(o => o.OrderID == selectedOrder.OrderID);
//                order.OrderDate = DateTime.Now;  // Replace with real input
//                _context.SaveChanges();
//                LoadData();  // Refresh UI
//            }
//        }

//        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
//        {
//            var selectedOrder = OrderGrid.SelectedItem as Order;
//            if (selectedOrder != null)
//            {
//                var order = _context.Orders.First(o => o.OrderID == selectedOrder.OrderID);
//                _context.Orders.Remove(order);
//                _context.SaveChanges();
//                LoadData();  // Refresh UI
//            }
//        }

//        #endregion

//        #region OrderDetail CRUD

//        private void AddOrderDetail_Click(object sender, RoutedEventArgs e)
//        {
//            var selectedOrder = OrderGrid.SelectedItem as Order;
//            if (selectedOrder != null)
//            {
//                var newOrderDetail = new Order_Detail { OrderID = selectedOrder.OrderID, ProductID = 1, Quantity = 1 };  // Replace with real input
//                _context.Order_Details.Add(newOrderDetail);
//                _context.SaveChanges();
//                LoadData();  // Refresh UI
//            }
//        }

//        private void EditOrderDetail_Click(object sender, RoutedEventArgs e)
//        {
//            var selectedOrderDetail = OrderDetailGrid.SelectedItem as Order_Detail;
//            if (selectedOrderDetail != null)
//            {
//                var orderDetail = _context.Order_Details.First(od => od.OrderID == selectedOrderDetail.OrderID && od.ProductID == selectedOrderDetail.ProductID);
//                orderDetail.Quantity = 2;  // Replace with real input
//                _context.SaveChanges();
//                LoadData();  // Refresh UI
//            }
//        }

//        private void DeleteOrderDetail_Click(object sender, RoutedEventArgs e)
//        {
//            var selectedOrderDetail = OrderDetailGrid.SelectedItem as Order_Detail;
//            if (selectedOrderDetail != null)
//            {
//                var orderDetail = _context.Order_Details.First(od => od.OrderID == selectedOrderDetail.OrderID && od.ProductID == selectedOrderDetail.ProductID);
//                _context.Order_Details.Remove(orderDetail);
//                _context.SaveChanges();
//                LoadData();  // Refresh UI
//            }
//        }

//        #endregion
//    }
//}
