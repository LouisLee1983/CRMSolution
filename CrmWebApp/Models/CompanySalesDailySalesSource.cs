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
        public int Id { get; set; }

        public int CompanySalesDailyId { get; set; }

        [StringLength(50)]
        public string SaleSource { get; set; }

        public int? EmployeeCount { get; set; }

        public int? TicketCount { get; set; }

        public int? EmployeePayment { get; set; }
    }
}
