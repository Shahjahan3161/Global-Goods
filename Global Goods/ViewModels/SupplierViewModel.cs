//// ViewModels/SupplierViewModel.cs
//using Global_Goods.Models;
//using Global_Goods.Data;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Windows.Input;
//using Microsoft.EntityFrameworkCore;

//namespace Global_Goods.ViewModels
//{
//    public class SupplierViewModel : INotifyPropertyChanged
//    {
//        private readonly ApplicationDbContext _context;

//        public ObservableCollection<Supplier> Suppliers { get; set; }

//        private Supplier _selectedSupplier;
//        public Supplier SelectedSupplier
//        {
//            get { return _selectedSupplier; }
//            set
//            {
//                _selectedSupplier = value;
//                OnPropertyChanged(nameof(SelectedSupplier));
//            }
//        }

//        public SupplierViewModel()
//        {
//            _context = new ApplicationDbContext();
//            LoadSuppliers();
//            SelectedSupplier = new Supplier();
//        }

//        public void LoadSuppliers()
//        {
//            Suppliers = new ObservableCollection<Supplier>(_context.Suppliers.ToList());
//        }

//        public ICommand SaveSupplierCommand => new RelayCommand(SaveSupplier);

//        private void SaveSupplier()
//        {
//            if (SelectedSupplier.SupplierId == 0)
//            {
//                _context.Suppliers.Add(SelectedSupplier);
//            }
//            else
//            {
//                _context.Entry(SelectedSupplier).State = EntityState.Modified;
//            }
//            _context.SaveChanges();
//            LoadSuppliers();
//            SelectedSupplier = new Supplier(); // Reset after saving
//        }

//        public ICommand DeleteSupplierCommand => new RelayCommand(DeleteSupplier);

//        private void DeleteSupplier()
//        {
//            if (SelectedSupplier != null)
//            {
//                _context.Suppliers.Remove(SelectedSupplier);
//                _context.SaveChanges();
//                LoadSuppliers();
//            }
//        }

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected virtual void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}
