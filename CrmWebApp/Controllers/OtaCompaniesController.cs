using System;
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
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            //根据user来确定公司结果
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SalesUserNameSortParm = string.IsNullOrEmpty(sortOrder) ? "salesUserName_desc" : "";
            ViewBag.CityNameSortParm = sortOrder == "cityName" ? "cityName_desc" : "cityName";
            ViewBag.LastMeetingDateSortParm = sortOrder == "lastMeetingDate" ? "lastMeetingDate_desc" : "lastMeetingDate";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var otaCompanys = from p in db.OtaCompany
                              select p;

            //搜索
            if (!string.IsNullOrEmpty(searchString))
            {
                otaCompanys = otaCompanys.Where(p => p.CompanyName.Contains(searchString)
                    || p.BossName.Contains(searchString)
                    || p.LegalPerson.Contains(searchString)
                    || p.CityName.Contains(searchString)
                    || p.BusnessRange.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "salesUserName_desc":
                    otaCompanys = otaCompanys.OrderByDescending(o => o.SalesUserName);
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
                    otaCompanys = otaCompanys.OrderBy(o => o.CompanyName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(otaCompanys.ToPagedList(pageNumber, pageSize));
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
            model.SalesUserName = User.Identity.Name;

            ViewData["ChinaCityList"] = GetChinaCityList("");
            ViewData["BusnessRangeList"] = GetBussinessTypeList("国内");

            return View(model);
        }
        
        private List<SelectListItem> GetBussinessTypeList(string defaultValue)
        {
            var bussinessTypes = from p in db.ParamDict
                                 where p.ParamName == "业务类型"
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
                    orderby p.ProvinceName,p.CityName
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
            ViewData["ChinaCityList"] = GetChinaCityList(otaCompany.CityName);
            ViewData["BusnessRangeList"] = GetBussinessTypeList(otaCompany.BusnessRange);

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
                return RedirectToAction("Index");
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
