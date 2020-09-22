using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomApplication.ViewModel
{
    public class EventViewModel
    {
        // format editor for yang kontrol menggunakan dataanotation
        //EditorFor is designed to work with view models and only supports data annotations. 
        //If you can not add annotation to the entities (not sure why not), then you should use seperate view models and an automaapper.
        //I think its poor design to the entities directly anyway.

        public int id { get; set; }
        //[Required(ErrorMessage = "Input data Nama Perusahaan")]
        [DisplayName("Transaction Code")]
        public string code { get; set; }
        
        [DisplayName("Event Name")]
        public string eventName { get; set; }
        
        [DisplayName("Event Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> startDate { get; set; }

        [DisplayName("Event End Date")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> endDate { get; set; }
        [DisplayName("Event Place")]
        public string place { get; set; }
        [DisplayName("Budget (Rp.)")]
        public Nullable<long> budget { get; set; }
        [DisplayName("Request By")]
        public int requestBy { get; set; }
        public int? requestBy2 { get; set; }

        public string requestByName { get; set; }

        [DisplayName("Request Date")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime requestDate { get; set; }

        public Nullable<DateTime> requestDate2 { get; set; }
        public Nullable<int> approvedBy { get; set; }
        public Nullable<System.DateTime> approveDate { get; set; }
        [DisplayName("Assign To")]
        public Nullable<int> assignTo { get; set; }
        public Nullable<System.DateTime> closedDate { get; set; }
        
        [DisplayName("Note")]
        public string note { get; set; }
        
        [DisplayName("Status")]
        public Nullable<int> status { get; set; }
        public string statusName { get; set; }
        public string rejectReason { get; set; }
        public Nullable<bool> isDelete { get; set; }
        
        [DisplayName("Create By")]
        public string createBy { get; set; }
        
        [DisplayName("Create Date")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> createDate { get; set; }
        public string updateBy { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }

        public int empId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
