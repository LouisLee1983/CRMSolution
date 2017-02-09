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
        public int Id { get; set; }

        public int CompanySalesDailyId { get; set; }

        [StringLength(50)]
        public string ParamName { get; set; }

        [StringLength(50)]
        public string SubParamItem { get; set; }

        public int? ItemAmount { get; set; }
    }
}
