//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dk.infomanager.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_Daily_Overview
    {
        public Nullable<long> rowNo { get; set; }
        public System.Guid dailyId { get; set; }
        public System.DateTime registerDateTime { get; set; }
        public int typeId { get; set; }
        public Nullable<int> optionId { get; set; }
        public Nullable<int> extrasQty { get; set; }
        public string remark { get; set; }
        public System.DateTime createDateTime { get; set; }
        public int createUserId { get; set; }
        public string typeTitle { get; set; }
        public string optionTitle { get; set; }
        public Nullable<System.DateTime> lastTimeTypeId { get; set; }
        public Nullable<System.DateTime> lastTimeOptionId { get; set; }
        public Nullable<int> qty24h { get; set; }
        public Nullable<int> qtyMidnight { get; set; }
        public Nullable<int> qtyMorning { get; set; }
    }
}
