using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_Goods.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public DateTime OrderDate { get; set; }
        public int ShipperID { get; set; }
        public Shipper Shipper { get; set; }

  
   

        // Navigation Property
        public ICollection<Order_Detail> Order_Details { get; set; }
    }
}
