using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrmWebApp.Models
{
    public class CompanyMeetingViewModel
    {

        [Display(Name = "拜访记录ID")]
        public int Id { get; set; }
        [Display(Name = "公司ID")]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "公司名称")]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "沟通方式")]
        public string MeetingType { get; set; }
        
        [Display(Name = "沟通日期")]
        public DateTime MeetDate { get; set; }

        [StringLength(128)]
        [Display(Name = "会面地址")]
        public string MeetAddress { get; set; }

        [StringLength(128)]
        [Display(Name = "拜访人")]
        public string MeetNames { get; set; }

        [StringLength(512)]
        [Display(Name = "纪要")]
        public string MeetSummary { get; set; }

        [StringLength(50)]
        [Display(Name = "录入人")]
        public string CreateUserName { get; set; }

        [Display(Name = "录入时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? CreateTime { get; set; }

        [Display(Name ="沟通记录")]
        public List<CompanyMeetingSubject> MeetingSubjectList { get; set; }

        [Display(Name ="附件")]
        public List<CompanyMedia> MediaList { get; set; }

        public CompanyMeetingViewModel() {  }

        public CompanyMeetingViewModel(CompanyMeeting item,List<CompanyMeetingSubject> meetingSubjects,List<CompanyMedia> mediaList)
        {
            this.CompanyId = item.CompanyId;
            this.CompanyName = item.CompanyName;
            this.CreateTime = item.CreateTime;
            this.CreateUserName = item.CreateUserName;
            this.Id = item.Id;
            this.MeetAddress = item.MeetAddress;
            this.MeetDate = item.MeetDate;
            this.MeetingType = item.MeetingType;
            this.MeetNames = item.MeetNames;
            this.MeetSummary = item.MeetSummary;
            
            this.MeetingSubjectList = meetingSubjects;
            this.MediaList = mediaList;
        }
    }
}