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
    public class S_CityController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: S_City
        public async Task<ActionResult> Index()
        {
            return View(await db.S_City.ToListAsync());
        }
        

        // GET: S_City/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            S_City s_City = await db.S_City.FindAsync(id);
            if (s_City == null)
            {
                return HttpNotFound();
            }
            return View(s_City);
        }

        // GET: S_City/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: S_City/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CityID,CityName,ZipCode,ProvinceID,DateCreated,DateUpdated")] S_City s_City)
        {
            if (ModelState.IsValid)
            {
                db.S_City.Add(s_City);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(s_City);
        }

        // GET: S_City/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            S_City s_City = await db.S_City.FindAsync(id);
            if (s_City == null)
            {
                return HttpNotFound();
            }
            return View(s_City);
        }

        // POST: S_City/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CityID,CityName,ZipCode,ProvinceID,DateCreated,DateUpdated")] S_City s_City)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s_City).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(s_City);
        }

        // GET: S_City/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            S_City s_City = await db.S_City.FindAsync(id);
            if (s_City == null)
            {
                return HttpNotFound();
            }
            return View(s_City);
        }

        // POST: S_City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            S_City s_City = await db.S_City.FindAsync(id);
            db.S_City.Remove(s_City);
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
