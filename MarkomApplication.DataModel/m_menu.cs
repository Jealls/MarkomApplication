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
    
    public partial class m_menu
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string controller { get; set; }
        public Nullable<int> parent_id { get; set; }
        public bool is_delete { get; set; }
        public string create_by { get; set; }
        public System.DateTime create_date { get; set; }
        public string update_by { get; set; }
        public Nullable<System.DateTime> update_date { get; set; }
    }
}
