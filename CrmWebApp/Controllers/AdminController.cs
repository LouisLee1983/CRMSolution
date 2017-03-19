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