using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayUMoneyGatway.Models
{
    public class RequestData
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public double Amount { get; set; }
    }
}