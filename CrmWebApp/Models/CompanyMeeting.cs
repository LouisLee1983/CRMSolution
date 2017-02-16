namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyMeeting")]
    public partial class CompanyMeeting
    {
        [Display(Name = "拜访记录ID")]
        public int Id { get; set; }
        [Display(Name = "公司ID")]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "公司名称")]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "沟通方式")]
        public string MeetingType { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "沟通日期")]
        public DateTime MeetDate { get; set; }

        [StringLength(128)]
        [Display(Name = "会面地址")]
        public string MeetAddress { get; set; }

        [StringLength(128)]
        [Display(Name = "拜访人")]
        public string MeetNames { get; set; }

        [StringLength(50)]
        [Display(Name = "录入人")]
        public string CreateUserName { get; set; }

        [Display(Name = "录入时间")]
        public DateTime? CreateTime { get; set; }
    }
}
