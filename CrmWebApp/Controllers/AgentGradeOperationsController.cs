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
    public class AgentGradeOperationsController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: AgentGradeOperations
        public async Task<ActionResult> Index(int? companyId)
        {
            var result = new List<AgentGradeOperation>();
            if (companyId != null && companyId > 0)
            {
                OtaCompany c = db.OtaCompany.First(p => p.Id == companyId);
                var model = from p in db.AgentGradeOperation
                            where p.agentName == c.CompanyName
                            select p;
                return View(await model.ToListAsync());
            }
            return View(result);
        }

        public void ImportCompany()
        {
            var cityList = (from cc in db.ChinaCity select cc).ToList();

            var exsitNames = (from c in db.OtaCompany select c.CompanyName).Distinct();
            List<string> exsitNameList = exsitNames.ToList();

            var q = (from p in db.AgentGradeOperation
                     select p.agentName).Distinct();
            foreach (string companyName in q)
            {
                if (string.IsNullOrEmpty(companyName) || exsitNameList.Contains(companyName))
                {
                    continue;
                }
                OtaCompany newItem = new OtaCompany();
                newItem.BossBackground = "";
                newItem.BossBusinessDesp = "";
                newItem.BossIdNo = "";
                newItem.CompanyName = companyName;
                newItem.CreateTime = DateTime.Now;
                newItem.BossName = "";
                newItem.BusnessRange = "";
                newItem.CapitalAsserts = "";
                newItem.CityName = "";

                foreach (var city in cityList)
                {
                    if (companyName.Contains(city.CityName.Replace("市", "")))
                    {
                        newItem.CityName = city.ProvinceName + "-" + city.CityName;
                        break;
                    }
                }

                newItem.LegalPerson = "ww";
                newItem.LegalPersonIdNo = "";
                newItem.LegalPersonPhone = "";
                newItem.OfficeNo = "";
                newItem.OtherInvest = "";
                newItem.RealAddress = "";
                newItem.RegisterAddress = "";
                newItem.SalesUserName = "ljy";

                db.OtaCompany.Add(newItem);
            }
            db.SaveChanges();
        }

        // GET: AgentGradeOperations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentGradeOperation agentGradeOperation = await db.AgentGradeOperation.FindAsync(id);
            if (agentGradeOperation == null)
            {
                return HttpNotFound();
            }
            return View(agentGradeOperation);
        }

        // GET: AgentGradeOperations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgentGradeOperations/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,agentDomain,agentName,promotion,agentManager,totalTicketNum,totalTicket,passRate,less60minRate,orderAlterRate,voluntaryRate,involuntaryRate,complainRate,qapassRate,phoneAnswerRate,messageTimeoutRate,qualification,whiteList,totalScore,status,statDate,statMonth,grade,CurDateTicketCount,CreateTime")] AgentGradeOperation agentGradeOperation)
        {
            if (ModelState.IsValid)
            {
                db.AgentGradeOperation.Add(agentGradeOperation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(agentGradeOperation);
        }

        // GET: AgentGradeOperations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentGradeOperation agentGradeOperation = await db.AgentGradeOperation.FindAsync(id);
            if (agentGradeOperation == null)
            {
                return HttpNotFound();
            }
            return View(agentGradeOperation);
        }

        // POST: AgentGradeOperations/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,agentDomain,agentName,promotion,agentManager,totalTicketNum,totalTicket,passRate,less60minRate,orderAlterRate,voluntaryRate,involuntaryRate,complainRate,qapassRate,phoneAnswerRate,messageTimeoutRate,qualification,whiteList,totalScore,status,statDate,statMonth,grade,CurDateTicketCount,CreateTime")] AgentGradeOperation agentGradeOperation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agentGradeOperation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(agentGradeOperation);
        }

        // GET: AgentGradeOperations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentGradeOperation agentGradeOperation = await db.AgentGradeOperation.FindAsync(id);
            if (agentGradeOperation == null)
            {
                return HttpNotFound();
            }
            return View(agentGradeOperation);
        }

        // POST: AgentGradeOperations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AgentGradeOperation agentGradeOperation = await db.AgentGradeOperation.FindAsync(id);
            db.AgentGradeOperation.Remove(agentGradeOperation);
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
