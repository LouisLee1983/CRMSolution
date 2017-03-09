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
using PagedList;

namespace CrmWebApp.Controllers
{
    public class ParamDictsController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: ParamDicts
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ParamNameSortParm = string.IsNullOrEmpty(sortOrder) ? "paramName_desc" : "";
            ViewBag.SubItemNameSortParm = sortOrder == "subItemName" ? "subItemName_desc" : "subItemName";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var paramDicts = from p in db.ParamDict
                             select p;

            //搜索
            if (!string.IsNullOrEmpty(searchString))
            {
                paramDicts = paramDicts.Where(p => p.ParamName.Contains(searchString) || p.SubItemName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "paramName_desc":
                    paramDicts = paramDicts.OrderByDescending(o => o.ParamName);
                    break;
                case "subItemName_desc":
                    paramDicts = paramDicts.OrderByDescending(o => o.SubItemName);
                    break;
                case "subItemName":
                    paramDicts = paramDicts.OrderBy(o => o.SubItemName);
                    break;
                default:
                    paramDicts = paramDicts.OrderBy(o => o.ParamName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(paramDicts.ToPagedList(pageNumber, pageSize));
        }

        // GET: ParamDicts/Details/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParamDict paramDict = await db.ParamDict.FindAsync(id);
            if (paramDict == null)
            {
                return HttpNotFound();
            }
            return View(paramDict);
        }

        // GET: ParamDicts/Create
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParamDicts/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Create([Bind(Include = "Id,ParamName,SubItemName")] ParamDict paramDict)
        {
            if (ModelState.IsValid)
            {
                db.ParamDict.Add(paramDict);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(paramDict);
        }

        // GET: ParamDicts/Edit/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParamDict paramDict = await db.ParamDict.FindAsync(id);
            if (paramDict == null)
            {
                return HttpNotFound();
            }
            return View(paramDict);
        }

        // POST: ParamDicts/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ParamName,SubItemName")] ParamDict paramDict)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paramDict).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(paramDict);
        }

        // GET: ParamDicts/Delete/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParamDict paramDict = await db.ParamDict.FindAsync(id);
            if (paramDict == null)
            {
                return HttpNotFound();
            }
            return View(paramDict);
        }

        // POST: ParamDicts/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ParamDict paramDict = await db.ParamDict.FindAsync(id);
            db.ParamDict.Remove(paramDict);
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
