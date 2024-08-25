using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_Goods.Models
{
    public class Customer
    {
        [Key]
        public int? CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        // Navigation Property
        public ICollection<Order> Orders { get; set; }
    }
}
