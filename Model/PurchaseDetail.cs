﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PurchaseDetail
    {
        public int Id { get; set; }    
        public int IdPurcharse { get; set; }          
        public string CodProd { get; set; }  
        public int Quantity { get; set; }          
        public decimal Price { get; set; }

        public string ProductName { get; set; }
        public decimal Subtotal { get; set; }
    }
}
