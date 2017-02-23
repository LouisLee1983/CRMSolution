namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChinaCity")]
    public partial class ChinaCity
    {
        public int ID { get; set; }
        
        [StringLength(50)]
        [Display(Name = "省")]
        public string ProvinceName { get; set; }

        [StringLength(50)]
        [Display(Name = "市")]
        public string CityName { get; set; }
    }
}
