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
    
    public partial class DailyInfo
    {
        public System.Guid dailyId { get; set; }
        public int extraId { get; set; }
        public string entry { get; set; }
        public System.DateTime createDateTime { get; set; }
        public int createUserId { get; set; }
    
        public virtual Daily Daily { get; set; }
        public virtual DailyTypeExtra DailyTypeExtra { get; set; }
    }
}
