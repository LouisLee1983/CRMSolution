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
                sb.Append(item.CompanyName).Append("\t").Append(item.LegalPerson).Append("\r\n");
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
                if (!string.IsNullOrEmpty(item.BusinessStatus))
                {
                    //更新业务
                    string businessRange = "";
                    if (item.BusinessStatus.Contains("国内"))
                    {
                        businessRange = "国内";
                    }
                    if (item.BusinessStatus.Contains("国际"))
                    {
                        businessRange = "国际";
                    }
                    if (item.BusinessStatus.Contains("国内") && item.BusinessStatus.Contains("国际"))
                    {
                        businessRange = "国内,国际";
                    }
                    item.BusinessRange = businessRange;
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