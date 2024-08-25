using Global_Goods.Data;
using Global_Goods.Models;
using System.Windows;

namespace Global_Goods.Views
{
    public partial class AddEmployeeWindow : Window
    {
        private readonly ApplicationDbContext _context;
        private readonly Employee _employee;

        internal AddEmployeeWindow(ApplicationDbContext context, Employee employee = null)
        {
            InitializeComponent();
            _context = context;

            if (employee != null)  // Edit existing employee
            {
                _employee = employee;
                FirstNameTextBox.Text = _employee.FirstName;
                LastNameTextBox.Text = _employee.LastName;
                BirthDatePicker.SelectedDate = _employee.BirthDate;
                PhotoTextBox.Text = _employee.Photo;
                NotesTextBox.Text = _employee.Notes;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_employee != null)  // Editing an existing employee
            {
                var employee = _context.Employees.Find(_employee.EmployeeID);
                if (employee != null)
                {
                    employee.FirstName = FirstNameTextBox.Text;
                    employee.LastName = LastNameTextBox.Text;
                    employee.BirthDate = BirthDatePicker.SelectedDate ?? employee.BirthDate;
                    employee.Photo = PhotoTextBox.Text;
                    employee.Notes = NotesTextBox.Text;
                    _context.Entry(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }
            else  // Adding a new employee
            {
                var newEmployee = new Employee
                {
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    BirthDate = BirthDatePicker.SelectedDate ?? DateTime.Now,
                    Photo = PhotoTextBox.Text,
                    Notes = NotesTextBox.Text
                };

                _context.Employees.Add(newEmployee);
            }

            _context.SaveChanges();
            this.DialogResult = true;  // Close the dialog and return success
            this.Close();
        }
    }
}
