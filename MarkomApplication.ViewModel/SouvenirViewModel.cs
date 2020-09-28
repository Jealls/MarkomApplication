using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomApplication.ViewModel
{
    public class SouvenirViewModel
    {
        public int id { get; set; }

        [DisplayName("Souvenir Code")]
        public string code { get; set; }

        [Required(ErrorMessage = "Input data Nama Souvenir")]
        [DisplayName("Souvenir Name")]
        public string name { get; set; }

        [DisplayName("Description")]
        public string description { get; set; }

        [Required(ErrorMessage = "Input data Unit Name")]
        [DisplayName("Unit Name")]
        public int mUnitId { get; set; }

        public int? unitId { get; set; }
        public string unitName { get; set; }

        public bool isDelete { get; set; }

        [DisplayName("Create By")]
        public string createBy { get; set; }

        [DisplayName("Create Date")]
        public System.DateTime createDate { get; set; }

        [DisplayName("Create Date")]
        public Nullable<System.DateTime> createDate2 { get; set; }
        public string updateBy { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
    }
}
