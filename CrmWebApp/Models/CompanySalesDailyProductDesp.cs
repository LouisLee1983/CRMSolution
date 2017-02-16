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
        [Display(Name = "业务信息产品类型ID")]
        public int Id { get; set; }
        [Display(Name = "业务信息ID")]
        public int CompanySalesDailyId { get; set; }

        [StringLength(50)]
        [Display(Name = "销售渠道")]
        public string SalesSource { get; set; }

        [StringLength(50)]
        [Display(Name = "产品类型")]
        public string SalesProduct { get; set; }
        [Display(Name = "销量")]
        public int? SalesCount { get; set; }
    }
}
