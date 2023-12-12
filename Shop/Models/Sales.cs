using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Sales
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public DateTime OrderDate { get; set; }

        public int Quantity { get; set; }

        public int UnitPrice { get; set; }

        public int Discount  { get; set; }

        public string CompanyName { get; set; }

        public string CustomerAddress { get; set; }

        public string CustomerCity  { get; set; }

        public string CustomerCountry { get; set; }

        public string EmployeeLastName { get; set; }

        public string EmployeeFirstName { get; set; }
    }
}