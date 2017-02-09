namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ParamDict")]
    public partial class ParamDict
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ParamName { get; set; }

        [Required]
        [StringLength(50)]
        public string SubItemName { get; set; }
    }
}
