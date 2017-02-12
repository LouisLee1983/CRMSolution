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
    public class SalesServeAreasController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: SalesServeAreas
        public async Task<ActionResult> Index()
        {
            return View(await db.SalesServeArea.ToListAsync());
        }

        // GET: SalesServeAreas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesServeArea salesServeArea = await db.SalesServeArea.FindAsync(id);
            if (salesServeArea == null)
            {
                return HttpNotFound();
            }
            return View(salesServeArea);
        }

        // GET: SalesServeAreas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesServeAreas/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,UserName,Province,City")] SalesServeArea salesServeArea)
        {
            if (ModelState.IsValid)
            {
                db.SalesServeArea.Add(salesServeArea);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(salesServeArea);
        }

        // GET: SalesServeAreas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesServeArea salesServeArea = await db.SalesServeArea.FindAsync(id);
            if (salesServeArea == null)
            {
                return HttpNotFound();
            }
            return View(salesServeArea);
        }

        // POST: SalesServeAreas/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UserName,Province,City")] SalesServeArea salesServeArea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesServeArea).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(salesServeArea);
        }

        // GET: SalesServeAreas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesServeArea salesServeArea = await db.SalesServeArea.FindAsync(id);
            if (salesServeArea == null)
            {
                return HttpNotFound();
            }
            return View(salesServeArea);
        }

        // POST: SalesServeAreas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SalesServeArea salesServeArea = await db.SalesServeArea.FindAsync(id);
            db.SalesServeArea.Remove(salesServeArea);
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
