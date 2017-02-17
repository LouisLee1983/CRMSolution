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
        public async Task<ActionResult> Index(int? companyId)
        {
            var model = from cbd in db.CompanyMeeting
                        select cbd;
            if (companyId.HasValue)
            {
                model = model.Where(p => p.CompanyId == companyId.Value);
                ViewBag.CompanyId = companyId.Value;
                ViewBag.CompanyName = db.OtaCompany.FirstOrDefault(p => p.Id == companyId.Value).CompanyName;
            }

            return View(await model.ToListAsync());
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
        public ActionResult Create(int? companyId)
        {
            var model = new CompanyMeeting();
            if (companyId.HasValue)
            {
                model.CompanyId = companyId.Value;
                var company = db.OtaCompany.FirstOrDefault(p => p.Id == companyId.Value);
                if (company != null)
                {
                    model.CompanyName = company.CompanyName;
                }
                model.CreateUserName = User.Identity.Name;
                model.MeetDate = DateTime.Today;
                model.CreateTime = DateTime.Now;
                model.MeetAddress = ""; //取上一个记录得数据
                model.MeetingType = ""; //取上一个记录得数据
                model.MeetNames = ""; //取上一个记录得数据
            }
            ViewData["MeetingTypeList"] = GetMeetingTypeList("上门");
            return View(model);
        }
        private List<SelectListItem> GetMeetingTypeList(string defaultValue)
        {
            var bussinessTypes = from p in db.ParamDict
                                 where p.ParamName == "MeetingType"
                                 select p;
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (var item in bussinessTypes)
            {
                SelectListItem selectItem = new SelectListItem();
                selectItem.Value = item.SubItemName;
                selectItem.Text = item.SubItemName;
                if (!string.IsNullOrEmpty(defaultValue) && defaultValue == item.SubItemName)
                {
                    selectItem.Selected = true;
                }
                result.Add(selectItem);
            }
            return result;
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
