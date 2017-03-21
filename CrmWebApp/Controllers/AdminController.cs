using CrmWebApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CrmWebApp.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateCompanyStatus()
        {
            OtaCrmModel db = new OtaCrmModel();
            List<string> salesList = (from p in db.AspNetUsers
                                      select p.TrueName).ToList();
            salesList.Add("自营");
            salesList.Add("携程");
            var q = from p in db.OtaCompany
                    select p;
            foreach (OtaCompany item in q)
            {
                if (string.IsNullOrEmpty(item.BusinessRange) || !"国内,国际".Contains(item.BusinessRange))
                {
                    item.BusinessRange = "国内";
                }
                if (string.IsNullOrEmpty(item.BusinessStatus)|| !"在线,下线,终止,待终止,待上线".Contains(item.BusinessStatus))
                {
                    item.BusinessStatus = "在线";
                }
                //销售名字+自营，如果不在内得，需要更新为未知
                if (!salesList.Contains(item.SalesUserName))
                {
                    item.SalesUserName = "未知";
                }
            }
            db.SaveChangesAsync();
            return Content("更新完成.");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ImportCompanyFromCms()
        {
            OtaCrmModel db = new OtaCrmModel();
            var cityList = (from cc in db.ChinaCity select cc).ToList();

            var exsitNames = (from c in db.OtaCompany select c.CompanyName).Distinct();
            List<string> exsitNameList = exsitNames.ToList();

            var q = (from p in db.CompanyCmsData
                     select p);
            int count = 0;
            foreach (CompanyCmsData cmsItem in q)
            {
                string companyName = cmsItem.CompanyName;
                if (string.IsNullOrEmpty(companyName) || exsitNameList.Contains(companyName))
                {
                    continue;
                }
                OtaCompany newItem = new OtaCompany();
                newItem.BossBackground = cmsItem.BossBackground;
                newItem.BossBusinessDesp = "";
                newItem.BossIdNo = "";
                newItem.CompanyName = companyName;
                newItem.CreateTime = DateTime.Now;
                newItem.BossName = cmsItem.ContactPerson;
                string businessRange = "";
                if (!string.IsNullOrEmpty(cmsItem.GuojiWebName))
                {
                    businessRange = "国内";
                }
                if (!string.IsNullOrEmpty(cmsItem.GuojiWebName))
                {
                    businessRange = "国际";
                }
                if (!string.IsNullOrEmpty(cmsItem.GuojiWebName) && !string.IsNullOrEmpty(cmsItem.GuojiWebName))
                {
                    businessRange = "国内,国际";
                }
                newItem.BusinessRange = businessRange;
                string businessStatus = "在线";
                if (!string.IsNullOrEmpty(cmsItem.TTSStatusDesp) && cmsItem.TTSStatusDesp.Contains("终止"))
                {
                    businessStatus = "终止";
                }
                newItem.BusinessStatus = businessStatus;
                newItem.CapitalAsserts = "";
                newItem.CityName = "";

                foreach (var city in cityList)
                {
                    if (!string.IsNullOrEmpty(cmsItem.RealAddress) && cmsItem.RealAddress.Contains(city.CityName.Replace("市", "")))
                    {
                        newItem.CityName = city.ProvinceName + "-" + city.CityName;
                        break;
                    }
                }

                newItem.LegalPerson = string.IsNullOrEmpty(cmsItem.LegalPerson) ? "未知" : cmsItem.LegalPerson;
                newItem.LegalPersonIdNo = "";
                newItem.LegalPersonPhone = cmsItem.ContactPhone;
                newItem.OfficeNo = "";
                newItem.OtherInvest = "";
                newItem.RealAddress = cmsItem.RealAddress;
                newItem.RegisterAddress = cmsItem.RegisterAddress;
                newItem.SalesUserName = cmsItem.SalesName;

                db.OtaCompany.Add(newItem);
                count++;
            }
            db.SaveChanges();

            return Content("导入成功:" + count.ToString());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ImportCompanyFromAgentGrade()
        {
            OtaCrmModel db = new OtaCrmModel();
            var cityList = (from cc in db.ChinaCity select cc).ToList();

            var exsitNames = (from c in db.OtaCompany select c.CompanyName).Distinct();
            List<string> exsitNameList = exsitNames.ToList();

            var q = (from p in db.AgentGradeOperation
                     select p.agentName).Distinct();
            int count = 0;
            foreach (string companyName in q)
            {
                if (string.IsNullOrEmpty(companyName) || exsitNameList.Contains(companyName))
                {
                    continue;
                }
                OtaCompany newItem = new OtaCompany();
                newItem.BossBackground = "";
                newItem.BossBusinessDesp = "";
                newItem.BossIdNo = "";
                newItem.CompanyName = companyName;
                newItem.CreateTime = DateTime.Now;
                newItem.BossName = "";
                newItem.BusinessRange = "国内";
                newItem.BusinessStatus = "在线";
                newItem.CapitalAsserts = "";
                newItem.CityName = "";

                foreach (var city in cityList)
                {
                    if (companyName.Contains(city.CityName.Replace("市", "")))
                    {
                        newItem.CityName = city.ProvinceName + "-" + city.CityName;
                        break;
                    }
                }

                newItem.LegalPerson = "未知";
                newItem.LegalPersonIdNo = "";
                newItem.LegalPersonPhone = "";
                newItem.OfficeNo = "";
                newItem.OtherInvest = "";
                newItem.RealAddress = "";
                newItem.RegisterAddress = "";
                newItem.SalesUserName = "未知";

                db.OtaCompany.Add(newItem);
                count++;
            }
            db.SaveChanges();

            return Content("导入成功:" + count.ToString());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateLegal()
        {
            OtaCrmModel db = new OtaCrmModel();

            var companycmss = from p in db.CompanyCmsData
                              select p;
            Dictionary<string, string> cmsDict = new Dictionary<string, string>();
            foreach (CompanyCmsData cmsItem in companycmss)
            {
                if (!cmsDict.ContainsKey(cmsItem.CompanyName))
                {
                    cmsDict.Add(cmsItem.CompanyName, cmsItem.LegalPerson);
                }
            }

            db = new OtaCrmModel();
            var q = from o in db.OtaCompany
                    select o;
            List<OtaCompany> otacompanys = q.ToList();
            foreach (OtaCompany cItem in otacompanys)
            {
                if (string.IsNullOrEmpty(cItem.LegalPerson) || cItem.LegalPerson == "未知")
                {
                    if (cmsDict.ContainsKey(cItem.CompanyName))
                    {
                        cItem.LegalPerson = cmsDict[cItem.CompanyName];
                    }
                }

                if (string.IsNullOrEmpty(cItem.LegalPerson) || cItem.LegalPerson == "ww")
                {
                    cItem.LegalPerson = "未知";
                }
            }
            db.SaveChangesAsync();
            return Content("完成");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult GetCms()
        {
            StringBuilder sb = new StringBuilder();
            OtaCrmModel db = new OtaCrmModel();
            var q = from p in db.CompanyCmsData
                    select p;
            List<CompanyCmsData> cList = q.ToList();
            foreach (CompanyCmsData item in cList)
            {
                sb.Append(item.CompanyName).Append("\t").Append(item.LegalPerson).Append("\t").Append(item.TTSStatusDesp).Append("\r\n");
            }
            return Content(sb.ToString());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult GetSalesName()
        {
            StringBuilder sb = new StringBuilder("公司名\t法人\t销售\r\n");
            OtaCrmModel db = new OtaCrmModel();
            var q = from p in db.OtaCompany
                    select p;
            List<OtaCompany> cList = q.ToList();
            foreach (OtaCompany item in cList)
            {
                sb.Append(item.CompanyName).Append("\t").Append(item.LegalPerson).Append("\t").Append(item.SalesUserName).Append("\r\n");
            }
            return Content(sb.ToString());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateSalesName(string paramdata)
        {
            string[] lines = paramdata.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> companyDict = new Dictionary<string, string>();
            foreach (string line in lines)
            {
                string[] fields = line.Split(new char[] { '\t' });
                string companyName = fields[0].Trim();
                string salesName = fields[1].Trim();
                if (!companyDict.ContainsKey(companyName))
                {
                    companyDict.Add(companyName, salesName);
                }
            }
            OtaCrmModel db = new OtaCrmModel();
            var q = from p in db.OtaCompany
                    select p;
            List<OtaCompany> cList = q.ToList();
            foreach (OtaCompany item in cList)
            {
                if (companyDict.ContainsKey(item.CompanyName))
                {
                    item.SalesUserName = companyDict[item.CompanyName];
                }                
            }
            db.SaveChangesAsync();

            return Content("完成");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Update(string paramdata)
        {
            string[] fields = paramdata.Split(new char[] { '-' });
            string OrgName = fields[0];
            string curName = fields[1];
            //
            OtaCrmModel db = new OtaCrmModel();
            var q = from p in db.OtaCompany
                    where p.SalesUserName == OrgName
                    select p;
            foreach (OtaCompany item in q)
            {
                item.SalesUserName = curName;
            }
            db.SaveChangesAsync();

            return Content("完成");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DelCompanyCmsData()
        {
            OtaCrmModel db = new OtaCrmModel();
            string sql = "Delete From CompanyCmsData";
            db.Database.ExecuteSqlCommand(sql, null);
            string result = "成功删除.";
            return Content(result);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UpdateOtaCompany()
        {
            OtaCrmModel db = new OtaCrmModel();

            var companycmss = from p in db.CompanyCmsData
                              select p;
            Dictionary<string, CompanyCmsData> cmsDict = new Dictionary<string, CompanyCmsData>();
            foreach (CompanyCmsData cmsItem in companycmss)
            {
                if (!cmsDict.ContainsKey(cmsItem.CompanyName))
                {
                    cmsDict.Add(cmsItem.CompanyName, cmsItem);
                }
            }
            var q = from o in db.OtaCompany
                    select o;
            List<OtaCompany> otacompanys = q.ToList();

            int updateCount = 0;
            bool isUpdate = false;
            foreach (OtaCompany cItem in otacompanys)
            {
                if (cmsDict.ContainsKey(cItem.CompanyName))
                {
                    CompanyCmsData cmsItem = cmsDict[cItem.CompanyName];
                    //需要修改得地方是这些
                    if (!string.IsNullOrEmpty(cmsItem.SalesName))
                    {
                        cItem.SalesUserName = cmsItem.SalesName;
                    }
                    if (string.IsNullOrEmpty(cItem.RegisterAddress) && !string.IsNullOrEmpty(cmsItem.RegisterAddress))
                    {
                        isUpdate = true;
                        cItem.RegisterAddress = cmsItem.RegisterAddress;
                    }
                    if (string.IsNullOrEmpty(cItem.RealAddress) && !string.IsNullOrEmpty(cmsItem.RealAddress))
                    {
                        isUpdate = true;
                        cItem.RealAddress = cmsItem.RealAddress;
                    }
                    if (string.IsNullOrEmpty(cItem.BossBackground) && !string.IsNullOrEmpty(cmsItem.BossBackground))
                    {
                        isUpdate = true;
                        cItem.BossBackground = cmsItem.BossBackground;
                    }
                    if (string.IsNullOrEmpty(cItem.BossName) && !string.IsNullOrEmpty(cmsItem.ContactPerson))
                    {
                        isUpdate = true;
                        cItem.BossName = cmsItem.ContactPerson;
                    }
                    if (string.IsNullOrEmpty(cItem.BusinessStatus) && !string.IsNullOrEmpty(cmsItem.TTSStatusDesp))
                    {
                        isUpdate = true;
                        cItem.BusinessStatus = cmsItem.TTSStatusDesp;
                    }
                    if (string.IsNullOrEmpty(cItem.LegalPerson) && !string.IsNullOrEmpty(cmsItem.LegalPerson))
                    {
                        isUpdate = true;
                        cItem.LegalPerson = cmsItem.LegalPerson;
                    }
                    if (string.IsNullOrEmpty(cItem.LegalPersonPhone) && !string.IsNullOrEmpty(cmsItem.ContactPhone))
                    {
                        isUpdate = true;
                        cItem.LegalPersonPhone = cmsItem.ContactPhone;
                    }
                    if (isUpdate)
                    {
                        updateCount++;
                    }
                }
                isUpdate = false;
            }
            db.SaveChangesAsync();

            string result = "成功更新:" + updateCount.ToString();
            return Content(result);
        }
    }
}