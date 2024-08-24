// ViewModels/OrderViewModel.cs
using Global_Goods.Models;
using Global_Goods.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Global_Goods.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        private readonly ApplicationDbContext _context;

        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<Shipper> Shippers { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }

        public OrderViewModel()
        {
            _context = new ApplicationDbContext();
            LoadOrders();
            LoadCustomers();
            LoadShippers();
            LoadEmployees();
            SelectedOrder = new Order();
        }

        public void LoadOrders()
        {
            Orders = new ObservableCollection<Order>(_context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Shipper)
                .Include(o => o.Employee)
                .ToList());
        }

        public void LoadCustomers()
        {
            Customers = new ObservableCollection<Customer>(_context.Customers.ToList());
        }

        public void LoadShippers()
        {
            Shippers = new ObservableCollection<Shipper>(_context.Shippers.ToList());
        }

        public void LoadEmployees()
        {
            Employees = new ObservableCollection<Employee>(_context.Employees.ToList());
        }

        //public ICommand SaveOrderCommand => new RelayCommand(SaveOrder);

        private void SaveOrder()
        {
            if (SelectedOrder.OrderID == 0)
            {
                _context.Orders.Add(SelectedOrder);
            }
            else
            {
                _context.Entry(SelectedOrder).State = EntityState.Modified;
            }
            _context.SaveChanges();
            LoadOrders();
            SelectedOrder = new Order(); // Reset after saving
        }

      //  public ICommand DeleteOrderCommand => new RelayCommand(DeleteOrder);

        private void DeleteOrder()
        {
            if (SelectedOrder != null)
            {
                _context.Orders.Remove(SelectedOrder);
                _context.SaveChanges();
                LoadOrders();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
