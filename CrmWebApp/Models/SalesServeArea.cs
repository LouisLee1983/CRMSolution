namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesServeArea")]
    public partial class SalesServeArea
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="�û���")]
        public string UserName { get; set; }

        [StringLength(50)]
        [Display(Name = "ʡ")]
        public string Province { get; set; }

        [StringLength(50)]
        [Display(Name = "��")]
        public string City { get; set; }
    }
}
