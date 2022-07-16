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
    
    public partial class Daily
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Daily()
        {
            this.DailyInfo = new HashSet<DailyInfo>();
        }
    
        public System.Guid dailyId { get; set; }
        public System.DateTime registerDateTime { get; set; }
        public int typeId { get; set; }
        public Nullable<int> optionId { get; set; }
        public string remark { get; set; }
        public System.DateTime createDateTime { get; set; }
        public int createUserId { get; set; }
    
        public virtual DailyType DailyType { get; set; }
        public virtual DailyTypeOption DailyTypeOption { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DailyInfo> DailyInfo { get; set; }
    }
}
