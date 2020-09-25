using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomApplication.ViewModel
{
    public class MenuViewModel
    {
        public int id { get; set; }
        [DisplayName("Code")]
        public string code { get; set; }

        [DisplayName("Menu Name")]
        [Required(ErrorMessage = "Input data Menu Nama")]
        public string name { get; set; }

        [Required(ErrorMessage = "Input data Controller Name")]
        public string controller { get; set; }
        
        [DisplayName("Parent")]
        public Nullable<int> parentId { get; set; }
        public string parentName { get; set; }
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
