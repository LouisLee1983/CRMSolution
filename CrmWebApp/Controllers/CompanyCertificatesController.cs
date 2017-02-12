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
    public class CompanyCertificatesController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanyCertificates
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyCertificate.ToListAsync());
        }

        // GET: CompanyCertificates/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyCertificate companyCertificate = await db.CompanyCertificate.FindAsync(id);
            if (companyCertificate == null)
            {
                return HttpNotFound();
            }
            return View(companyCertificate);
        }

        // GET: CompanyCertificates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyCertificates/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyId,CertificateName,CompanyName,PictureUrl,CreateTime,CreateUserName")] CompanyCertificate companyCertificate)
        {
            if (ModelState.IsValid)
            {
                db.CompanyCertificate.Add(companyCertificate);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyCertificate);
        }

        // GET: CompanyCertificates/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyCertificate companyCertificate = await db.CompanyCertificate.FindAsync(id);
            if (companyCertificate == null)
            {
                return HttpNotFound();
            }
            return View(companyCertificate);
        }

        // POST: CompanyCertificates/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyId,CertificateName,CompanyName,PictureUrl,CreateTime,CreateUserName")] CompanyCertificate companyCertificate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyCertificate).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyCertificate);
        }

        // GET: CompanyCertificates/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyCertificate companyCertificate = await db.CompanyCertificate.FindAsync(id);
            if (companyCertificate == null)
            {
                return HttpNotFound();
            }
            return View(companyCertificate);
        }

        // POST: CompanyCertificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyCertificate companyCertificate = await db.CompanyCertificate.FindAsync(id);
            db.CompanyCertificate.Remove(companyCertificate);
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
