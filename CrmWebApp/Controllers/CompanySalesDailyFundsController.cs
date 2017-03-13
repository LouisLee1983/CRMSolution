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
    public class CompanySalesDailyFundsController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanySalesDailyFunds
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanySalesDailyFund.ToListAsync());
        }

        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult AddNew(int dailyId)
        {
            var model = new CompanySalesDailyFund();
            model.CompanySalesDailyId = dailyId;
            model.Id = 0;
            model.FreezeFund = 0;
            model.NeededFund = 0;
            model.SalesSource = "其他";
            model.WorkingFund = 0;

            return PartialView("_PartialAddSalesDailyFund", model);
        }

        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult AddNew(CompanySalesDailyFund model)
        {
            db.CompanySalesDailyFund.Add(model);
            db.SaveChanges();

            return RedirectToAction("Edit", "CompanySalesDailies", new { id = model.CompanySalesDailyId });
        }

        // GET: CompanySalesDailyFunds/Details/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanySalesDailyFund companySalesDailyFund = await db.CompanySalesDailyFund.FindAsync(id);
            if (companySalesDailyFund == null)
            {
                return HttpNotFound();
            }
            return View(companySalesDailyFund);
        }

        // GET: CompanySalesDailyFunds/Create
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanySalesDailyFunds/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanySalesDailyId,SalesSource,FreezeFund,WorkingFund,NeededFund")] CompanySalesDailyFund companySalesDailyFund)
        {
            if (ModelState.IsValid)
            {
                db.CompanySalesDailyFund.Add(companySalesDailyFund);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companySalesDailyFund);
        }

        // GET: CompanySalesDailyFunds/Edit/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanySalesDailyFund companySalesDailyFund = await db.CompanySalesDailyFund.FindAsync(id);
            if (companySalesDailyFund == null)
            {
                return HttpNotFound();
            }
            return View(companySalesDailyFund);
        }

        // POST: CompanySalesDailyFunds/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanySalesDailyId,SalesSource,FreezeFund,WorkingFund,NeededFund")] CompanySalesDailyFund companySalesDailyFund)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companySalesDailyFund).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companySalesDailyFund);
        }

        // GET: CompanySalesDailyFunds/Delete/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanySalesDailyFund companySalesDailyFund = await db.CompanySalesDailyFund.FindAsync(id);
            if (companySalesDailyFund == null)
            {
                return HttpNotFound();
            }
            return View(companySalesDailyFund);
        }

        // POST: CompanySalesDailyFunds/Delete/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanySalesDailyFund companySalesDailyFund = await db.CompanySalesDailyFund.FindAsync(id);
            db.CompanySalesDailyFund.Remove(companySalesDailyFund);
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
