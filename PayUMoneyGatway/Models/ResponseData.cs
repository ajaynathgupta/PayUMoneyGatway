using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayUMoneyGatway.Models
{
    public class ResponseData
    {
        public string mode { get; set; }
        public string status { get; set; }
        public string key { get; set; }
        public string txnid { get; set; }
        public string amount { get; set; }
        public string productinfo { get; set; }
        public string firstname { get; set; }
        public string hash { get; set; }
        public string Error { get; set; }
        public string PG_TYPE { get; set; }
        public string bank_ref_num { get; set; }
        public string payuMoneyId { get; set; }
        public string additionalCharges { get; set; }
    }
}