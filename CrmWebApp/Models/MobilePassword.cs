namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobilePassword")]
    public partial class MobilePassword
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string MobileNum { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
