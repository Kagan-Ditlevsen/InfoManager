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
    
    public partial class SysUser
    {
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string password { get; set; }
        public Nullable<System.DateTime> statLastLogon { get; set; }
        public Nullable<System.DateTime> statGoodMorning { get; set; }
        public Nullable<System.DateTime> statGoodNight { get; set; }
        public Nullable<int> statCigQty { get; set; }
        public int isAwaken { get; set; }
        public Nullable<int> statSleepMs { get; set; }
        public Nullable<int> statAwakenMs { get; set; }
        public Nullable<System.DateTime> modifyDateTime { get; set; }
        public System.DateTime createDateTime { get; set; }
    }
}
