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
        [Display(Name = "运营记录ID")]
        public int CompanyBusinessDailyId { get; set; }
        
        [StringLength(50)]
        [Display(Name = "录音名称")]
        public string SoundRecordName { get; set; }
        
        [StringLength(128)]
        [Display(Name = "录音地址")]
        public string SoundRecordUrl { get; set; }
    }
}
