﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrmWebApp.Models;
using PagedList;

namespace CrmWebApp.Controllers
{
    public class OtaCompaniesController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        // GET: OtaCompanies
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, string businessStatus, int? page)
        {
            //根据user来确定公司结果
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SalesUserNameSortParm = string.IsNullOrEmpty(sortOrder) ? "salesUserName_desc" : "salesUserName";
            ViewBag.CityNameSortParm = sortOrder == "cityName" ? "cityName_desc" : "cityName";
            ViewBag.LastMeetingDateSortParm = sortOrder == "lastMeetingDate" ? "lastMeetingDate_desc" : "lastMeetingDate";
            ViewBag.BusinessStatusSortParm = sortOrder == "businessStatus" ? "businessStatus_desc" : "businessStatus";
            ViewData["BusinessStatusList"] = GetParamDictList("业务状态", businessStatus);
            if (businessStatus == null)
            {
                ViewBag.BusinessStatus = "";
            }
            else
            {
                ViewBag.BusinessStatus = businessStatus;
            }
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            //总监能看到所有人的，区域经理能看到本区域的，需要进行打勾，其他的销售只能看到自己的

            var otaCompanys = from p in db.OtaCompany
                              select p;
            if (!string.IsNullOrEmpty(businessStatus))
            {
                otaCompanys = otaCompanys.Where(p => p.BusinessStatus == businessStatus);
            }
            if (!User.IsInRole("SalesDirector") && !User.IsInRole("Admin"))
            {
                if (User.IsInRole("AreaManager"))
                {
                    //找出本区域的所有人，并把它作为条件加入查询结果中
                    ServeAreasController sac = new ServeAreasController();
                    List<string> myAreaUserNameList = sac.GetMyAreaUserNames(User.Identity.Name);
                    AccountController ac = new AccountController();
                    List<string> realNameList = new List<string>();
                    realNameList.Add("未知");
                    foreach (string userName in myAreaUserNameList)
                    {
                        string realName = ac.GetRealName(userName);
                        realNameList.Add(realName);
                    }
                    otaCompanys = otaCompanys.Where(p => realNameList.Contains(p.SalesUserName));
                }
                else if (User.IsInRole("OtaSales"))
                {
                    //销售只能看到本人的                    
                    AccountController ac = new AccountController();
                    string realName = ac.GetRealName(User.Identity.Name);
                    otaCompanys = otaCompanys.Where(p => p.SalesUserName == realName || p.SalesUserName == "未知");
                }
            }
            //搜索
            if (!string.IsNullOrEmpty(searchString))
            {
                otaCompanys = otaCompanys.Where(p => p.CompanyName.Contains(searchString)
                    || p.SalesUserName == searchString
                    || p.BossName.Contains(searchString)
                    || p.LegalPerson.Contains(searchString)
                    || p.CityName.Contains(searchString)
                    || p.BusinessStatus.Contains(searchString)
                    || p.BusinessRange.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "salesUserName_desc":
                    otaCompanys = otaCompanys.OrderByDescending(o => o.SalesUserName);
                    break;
                case "salesUserName":
                    otaCompanys = otaCompanys.OrderBy(o => o.SalesUserName);
                    break;
                case "businessStatus_desc":
                    otaCompanys = otaCompanys.OrderByDescending(o => o.BusinessStatus);
                    break;
                case "businessStatus":
                    otaCompanys = otaCompanys.OrderBy(o => o.BusinessStatus);
                    break;
                case "cityName_desc":
                    otaCompanys = otaCompanys.OrderByDescending(o => o.CityName);
                    break;
                case "cityName":
                    otaCompanys = otaCompanys.OrderBy(o => o.CityName);
                    break;
                case "lastMeetingDate":
                    otaCompanys = otaCompanys.OrderBy(o => o.LastMeetingDate);
                    break;
                case "lastMeetingDate_desc":
                    otaCompanys = otaCompanys.OrderByDescending(o => o.LastMeetingDate);
                    break;
                default:
                    otaCompanys = otaCompanys.OrderBy(o => o.BusinessStatus);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(otaCompanys.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CompanyMap()
        {
            return View();
        }

        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public JsonResult GetCompanyJson()
        {
            var res = new JsonResult();
            OtaCrmModel db = new OtaCrmModel();
            var list = from p in db.OtaCompany
                       where p.RealAddress != null
                       select new
                       {
                           id = p.Id,
                           name = p.CompanyName,
                           address = p.RealAddress,
                           salesname = p.SalesUserName
                       };
            //记得这里要select new 否则会报错：序列化类型 System.Data.Entity.DynamicProxies XXXXX 的对象时检测到循环引用。  
            //不select new 也行的加上这句 //db.ContextOptions.ProxyCreationEnabled = false;  
            res.Data = list;//返回列表  

            //var name = "小华";
            //var age = "12";
            //var name1 = "小华";
            //var age1 = "12";
            //res.Data = new object[] { new { name, age }, new { name1, age1 } };//返回一个自定义的object数组  

            //var person = new { Name = "小明", Age = 22, Sex = "男" };
            //res.Data = person;//返回单个对象；  

            //res.Data = "这是个字符串";//返回一个字符串,意义不大；  

            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;//允许使用GET方式获取，否则用GET获取是会报错。  
            return res;
        }

        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult EditCompanyAll()
        {
            AccountController ac = new AccountController();
            string realName = ac.GetRealName(User.Identity.Name);

            var model = new List<CompanyEditViewModel>();
            //把未知的和自己的客户列出去，未知的排在前面。然后用radio的方式给展示
            var q = (from p in db.OtaCompany
                     where p.SalesUserName == "未知" || p.SalesUserName == realName
                     orderby p.CreateTime, p.SalesUserName
                     select p).Take(50);
            foreach (OtaCompany item in q)
            {
                CompanyEditViewModel editItem = new CompanyEditViewModel();
                editItem.BusinessRange = item.BusinessRange;
                editItem.BusinessStatus = item.BusinessStatus;
                editItem.CompanyName = item.CompanyName;
                editItem.Id = item.Id;
                editItem.SalesUserName = item.SalesUserName;
                model.Add(editItem);
            }
            var BusinessRangeList = (from p in db.ParamDict
                                     where p.ParamName == "业务类型"
                                     select p.SubItemName).ToList();
            ViewData["BusinessRangeList"] = BusinessRangeList;
            var BusinessStatusList = (from p in db.ParamDict
                                      where p.ParamName == "业务状态"
                                      select p.SubItemName).ToList();
            ViewData["BusinessStatusList"] = BusinessStatusList;
            List<string> allRealNames = ac.GetAllRealName();
            allRealNames.Remove("admin");
            allRealNames.Remove("郑亚军");
            allRealNames.Add("未知");
            ViewData["SalesNameList"] = allRealNames;

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult EditCompanySingle(int id, string businessRange, string businessStatus, string salesName)
        {
            var OtaCompany = db.OtaCompany.FirstOrDefault(p => p.Id == id);
            OtaCompany.BusinessRange = businessRange;
            OtaCompany.BusinessStatus = businessStatus;
            OtaCompany.SalesUserName = salesName;
            OtaCompany.CreateTime = DateTime.Now;
            db.SaveChanges();
            return Content("更新完成.");
        }

        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        // GET: OtaCompanies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtaCompany otaCompany = await db.OtaCompany.FindAsync(id);
            if (otaCompany == null)
            {
                return HttpNotFound();
            }

            ViewData["CompanyCertificateList"] = db.CompanyCertificate.Where(p => p.CompanyId == id).ToList();
            return View(otaCompany);
        }

        // GET: OtaCompanies/Create
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult Create()
        {
            var model = new OtaCompany();
            model.BossBackground = "机票代理人";
            model.BossBusinessDesp = "民航";
            model.CreateTime = DateTime.Now;
            string realName = new AccountController().GetRealName(User.Identity.Name);
            model.SalesUserName = realName;

            ViewData["ChinaCityList"] = GetChinaCityList("");
            ViewData["BusinessRangeList"] = GetParamDictList("业务类型", "国内");
            ViewData["BusinessStatusList"] = GetParamDictList("业务状态", "在线");

            return View(model);
        }

        private List<SelectListItem> GetSalesNameList(string defaultValue)
        {
            List<string> realNameList = (from p in db.AspNetUsers
                                         select p.TrueName).ToList();
            realNameList.Add("自营");
            realNameList.Add("航司");
            realNameList.Add("携程");
            List<SelectListItem> result = new List<SelectListItem>();

            SelectListItem selectItem = new SelectListItem();
            selectItem.Value = "未知";
            selectItem.Text = "未知";
            if (string.IsNullOrEmpty(defaultValue))
            {
                selectItem.Selected = true;
            }
            result.Add(selectItem);
            foreach (string realName in realNameList)
            {
                selectItem = new SelectListItem();
                selectItem.Value = realName;
                selectItem.Text = realName;
                if (defaultValue == realName)
                {
                    selectItem.Selected = true;
                }
                result.Add(selectItem);
            }

            return result;
        }

        private List<SelectListItem> GetParamDictList(string paramName, string defaultValue)
        {
            var bussinessTypes = from p in db.ParamDict
                                 where p.ParamName == paramName
                                 select p;
            List<SelectListItem> result = new List<SelectListItem>();

            SelectListItem selectItem = new SelectListItem();
            selectItem.Value = "";
            selectItem.Text = "";
            if (string.IsNullOrEmpty(defaultValue))
            {
                selectItem.Selected = true;
            }
            result.Add(selectItem);

            foreach (var item in bussinessTypes)
            {
                SelectListItem newItem = new SelectListItem();
                newItem.Value = item.SubItemName;
                newItem.Text = item.SubItemName;
                if (!string.IsNullOrEmpty(defaultValue) && defaultValue == item.SubItemName)
                {
                    newItem.Selected = true;
                }
                result.Add(newItem);
            }
            return result;
        }

        public List<SelectListItem> GetChinaCityList(string defaultValue)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            SelectListItem selectListItem = new SelectListItem();
            selectListItem.Text = "";
            selectListItem.Value = "";
            if (string.IsNullOrEmpty(defaultValue))
            {
                selectListItem.Selected = true;
            }
            result.Add(selectListItem);

            var c = from p in db.ChinaCity
                    orderby p.ProvinceName, p.CityName
                    select p.ProvinceName + "-" + p.CityName;
            foreach (var item in c)
            {
                SelectListItem newItem = new SelectListItem();
                newItem.Text = item.ToString();
                newItem.Value = item.ToString();
                if (item.ToString() == defaultValue)
                {
                    newItem.Selected = true;
                }
                result.Add(newItem);
            }
            return result;
        }

        // POST: OtaCompanies/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OtaCompany otaCompany)
        {
            if (ModelState.IsValid)
            {
                db.OtaCompany.Add(otaCompany);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(otaCompany);
        }

        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        // GET: OtaCompanies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtaCompany otaCompany = await db.OtaCompany.FindAsync(id);
            if (otaCompany == null)
            {
                return HttpNotFound();
            }
            //如果状态不在范围内，需要重置
            ViewData["ChinaCityList"] = GetChinaCityList(otaCompany.CityName);
            ViewData["BusinessRangeList"] = GetParamDictList("业务类型", otaCompany.BusinessRange);
            ViewData["BusinessStatusList"] = GetParamDictList("业务状态", otaCompany.BusinessStatus);
            ViewData["SalesNameList"] = GetSalesNameList(otaCompany.SalesUserName);

            return View(otaCompany);
        }

        // POST: OtaCompanies/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OtaCompany otaCompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(otaCompany).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = otaCompany.Id });
            }
            return View(otaCompany);
        }

        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        // GET: OtaCompanies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtaCompany otaCompany = await db.OtaCompany.FindAsync(id);
            if (otaCompany == null)
            {
                return HttpNotFound();
            }
            return View(otaCompany);
        }


        // POST: OtaCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OtaCompany otaCompany = await db.OtaCompany.FindAsync(id);
            db.OtaCompany.Remove(otaCompany);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
