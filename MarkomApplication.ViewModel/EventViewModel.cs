using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomApplication.ViewModel
{
    public class EventViewModel
    {
        public int id { get; set; }
        //[Required(ErrorMessage = "Input data Nama Perusahaan")]
        [DisplayName("Transaction Code")]
        public string code { get; set; }
        
        [DisplayName("Event Name")]
        public string eventName { get; set; }
        
        [DisplayName("Event Start Date")]
        public Nullable<System.DateTime> startDate { get; set; }

        [DisplayName("Event End Date")]
        public Nullable<System.DateTime> endDate { get; set; }
        [DisplayName("Event Place")]
        public string place { get; set; }
        [DisplayName("Budget (Rp.)")]
        public Nullable<long> budget { get; set; }
        [DisplayName("Request By")]
        public int requestBy { get; set; }
        [DisplayName("Request Date")]
        public System.DateTime requestDate { get; set; }
        public Nullable<int> approvedBy { get; set; }
        public Nullable<System.DateTime> approveDate { get; set; }
        [DisplayName("Assign To")]
        public Nullable<int> assignTo { get; set; }
        public Nullable<System.DateTime> closedDate { get; set; }
        
        [DisplayName("Note")]
        public string note { get; set; }
        
        [DisplayName("Status")]
        public Nullable<int> status { get; set; }
        public string rejectReason { get; set; }
        public Nullable<bool> isDelete { get; set; }
        
        [DisplayName("Create By")]
        public string createBy { get; set; }
        
        [DisplayName("Create Date")]
        public Nullable<System.DateTime> createDate { get; set; }
        public string updateBy { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
    }
}
