using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestProjectAPI.Models
{
    public class Company
    {

       
        public int id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        
        public string CompanyAddress { get; set; }
        public string CompanyContact { get; set; }
    }
}