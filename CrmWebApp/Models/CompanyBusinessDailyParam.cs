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

        public int CompanyBusinessDailyId { get; set; }

        [Required]
        [StringLength(50)]
        public string ParamName { get; set; }

        [Required]
        [StringLength(50)]
        public string SubParamItem { get; set; }

        public int? ItemAmount { get; set; }
    }
}
