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
    public class ServeAreasController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: ServeAreas
        public async Task<ActionResult> Index()
        {
            return View(await db.ServeArea.ToListAsync());
        }

        // GET: ServeAreas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServeArea serveArea = await db.ServeArea.FindAsync(id);
            if (serveArea == null)
            {
                return HttpNotFound();
            }
            return View(serveArea);
        }

        // GET: ServeAreas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServeAreas/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,UserName,ServeAreaName")] ServeArea serveArea)
        {
            if (ModelState.IsValid)
            {
                db.ServeArea.Add(serveArea);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(serveArea);
        }

        // GET: ServeAreas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServeArea serveArea = await db.ServeArea.FindAsync(id);
            if (serveArea == null)
            {
                return HttpNotFound();
            }
            return View(serveArea);
        }

        // POST: ServeAreas/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,UserName,ServeAreaName")] ServeArea serveArea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serveArea).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(serveArea);
        }

        // GET: ServeAreas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServeArea serveArea = await db.ServeArea.FindAsync(id);
            if (serveArea == null)
            {
                return HttpNotFound();
            }
            return View(serveArea);
        }

        // POST: ServeAreas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ServeArea serveArea = await db.ServeArea.FindAsync(id);
            db.ServeArea.Remove(serveArea);
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
