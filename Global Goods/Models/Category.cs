using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_Goods.Models
{
    public class Category
    {
        [Key]
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        // Navigation Property
        public ICollection<Product> Products { get; set; }
    }
}
