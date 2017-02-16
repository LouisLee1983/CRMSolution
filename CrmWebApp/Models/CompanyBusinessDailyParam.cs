namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyBusinessDailyParam")]
    public partial class CompanyBusinessDailyParam
    {
        public int Id { get; set; }
        [Display(Name ="运营记录ID")]
        public int CompanyBusinessDailyId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="运营分类名")]
        public string ParamName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="子类名")]
        public string SubParamItem { get; set; }
        [Display(Name ="数量")]
        public int? ItemAmount { get; set; }
    }
}
