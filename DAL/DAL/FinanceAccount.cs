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
    
    public partial class FinanceAccount
    {
        public int accountId { get; set; }
        public Nullable<int> parentAccountId { get; set; }
        public string typeId { get; set; }
        public string title { get; set; }
        public decimal sumInitial { get; set; }
        public Nullable<decimal> sumCurrent { get; set; }
        public string sortOrder { get; set; }
        public Nullable<byte> lvl { get; set; }
        public string lvlTitle { get; set; }
        public string breadcrum { get; set; }
    
        public virtual FinanceAccountType FinanceAccountType { get; set; }
    }
}
