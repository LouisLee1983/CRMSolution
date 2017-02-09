namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class S_City
    {
        [Key]
        public long CityID { get; set; }

        [StringLength(50)]
        public string CityName { get; set; }

        [StringLength(50)]
        public string ZipCode { get; set; }

        public long? ProvinceID { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}
