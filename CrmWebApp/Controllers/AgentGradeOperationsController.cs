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
        public async Task<ActionResult> Index()
        {
            return View(await db.AgentGradeOperation.ToListAsync());
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
