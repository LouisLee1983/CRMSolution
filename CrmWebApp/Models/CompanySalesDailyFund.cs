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
        public int Id { get; set; }

        public int CompanySalesDailyId { get; set; }

        [StringLength(50)]
        public string SalesSource { get; set; }

        public int? FreezeFund { get; set; }

        public int? WorkingFund { get; set; }

        public int? NeededFund { get; set; }
    }
}
