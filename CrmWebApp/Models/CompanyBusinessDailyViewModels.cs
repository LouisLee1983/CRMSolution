using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrmWebApp.Models
{
    public class CompanyBusinessDailyViewModel
    {
        public int Id { get; set; }
        [Display(Name = "公司ID")]
        public int CompanyId { get; set; }

        [StringLength(50)]
        [Display(Name = "公司名")]
        public string CompanyName { get; set; }

        [StringLength(50)]
        [Display(Name = "业务类型")]
        public string BussinessType { get; set; }

        [StringLength(50)]
        [Display(Name = "运营负责人")]
        public string ManagerName { get; set; }

        [StringLength(50)]
        [Display(Name = "录入人")]
        public string CreateUserName { get; set; }

        [Display(Name = "录入时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? CreateTime { get; set; }
        
        [Display(Name = "运营状况日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? BussinessLogDate { get; set; }

        [Display(Name ="员工数量")]
        public List<CompanyBusinessDailyParam> EmployeeList { get; set; }
        [Display(Name = "软件系统")]
        public List<CompanyBusinessDailyParam> ItSystemList { get; set; }
        [Display(Name = "业务结构")]
        public List<CompanyBusinessDailyParam> BusinessAmountList { get; set; }
        [Display(Name = "新业务量")]
        public List<CompanyBusinessDailyParam> NewBusinessList { get; set; }
        [Display(Name = "照片")]
        public List<CompanyBusinessDailyPhoto> PhotoList { get; set; }
        [Display(Name = "录音")]
        public List<CompanyBusinessDailySoundRecord> SoundRecordList { get; set; }

        public CompanyBusinessDailyViewModel()
        {

        }

        public CompanyBusinessDailyViewModel(CompanyBusinessDaily daily,List<CompanyBusinessDailyParam> paramItems,List<CompanyBusinessDailyPhoto> photos,List<CompanyBusinessDailySoundRecord> sounds)
        {
            this.Id = daily.Id;
            this.CompanyId = daily.CompanyId;
            this.CompanyName = daily.CompanyName;
            this.CreateUserName = daily.CreateUserName;
            this.CreateTime = daily.CreateTime;
            this.ManagerName = daily.ManagerName;
            this.BussinessLogDate = daily.BussinessLogDate;
            this.BussinessType = daily.BussinessType;
           
            this.EmployeeList = new List<CompanyBusinessDailyParam>();
            this.ItSystemList = new List<CompanyBusinessDailyParam>();
            this.BusinessAmountList = new List<CompanyBusinessDailyParam>();
            this.NewBusinessList = new List<CompanyBusinessDailyParam>();
            this.PhotoList = photos;
            this.SoundRecordList = sounds;

            foreach (CompanyBusinessDailyParam item in paramItems)
            {
                switch (item.ParamName)
                {
                    case "员工数量":
                        this.EmployeeList.Add(item);
                        break;
                    case "业务结构":
                        this.BusinessAmountList.Add(item);
                        break;
                    case "软件系统":
                        this.ItSystemList.Add(item);
                        break;
                    case "新业务量":
                        this.NewBusinessList.Add(item);
                        break;
                    default:
                        break;
                }
            }
        }
    }
    
}