// ViewModels/MainViewModel.cs
using Global_Goods.Models;
using Global_Goods.Data;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Global_Goods.ViewModels
{
    public class MainViewModel
    {
        private readonly ApplicationDbContext _context;

        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<Supplier> Suppliers { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Shipper> Shippers { get; set; }

        public Order SelectedOrder { get; set; }
        public Product SelectedProduct { get; set; }
        public Category SelectedCategory { get; set; }
        public Customer SelectedCustomer { get; set; }
        public Supplier SelectedSupplier { get; set; }
        public Employee SelectedEmployee { get; set; }
        public Shipper SelectedShipper { get; set; }

        public MainViewModel()
        {
            _context = new ApplicationDbContext();

            try
            {
                LoadOrders();
                LoadProducts();
                LoadCategories();
                LoadCustomers();
                LoadSuppliers();
                LoadEmployees();
                LoadShippers();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing MainViewModel: {ex.Message}");
            }
        }


        // Orders
        public void LoadOrders()
        {
            Orders = new ObservableCollection<Order>(_context.Orders.Include(o => o.Customer).ToList());
        }

        public void LoadOrderProducts()
        {
            if (SelectedOrder != null)
            {
                Orders = new ObservableCollection<Order>(_context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Shipper)
                .ToList());
                OnPropertyChanged(nameof(Orders));
            }
        }

        // Products
        public void LoadProducts()
        {
            Products = new ObservableCollection<Product>(_context.Products.Include(p => p.Category).ToList());
        }

        public void AddProduct()
        {
            SelectedProduct = new Product();
            // Logic to add new product
        }

        public void UpdateProduct()
        {
            if (SelectedProduct != null)
            {
                _context.Entry(SelectedProduct).State = EntityState.Modified;
                _context.SaveChanges();
                LoadProducts();
            }
        }

        public void DeleteProduct()
        {
            if (SelectedProduct != null)
            {
                _context.Products.Remove(SelectedProduct);
                _context.SaveChanges();
                LoadProducts();
            }
        }

        // Categories
        public void LoadCategories()
        {
            Categories = new ObservableCollection<Category>(_context.Categories.ToList());
        }

        public void AddCategory()
        {
            SelectedCategory = new Category();
            // Logic to add new category
        }

        public void UpdateCategory()
        {
            if (SelectedCategory != null)
            {
                _context.Entry(SelectedCategory).State = EntityState.Modified;
                _context.SaveChanges();
                LoadCategories();
            }
        }

        public void DeleteCategory()
        {
            if (SelectedCategory != null)
            {
                _context.Categories.Remove(SelectedCategory);
                _context.SaveChanges();
                LoadCategories();
            }
        }

        // Customers
        public void LoadCustomers()
        {
            Customers = new ObservableCollection<Customer>(_context.Customers.ToList());
        }

        public void RegisterCustomer()
        {
            SelectedCustomer = new Customer();
            // Logic to register new customer
        }

        public void UpdateCustomer()
        {
            if (SelectedCustomer != null)
            {
                _context.Entry(SelectedCustomer).State = EntityState.Modified;
                _context.SaveChanges();
                LoadCustomers();
            }
        }

        public void DeleteCustomer()
        {
            if (SelectedCustomer != null)
            {
                _context.Customers.Remove(SelectedCustomer);
                _context.SaveChanges();
                LoadCustomers();
            }
        }

        // Suppliers
        public void LoadSuppliers()
        {
            Suppliers = new ObservableCollection<Supplier>(_context.Suppliers.ToList());
        }

        public void RegisterSupplier()
        {
            SelectedSupplier = new Supplier();
            // Logic to register new supplier
        }

        public void UpdateSupplier()
        {
            if (SelectedSupplier != null)
            {
                _context.Entry(SelectedSupplier).State = EntityState.Modified;
                _context.SaveChanges();
                LoadSuppliers();
            }
        }

        public void DeleteSupplier()
        {
            if (SelectedSupplier != null)
            {
                _context.Suppliers.Remove(SelectedSupplier);
                _context.SaveChanges();
                LoadSuppliers();
            }
        }

        // Employees
        public void LoadEmployees()
        {
            Employees = new ObservableCollection<Employee>(_context.Employees.ToList());
        }

        public void RegisterEmployee()
        {
            SelectedEmployee = new Employee();
            // Logic to register new employee
        }

        public void UpdateEmployee()
        {
            if (SelectedEmployee != null)
            {
                _context.Entry(SelectedEmployee).State = EntityState.Modified;
                _context.SaveChanges();
                LoadEmployees();
            }
        }

        public void DeleteEmployee()
        {
            if (SelectedEmployee != null)
            {
                _context.Employees.Remove(SelectedEmployee);
                _context.SaveChanges();
                LoadEmployees();
            }
        }

        // Shippers
        public void LoadShippers()
        {
            Shippers = new ObservableCollection<Shipper>(_context.Shippers.ToList());
        }

        public void RegisterShipper()
        {
            SelectedShipper = new Shipper();
            // Logic to register new shipper
        }

        public void UpdateShipper()
        {
            if (SelectedShipper != null)
            {
                _context.Entry(SelectedShipper).State = EntityState.Modified;
                _context.SaveChanges();
                LoadShippers();
            }
        }

        public void DeleteShipper()
        {
            if (SelectedShipper != null)
            {
                _context.Shippers.Remove(SelectedShipper);
                _context.SaveChanges();
                LoadShippers();
            }
        }

        // Implementing the INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
