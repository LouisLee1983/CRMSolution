namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyBusinessDailyPhoto")]
    public partial class CompanyBusinessDailyPhoto
    {
        public int Id { get; set; }

        public int CompanyBusinessDailyId { get; set; }

        [Required]
        [StringLength(128)]
        public string PhotoUrl { get; set; }

        [Required]
        [StringLength(50)]
        public string PhotoName { get; set; }
    }
}
