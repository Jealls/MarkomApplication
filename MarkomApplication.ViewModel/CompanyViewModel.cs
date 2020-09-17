using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MarkomApplication.ViewModel
{
    public class CompanyViewModel
    {
        public int id { get; set; }

        [DisplayName("Company Code")]
        public string code { get; set; }


        [Required(ErrorMessage = "Input data Nama Perusahaan")]
        [DisplayName("Company Name")]
        public string name { get; set; }

        public string address { get; set; }
        public string phone { get; set; }

        //[RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
        public string email { get; set; }
        public bool isDelete { get; set; }

        [DisplayName("Create By")]
        public string createBy { get; set; }

        [DisplayName("Create Date")]
        public System.DateTime createDate { get; set; }
        public Nullable<DateTime> createDate2 { get; set; }
        public string updateBy { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }


    }
}
