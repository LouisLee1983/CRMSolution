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

namespace CrmWebApp.Controllers
{
    public class CompanyBusinessDailyParamsController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanyBusinessDailyParams
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Index(int? dailyId)
        {
            var model = from cbd in db.CompanyBusinessDailyParam
                        select cbd;
            if (dailyId.HasValue)
            {
                model = model.Where(p => p.CompanyBusinessDailyId == dailyId.Value);
                ViewBag.DailyId = dailyId.Value;
            }

            return View(await model.ToListAsync());
        }

        // GET: CompanyBusinessDailyParams/Details/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailyParam companyBusinessDailyParam = await db.CompanyBusinessDailyParam.FindAsync(id);
            if (companyBusinessDailyParam == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailyParam);
        }

        // GET: CompanyBusinessDailyParams/Create
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult Create(int? dailyId)
        {
            var model = new CompanyBusinessDailyParam();
            if (dailyId.HasValue)
            {
                model.CompanyBusinessDailyId = dailyId.Value;
            }
            return View(model);
        }

        // POST: CompanyBusinessDailyParams/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyBusinessDailyId,ParamName,SubParamItem,ItemAmount")] CompanyBusinessDailyParam companyBusinessDailyParam)
        {
            if (ModelState.IsValid)
            {
                db.CompanyBusinessDailyParam.Add(companyBusinessDailyParam);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyBusinessDailyParam);
        }

        // GET: CompanyBusinessDailyParams/Edit/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailyParam companyBusinessDailyParam = await db.CompanyBusinessDailyParam.FindAsync(id);
            if (companyBusinessDailyParam == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailyParam);
        }

        // POST: CompanyBusinessDailyParams/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyBusinessDailyId,ParamName,SubParamItem,ItemAmount")] CompanyBusinessDailyParam companyBusinessDailyParam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyBusinessDailyParam).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyBusinessDailyParam);
        }

        // GET: CompanyBusinessDailyParams/Delete/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailyParam companyBusinessDailyParam = await db.CompanyBusinessDailyParam.FindAsync(id);
            if (companyBusinessDailyParam == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailyParam);
        }

        // POST: CompanyBusinessDailyParams/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyBusinessDailyParam companyBusinessDailyParam = await db.CompanyBusinessDailyParam.FindAsync(id);
            db.CompanyBusinessDailyParam.Remove(companyBusinessDailyParam);
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
