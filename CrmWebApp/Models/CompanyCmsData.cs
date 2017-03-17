namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyCmsData")]
    public partial class CompanyCmsData
    {
        public int Id { get; set; }

        public int? CmsId { get; set; }

        [StringLength(50)]
        [Display(Name = "公司名")]
        public string CompanyName { get; set; }
        
        [StringLength(512)]
        [Display(Name = "简介")]
        public string CompanyDesp { get; set; }

        [StringLength(128)]
        [Display(Name = "注册地址")]
        public string RegisterAddress { get; set; }

        [StringLength(128)]
        [Display(Name = "经营地址")]
        public string RealAddress { get; set; }

        [StringLength(50)]
        [Display(Name = "营业执照号")]
        public string CompanyIdNo { get; set; }

        [StringLength(50)]
        [Display(Name = "国内TTS")]
        public string GuoneiWebName { get; set; }

        [StringLength(50)]
        [Display(Name = "国际TTS")]
        public string GuojiWebName { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        public string CompanyEmail { get; set; }

        [StringLength(50)]
        [Display(Name = "法人")]
        public string LegalPerson { get; set; }

        [StringLength(50)]
        [Display(Name = "联系人")]
        public string ContactPerson { get; set; }

        [StringLength(50)]
        [Display(Name = "联系电话")]
        public string ContactPhone { get; set; }

        [StringLength(512)]
        [Display(Name = "公司背景")]
        public string BossBackground { get; set; }

        [StringLength(50)]
        [Display(Name = "TTS账号")]
        public string TTSAdminAccount { get; set; }

        [StringLength(50)]
        [Display(Name = "销售")]
        public string SalesName { get; set; }

        [StringLength(50)]
        [Display(Name = "TTS状态")]
        public string TTSStatusDesp { get; set; }
        
    }
}
