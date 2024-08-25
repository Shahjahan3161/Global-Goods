using Global_Goods.Data;
using Global_Goods.Models;
using System.Windows;

namespace Global_Goods
{
    public partial class AddProductWindow : Window
    {
        private readonly ApplicationDbContext _context;
        private readonly Product _product;

        // working with dp on add
        //internal AddProductWindow(ApplicationDbContext context, Product product = null)
        //{
        //    InitializeComponent();
        //    _context = context;

        //    // Load Suppliers and Categories
        //    LoadSuppliersAndCategories();

        //    if (product != null)
        //    {
        //        _product = product;

        //        // Set the text fields
        //        ProductNameTextBox.Text = _product.ProductName;
        //        UnitTextBox.Text = _product.Unit;

        //        // Set the selected values for ComboBoxes
        //        SupplierComboBox.SelectedValue = _product.SupplierID;
        //        CategoryComboBox.SelectedValue = _product.CategoryID;
        //    }

        //}



        internal AddProductWindow(ApplicationDbContext context, Product product = null)
        {
            InitializeComponent();
            _context = context;

            // Load Suppliers and Categories into ComboBoxes
            LoadSuppliersAndCategories();

            if (product != null)  // Edit existing product
            {
                _product = product;

                // Populate fields with existing product data
                ProductNameTextBox.Text = _product.ProductName;
                UnitTextBox.Text = _product.Unit;

                // Set selected values for Supplier and Category based on the product
                SupplierComboBox.SelectedValue = _product.SupplierID;
                CategoryComboBox.SelectedValue = _product.CategoryID;
            }
        }


        // Method to load suppliers and categories from the database
        private void LoadSuppliersAndCategories()
        {
            // Load supplier data and bind it to SupplierComboBox
            var suppliers = _context.Suppliers.Select(s => new
            {
                SupplierID = s.SupplierID,
                SupplierName = s.SupplierName
            }).ToList();
            SupplierComboBox.ItemsSource = suppliers;
            SupplierComboBox.DisplayMemberPath = "SupplierName";
            SupplierComboBox.SelectedValuePath = "SupplierID";

            // Load category data and bind it to CategoryComboBox
            var categories = _context.Categories.Select(c => new
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName
            }).ToList();
            CategoryComboBox.ItemsSource = categories;
            CategoryComboBox.DisplayMemberPath = "CategoryName";
            CategoryComboBox.SelectedValuePath = "CategoryID";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_product != null)  // Editing an existing product
            {
                // Find the product in the database and update its fields
                var product = _context.Products.Find(_product.ProductID);
                if (product != null)
                {
                    product.ProductName = ProductNameTextBox.Text;
                    product.CategoryID = (int)CategoryComboBox.SelectedValue;
                    product.SupplierID = (int)SupplierComboBox.SelectedValue;
                    product.Unit = UnitTextBox.Text;
                    product.Price = decimal.Parse(PriceTextBox.Text);

                    // Mark the product entity as modified
                    _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }
            else  // Adding a new product
            {
                var newProduct = new Product
                {
                    ProductName = ProductNameTextBox.Text,
                    CategoryID = (int)CategoryComboBox.SelectedValue,
                    SupplierID = (int)SupplierComboBox.SelectedValue,
                    Unit = UnitTextBox.Text,
                    Price = decimal.Parse(PriceTextBox.Text)
                };

                // Add the new product to the context
                _context.Products.Add(newProduct);
            }

            // Save the changes to the database
            _context.SaveChanges();

            // Set the dialog result to true (indicating success) and close the window
            this.DialogResult = true;
            this.Close();
        }


        //working with ids
        //private void SaveButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (_product != null)
        //    {
        //        // Editing an existing product
        //        var product = _context.Products.Find(_product.ProductID);
        //        if (product != null)
        //        {
        //            product.ProductName = ProductNameTextBox.Text;
        //            product.CategoryID = int.Parse(CategoryIDTextBox.Text);
        //            product.SupplierID = int.Parse(SupplierIDTextBox.Text);
        //            product.Unit = UnitTextBox.Text;
        //            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //        }
        //    }
        //    else
        //    {
        //        // Adding a new product
        //        var newProduct = new Product
        //        {
        //            ProductName = ProductNameTextBox.Text,
        //            CategoryID = int.Parse(CategoryIDTextBox.Text),
        //            SupplierID = int.Parse(SupplierIDTextBox.Text),
        //            Unit = UnitTextBox.Text
        //        };

        //        _context.Products.Add(newProduct);
        //    }

        //    _context.SaveChanges();
        //    this.DialogResult = true; // Close the dialog and return a success result
        //    this.Close();
        //}

        // workig in insert case
        //private void SaveButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // If editing an existing product, update its properties
        //    if (_product != null)
        //    {
        //        _product.ProductName = ProductNameTextBox.Text;
        //        _product.CategoryID = int.Parse(CategoryIDTextBox.Text);
        //        _product.SupplierID = int.Parse(SupplierIDTextBox.Text);
        //        _product.Unit = UnitTextBox.Text;
        //    }
        //    else // Add a new product
        //    {
        //        var newProduct = new Product
        //        {
        //            ProductName = ProductNameTextBox.Text,
        //            CategoryID = int.Parse(CategoryIDTextBox.Text),
        //            SupplierID = int.Parse(SupplierIDTextBox.Text),
        //            Unit = UnitTextBox.Text
        //        };

        //        _context.Products.Add(newProduct);
        //    }

        //    _context.SaveChanges();
        //    this.DialogResult = true; // Close the dialog and return a success result
        //    this.Close();
        //}
    }
}
