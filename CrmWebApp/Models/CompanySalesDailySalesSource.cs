namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanySalesDailySalesSource")]
    public partial class CompanySalesDailySalesSource
    {
        [Display(Name = "业务信息人员投入ID")]
        public int Id { get; set; }
        [Display(Name = "业务信息ID")]
        public int CompanySalesDailyId { get; set; }

        [StringLength(50)]
        [Display(Name = "销售渠道")]
        public string SaleSource { get; set; }
        [Display(Name = "投入人数")]
        public int? EmployeeCount { get; set; }
        [Display(Name = "票量")]
        public int? TicketCount { get; set; }
        [Display(Name = "人均工资")]
        public int? EmployeePayment { get; set; }
    }
}
