namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyMeetingSubject")]
    public partial class CompanyMeetingSubject
    {
        [Display(Name = "反馈问题ID")]
        public int Id { get; set; }
        [Display(Name = "拜访记录ID")]
        public int CompanyMeetingId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "问题类型")]
        public string Subject { get; set; }

        [StringLength(256)]
        [Display(Name = "具体描述")]
        public string Problem { get; set; }

        [StringLength(256)]
        [Display(Name = "解决情况")]
        public string Resolve { get; set; }
        
        [Display(Name = "解决日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ResolveTime { get; set; }
    }
}
