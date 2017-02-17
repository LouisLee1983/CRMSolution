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
    public class OtaCompaniesController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: OtaCompanies
        public async Task<ActionResult> Index()
        {
            //根据user来确定公司结果
            
            return View(await db.OtaCompany.ToListAsync());
        }

        // GET: OtaCompanies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtaCompany otaCompany = await db.OtaCompany.FindAsync(id);
            if (otaCompany == null)
            {
                return HttpNotFound();
            }

            ViewData["CompanyCertificateList"] = db.CompanyCertificate.Where(p => p.CompanyId == id).ToList();
            return View(otaCompany);
        }

        // GET: OtaCompanies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OtaCompanies/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyName,LegalPerson,LegalPersonIdNo,LegalPersonPhone,RegisterAddress,RealAddress,OfficeNo,BossName,BossIdNo,BossBackground,BossBusinessDesp,OtherInvest,CapitalAsserts,SalesUserName,CityId,CityName")] OtaCompany otaCompany)
        {
            if (ModelState.IsValid)
            {
                db.OtaCompany.Add(otaCompany);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(otaCompany);
        }

        // GET: OtaCompanies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtaCompany otaCompany = await db.OtaCompany.FindAsync(id);
            if (otaCompany == null)
            {
                return HttpNotFound();
            }
            return View(otaCompany);
        }

        // POST: OtaCompanies/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyName,LegalPerson,LegalPersonIdNo,LegalPersonPhone,RegisterAddress,RealAddress,OfficeNo,BossName,BossIdNo,BossBackground,BossBusinessDesp,OtherInvest,CapitalAsserts,SalesUserName,CityId,CityName")] OtaCompany otaCompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(otaCompany).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(otaCompany);
        }

        // GET: OtaCompanies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtaCompany otaCompany = await db.OtaCompany.FindAsync(id);
            if (otaCompany == null)
            {
                return HttpNotFound();
            }
            return View(otaCompany);
        }

        // POST: OtaCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OtaCompany otaCompany = await db.OtaCompany.FindAsync(id);
            db.OtaCompany.Remove(otaCompany);
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
