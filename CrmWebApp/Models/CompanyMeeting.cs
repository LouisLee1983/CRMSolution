namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyMeeting")]
    public partial class CompanyMeeting
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        [Required]
        [StringLength(128)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(50)]
        public string MeetingType { get; set; }

        [Column(TypeName = "date")]
        public DateTime MeetDate { get; set; }

        [StringLength(128)]
        public string MeetAddress { get; set; }

        [StringLength(128)]
        public string MeetNames { get; set; }

        [StringLength(50)]
        public string CreateUserName { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
