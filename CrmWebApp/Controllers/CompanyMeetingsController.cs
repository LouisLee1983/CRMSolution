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
using System.Data.SqlClient;
using PagedList;

namespace CrmWebApp.Controllers
{
    public class CompanyMeetingsController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanyMeetings
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult Index(int? companyId, int? page)
        {
            var model = from cbd in db.CompanyMeeting
                        select cbd;
            //orderby cbd.Id descending
            if (companyId.HasValue)
            {
                ViewBag.CompanyId = companyId.Value;
                ViewBag.CompanyName = db.OtaCompany.FirstOrDefault(p => p.Id == companyId.Value).CompanyName;
                model = model.Where(p => p.CompanyId == companyId.Value);
            }
            else
            {
                if (!User.IsInRole("SalesDirector") && !User.IsInRole("Admin"))
                {
                    if (User.IsInRole("AreaManager"))
                    {
                        //找出本区域的所有人，并把它作为条件加入查询结果中
                        ServeAreasController sac = new ServeAreasController();
                        List<string> myAreaUserNameList = sac.GetMyAreaUserNames(User.Identity.Name);
                        model = model.Where(p => myAreaUserNameList.Contains(p.CreateUserName));
                    }
                    else if (User.IsInRole("OtaSales"))
                    {
                        //销售只能看到本人的                    
                        model = model.Where(p => p.CreateUserName == User.Identity.Name);
                    }
                }
            }
            model = model.OrderByDescending(p => p.Id);

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(model.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult ShowViewPartial(int id)
        {
            CompanyMeeting dailyItem = db.CompanyMeeting.FirstOrDefault(p => p.Id == id);
            var subjectList = from p in db.CompanyMeetingSubject
                              where p.CompanyMeetingId == id
                              select p;
            var mediaList = from p in db.CompanyMedia
                            where p.OuterKeyId == id && p.MediaFor == "拜访记录"
                            select p;

            var model = new CompanyMeetingViewModel(dailyItem, subjectList.ToList(), mediaList.ToList());
            return PartialView("_PartialCompanyMeetingView", model);
        }

        // GET: CompanyMeetings/Details/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
                model.MeetSummary = "";
            }
            ViewData["MeetingTypeList"] = GetMeetingTypeList("上门拜访");
            return View(model);
        }
        private List<SelectListItem> GetMeetingTypeList(string defaultValue)
        {
            var bussinessTypes = from p in db.ParamDict
                                 where p.ParamName == "沟通方式"
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
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CompanyMeeting companyMeeting)
        {
            ViewData["MeetingTypeList"] = GetMeetingTypeList("上门拜访");
            if (ModelState.IsValid)
            {
                //更新公司的lastupdatedate
                var company = db.OtaCompany.FirstOrDefault(p => p.Id == companyMeeting.CompanyId);
                company.LastMeetingDate = DateTime.Now;
                db.SaveChanges();

                db.CompanyMeeting.Add(companyMeeting);
                await db.SaveChangesAsync();

                return RedirectToAction("Edit", new { Id = companyMeeting.Id });
            }
            return View(companyMeeting);
        }

        // GET: CompanyMeetings/Edit/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
            var meetingSubjects = from p in db.CompanyMeetingSubject
                                  where p.CompanyMeetingId == id.Value
                                  select p;
            var mediaList = from p in db.CompanyMedia
                            where p.OuterKeyId == id.Value && p.MediaFor == "拜访记录"
                            select p;
            var model = new CompanyMeetingViewModel(companyMeeting, meetingSubjects.ToList(), mediaList.ToList());
            ViewData["MeetingTypeList"] = GetMeetingTypeList("上门拜访");
            return View(model);
        }

        // POST: CompanyMeetings/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CompanyMeetingViewModel model, List<CompanyMeetingSubject> meetingSubjectList)
        {
            CompanyMeeting companyMeeting = db.CompanyMeeting.FirstOrDefault(p => p.Id == model.Id);
            companyMeeting.MeetAddress = model.MeetAddress;
            companyMeeting.MeetDate = model.MeetDate;
            companyMeeting.MeetingType = model.MeetingType;
            companyMeeting.MeetNames = model.MeetNames;
            companyMeeting.MeetSummary = model.MeetSummary;
            await db.SaveChangesAsync();

            if (meetingSubjectList != null && meetingSubjectList.Count > 0)
            {
                //保存子项目
                string sql = "Delete From CompanyMeetingSubject Where CompanyMeetingId=@CompanyMeetingId";
                SqlParameter[] paras = new SqlParameter[] {
                     new SqlParameter("@CompanyMeetingId",model.Id)
                    };
                db.Database.ExecuteSqlCommand(sql, paras);
                foreach (CompanyMeetingSubject item in meetingSubjectList)
                {
                    if (!string.IsNullOrEmpty(item.Subject))
                    {
                        db.CompanyMeetingSubject.Add(item);
                    }
                }

                db.SaveChanges();
            }

            return RedirectToAction("Index", new { companyId = model.CompanyId });
        }

        // GET: CompanyMeetings/Delete/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
