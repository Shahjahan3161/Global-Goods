//// ViewModels/ShipperViewModel.cs
//using Global_Goods.Models;
//using Global_Goods.Data;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Windows.Input;
//using Microsoft.EntityFrameworkCore;

//namespace Global_Goods.ViewModels
//{
//    public class ShipperViewModel : INotifyPropertyChanged
//    {
//        private readonly ApplicationDbContext _context;

//        public ObservableCollection<Shipper> Shippers { get; set; }

//        private Shipper _selectedShipper;
//        public Shipper SelectedShipper
//        {
//            get { return _selectedShipper; }
//            set
//            {
//                _selectedShipper = value;
//                OnPropertyChanged(nameof(SelectedShipper));
//            }
//        }

//        public ShipperViewModel()
//        {
//            _context = new ApplicationDbContext();
//            LoadShippers();
//            SelectedShipper = new Shipper();
//        }

//        public void LoadShippers()
//        {
//            Shippers = new ObservableCollection<Shipper>(_context.Shippers.ToList());
//        }

//        public ICommand SaveShipperCommand => new RelayCommand(SaveShipper);

//        private void SaveShipper()
//        {
//            if (SelectedShipper.ShipperId == 0)
//            {
//                _context.Shippers.Add(SelectedShipper);
//            }
//            else
//            {
//                _context.Entry(SelectedShipper).State = EntityState.Modified;
//            }
//            _context.SaveChanges();
//            LoadShippers();
//            SelectedShipper = new Shipper(); // Reset after saving
//        }

//        public ICommand DeleteShipperCommand => new RelayCommand(DeleteShipper);

//        private void DeleteShipper()
//        {
//            if (SelectedShipper != null)
//            {
//                _context.Shippers.Remove(SelectedShipper);
//                _context.SaveChanges();
//                LoadShippers();
//            }
//        }

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected virtual void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}
