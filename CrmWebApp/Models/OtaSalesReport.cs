namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OtaSalesReport")]
    public partial class OtaSalesReport
    {
        public int ID { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "开始日期")]
        public DateTime? StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "结束日期")]
        public DateTime? EndDate { get; set; }

        [StringLength(50)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "发送邮箱")]
        public string SendToEmails { get; set; }

        [StringLength(50)]
        [Display(Name = "标题")]
        public string ReportTitle { get; set; }

        [Display(Name = "内容")]
        public string ReportContent { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }
    }
}
