using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_Goods.Models
{
    public class Shipper
    {
        [Key]
        public int ShipperID { get; set; }
        public string ShipperName { get; set; }
        public string Phone { get; set; }
        // Navigation Property
        public ICollection<Order> Orders { get; set; }
    }
}
