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
    
    public partial class WorkTaskTemplate
    {
        public string templateId { get; set; }
        public int sortOrder { get; set; }
        public int typeId { get; set; }
        public Nullable<System.DateTime> startDateTime { get; set; }
        public Nullable<System.DateTime> endDateTime { get; set; }
        public string addressText { get; set; }
        public string vehicleNumberplate { get; set; }
        public string linkNumberplate { get; set; }
        public string dollyNumberplate { get; set; }
        public string trailerNumberplate { get; set; }
        public string remark { get; set; }
        public string systemRemark { get; set; }
        public Nullable<byte> reportColumnNumber { get; set; }
        public System.DateTime createDateTime { get; set; }
        public int createUserId { get; set; }
    
        public virtual WorkTaskType WorkTaskType { get; set; }
    }
}
