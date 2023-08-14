using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestProjectAPI.Models
{
    public class Response
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Array data { get; set; }
    }

    }