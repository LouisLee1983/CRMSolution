namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyBusinessDailySoundRecord")]
    public partial class CompanyBusinessDailySoundRecord
    {
        public int Id { get; set; }

        public int CompanyBusinessDailyId { get; set; }

        [Required]
        [StringLength(50)]
        public string SoundRecordName { get; set; }

        [Required]
        [StringLength(128)]
        public string SoundRecordUrl { get; set; }
    }
}
