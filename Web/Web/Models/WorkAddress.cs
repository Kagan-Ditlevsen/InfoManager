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
    
    public partial class WorkAddress
    {
        public int addressId { get; set; }
        public string title { get; set; }
        public string gpsAddress { get; set; }
        public string gpsPlusCode { get; set; }
        public string systemRemark { get; set; }
        public string locationCode { get; set; }
        public bool isArchived { get; set; }
    }
}