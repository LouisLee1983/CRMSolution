namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyCertificate")]
    public partial class CompanyCertificate
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        [StringLength(50)]
        public string CertificateName { get; set; }

        [StringLength(128)]
        public string CompanyName { get; set; }

        [StringLength(50)]
        public string PictureUrl { get; set; }

        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string CreateUserName { get; set; }
    }
}
