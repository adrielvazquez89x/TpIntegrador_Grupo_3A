using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Sale
    {
        public string OrderNumber { get; set; }
        public string Customer { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public string PaymentStatus { get; set; }
        public int Items { get; set; }
        public string DeliveryMethod { get; set; }
    }
}
