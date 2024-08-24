//// ViewModels/EmployeeViewModel.cs
//using Global_Goods.Models;
//using Global_Goods.Data;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Windows.Input;
//using Microsoft.EntityFrameworkCore;

//namespace Global_Goods.ViewModels
//{
//    public class EmployeeViewModel : INotifyPropertyChanged
//    {
//        private readonly ApplicationDbContext _context;

//        public ObservableCollection<Employee> Employees { get; set; }

//        private Employee _selectedEmployee;
//        public Employee SelectedEmployee
//        {
//            get { return _selectedEmployee; }
//            set
//            {
//                _selectedEmployee = value;
//                OnPropertyChanged(nameof(SelectedEmployee));
//            }
//        }

//        public EmployeeViewModel()
//        {
//            _context = new ApplicationDbContext();
//            LoadEmployees();
//            SelectedEmployee = new Employee();
//        }

//        public void LoadEmployees()
//        {
//            Employees = new ObservableCollection<Employee>(_context.Employees.ToList());
//        }

//        public ICommand SaveEmployeeCommand => new RelayCommand(SaveEmployee);

//        private void SaveEmployee()
//        {
//            if (SelectedEmployee.EmployeeId == 0)
//            {
//                _context.Employees.Add(SelectedEmployee);
//            }
//            else
//            {
//                _context.Entry(SelectedEmployee).State = EntityState.Modified;
//            }
//            _context.SaveChanges();
//            LoadEmployees();
//            SelectedEmployee = new Employee(); // Reset after saving
//        }

//        public ICommand DeleteEmployeeCommand => new RelayCommand(DeleteEmployee);

//        private void DeleteEmployee()
//        {
//            if (SelectedEmployee != null)
//            {
//                _context.Employees.Remove(SelectedEmployee);
//                _context.SaveChanges();
//                LoadEmployees();
//            }
//        }

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected virtual void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}
