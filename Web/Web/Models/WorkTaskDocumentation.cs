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
    
    public partial class WorkTaskDocumentation
    {
        public System.Guid documentationId { get; set; }
        public System.Guid workId { get; set; }
        public System.Guid taskId { get; set; }
        public System.DateTime registerDateTime { get; set; }
        public string remark { get; set; }
        public System.DateTime createDateTime { get; set; }
        public int createUserId { get; set; }
    
        public virtual Work Work { get; set; }
        public virtual WorkTask WorkTask { get; set; }
    }
}