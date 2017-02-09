namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanySalesDaily")]
    public partial class CompanySalesDaily
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }

        [StringLength(50)]
        public string ManagerName { get; set; }

        [StringLength(50)]
        public string ManagerPhone { get; set; }

        [StringLength(50)]
        public string SalesType { get; set; }

        [StringLength(50)]
        public string CreateUserName { get; set; }

        public DateTime? CreateTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SalesLogDate { get; set; }
    }
}
