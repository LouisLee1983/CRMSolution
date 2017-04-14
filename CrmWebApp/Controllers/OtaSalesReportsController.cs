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
using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Web.Helpers;

namespace CrmWebApp.Controllers
{
    public class OtaSalesReportsController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: OtaSalesReports
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.OtaSalesReport.ToListAsync());
        }

        // GET: OtaSalesReports/Details/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OtaSalesReport otaSalesReport)
        {
            if (ModelState.IsValid)
            {
                //自动根据时间读取拜访记录生成周报,有html的格式
                otaSalesReport.ReportContent = GenerateReport(otaSalesReport.StartDate.Value, otaSalesReport.EndDate.Value);
                db.OtaSalesReport.Add(otaSalesReport);
                db.SaveChanges();
                //生成周报的时候，要同时生成相应的报表统计图表存放到对应的id的文件夹里面
                SavePersonCompanyTicketChart(otaSalesReport.UserName, otaSalesReport.ID, otaSalesReport.StartDate.Value, otaSalesReport.EndDate.Value);

                return RedirectToAction("Edit", new { id = otaSalesReport.ID });
            }

            return View(otaSalesReport);
        }

        public string SavePersonCompanyTicketChart(string userName, int reportId, DateTime startDate, DateTime endDate)
        {
            AccountController ac = new AccountController();
            string realName = ac.GetRealName(userName);
            //读取两周的数据,自己客户的票量,按照日期展示曲线图
            OtaCrmModel db = new OtaCrmModel();
            var companyNames = from c in db.OtaCompany
                               where c.SalesUserName == realName
                               select c.CompanyName;

            var q = from p in db.AgentGradeOperation
                    where p.statDate.Value >= startDate && p.statDate.Value <= endDate && companyNames.Contains(p.agentName)
                    group p by p.statDate
                         into g
                    orderby g.Key
                    select new { ticketSum = g.Sum(b => b.CurDateTicketCount.Value), ticketDay = g.Key };

            List<string> dateList = new List<string>();
            List<int> ticketSumList = new List<int>();
            foreach (var item in q)
            {
                dateList.Add(item.ticketDay.Value.ToString("yyyyMMdd"));
                ticketSumList.Add(item.ticketSum);
            }

            System.Web.Helpers.Chart chart = new System.Web.Helpers.Chart(width: 500, height: 300, theme: ChartTheme.Blue, themePath: null);
            chart.AddTitle(text: userName + "客户票量统计", name: userName + "_CompanyTicketSum");
            chart.AddSeries(name: "票量"
                , chartType: "Column"
                , chartArea: ""
                , axisLabel: "张"
                , legend: "票量合计"
                , markerStep: 1
                , xValue: dateList
                , xField: "日期"
                , yValues: ticketSumList
                , yFields: "票量");

            string filePath = Server.MapPath("~/CompanyImages/Reports/" + reportId.ToString());
            CompanyBusinessDailyPhotoesController cbd = new CompanyBusinessDailyPhotoesController();
            cbd.CreateFolderIfNeeded(filePath);
            string fileName = userName + "_CompanyTicketSum_" + startDate.ToString("yyyyMMdd") + "_" + endDate.ToString("yyyyMMdd") + ".jpg";
            chart.Save(path: Path.Combine(filePath, fileName), format: "jpeg");

            return fileName;
        }

        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public string GenerateReport(DateTime startDate, DateTime endDate)
        {
            StringBuilder sb = new StringBuilder();
            var meetings = from p in db.CompanyMeeting
                           where p.CreateUserName == User.Identity.Name && p.MeetDate >= startDate && p.MeetDate <= endDate
                           select p;
            List<CompanyMeeting> meetingList = meetings.ToList();
            string tableHead = "<br><table class='table table-bordered'><tbody><tr><th>客户</th><th>拜访日期</th><th>拜访纪要</th></tr>";

            sb.Append(tableHead);
            Dictionary<int, string> meetingIdDict = new Dictionary<int, string>();
            string[] specialChars = new string[] {"_","","!","|","~","`","(",")","#","$","%","^","&","*","{","}",":",";","<",">","?","/","，","'","'","" };
            foreach (var meeting in meetingList)
            {
                string summary = string.IsNullOrEmpty(meeting.MeetSummary) ? "" : meeting.MeetSummary;

                //-_,!|~`()#$%^&*{}:;"<>?/, '' 这些特殊字符需要去掉
                foreach (string specialChar in specialChars)
                {
                    summary = summary.Replace(specialChar, "");
                }

                sb.Append("<tr>")
                    .Append("<td>").Append(meeting.CompanyName.Trim()).Append("</td>")
                    .Append("<td>").Append(meeting.MeetDate.ToString("yyyy-MM-dd")).Append("</td>")
                    .Append("<td>").Append(summary.Replace("\r\n", "<br>").Replace("\r", "").Replace("\n", "").Trim()).Append("</td>")
                    .Append("</tr>");

                if (!meetingIdDict.ContainsKey(meeting.Id))
                {
                    meetingIdDict.Add(meeting.Id, meeting.CompanyName.Trim());
                }
            }
            sb.Append("</tbody></table>");
            sb.Append("<br><br>");
            //反馈问题的table
            tableHead = "<table class='table table-bordered'><tbody><tr><th>问题类型</th><th>反馈客户</th><th>具体描述</th><th>解决方案</th></tr>";
            sb.Append(tableHead);
            var meetingSubjects = from m in db.CompanyMeetingSubject
                                  where meetingIdDict.Keys.ToList().Contains(m.CompanyMeetingId)
                                  orderby m.Subject, m.CompanyMeetingId
                                  select m;
            foreach (var meetingsubject in meetingSubjects)
            {
                string problem = string.IsNullOrEmpty(meetingsubject.Problem) ? "" : meetingsubject.Problem;
                string resolve = string.IsNullOrEmpty(meetingsubject.Resolve) ? "" : meetingsubject.Resolve;
                sb.Append("<tr>")
                    .Append("<td>").Append(meetingsubject.Subject).Append("</td>")
                    .Append("<td>").Append(meetingIdDict[meetingsubject.CompanyMeetingId].Trim()).Append("</td>")
                    .Append("<td>").Append(problem.Replace("\r\n", "<br>").Replace("\r", "").Replace("\n", "").Replace("\t", "")).Append("</td>")
                    .Append("<td>").Append(resolve.Replace("\r\n", "<br>").Replace("\r", "").Replace("\n", "").Replace("\t", "")).Append("</td>")
                    .Append("</tr>");
            }
            sb.Append("</tbody></table>");
            sb.Append("<br>");
            return sb.ToString();
        }

        // GET: OtaSalesReports/Edit/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
        public string[] GetAllFileName(string filePath)
        {
            DirectoryInfo dir = new DirectoryInfo(filePath);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            FileInfo[] finfo = dir.GetFiles();
            List<string> result = new List<string>();
            for (int i = 0; i < finfo.Length; i++)
            {
                result.Add(finfo[i].Name);
            }
            return result.ToArray();
        }

        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
            mail.IsBodyHtml = true;//设置显示htmls
            mail.BodyEncoding = Encoding.GetEncoding(936);    //邮件正文的编码， 设置不正确， 接收者会收到乱码  

            string htmlHead = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=gb2312'><style>body {font-size: 9pt;}table {border: 1px;padding: 0;border-collapse: collapse;border: none;}th {border: solid windowtext 1.0pt;background: #BFBFBF;padding: 3px;font-size: 10pt;font-weight: bold;margin:8px;}td {border: solid windowtext 1.0pt;border-top: none;font-size: 10pt;padding: 8px;}</style></head><body><div>";
            string htmlEnd = "</div></body></html>";

            string Themessage = htmlHead;
            //Add Image，多张图片需要循环
            var path = "~/CompanyImages/Reports/" + id.ToString();
            var imgpath = Server.MapPath(path);
            string[] imgFiles = GetAllFileName(imgpath);
            foreach (string imgFile in imgFiles)
            {
                string imgHtml = "<img src='cid:" + imgFile.Replace(".jpg", "") + "' /><br><br>";
                Themessage += imgHtml;
            }
            Themessage += otaSalesReport.ReportContent + htmlEnd;

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Themessage, null, "text/html");
            foreach (string imgFile in imgFiles)
            {
                LinkedResource theEmailImage = new LinkedResource(Path.Combine(imgpath, imgFile), MediaTypeNames.Image.Jpeg);
                theEmailImage.ContentId = imgFile.Replace(".jpg", "");
                htmlView.LinkedResources.Add(theEmailImage);
            }

            mail.AlternateViews.Add(htmlView);
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
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
