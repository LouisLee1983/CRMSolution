namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyBusinessDaily")]
    public partial class CompanyBusinessDaily
    {
        public int Id { get; set; }
        [Display(Name ="公司ID")]
        public int CompanyId { get; set; }

        [StringLength(50)]
        [Display(Name ="公司名")]
        public string CompanyName { get; set; }

        [StringLength(50)]
        [Display(Name ="业务类型")]
        public string BussinessType { get; set; }

        [StringLength(50)]
        [Display(Name ="运营负责人")]
        public string ManagerName { get; set; }

        [StringLength(50)]
        [Display(Name ="录入人")]
        public string CreateUserName { get; set; }

        [Display(Name ="录入时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? CreateTime { get; set; }

        [Column(TypeName = "date")]
        [Display(Name ="运营状况日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? BussinessLogDate { get; set; }
    }
}
