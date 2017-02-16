namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyCertificate")]
    public partial class CompanyCertificate
    {
        public int Id { get; set; }
        [Display(Name = "公司ID")]
        public int CompanyId { get; set; }

        [StringLength(50)]
        [Display(Name = "资质名称")]
        public string CertificateName { get; set; }

        [StringLength(128)]
        [Display(Name = "公司名")]
        public string CompanyName { get; set; }

        [StringLength(50)]
        [Display(Name = "图片网址")]
        public string PictureUrl { get; set; }
        [Display(Name = "录入时间")]
        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        [Display(Name = "录入人")]
        public string CreateUserName { get; set; }
    }
}
