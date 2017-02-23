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
using System.Text;

namespace CrmWebApp.Controllers
{
    public class OtaSalesReportsController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: OtaSalesReports
        public async Task<ActionResult> Index()
        {
            return View(await db.OtaSalesReport.ToListAsync());
        }

        // GET: OtaSalesReports/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtaSalesReport otaSalesReport = await db.OtaSalesReport.FindAsync(id);
            if (otaSalesReport == null)
            {
                return HttpNotFound();
            }
            return View(otaSalesReport);
        }

        // GET: OtaSalesReports/Create
        public ActionResult Create()
        {
            var model = new OtaSalesReport();
            model.CreateTime = DateTime.Now;

            DateTime startWeek = DateTime.Now.AddDays(1 - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d")));
            DateTime endWeek = startWeek.AddDays(6);

            model.StartDate = startWeek;
            model.EndDate = endWeek;

            model.ReportContent = "";

            var Db = new ApplicationDbContext();
            var user = Db.Users.First(u => u.UserName == User.Identity.Name);
            model.SendToEmails = user.Email;
            model.ReportTitle = "销售周报-" + user.TrueName + "-" + model.StartDate.Value.ToString("yyyyMMdd") + "-" + model.EndDate.Value.ToString("yyyyMMdd");

            model.UserName = User.Identity.Name;

            return View(model);
        }

        // POST: OtaSalesReports/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OtaSalesReport otaSalesReport)
        {
            if (ModelState.IsValid)
            {
                //自动根据时间读取拜访记录生成周报,有html的格式
                otaSalesReport.ReportContent = GenerateReport(otaSalesReport.StartDate.Value, otaSalesReport.EndDate.Value);
                db.OtaSalesReport.Add(otaSalesReport);
                await db.SaveChangesAsync();
                return RedirectToAction("Edit", new { id = otaSalesReport.ID });
            }

            return View(otaSalesReport);
        }

        public string GenerateReport(DateTime startDate, DateTime endDate)
        {
            StringBuilder sb = new StringBuilder();
            var meetings = from p in db.CompanyMeeting
                           where p.CreateUserName == User.Identity.Name && p.MeetDate >= startDate && p.MeetDate <= endDate
                           select p;
            List<CompanyMeeting> meetingList = meetings.ToList();
            sb.Append("<table><tr><th>客户</th><th>日期</th><th>概况</th></tr>");
            foreach (var meeting in meetingList)
            {
                sb.Append("<tr>")
                    .Append("<td>").Append(meeting.CompanyName).Append("</td>")
                    .Append("<td>").Append(meeting.MeetDate.ToString("yyyy-MM-dd")).Append("</td>")
                    .Append("<td>").Append(meeting.MeetSummary).Append("</td>")
                    .Append("</tr>");
                sb.Append("<tr><td colspan='3'>");
                //反馈问题的table
                var meetingSubjects = from m in db.CompanyMeetingSubject
                                      where m.CompanyMeetingId == meeting.Id
                                      select m;
                sb.Append("<table>");
                foreach (var meetingsubject in meetingSubjects)
                {
                    sb.Append("<tr>")
                        .Append("<td>").Append(meetingsubject.Subject).Append("</td>")
                        .Append("<td>").Append(meetingsubject.Problem).Append("</td>")
                        .Append("<td>").Append(meetingsubject.Resolve).Append("</td>")
                        .Append("</tr>");
                }
                sb.Append("</table>");
                sb.Append("</td></tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }

        // GET: OtaSalesReports/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtaSalesReport otaSalesReport = await db.OtaSalesReport.FindAsync(id);
            if (otaSalesReport == null)
            {
                return HttpNotFound();
            }
            return View(otaSalesReport);
        }

        // POST: OtaSalesReports/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,StartDate,EndDate,UserName,SendToEmails,ReportTitle,ReportContent,CreateTime")] OtaSalesReport otaSalesReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(otaSalesReport).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(otaSalesReport);
        }

        // GET: OtaSalesReports/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtaSalesReport otaSalesReport = await db.OtaSalesReport.FindAsync(id);
            if (otaSalesReport == null)
            {
                return HttpNotFound();
            }
            return View(otaSalesReport);
        }

        // POST: OtaSalesReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OtaSalesReport otaSalesReport = await db.OtaSalesReport.FindAsync(id);
            db.OtaSalesReport.Remove(otaSalesReport);
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
