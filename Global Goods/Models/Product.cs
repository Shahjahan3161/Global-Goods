using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_Goods.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
  
        // Navigation Property
        public ICollection<Order_Detail> Order_Details { get; set; }
    }
}
