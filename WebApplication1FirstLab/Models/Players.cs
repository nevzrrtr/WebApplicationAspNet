using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1FirstLab.Models
{
    public class Players
    {
        public string playerID { get; set; }
        public string jersey { get; set; }
        public string fname { get; set; }
        public string sname { get; set; }
        public string position { get; set; }
        public string birthday { get; set; }
        public string weight { get; set; }
        public string height { get; set; }
        public string birthcity { get; set; } // for only date use .ToShortDateString()
        public string birthstate { get; set; }
    }
}
