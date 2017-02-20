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
    public class CompanySalesDailyProductDespsController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanySalesDailyProductDesps
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanySalesDailyProductDesp.ToListAsync());
        }


        public ActionResult AddNew(int dailyId)
        {
            var model = new CompanySalesDailyProductDesp();
            model.CompanySalesDailyId = dailyId;
            model.Id = 0;
            model.SalesCount = 0;
            model.SalesProduct = "其他";
            model.SalesSource = "其他";

            return PartialView("_PartialAddDailySalesProductDesp", model);
        }

        [HttpPost]
        public ActionResult AddNew(CompanySalesDailyProductDesp model)
        {
            db.CompanySalesDailyProductDesp.Add(model);
            db.SaveChanges();

            return RedirectToAction("Edit", "CompanySalesDailies", new { id = model.CompanySalesDailyId });
        }

        // GET: CompanySalesDailyProductDesps/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanySalesDailyProductDesp companySalesDailyProductDesp = await db.CompanySalesDailyProductDesp.FindAsync(id);
            if (companySalesDailyProductDesp == null)
            {
                return HttpNotFound();
            }
            return View(companySalesDailyProductDesp);
        }

        // GET: CompanySalesDailyProductDesps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanySalesDailyProductDesps/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanySalesDailyId,SalesSource,SalesProduct,SalesCount")] CompanySalesDailyProductDesp companySalesDailyProductDesp)
        {
            if (ModelState.IsValid)
            {
                db.CompanySalesDailyProductDesp.Add(companySalesDailyProductDesp);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companySalesDailyProductDesp);
        }

        // GET: CompanySalesDailyProductDesps/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanySalesDailyProductDesp companySalesDailyProductDesp = await db.CompanySalesDailyProductDesp.FindAsync(id);
            if (companySalesDailyProductDesp == null)
            {
                return HttpNotFound();
            }
            return View(companySalesDailyProductDesp);
        }

        // POST: CompanySalesDailyProductDesps/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanySalesDailyId,SalesSource,SalesProduct,SalesCount")] CompanySalesDailyProductDesp companySalesDailyProductDesp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companySalesDailyProductDesp).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companySalesDailyProductDesp);
        }

        // GET: CompanySalesDailyProductDesps/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanySalesDailyProductDesp companySalesDailyProductDesp = await db.CompanySalesDailyProductDesp.FindAsync(id);
            if (companySalesDailyProductDesp == null)
            {
                return HttpNotFound();
            }
            return View(companySalesDailyProductDesp);
        }

        // POST: CompanySalesDailyProductDesps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanySalesDailyProductDesp companySalesDailyProductDesp = await db.CompanySalesDailyProductDesp.FindAsync(id);
            db.CompanySalesDailyProductDesp.Remove(companySalesDailyProductDesp);
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
