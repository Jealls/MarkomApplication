using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomApplication.ViewModel
{
    public class CompanyListViewModel
    {
        public int id { get; set; }

        [DisplayName("Company Code")]
        public string code { get; set; }


        [Required(ErrorMessage = "Input data Nama Perusahaan")]
        [DisplayName("Company Name")]
        public string name { get; set; }

        [DisplayName("Create By")]
        public string createBy { get; set; }

        [DisplayName("Create Date")]
        public Nullable<DateTime> createDate { get; set; }
    }
}
