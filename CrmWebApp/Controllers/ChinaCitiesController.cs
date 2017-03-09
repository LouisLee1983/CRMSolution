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
    public class ChinaCitiesController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: ChinaCities
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.ChinaCity.ToListAsync());
        }

        // GET: ChinaCities/Details/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChinaCity chinaCity = await db.ChinaCity.FindAsync(id);
            if (chinaCity == null)
            {
                return HttpNotFound();
            }
            return View(chinaCity);
        }

        // GET: ChinaCities/Create
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChinaCities/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ProvinceName,CityName")] ChinaCity chinaCity)
        {
            if (ModelState.IsValid)
            {
                db.ChinaCity.Add(chinaCity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(chinaCity);
        }

        // GET: ChinaCities/Edit/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChinaCity chinaCity = await db.ChinaCity.FindAsync(id);
            if (chinaCity == null)
            {
                return HttpNotFound();
            }
            return View(chinaCity);
        }

        // POST: ChinaCities/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ProvinceName,CityName")] ChinaCity chinaCity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chinaCity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(chinaCity);
        }

        // GET: ChinaCities/Delete/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChinaCity chinaCity = await db.ChinaCity.FindAsync(id);
            if (chinaCity == null)
            {
                return HttpNotFound();
            }
            return View(chinaCity);
        }

        // POST: ChinaCities/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ChinaCity chinaCity = await db.ChinaCity.FindAsync(id);
            db.ChinaCity.Remove(chinaCity);
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
