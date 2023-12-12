using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public int UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal Discount { get; set; }
    }
}