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
    
    public partial class vw_Daily_Days
    {
        public int typeId { get; set; }
        public string internalTitle { get; set; }
        public System.DateTime startDateTime { get; set; }
        public Nullable<System.DateTime> endDateTime { get; set; }
        public Nullable<System.DateTime> elapsedTime { get; set; }
        public int createUserId { get; set; }
    }
}
