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
    public class CompanyMediasController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanyMedias
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyMedia.ToListAsync());
        }

        // GET: CompanyMedias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMedia companyMedia = await db.CompanyMedia.FindAsync(id);
            if (companyMedia == null)
            {
                return HttpNotFound();
            }
            return View(companyMedia);
        }

        // GET: CompanyMedias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyMedias/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,OuterKeyId,MediaFor,MediaName,MediaUrl")] CompanyMedia companyMedia)
        {
            if (ModelState.IsValid)
            {
                db.CompanyMedia.Add(companyMedia);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyMedia);
        }

        // GET: CompanyMedias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMedia companyMedia = await db.CompanyMedia.FindAsync(id);
            if (companyMedia == null)
            {
                return HttpNotFound();
            }
            return View(companyMedia);
        }

        // POST: CompanyMedias/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,OuterKeyId,MediaFor,MediaName,MediaUrl")] CompanyMedia companyMedia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyMedia).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyMedia);
        }

        // GET: CompanyMedias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMedia companyMedia = await db.CompanyMedia.FindAsync(id);
            if (companyMedia == null)
            {
                return HttpNotFound();
            }
            return View(companyMedia);
        }

        // POST: CompanyMedias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyMedia companyMedia = await db.CompanyMedia.FindAsync(id);
            db.CompanyMedia.Remove(companyMedia);
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
