namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanySalesDaily")]
    public partial class CompanySalesDaily
    {
        [Display(Name = "公司业务信息ID")]
        public int Id { get; set; }
        [Display(Name = "公司ID")]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "公司名称")]
        public string CompanyName { get; set; }

        [StringLength(50)]
        [Display(Name = "负责人")]
        public string ManagerName { get; set; }

        [StringLength(50)]
        [Display(Name = "负责人手机")]
        public string ManagerPhone { get; set; }

        [StringLength(50)]
        [Display(Name = "业务类型")]
        public string SalesType { get; set; }

        [StringLength(50)]
        [Display(Name = "录入人")]
        public string CreateUserName { get; set; }
        [Display(Name = "录入时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? CreateTime { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "业务记录日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? SalesLogDate { get; set; }        
    }
}
