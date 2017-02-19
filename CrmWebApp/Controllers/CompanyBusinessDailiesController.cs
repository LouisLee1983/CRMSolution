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
using System.Data.SqlClient;

namespace CrmWebApp.Controllers
{
    public class CompanyBusinessDailiesController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanyBusinessDailies
        public async Task<ActionResult> Index(int? companyId)
        {
            var model = from cbd in db.CompanyBusinessDaily
                        select cbd;
            if (companyId.HasValue)
            {
                model = model.Where(p => p.CompanyId == companyId.Value);
                ViewBag.CompanyId = companyId.Value;
                ViewBag.CompanyName = db.OtaCompany.FirstOrDefault(p => p.Id == companyId.Value).CompanyName;
            }

            return View(await model.ToListAsync());
        }
        public ActionResult ShowViewPartial(int companyBusinessDailyId)
        {
            CompanyBusinessDaily dailyItem = db.CompanyBusinessDaily.FirstOrDefault(p => p.Id == companyBusinessDailyId);
            var paramList = from p in db.CompanyBusinessDailyParam
                            where p.CompanyBusinessDailyId == companyBusinessDailyId
                            select p;
            var photoList = from p in db.CompanyBusinessDailyPhoto
                            where p.CompanyBusinessDailyId == companyBusinessDailyId
                            select p;
            var soundList = from p in db.CompanyBusinessDailySoundRecord
                            where p.CompanyBusinessDailyId == companyBusinessDailyId
                            select p;
            var model = new CompanyBusinessDailyViewModel(dailyItem, paramList.ToList(), photoList.ToList(), soundList.ToList());
            return PartialView("_PartialCompanyBusinessDailyView", model);
        }

        // GET: CompanyBusinessDailies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDaily companyBusinessDaily = await db.CompanyBusinessDaily.FindAsync(id);
            if (companyBusinessDaily == null)
            {
                return HttpNotFound();
            }
            var paramList = from p in db.CompanyBusinessDailyParam
                            where p.CompanyBusinessDailyId == id.Value
                            select p;
            var photoList = from p in db.CompanyBusinessDailyPhoto
                            where p.CompanyBusinessDailyId == id.Value
                            select p;
            var soundList = from p in db.CompanyBusinessDailySoundRecord
                            where p.CompanyBusinessDailyId == id.Value
                            select p;
            var model = new CompanyBusinessDailyViewModel(companyBusinessDaily, paramList.ToList(), photoList.ToList(), soundList.ToList());
            return View(model);
        }

        // GET: CompanyBusinessDailies/Create
        public ActionResult Create(int? companyId)
        {
            var model = new CompanyBusinessDaily();
            if (companyId.HasValue)
            {
                model.CompanyId = companyId.Value;
                var company = db.OtaCompany.FirstOrDefault(p => p.Id == companyId.Value);
                if (company != null)
                {
                    model.CompanyName = company.CompanyName;
                }
                model.CreateUserName = User.Identity.Name;
                model.BussinessLogDate = DateTime.Today;
                model.CreateTime = DateTime.Now;
                model.ManagerName = ""; //取上一个记录得数据
                model.BussinessType = ""; //取上一个记录得数据
            }
            ViewData["BussinessTypeList"] = GetBussinessTypeList("国内");
            return View(model);
        }

        private List<SelectListItem> GetBussinessTypeList(string defaultValue)
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

        // POST: CompanyBusinessDailies/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CompanyId,CompanyName,BussinessType,ManagerName,CreateUserName,CreateTime,BussinessLogDate")] CompanyBusinessDaily companyBusinessDaily)
        {
            if (ModelState.IsValid)
            {
                db.CompanyBusinessDaily.Add(companyBusinessDaily);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = companyBusinessDaily.Id });
            }

            return View(companyBusinessDaily);
        }

        // GET: CompanyBusinessDailies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDaily companyBusinessDaily = await db.CompanyBusinessDaily.FindAsync(id);
            if (companyBusinessDaily == null)
            {
                return HttpNotFound();
            }

            var paramList = from p in db.CompanyBusinessDailyParam
                            where p.CompanyBusinessDailyId == id.Value
                            select p;

            var photoList = from p in db.CompanyBusinessDailyPhoto
                            where p.CompanyBusinessDailyId == id.Value
                            select p;
            var soundList = from p in db.CompanyBusinessDailySoundRecord
                            where p.CompanyBusinessDailyId == id.Value
                            select p;
            var model = new CompanyBusinessDailyViewModel(companyBusinessDaily, paramList.ToList(), photoList.ToList(), soundList.ToList());
            //如果没有默认值，就需要赋值默认值
            if (model.BusinessAmountList == null || model.BusinessAmountList.Count == 0)
            {
                model.BusinessAmountList = GetDefaultDailyParamList(id.Value, "业务结构");
            }
            if (model.EmployeeList == null || model.EmployeeList.Count == 0)
            {
                model.EmployeeList = GetDefaultDailyParamList(id.Value, "员工数量");
            }
            if (model.NewBusinessList == null || model.NewBusinessList.Count == 0)
            {
                model.NewBusinessList = GetDefaultDailyParamList(id.Value, "新业务量");
            }
            if (model.ItSystemList == null || model.ItSystemList.Count == 0)
            {
                model.ItSystemList = GetDefaultDailyParamList(id.Value, "软件系统");
            }

            return View(model);
        }

        public List<CompanyBusinessDailyParam> GetDefaultDailyParamList(int dailyId, string paramName)
        {
            //从字典表里面读取并赋值
            List<CompanyBusinessDailyParam> result = new List<Models.CompanyBusinessDailyParam>();
            var paramDicts = from p in db.ParamDict
                             where p.ParamName == paramName
                             select p;
            foreach (ParamDict paramDict in paramDicts)
            {
                CompanyBusinessDailyParam companyBusinessDailyParam = new CompanyBusinessDailyParam();
                companyBusinessDailyParam.CompanyBusinessDailyId = dailyId;
                companyBusinessDailyParam.Id = 0;
                companyBusinessDailyParam.ParamName = paramName;
                companyBusinessDailyParam.SubParamItem = paramDict.SubItemName;
                companyBusinessDailyParam.ItemAmount = 0;

                result.Add(companyBusinessDailyParam);
            }
            return result;
        }

        // POST: CompanyBusinessDailies/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CompanyBusinessDailyViewModel model, List<CompanyBusinessDailyParam> employeeList, List<CompanyBusinessDailyParam> businessAmountList,
            List<CompanyBusinessDailyParam> newBusinessList, List<CompanyBusinessDailyParam> itSystemList)
        {
            CompanyBusinessDaily companyBusinessDaily = db.CompanyBusinessDaily.FirstOrDefault(p => p.Id == model.Id);
            companyBusinessDaily.BussinessLogDate = model.BussinessLogDate;
            companyBusinessDaily.ManagerName = model.ManagerName;
            await db.SaveChangesAsync();    //看看保存运营记录，保存相关的具体运营信息

            string sql = "Delete From CompanyBusinessDailyParam Where CompanyBusinessDailyId=@CompanyBusinessDailyId";
            SqlParameter[] paras = new SqlParameter[] {
                     new SqlParameter("@CompanyBusinessDailyId",model.Id)
                    };
            db.Database.ExecuteSqlCommand(sql, paras);
            //有id的更新，id=0的新增
            foreach (CompanyBusinessDailyParam paramItem in employeeList)
            {
                db.CompanyBusinessDailyParam.Add(paramItem);
            }
            foreach (CompanyBusinessDailyParam paramItem in businessAmountList)
            {
                db.CompanyBusinessDailyParam.Add(paramItem);
            }
            foreach (CompanyBusinessDailyParam paramItem in newBusinessList)
            {
                db.CompanyBusinessDailyParam.Add(paramItem);
            }
            foreach (CompanyBusinessDailyParam paramItem in itSystemList)
            {
                db.CompanyBusinessDailyParam.Add(paramItem);
            }
            await db.SaveChangesAsync();


            return RedirectToAction("Index", new { companyId = model.CompanyId });
        }

        public ActionResult AddBusinessDailyParam(int dailyId, string paramName)
        {
            var model = new CompanyBusinessDailyParam();
            model.CompanyBusinessDailyId = dailyId;
            model.Id = 0;
            model.ItemAmount = 0;
            model.ParamName = paramName;
            model.SubParamItem = "其他";

            return PartialView("_PartialBusinessParamEditor", model);
        }

        [HttpPost]
        public ActionResult AddBusinessDailyParam(CompanyBusinessDailyParam model)
        {
            db.CompanyBusinessDailyParam.Add(model);
            db.SaveChanges();

            return RedirectToAction("Edit", new { id = model.CompanyBusinessDailyId });
        }

        // GET: CompanyBusinessDailies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDaily companyBusinessDaily = await db.CompanyBusinessDaily.FindAsync(id);
            if (companyBusinessDaily == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDaily);
        }

        // POST: CompanyBusinessDailies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyBusinessDaily companyBusinessDaily = await db.CompanyBusinessDaily.FindAsync(id);
            db.CompanyBusinessDaily.Remove(companyBusinessDaily);
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
