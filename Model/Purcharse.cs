using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Purchase
    {
        public int Id { get; set; }              
        public int IdUser { get; set; }           
        public DateTime date { get; set; }     
        public decimal Total { get; set; }        
        public string State { get; set; }            
        public List<PurchaseDetail> Details { get; set; } 
    }
}
