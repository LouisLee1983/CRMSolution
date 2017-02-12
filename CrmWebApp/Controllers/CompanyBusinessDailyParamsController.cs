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
    public class CompanyBusinessDailyParamsController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanyBusinessDailyParams
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyBusinessDailyParam.ToListAsync());
        }

        // GET: CompanyBusinessDailyParams/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailyParam companyBusinessDailyParam = await db.CompanyBusinessDailyParam.FindAsync(id);
            if (companyBusinessDailyParam == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailyParam);
        }

        // GET: CompanyBusinessDailyParams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyBusinessDailyParams/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyBusinessDailyId,ParamName,SubParamItem,ItemAmount")] CompanyBusinessDailyParam companyBusinessDailyParam)
        {
            if (ModelState.IsValid)
            {
                db.CompanyBusinessDailyParam.Add(companyBusinessDailyParam);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyBusinessDailyParam);
        }

        // GET: CompanyBusinessDailyParams/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailyParam companyBusinessDailyParam = await db.CompanyBusinessDailyParam.FindAsync(id);
            if (companyBusinessDailyParam == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailyParam);
        }

        // POST: CompanyBusinessDailyParams/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyBusinessDailyId,ParamName,SubParamItem,ItemAmount")] CompanyBusinessDailyParam companyBusinessDailyParam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyBusinessDailyParam).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyBusinessDailyParam);
        }

        // GET: CompanyBusinessDailyParams/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailyParam companyBusinessDailyParam = await db.CompanyBusinessDailyParam.FindAsync(id);
            if (companyBusinessDailyParam == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailyParam);
        }

        // POST: CompanyBusinessDailyParams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyBusinessDailyParam companyBusinessDailyParam = await db.CompanyBusinessDailyParam.FindAsync(id);
            db.CompanyBusinessDailyParam.Remove(companyBusinessDailyParam);
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
