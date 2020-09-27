using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomApplication.ViewModel
{
    public class UnitViewModel
    {
        public int id { get; set; }

        [DisplayName("Unit Code")]
        public string code { get; set; }

        [Required(ErrorMessage = "Input data Nama Unit")]
        [DisplayName("Unit Name")]
        public string name { get; set; }
        public string description { get; set; }
        public bool isDelete { get; set; }

        [DisplayName("Create By")]
        public string createBy { get; set; }

        [DisplayName("Create Date")]
        public System.DateTime createDate { get; set; }
        public Nullable<System.DateTime> createDate2 { get; set; }
        public string updateBy { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
    }
}
