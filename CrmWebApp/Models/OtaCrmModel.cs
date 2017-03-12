namespace CrmWebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OtaCrmModel : DbContext
    {
        public OtaCrmModel()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<CompanyBusinessDaily> CompanyBusinessDaily { get; set; }
        public virtual DbSet<CompanyBusinessDailyParam> CompanyBusinessDailyParam { get; set; }
        public virtual DbSet<CompanyBusinessDailyPhoto> CompanyBusinessDailyPhoto { get; set; }
        public virtual DbSet<CompanyBusinessDailySoundRecord> CompanyBusinessDailySoundRecord { get; set; }
        public virtual DbSet<CompanyCertificate> CompanyCertificate { get; set; }
        public virtual DbSet<CompanyMedia> CompanyMedia { get; set; }
        public virtual DbSet<CompanyMeeting> CompanyMeeting { get; set; }
        public virtual DbSet<CompanyMeetingSubject> CompanyMeetingSubject { get; set; }
        public virtual DbSet<CompanySalesDaily> CompanySalesDaily { get; set; }
        public virtual DbSet<CompanySalesDailyFund> CompanySalesDailyFund { get; set; }
        public virtual DbSet<CompanySalesDailyParam> CompanySalesDailyParam { get; set; }
        public virtual DbSet<CompanySalesDailyProductDesp> CompanySalesDailyProductDesp { get; set; }
        public virtual DbSet<CompanySalesDailySalesSource> CompanySalesDailySalesSource { get; set; }
        public virtual DbSet<OtaCompany> OtaCompany { get; set; }
        public virtual DbSet<ParamDict> ParamDict { get; set; }
        public virtual DbSet<S_City> S_City { get; set; }
        public virtual DbSet<S_Province> S_Province { get; set; }
        public virtual DbSet<SalesServeArea> SalesServeArea { get; set; }
        public virtual DbSet<ChinaCity> ChinaCity { get; set; }
        public virtual DbSet<ServeArea> ServeArea { get; set; }
        public virtual DbSet<OtaSalesReport> OtaSalesReport { get; set; }
        public virtual DbSet<AgentGradeOperation> AgentGradeOperation { get; set; }
        public virtual DbSet<MobilePassword> MobilePassword { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
