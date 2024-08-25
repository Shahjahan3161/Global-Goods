using Global_Goods.Data;
using Global_Goods.Models;
using System.Windows;

namespace Global_Goods
{
    public partial class AddCategoryWindow : Window
    {
        private readonly ApplicationDbContext _context;
        private readonly Category _category;

        internal AddCategoryWindow(ApplicationDbContext context, Category category = null)
        {
            InitializeComponent();
            _context = context;

            if (category != null)
            {
                _category = category;
                CategoryNameTextBox.Text = _category.CategoryName;
                DescriptionTextBox.Text = _category.Description;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_category != null)
            {
                // Editing an existing category
                var category = _context.Categories.Find(_category.CategoryID);
                if (category != null)
                {
                    category.CategoryName = CategoryNameTextBox.Text;
                    category.Description = DescriptionTextBox.Text;
                    _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }
            else
            {
                // Adding a new category
                var newCategory = new Category
                {
                    CategoryName = CategoryNameTextBox.Text,
                    Description = DescriptionTextBox.Text
                };

                _context.Categories.Add(newCategory);
            }

            _context.SaveChanges();
            this.DialogResult = true; // Close the dialog and return a success result
            this.Close();
        }
    }
}
