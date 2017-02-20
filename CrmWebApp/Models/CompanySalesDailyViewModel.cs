using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrmWebApp.Models
{
    public class CompanySalesDailyViewModel
    {
        [Display(Name = "公司业务信息ID")]
        public int Id { get; set; }
        [Display(Name = "公司ID")]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "公司名称")]
        public string CompanyName { get; set; }

        [StringLength(50)]
        [Display(Name = "负责人")]
        public string ManagerName { get; set; }

        [StringLength(50)]
        [Display(Name = "负责人手机")]
        public string ManagerPhone { get; set; }

        [StringLength(50)]
        [Display(Name = "业务类型")]
        public string SalesType { get; set; }

        [StringLength(50)]
        [Display(Name = "录入人")]
        public string CreateUserName { get; set; }
        [Display(Name = "录入时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? CreateTime { get; set; }

        [Display(Name = "业务记录日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? SalesLogDate { get; set; }

        [Display(Name = "人员投入")]
        public List<CompanySalesDailySalesSource> SalesSourceList { get; set; }

        [Display(Name = "产品销量")]
        public List<CompanySalesDailyProductDesp> SalesProductDespList { get; set; }

        [Display(Name = "产品比例")]
        public List<CompanySalesDailyParam> SalesProductPercentList { get; set; }

        [Display(Name = "营收信息")]
        public List<CompanySalesDailyParam> SalesProfitList { get; set; }
        
        [Display(Name = "资金情况")]
        public List<CompanySalesDailyFund> SalesFundList { get; set; }

        public CompanySalesDailyViewModel() { }

        public CompanySalesDailyViewModel(CompanySalesDaily dailyItem, List<CompanySalesDailySalesSource> salesSources, List<CompanySalesDailyProductDesp> salesProductDesps,
            List<CompanySalesDailyParam> paramList, List<CompanySalesDailyFund> salesFunds)
        {

            this.SalesType = dailyItem.SalesType;
            this.CompanyId = dailyItem.CompanyId;
            this.CompanyName = dailyItem.CompanyName;
            this.CreateTime = dailyItem.CreateTime;
            this.CreateUserName = dailyItem.CreateUserName;
            this.Id = dailyItem.Id;
            this.ManagerName = dailyItem.ManagerName;
            this.ManagerPhone = dailyItem.ManagerPhone;
            this.SalesLogDate = dailyItem.SalesLogDate;

            //人员投入
            this.SalesSourceList = salesSources;
            //资金情况
            this.SalesFundList = salesFunds;
            //产品销量        
            this.SalesProductDespList = salesProductDesps;
            //产品比例
            this.SalesProductPercentList = new List<CompanySalesDailyParam>();
            //营收信息
            this.SalesProfitList = new List<CompanySalesDailyParam>();

            foreach (CompanySalesDailyParam item in paramList)
            {
                switch (item.ParamName)
                {
                    case "产品结构":
                        this.SalesProductPercentList.Add(item);
                        break;
                    case "营收信息":
                        this.SalesProfitList.Add(item);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}