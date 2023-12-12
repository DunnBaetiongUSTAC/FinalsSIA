using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public int CustomerId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime RequiredDate { get; set; }

        public DateTime ShippedDate { get; set; }

        public string ShipVia { get; set; }

        public string Freight { get; set; }

        public string ShipName { get; set; }

        public string ShipCity { get; set; }

        public string ShipRegion { get; set; }

        public int ShipPostalCode { get; set; }

        public string ShipCountry { get; set; }
    }
}