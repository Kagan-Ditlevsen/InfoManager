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
    
    public partial class DailyTypeExtra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DailyTypeExtra()
        {
            this.DailyInfo = new HashSet<DailyInfo>();
        }
    
        public int extraId { get; set; }
        public int typeId { get; set; }
        public string internalTitle { get; set; }
        public int sortOrder { get; set; }
        public bool isActive { get; set; }
        public bool isRequired { get; set; }
        public string inputType { get; set; }
        public string inputData { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DailyInfo> DailyInfo { get; set; }
        public virtual DailyType DailyType { get; set; }
    }
}
