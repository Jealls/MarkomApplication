using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomApplication.ViewModel
{
    public class CompanyViewModel
    {
        public int id { get; set; }

        [DisplayName("Company Code")]
        public string code { get; set; }

        [DisplayName("Company Name")]
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool isDelete { get; set; }

        [DisplayName("Create By")]
        public string createBy { get; set; }

        [DisplayName("Create Date")]
        public System.DateTime createDate { get; set; }
        public string updateBy { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
    }
}
