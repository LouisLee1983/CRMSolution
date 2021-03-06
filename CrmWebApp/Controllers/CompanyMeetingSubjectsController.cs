﻿using System;
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
    public class CompanyMeetingSubjectsController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanyMeetingSubjects
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyMeetingSubject.ToListAsync());
        }

        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult AddNew(int dailyId)
        {
            var model = new CompanyMeetingSubject();
            model.CompanyMeetingId = dailyId;
            model.Id = 0;
            model.Problem = "问题";
            model.Resolve = "解决办法";
            model.ResolveTime = DateTime.Today;
            model.Subject = "产品";

            ViewData["MeetingSubjectList"] = GetMeetingSubjectList("产品");

            return PartialView("_PartialAddMeetingSubject", model);
        }
        
        public List<SelectListItem> GetMeetingSubjectList(string defaultValue)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            var q = from p in db.ParamDict
                    where p.ParamName == "反馈分类"
                    select p;
            foreach (var item in q)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = item.SubItemName;
                selectListItem.Value = item.SubItemName;
                if (item.SubItemName == defaultValue)
                {
                    selectListItem.Selected = true;
                }
                result.Add(selectListItem);
            }
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult AddNew(CompanyMeetingSubject model)
        {
            db.CompanyMeetingSubject.Add(model);
            db.SaveChanges();

            return RedirectToAction("Edit", "CompanyMeetings", new { id = model.CompanyMeetingId });
        }

        // GET: CompanyMeetingSubjects/Details/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMeetingSubject companyMeetingSubject = await db.CompanyMeetingSubject.FindAsync(id);
            if (companyMeetingSubject == null)
            {
                return HttpNotFound();
            }
            return View(companyMeetingSubject);
        }

        // GET: CompanyMeetingSubjects/Create
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyMeetingSubjects/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyMeetingId,Subject,Problem,Resolve,CreateTime,ResolveTime")] CompanyMeetingSubject companyMeetingSubject)
        {
            if (ModelState.IsValid)
            {
                db.CompanyMeetingSubject.Add(companyMeetingSubject);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(companyMeetingSubject);
        }

        // GET: CompanyMeetingSubjects/Edit/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMeetingSubject companyMeetingSubject = await db.CompanyMeetingSubject.FindAsync(id);
            if (companyMeetingSubject == null)
            {
                return HttpNotFound();
            }
            return View(companyMeetingSubject);
        }

        // POST: CompanyMeetingSubjects/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyMeetingId,Subject,Problem,Resolve,CreateTime,ResolveTime")] CompanyMeetingSubject companyMeetingSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyMeetingSubject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyMeetingSubject);
        }

        // GET: CompanyMeetingSubjects/Delete/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMeetingSubject companyMeetingSubject = await db.CompanyMeetingSubject.FindAsync(id);
            if (companyMeetingSubject == null)
            {
                return HttpNotFound();
            }
            return View(companyMeetingSubject);
        }

        // POST: CompanyMeetingSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyMeetingSubject companyMeetingSubject = await db.CompanyMeetingSubject.FindAsync(id);
            db.CompanyMeetingSubject.Remove(companyMeetingSubject);
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
