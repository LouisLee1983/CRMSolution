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
    public class CompanyBusinessDailySoundRecordsController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanyBusinessDailySoundRecords
        public async Task<ActionResult> Index(int? dailyId)
        {
            var model = from cbd in db.CompanyBusinessDailySoundRecord
                        select cbd;
            if (dailyId.HasValue)
            {
                model = model.Where(p => p.CompanyBusinessDailyId == dailyId.Value);
                ViewBag.DailyId = dailyId.Value;
            }

            return View(await model.ToListAsync());
        }

        // GET: CompanyBusinessDailySoundRecords/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailySoundRecord companyBusinessDailySoundRecord = await db.CompanyBusinessDailySoundRecord.FindAsync(id);
            if (companyBusinessDailySoundRecord == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailySoundRecord);
        }

        // GET: CompanyBusinessDailySoundRecords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyBusinessDailySoundRecords/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyBusinessDailyId,SoundRecordName,SoundRecordUrl")] CompanyBusinessDailySoundRecord companyBusinessDailySoundRecord)
        {
            if (ModelState.IsValid)
            {
                db.CompanyBusinessDailySoundRecord.Add(companyBusinessDailySoundRecord);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyBusinessDailySoundRecord);
        }

        // GET: CompanyBusinessDailySoundRecords/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailySoundRecord companyBusinessDailySoundRecord = await db.CompanyBusinessDailySoundRecord.FindAsync(id);
            if (companyBusinessDailySoundRecord == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailySoundRecord);
        }

        // POST: CompanyBusinessDailySoundRecords/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyBusinessDailyId,SoundRecordName,SoundRecordUrl")] CompanyBusinessDailySoundRecord companyBusinessDailySoundRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyBusinessDailySoundRecord).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyBusinessDailySoundRecord);
        }

        // GET: CompanyBusinessDailySoundRecords/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailySoundRecord companyBusinessDailySoundRecord = await db.CompanyBusinessDailySoundRecord.FindAsync(id);
            if (companyBusinessDailySoundRecord == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailySoundRecord);
        }

        // POST: CompanyBusinessDailySoundRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyBusinessDailySoundRecord companyBusinessDailySoundRecord = await db.CompanyBusinessDailySoundRecord.FindAsync(id);
            db.CompanyBusinessDailySoundRecord.Remove(companyBusinessDailySoundRecord);
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
