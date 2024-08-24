//// ViewModels/ProductViewModel.cs
//using Global_Goods.Models;
//using Global_Goods.Data;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Windows.Input;

//namespace Global_Goods.ViewModels
//{
//    public class ProductViewModel : INotifyPropertyChanged
//    {
//        private readonly ApplicationDbContext _context;

//        public ObservableCollection<Product> Products { get; set; }
//        public ObservableCollection<Category> Categories { get; set; }

//        private Product _selectedProduct;
//        public Product SelectedProduct
//        {
//            get { return _selectedProduct; }
//            set
//            {
//                _selectedProduct = value;
//                OnPropertyChanged(nameof(SelectedProduct));
//            }
//        }

//        public ProductViewModel()
//        {
//            _context = new ApplicationDbContext();
//            LoadProducts();
//            LoadCategories();
//            SelectedProduct = new Product();
//        }

//        public void LoadProducts()
//        {
//            Products = new ObservableCollection<Product>(_context.Products.Include(p => p.Category).ToList());
//        }

//        public void LoadCategories()
//        {
//            Categories = new ObservableCollection<Category>(_context.Categories.ToList());
//        }

//        public ICommand SaveProductCommand => new RelayCommand(SaveProduct);

//        private void SaveProduct()
//        {
//            if (SelectedProduct.ProductId == 0)
//            {
//                _context.Products.Add(SelectedProduct);
//            }
//            else
//            {
//                _context.Entry(SelectedProduct).State = EntityState.Modified;
//            }
//            _context.SaveChanges();
//            LoadProducts();
//            SelectedProduct = new Product(); // Reset after saving
//        }

//        public ICommand DeleteProductCommand => new RelayCommand(DeleteProduct);

//        private void DeleteProduct()
//        {
//            if (SelectedProduct != null)
//            {
//                _context.Products.Remove(SelectedProduct);
//                _context.SaveChanges();
//                LoadProducts();
//            }
//        }

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected virtual void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}
