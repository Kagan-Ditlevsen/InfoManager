//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace infomanager.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkVehicleType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkVehicleType()
        {
            this.WorkVehicle = new HashSet<WorkVehicle>();
        }
    
        public int typeId { get; set; }
        public string icon { get; set; }
        public string internalTitle { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkVehicle> WorkVehicle { get; set; }
    }
}
