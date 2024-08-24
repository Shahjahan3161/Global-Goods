using Global_Goods.Models;
using Global_Goods.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Global_Goods.ViewModels
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private readonly ApplicationDbContext _context;

        public ObservableCollection<Customer> Customers { get; set; }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
            }
        }

        public CustomerViewModel()
        {
            _context = new ApplicationDbContext();
            LoadCustomers();
            SelectedCustomer = new Customer(); // Initialize to a new Customer
        }

        public void LoadCustomers()
        {
            Customers = new ObservableCollection<Customer>(_context.Customers.ToList());
        }

        public void SaveCustomer()
        {
            if (SelectedCustomer.CustomerID == 0)
            {
                _context.Customers.Add(SelectedCustomer);
            }
            else
            {
                _context.Entry(SelectedCustomer).State = EntityState.Modified;
            }
            _context.SaveChanges();
            LoadCustomers(); // Refresh the customer list
            SelectedCustomer = new Customer(); // Reset after saving
        }

        public void DeleteCustomer()
        {
            if (SelectedCustomer != null && SelectedCustomer.CustomerID != 0)
            {
                _context.Customers.Remove(SelectedCustomer);
                _context.SaveChanges();
                LoadCustomers(); // Refresh the customer list
                SelectedCustomer = new Customer(); // Reset after deleting
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
