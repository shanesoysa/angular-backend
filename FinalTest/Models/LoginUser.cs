using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalTest.Models
{
    public class LoginUser
    {
        public int id { get; set; }
        public string name { get; set; }
        public string uname { get; set; }
        public string pass { get; set; }

        public string repass { get; set; }
        public string auth { get; set; }
        public string hint { get; set; }
        public string email { get; set; }
        public string status { get; set; }
        public string IdentfyNo { get; set; }



    }
}