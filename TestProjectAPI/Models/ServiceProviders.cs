using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestProjectAPI.Models
{
    public class ServiceProviders
    {

        public int id { get; set; }
        [Required]
        public string ServiceProviderName { get; set; }
        public string ServiceProviderAddress { get; set; }
        public string ServiceProviderContact { get; set; }
    }
}