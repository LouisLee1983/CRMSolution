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
    public class AgentGradeOperationsController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: AgentGradeOperations
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Index(int? companyId)
        {
            var result = new List<AgentGradeOperation>();
            if (companyId != null && companyId > 0)
            {
                OtaCompany c = db.OtaCompany.First(p => p.Id == companyId);
                var model = from p in db.AgentGradeOperation
                            where p.agentName == c.CompanyName
                            select p;
                ViewBag.CompanyName = c.CompanyName;
                ViewBag.CompanyId = c.Id;
                return View(await model.ToListAsync());
            }
            return View(result);
        }
        

        // GET: AgentGradeOperations/Details/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Details(int? id,int? companyId)
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
            ViewBag.CompanyId = companyId;
            return View(agentGradeOperation);
        }

        // GET: AgentGradeOperations/Create
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgentGradeOperations/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
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
