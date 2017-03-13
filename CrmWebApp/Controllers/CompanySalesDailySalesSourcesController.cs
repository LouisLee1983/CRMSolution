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
    public class CompanySalesDailySalesSourcesController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanySalesDailySalesSources
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanySalesDailySalesSource.ToListAsync());
        }

        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult AddNew(int dailyId)
        {
            var model = new CompanySalesDailySalesSource();
            model.CompanySalesDailyId = dailyId;
            model.Id = 0;
            model.EmployeeCount = 0;
            model.EmployeePayment = 0;
            model.SaleSource = "其他";
            model.TicketCount = 0;

            return PartialView("_PartialAddDailySalesSource", model);
        }

        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult AddNew(CompanySalesDailySalesSource model)
        {
            db.CompanySalesDailySalesSource.Add(model);
            db.SaveChanges();

            return RedirectToAction("Edit", "CompanySalesDailies", new { id = model.CompanySalesDailyId });
        }

        // GET: CompanySalesDailySalesSources/Details/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanySalesDailySalesSource companySalesDailySalesSource = await db.CompanySalesDailySalesSource.FindAsync(id);
            if (companySalesDailySalesSource == null)
            {
                return HttpNotFound();
            }
            return View(companySalesDailySalesSource);
        }

        // GET: CompanySalesDailySalesSources/Create
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanySalesDailySalesSources/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanySalesDailyId,SaleSource,EmployeeCount,TicketCount,EmployeePayment")] CompanySalesDailySalesSource companySalesDailySalesSource)
        {
            if (ModelState.IsValid)
            {
                db.CompanySalesDailySalesSource.Add(companySalesDailySalesSource);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companySalesDailySalesSource);
        }

        // GET: CompanySalesDailySalesSources/Edit/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanySalesDailySalesSource companySalesDailySalesSource = await db.CompanySalesDailySalesSource.FindAsync(id);
            if (companySalesDailySalesSource == null)
            {
                return HttpNotFound();
            }
            return View(companySalesDailySalesSource);
        }

        // POST: CompanySalesDailySalesSources/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanySalesDailyId,SaleSource,EmployeeCount,TicketCount,EmployeePayment")] CompanySalesDailySalesSource companySalesDailySalesSource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companySalesDailySalesSource).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companySalesDailySalesSource);
        }

        // GET: CompanySalesDailySalesSources/Delete/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanySalesDailySalesSource companySalesDailySalesSource = await db.CompanySalesDailySalesSource.FindAsync(id);
            if (companySalesDailySalesSource == null)
            {
                return HttpNotFound();
            }
            return View(companySalesDailySalesSource);
        }

        // POST: CompanySalesDailySalesSources/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanySalesDailySalesSource companySalesDailySalesSource = await db.CompanySalesDailySalesSource.FindAsync(id);
            db.CompanySalesDailySalesSource.Remove(companySalesDailySalesSource);
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
