using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_Goods.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }
        public string Notes { get; set; }

        // Navigation Property
        public ICollection<Order> Orders { get; set; }
    }
}
