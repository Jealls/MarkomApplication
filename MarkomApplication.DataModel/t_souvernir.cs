//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MarkomApplication.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class t_souvernir
    {
        public int id { get; set; }
        public string code { get; set; }
        public string type { get; set; }
        public Nullable<int> t_event_id { get; set; }
        public int request_by { get; set; }
        public Nullable<System.DateTime> request_date { get; set; }
        public Nullable<System.DateTime> request_due_date { get; set; }
        public Nullable<int> approved_by { get; set; }
        public Nullable<System.DateTime> approve_date { get; set; }
        public Nullable<int> receive_by { get; set; }
        public Nullable<System.DateTime> receive_date { get; set; }
        public Nullable<int> settlement_by { get; set; }
        public Nullable<System.DateTime> settlement_date { get; set; }
        public Nullable<int> settlement_approve_by { get; set; }
        public Nullable<System.DateTime> settlement_approve_date { get; set; }
        public Nullable<int> status { get; set; }
        public string note { get; set; }
        public string reject_reason { get; set; }
        public Nullable<bool> is_delete { get; set; }
        public string create_by { get; set; }
        public Nullable<System.DateTime> create_date { get; set; }
        public string update_by { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
    }
}