namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyMeetingSubject")]
    public partial class CompanyMeetingSubject
    {
        public int Id { get; set; }

        public int CompanyMeetingId { get; set; }

        [Required]
        [StringLength(50)]
        public string Subject { get; set; }

        [StringLength(256)]
        public string Problem { get; set; }

        [StringLength(256)]
        public string Resolve { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? ResolveTime { get; set; }
    }
}
