namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanySalesDailyProductDesp")]
    public partial class CompanySalesDailyProductDesp
    {
        public int Id { get; set; }

        public int CompanySalesDailyId { get; set; }

        [StringLength(50)]
        public string SalesSource { get; set; }

        [StringLength(50)]
        public string SalesProduct { get; set; }

        public int? SalesCount { get; set; }
    }
}
