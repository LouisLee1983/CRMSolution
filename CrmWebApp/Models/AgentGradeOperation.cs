namespace CrmWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AgentGradeOperation")]
    public partial class AgentGradeOperation
    {
        public int id { get; set; }
        
        [StringLength(50)]
        [Display(Name = "域名")]
        public string agentDomain { get; set; }
                
        [StringLength(50)]
        [Display(Name = "公司名")]
        public string agentName { get; set; }

        [StringLength(50)]
        [Display(Name = "推广名")]
        public string promotion { get; set; }

        [StringLength(50)]
        [Display(Name = "运营主管")]
        public string agentManager { get; set; }
        
        [Display(Name = "月累积票量")]
        public int? totalTicketNum { get; set; }
        
        [Display(Name = "票量分")]
        public decimal? totalTicket { get; set; }
        
        [Display(Name = "出票合格率")]
        public decimal? passRate { get; set; }
        
        [Display(Name = "出票效率")]
        public decimal? less60minRate { get; set; }
        
        [Display(Name = "改签效率")]
        public decimal? orderAlterRate { get; set; }
        
        [Display(Name = "自愿退票分")]
        public decimal? voluntaryRate { get; set; }
        
        [Display(Name = "非自愿退票分")]
        public decimal? involuntaryRate { get; set; }
        
        [Display(Name = "投诉分")]
        public decimal? complainRate { get; set; }
        
        [Display(Name = "质检分")]
        public decimal? qapassRate { get; set; }
        
        [Display(Name = "电话分")]
        public decimal? phoneAnswerRate { get; set; }
        
        [Display(Name = "消息超时分")]
        public decimal? messageTimeoutRate { get; set; }

        [Display(Name = "资质分")]
        public decimal? qualification { get; set; }

        [Display(Name = "白名单分")]
        public decimal? whiteList { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public decimal? totalScore { get; set; }

        [Display(Name ="状态")]
        public int status { get; set; }

        [Column(TypeName = "date")]
        [Display(Name ="日期")]
        public DateTime statDate { get; set; }

        [Display(Name ="月份")]
        public string statMonth { get; set; }

        [Display(Name ="等级")]
        public string grade { get; set; }

        [Display(Name ="当天票量")]
        public int? CurDateTicketCount { get; set; }

        [Display(Name ="录入时间")]
        public DateTime? CreateTime { get; set; }
    }
}
