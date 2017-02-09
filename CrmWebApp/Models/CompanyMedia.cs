namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyMedia")]
    public partial class CompanyMedia
    {
        public int Id { get; set; }

        public int OuterKeyId { get; set; }

        [Required]
        [StringLength(50)]
        public string MediaFor { get; set; }

        [StringLength(50)]
        public string MediaName { get; set; }

        [StringLength(128)]
        public string MediaUrl { get; set; }
    }
}
