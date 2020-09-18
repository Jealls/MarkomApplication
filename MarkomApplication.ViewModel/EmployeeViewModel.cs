using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomApplication.ViewModel
{
    public class EmployeeViewModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Input data Id Number")]
        [DisplayName("Emp ID Number")]
        public string code { get; set; }

        [Required(ErrorMessage = "Input data Nama")]
        [DisplayName("First Name")]
        public string firstName { get; set; }

        [DisplayName("Last Name")]
        public string lastName { get; set; }

        [DisplayName("Employee Name")]
        public string fullName { get; set; }

        [Required(ErrorMessage = "Input data Nama Perusahaan")]
        [DisplayName("Company Name")]
        public Nullable<int> mCompanyId { get; set; }

        
        [DisplayName("Company Name")]
        public string companyName { get; set; }

        [DisplayName("Email")]
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
