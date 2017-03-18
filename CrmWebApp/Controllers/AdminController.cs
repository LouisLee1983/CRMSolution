using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrmWebApp.Models;

namespace CrmWebApp.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
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
            var otacompanys = from p in db.OtaCompany
                              select p;

            foreach (OtaCompany cItem in otacompanys)
            {
                if (cmsDict.ContainsKey(cItem.CompanyName))
                {
                    CompanyCmsData cmsItem = cmsDict[cItem.CompanyName];
                    //需要修改得地方是这些
                    cItem.SalesUserName = string.IsNullOrEmpty(cItem.SalesUserName)? cmsItem.SalesName:"";
                    cItem.RegisterAddress = string.IsNullOrEmpty(cItem.RegisterAddress)? cmsItem.RegisterAddress:"";
                    cItem.RealAddress = string.IsNullOrEmpty(cItem.RealAddress)? cmsItem.RealAddress:"";
                    cItem.BossBackground = string.IsNullOrEmpty(cItem.BossBackground)? cmsItem.BossBackground:"";
                    cItem.BossName = string.IsNullOrEmpty(cItem.BossName)? cmsItem.ContactPerson:"";
                    cItem.BusinessStatus = string.IsNullOrEmpty(cItem.BusinessStatus)? cmsItem.TTSStatusDesp:"";
                    cItem.LegalPerson = string.IsNullOrEmpty(cItem.LegalPerson)? cmsItem.LegalPerson:"";
                    cItem.LegalPersonPhone = string.IsNullOrEmpty(cItem.LegalPersonPhone)? cmsItem.ContactPhone:"";
                    
                }
            }
        }
    }
}