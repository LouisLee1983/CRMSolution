namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanySalesDailyFund")]
    public partial class CompanySalesDailyFund
    {
        [Display(Name = "资金情况ID")]
        public int Id { get; set; }
        [Display(Name = "业务信息ID")]
        public int CompanySalesDailyId { get; set; }

        [StringLength(50)]
        [Display(Name = "运营记录ID")]
        public string SalesSource { get; set; }
        [Display(Name = "冻结资金")]
        public int? FreezeFund { get; set; }

        [Display(Name = "流动资金")]
        public int? WorkingFund { get; set; }

        [Display(Name = "缺少资金")]
        public int? NeededFund { get; set; }
    }
}
