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
    public class CompanyMeetingsController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanyMeetings
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyMeeting.ToListAsync());
        }

        // GET: CompanyMeetings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMeeting companyMeeting = await db.CompanyMeeting.FindAsync(id);
            if (companyMeeting == null)
            {
                return HttpNotFound();
            }
            return View(companyMeeting);
        }

        // GET: CompanyMeetings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyMeetings/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyId,CompanyName,MeetingType,MeetDate,MeetAddress,MeetNames,CreateUserName,CreateTime")] CompanyMeeting companyMeeting)
        {
            if (ModelState.IsValid)
            {
                db.CompanyMeeting.Add(companyMeeting);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyMeeting);
        }

        // GET: CompanyMeetings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMeeting companyMeeting = await db.CompanyMeeting.FindAsync(id);
            if (companyMeeting == null)
            {
                return HttpNotFound();
            }
            return View(companyMeeting);
        }

        // POST: CompanyMeetings/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyId,CompanyName,MeetingType,MeetDate,MeetAddress,MeetNames,CreateUserName,CreateTime")] CompanyMeeting companyMeeting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyMeeting).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyMeeting);
        }

        // GET: CompanyMeetings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMeeting companyMeeting = await db.CompanyMeeting.FindAsync(id);
            if (companyMeeting == null)
            {
                return HttpNotFound();
            }
            return View(companyMeeting);
        }

        // POST: CompanyMeetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyMeeting companyMeeting = await db.CompanyMeeting.FindAsync(id);
            db.CompanyMeeting.Remove(companyMeeting);
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
