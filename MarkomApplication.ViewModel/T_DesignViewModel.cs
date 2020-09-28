using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomApplication.ViewModel
{
    public class T_DesignViewModel
    {
        public int id { get; set; }

        [DisplayName("Transaction Code")]
        public string code { get; set; }

        public int tEventId { get; set; }
        [DisplayName("Event Code")]
        public string eventCode { get; set; }

        [DisplayName("Design Title")]
        public string titleHeader { get; set; }

        [DisplayName("Request By")]
        public int requestBy { get; set; }

        [DisplayName("Request Date")]
        public System.DateTime requestDate { get; set; }
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
        public System.DateTime createDate { get; set; }
        public string updateBy { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> approveBy { get; set; }
    }
}
