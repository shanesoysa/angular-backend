using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalTest.Models
{
    public class BuyerMaster
    {
        public int BID { get; set; }
        public string designation { get; set; }
        public string buyer_name { get; set; }
        public string brand { get; set; }
        public string address { get; set; }
        public string contact1 { get; set; }

        public string contact2 { get; set; }
        public string email1 { get; set; }
        public string email2 { get; set; }
    }
}