//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InfoManager.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_Daily_Statistics
    {
        public int typeId { get; set; }
        public string typeTitle { get; set; }
        public Nullable<int> optionId { get; set; }
        public string optionTitle { get; set; }
        public Nullable<int> qtyMorning { get; set; }
        public Nullable<int> qty24h { get; set; }
        public Nullable<int> qtyMidnight { get; set; }
        public Nullable<System.DateTime> lastTimeTypeId { get; set; }
        public Nullable<System.DateTime> lastTimeOptionId { get; set; }
        public int createUserId { get; set; }
        public Nullable<System.DateTime> lastRegisterDateTime { get; set; }
    }
}