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
    public class CompanyBusinessDailyPhotoesController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanyBusinessDailyPhotoes
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyBusinessDailyPhoto.ToListAsync());
        }

        // GET: CompanyBusinessDailyPhotoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailyPhoto companyBusinessDailyPhoto = await db.CompanyBusinessDailyPhoto.FindAsync(id);
            if (companyBusinessDailyPhoto == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailyPhoto);
        }

        // GET: CompanyBusinessDailyPhotoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyBusinessDailyPhotoes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyBusinessDailyId,PhotoUrl,PhotoName")] CompanyBusinessDailyPhoto companyBusinessDailyPhoto)
        {
            if (ModelState.IsValid)
            {
                db.CompanyBusinessDailyPhoto.Add(companyBusinessDailyPhoto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyBusinessDailyPhoto);
        }

        // GET: CompanyBusinessDailyPhotoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailyPhoto companyBusinessDailyPhoto = await db.CompanyBusinessDailyPhoto.FindAsync(id);
            if (companyBusinessDailyPhoto == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailyPhoto);
        }

        // POST: CompanyBusinessDailyPhotoes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyBusinessDailyId,PhotoUrl,PhotoName")] CompanyBusinessDailyPhoto companyBusinessDailyPhoto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyBusinessDailyPhoto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyBusinessDailyPhoto);
        }

        // GET: CompanyBusinessDailyPhotoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailyPhoto companyBusinessDailyPhoto = await db.CompanyBusinessDailyPhoto.FindAsync(id);
            if (companyBusinessDailyPhoto == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailyPhoto);
        }

        // POST: CompanyBusinessDailyPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyBusinessDailyPhoto companyBusinessDailyPhoto = await db.CompanyBusinessDailyPhoto.FindAsync(id);
            db.CompanyBusinessDailyPhoto.Remove(companyBusinessDailyPhoto);
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
