namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServeArea")]
    public partial class ServeArea
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Display(Name = "用户")]
        public string UserName { get; set; }

        [StringLength(50)]
        [Display(Name = "区域")]
        public string ServeAreaName { get; set; }
    }
}
