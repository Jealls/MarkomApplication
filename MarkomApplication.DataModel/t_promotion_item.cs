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
    
    public partial class t_promotion_item
    {
        public int id { get; set; }
        public int t_promotion_id { get; set; }
        public Nullable<int> t_design_item_id { get; set; }
        public int m_product_id { get; set; }
        public string title { get; set; }
        public int request_pic { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<System.DateTime> end_date { get; set; }
        public Nullable<System.DateTime> request_due_date { get; set; }
        public Nullable<long> qty { get; set; }
        public int todo { get; set; }
        public string note { get; set; }
        public Nullable<bool> is_delete { get; set; }
        public string create_by { get; set; }
        public System.DateTime create_date { get; set; }
        public string update_by { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
    }
}
