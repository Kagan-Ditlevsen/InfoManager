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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Db : DbContext
    {
        public Db()
            : base("name=Db")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Daily> Daily { get; set; }
        public virtual DbSet<DailyInfo> DailyInfo { get; set; }
        public virtual DbSet<DailyType> DailyType { get; set; }
        public virtual DbSet<DailyTypeExtra> DailyTypeExtra { get; set; }
        public virtual DbSet<DailyTypeOption> DailyTypeOption { get; set; }
        public virtual DbSet<Finance> Finance { get; set; }
        public virtual DbSet<FinanceAccount> FinanceAccount { get; set; }
        public virtual DbSet<FinanceAccountType> FinanceAccountType { get; set; }
        public virtual DbSet<FinanceLine> FinanceLine { get; set; }
        public virtual DbSet<Info> Info { get; set; }
        public virtual DbSet<InfoCategory> InfoCategory { get; set; }
        public virtual DbSet<InfoEntry> InfoEntry { get; set; }
        public virtual DbSet<InfoEntryType> InfoEntryType { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<Work> Work { get; set; }
        public virtual DbSet<WorkAddress> WorkAddress { get; set; }
        public virtual DbSet<WorkStatusEnum> WorkStatusEnum { get; set; }
        public virtual DbSet<WorkTask> WorkTask { get; set; }
        public virtual DbSet<WorkTaskDocumentation> WorkTaskDocumentation { get; set; }
        public virtual DbSet<WorkTaskTemplate> WorkTaskTemplate { get; set; }
        public virtual DbSet<WorkTaskType> WorkTaskType { get; set; }
        public virtual DbSet<WorkVehicle> WorkVehicle { get; set; }
        public virtual DbSet<WorkVehicleType> WorkVehicleType { get; set; }
        public virtual DbSet<vw_Daily_Days> vw_Daily_Days { get; set; }
        public virtual DbSet<vw_Daily_Overview> vw_Daily_Overview { get; set; }
        public virtual DbSet<vw_Daily_Statistics> vw_Daily_Statistics { get; set; }
        public virtual DbSet<vw_DailyType_Tree> vw_DailyType_Tree { get; set; }
    
        [DbFunction("Db", "fnc_Work_Salary")]
        public virtual IQueryable<fnc_Work_Salary_Result> fnc_Work_Salary(string workId, Nullable<System.DateTime> sd, Nullable<System.DateTime> ed, Nullable<decimal> rate0618, Nullable<decimal> rate1823, Nullable<decimal> rate2306)
        {
            var workIdParameter = workId != null ?
                new ObjectParameter("workId", workId) :
                new ObjectParameter("workId", typeof(string));
    
            var sdParameter = sd.HasValue ?
                new ObjectParameter("sd", sd) :
                new ObjectParameter("sd", typeof(System.DateTime));
    
            var edParameter = ed.HasValue ?
                new ObjectParameter("ed", ed) :
                new ObjectParameter("ed", typeof(System.DateTime));
    
            var rate0618Parameter = rate0618.HasValue ?
                new ObjectParameter("rate0618", rate0618) :
                new ObjectParameter("rate0618", typeof(decimal));
    
            var rate1823Parameter = rate1823.HasValue ?
                new ObjectParameter("rate1823", rate1823) :
                new ObjectParameter("rate1823", typeof(decimal));
    
            var rate2306Parameter = rate2306.HasValue ?
                new ObjectParameter("rate2306", rate2306) :
                new ObjectParameter("rate2306", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fnc_Work_Salary_Result>("[Db].[fnc_Work_Salary](@workId, @sd, @ed, @rate0618, @rate1823, @rate2306)", workIdParameter, sdParameter, edParameter, rate0618Parameter, rate1823Parameter, rate2306Parameter);
        }
    }
}
