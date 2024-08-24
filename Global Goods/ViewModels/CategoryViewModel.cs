// ViewModels/CategoryViewModel.cs
using Global_Goods.Models;
using Global_Goods.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace Global_Goods.ViewModels
{
    public class CategoryViewModel : INotifyPropertyChanged
    {
        private readonly ApplicationDbContext _context;

        public ObservableCollection<Category> Categories { get; set; }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public CategoryViewModel()
        {
            _context = new ApplicationDbContext();
            LoadCategories();
            SelectedCategory = new Category();
        }

        public void LoadCategories()
        {
            Categories = new ObservableCollection<Category>(_context.Categories.ToList());
        }

       

        private void SaveCategory()
        {
            if (SelectedCategory.CategoryID == 0)
            {
                _context.Categories.Add(SelectedCategory);
            }
            else
            {
                _context.Entry(SelectedCategory).State = EntityState.Modified;
            }
            _context.SaveChanges();
            LoadCategories();
            SelectedCategory = new Category(); // Reset after saving
        }

      

        private void DeleteCategory()
        {
            if (SelectedCategory != null)
            {
                _context.Categories.Remove(SelectedCategory);
                _context.SaveChanges();
                LoadCategories();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
