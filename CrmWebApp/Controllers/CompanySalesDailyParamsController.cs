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
    public class CompanySalesDailyParamsController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanySalesDailyParams
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanySalesDailyParam.ToListAsync());
        }

        // GET: CompanySalesDailyParams/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanySalesDailyParam companySalesDailyParam = await db.CompanySalesDailyParam.FindAsync(id);
            if (companySalesDailyParam == null)
            {
                return HttpNotFound();
            }
            return View(companySalesDailyParam);
        }

        // GET: CompanySalesDailyParams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanySalesDailyParams/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanySalesDailyId,ParamName,SubParamItem,ItemAmount")] CompanySalesDailyParam companySalesDailyParam)
        {
            if (ModelState.IsValid)
            {
                db.CompanySalesDailyParam.Add(companySalesDailyParam);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companySalesDailyParam);
        }

        // GET: CompanySalesDailyParams/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanySalesDailyParam companySalesDailyParam = await db.CompanySalesDailyParam.FindAsync(id);
            if (companySalesDailyParam == null)
            {
                return HttpNotFound();
            }
            return View(companySalesDailyParam);
        }

        // POST: CompanySalesDailyParams/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanySalesDailyId,ParamName,SubParamItem,ItemAmount")] CompanySalesDailyParam companySalesDailyParam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companySalesDailyParam).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companySalesDailyParam);
        }

        // GET: CompanySalesDailyParams/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanySalesDailyParam companySalesDailyParam = await db.CompanySalesDailyParam.FindAsync(id);
            if (companySalesDailyParam == null)
            {
                return HttpNotFound();
            }
            return View(companySalesDailyParam);
        }

        // POST: CompanySalesDailyParams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanySalesDailyParam companySalesDailyParam = await db.CompanySalesDailyParam.FindAsync(id);
            db.CompanySalesDailyParam.Remove(companySalesDailyParam);
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
