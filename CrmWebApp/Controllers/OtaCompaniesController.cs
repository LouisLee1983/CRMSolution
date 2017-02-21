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

        // GET: OtaCompanies
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            //根据user来确定公司结果
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SalesUserNameSortParm = string.IsNullOrEmpty(sortOrder) ? "salesUserName_desc" : "";
            ViewBag.CityNameSortParm = sortOrder == "cityName" ? "cityName_desc" : "cityName";
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
                otaCompanys = otaCompanys.Where(p => p.CompanyName.Contains(searchString) || p.BossName.Contains(searchString) || p.LegalPerson.Contains(searchString));
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
                default:
                    otaCompanys = otaCompanys.OrderBy(o => o.CompanyName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(otaCompanys.ToPagedList(pageNumber, pageSize));
        }

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
        public ActionResult Create()
        {
            var model = new OtaCompany();
            model.BossBackground = "机票代理人";
            model.BossBusinessDesp = "民航";
            model.CreateTime = DateTime.Now;
            model.SalesUserName = User.Identity.Name;
            return View(model);
        }

        // POST: OtaCompanies/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
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
            return View(otaCompany);
        }

        // POST: OtaCompanies/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
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
