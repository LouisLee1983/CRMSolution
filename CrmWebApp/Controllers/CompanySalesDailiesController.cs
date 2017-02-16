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
    public class CompanySalesDailiesController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanySalesDailies
        public async Task<ActionResult> Index(int? companyId)
        {
            var model = from cbd in db.CompanySalesDaily
                        select cbd;
            if (companyId.HasValue)
            {
                model = model.Where(p => p.CompanyId == companyId.Value);
                ViewBag.CompanyId = companyId.Value;
                ViewBag.CompanyName = db.OtaCompany.FirstOrDefault(p => p.Id == companyId.Value).CompanyName;
            }

            return View(await model.ToListAsync());
        }

        // GET: CompanySalesDailies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanySalesDaily companySalesDaily = await db.CompanySalesDaily.FindAsync(id);
            if (companySalesDaily == null)
            {
                return HttpNotFound();
            }
            return View(companySalesDaily);
        }

        // GET: CompanySalesDailies/Create
        public ActionResult Create(int? companyId)
        {
            var model = new CompanySalesDaily();
            if (companyId.HasValue)
            {
                model.CompanyId = companyId.Value;
                var company = db.OtaCompany.FirstOrDefault(p => p.Id == companyId.Value);
                if (company != null)
                {
                    model.CompanyName = company.CompanyName;
                }
                model.CreateUserName = User.Identity.Name;
                model.SalesLogDate = DateTime.Today;
                model.CreateTime = DateTime.Now;
                model.ManagerName = ""; //取上一个记录得数据
                model.ManagerPhone = ""; //取上一个记录得数据
                model.SalesType = "";
            }
            ViewData["SalesTypeList"] = GetSalesTypeList("国内");
            return View(model);
        }

        private List<SelectListItem> GetSalesTypeList(string defaultValue)
        {
            var bussinessTypes = from p in db.ParamDict
                                 where p.ParamName == "BussinessType"
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

        // POST: CompanySalesDailies/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyId,CompanyName,ManagerName,ManagerPhone,SalesType,CreateUserName,CreateTime,SalesLogDate")] CompanySalesDaily companySalesDaily)
        {
            if (ModelState.IsValid)
            {
                db.CompanySalesDaily.Add(companySalesDaily);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { companyId = companySalesDaily.CompanyId });
            }

            return View(companySalesDaily);
        }

        // GET: CompanySalesDailies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanySalesDaily companySalesDaily = await db.CompanySalesDaily.FindAsync(id);
            if (companySalesDaily == null)
            {
                return HttpNotFound();
            }
            return View(companySalesDaily);
        }

        // POST: CompanySalesDailies/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyId,CompanyName,ManagerName,ManagerPhone,SalesType,CreateUserName,CreateTime,SalesLogDate")] CompanySalesDaily companySalesDaily)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companySalesDaily).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companySalesDaily);
        }

        // GET: CompanySalesDailies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanySalesDaily companySalesDaily = await db.CompanySalesDaily.FindAsync(id);
            if (companySalesDaily == null)
            {
                return HttpNotFound();
            }
            return View(companySalesDaily);
        }

        // POST: CompanySalesDailies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanySalesDaily companySalesDaily = await db.CompanySalesDaily.FindAsync(id);
            db.CompanySalesDaily.Remove(companySalesDaily);
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
