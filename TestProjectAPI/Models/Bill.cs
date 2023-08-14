using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestProjectAPI.Models
{
    public class Bill
    {

        public int Bill_ID { get; set; }

        public string Staff { get; set; }
        public string ServiceProvider { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string Amount { get; set; }
        public string BillDate { get; set; }
        public string Timestamp { get; set; }
    }
}