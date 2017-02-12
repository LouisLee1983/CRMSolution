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
    public class CompanyBusinessDailiesController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanyBusinessDailies
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyBusinessDaily.ToListAsync());
        }

        // GET: CompanyBusinessDailies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDaily companyBusinessDaily = await db.CompanyBusinessDaily.FindAsync(id);
            if (companyBusinessDaily == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDaily);
        }

        // GET: CompanyBusinessDailies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyBusinessDailies/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyId,CompanyName,BussinessType,ManagerName,CreateUserName,CreateTime,BussinessLogDate")] CompanyBusinessDaily companyBusinessDaily)
        {
            if (ModelState.IsValid)
            {
                db.CompanyBusinessDaily.Add(companyBusinessDaily);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyBusinessDaily);
        }

        // GET: CompanyBusinessDailies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDaily companyBusinessDaily = await db.CompanyBusinessDaily.FindAsync(id);
            if (companyBusinessDaily == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDaily);
        }

        // POST: CompanyBusinessDailies/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyId,CompanyName,BussinessType,ManagerName,CreateUserName,CreateTime,BussinessLogDate")] CompanyBusinessDaily companyBusinessDaily)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyBusinessDaily).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyBusinessDaily);
        }

        // GET: CompanyBusinessDailies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDaily companyBusinessDaily = await db.CompanyBusinessDaily.FindAsync(id);
            if (companyBusinessDaily == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDaily);
        }

        // POST: CompanyBusinessDailies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyBusinessDaily companyBusinessDaily = await db.CompanyBusinessDaily.FindAsync(id);
            db.CompanyBusinessDaily.Remove(companyBusinessDaily);
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
