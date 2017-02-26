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
using System.Net.Mail;

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
            sb.Append("<table border='1'><tr><th>客户</th><th>日期</th><th>概况</th></tr>");
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
                sb.Append("<table border='1'>");
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

        public ActionResult SendEmail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtaSalesReport otaSalesReport = db.OtaSalesReport.Find(id);
            if (otaSalesReport == null)
            {
                return HttpNotFound();
            }
            //發送email
            #region 发送邮件
            //填写电子邮件地址，和显示名称
            System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress("425451886@qq.com", "ljy");
            //填写邮件的收件人地址和名称
            System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(otaSalesReport.SendToEmails, otaSalesReport.UserName);
            //设置好发送地址，和接收地址，接收地址可以是多个

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.Priority = MailPriority.Normal;
            mail.From = from;
            mail.To.Add(to);
            mail.Subject = otaSalesReport.ReportTitle;
            mail.SubjectEncoding = Encoding.GetEncoding(936); //这里非常重要，如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。  
            mail.Body = otaSalesReport.ReportContent;
            mail.IsBodyHtml = true;//设置显示htmls
            mail.BodyEncoding = Encoding.GetEncoding(936);    //邮件正文的编码， 设置不正确， 接收者会收到乱码  
                                                              //设置好发送邮件服务地址
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Host = "smtp.qq.com"; //这里发邮件用的是126，所以为"smtp.126.com"
            client.Port = 25;
            client.EnableSsl = true;
            client.UseDefaultCredentials = true;

            //填写服务器地址相关的用户名和密码信息
            client.Credentials = new System.Net.NetworkCredential("425451886@qq.com", "cpkehayuptjibgbc");
            //发送邮件
            client.Send(mail);

            #endregion
            ViewBag.Message = "发送成功.";
            return RedirectToAction("Index");
        }

        // POST: OtaSalesReports/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(OtaSalesReport otaSalesReport)
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
