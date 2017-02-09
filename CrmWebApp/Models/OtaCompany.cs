namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OtaCompany")]
    public partial class OtaCompany
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(50)]
        public string LegalPerson { get; set; }

        [StringLength(50)]
        public string LegalPersonIdNo { get; set; }

        [StringLength(50)]
        public string LegalPersonPhone { get; set; }

        [StringLength(128)]
        public string RegisterAddress { get; set; }

        [StringLength(128)]
        public string RealAddress { get; set; }

        [StringLength(50)]
        public string OfficeNo { get; set; }

        [StringLength(50)]
        public string BossName { get; set; }

        [StringLength(50)]
        public string BossIdNo { get; set; }

        [StringLength(256)]
        public string BossBackground { get; set; }

        [StringLength(256)]
        public string BossBusinessDesp { get; set; }

        [StringLength(256)]
        public string OtherInvest { get; set; }

        [StringLength(256)]
        public string CapitalAsserts { get; set; }

        [StringLength(50)]
        public string SalesUserName { get; set; }

        public int? CityId { get; set; }

        [StringLength(50)]
        public string CityName { get; set; }
    }
}
