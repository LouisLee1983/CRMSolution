namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OtaCompany")]
    public partial class OtaCompany
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "公司名")]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "法人")]
        public string LegalPerson { get; set; }

        [StringLength(50)]
        [Display(Name = "法人身份证")]
        public string LegalPersonIdNo { get; set; }

        [StringLength(50)]
        [Display(Name = "法人电话")]
        public string LegalPersonPhone { get; set; }

        [StringLength(128)]
        [Display(Name = "注册地址")]
        public string RegisterAddress { get; set; }

        [StringLength(128)]
        [Display(Name = "营业地址")]
        public string RealAddress { get; set; }

        [StringLength(50)]
        [Display(Name = "Office号")]
        public string OfficeNo { get; set; }

        [StringLength(50)]
        [Display(Name = "实际老板")]
        public string BossName { get; set; }

        [StringLength(50)]
        [Display(Name = "老板身份证")]
        public string BossIdNo { get; set; }

        [StringLength(256)]
        [Display(Name = "老板个人背景")]
        public string BossBackground { get; set; }

        [StringLength(256)]
        [Display(Name = "老板行业背景")]
        public string BossBusinessDesp { get; set; }

        [StringLength(256)]
        [Display(Name = "其他投资")]
        public string OtherInvest { get; set; }

        [StringLength(256)]
        [Display(Name = "固定资产")]
        public string CapitalAsserts { get; set; }

        [StringLength(50)]
        [Display(Name = "销售")]
        public string SalesUserName { get; set; }
        
        [StringLength(50)]
        [Display(Name = "所在城市")]
        public string CityName { get; set; }

        [Display(Name ="业务")]
        public string BusnessRange { get; set; }

        [Display(Name = "拜访")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? LastMeetingDate { get; set; }

        [Display(Name = "录入时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? CreateTime { get; set; }
    }
}
