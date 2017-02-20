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
        [Display(Name = "外部ID")]
        public int OuterKeyId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "媒体类属")]
        public string MediaFor { get; set; }

        [StringLength(50)]
        [Display(Name = "名称")]
        public string MediaName { get; set; }

        [StringLength(128)]
        [Display(Name = "网址")]
        public string MediaUrl { get; set; }
    }
}
