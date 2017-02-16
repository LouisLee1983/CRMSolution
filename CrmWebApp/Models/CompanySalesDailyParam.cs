namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanySalesDailyParam")]
    public partial class CompanySalesDailyParam
    {
        [Display(Name = "业务信息属性ID")]
        public int Id { get; set; }
        [Display(Name = "业务信息ID")]
        public int CompanySalesDailyId { get; set; }

        [StringLength(50)]
        [Display(Name = "属性名")]
        public string ParamName { get; set; }

        [StringLength(50)]
        [Display(Name = "属性子类")]
        public string SubParamItem { get; set; }
        [Display(Name = "数量")]
        public int? ItemAmount { get; set; }
    }
}
