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

        public ActionResult ShowViewPartial(int id)
        {
            CompanySalesDaily dailyItem = db.CompanySalesDaily.FirstOrDefault(p => p.Id == id);
            var paramList = from p in db.CompanySalesDailyParam
                            where p.CompanySalesDailyId == id
                            select p;

            var salesSources = from p in db.CompanySalesDailySalesSource
                               where p.CompanySalesDailyId == id
                               select p;

            var salesProductDesps = from p in db.CompanySalesDailyProductDesp
                                    where p.CompanySalesDailyId == id
                                    select p;

            var salesFunds = from p in db.CompanySalesDailyFund
                             where p.CompanySalesDailyId == id
                             select p;

            var model = new CompanySalesDailyViewModel(dailyItem, salesSources.ToList(), salesProductDesps.ToList(), paramList.ToList(), salesFunds.ToList());
            return PartialView("_PartialCompanySalesDailyView", model);
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
                                 where p.ParamName == "业务类型"
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
                return RedirectToAction("Edit", new { companyId = companySalesDaily.CompanyId });
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

            var paramList = from p in db.CompanySalesDailyParam
                            where p.CompanySalesDailyId == id
                            select p;

            var salesSources = from p in db.CompanySalesDailySalesSource
                               where p.CompanySalesDailyId == id
                               select p;

            var salesProductDesps = from p in db.CompanySalesDailyProductDesp
                                    where p.CompanySalesDailyId == id
                                    select p;

            var salesFunds = from p in db.CompanySalesDailyFund
                             where p.CompanySalesDailyId == id
                             select p;

            var model = new CompanySalesDailyViewModel(companySalesDaily, salesSources.ToList(), salesProductDesps.ToList(), paramList.ToList(), salesFunds.ToList());
            //如果没有默认值，就需要赋值默认值
            if (model.SalesSourceList == null || model.SalesSourceList.Count == 0)
            {
                model.SalesSourceList = GetDefaultSalesSource(id.Value);
            }
            if (model.SalesProductDespList == null || model.SalesProductDespList.Count == 0)
            {
                model.SalesProductDespList = GetDefaultSalesProductDesp(id.Value);
            }
            if (model.SalesProductPercentList == null || model.SalesProductPercentList.Count == 0)
            {
                model.SalesProductPercentList = GetDefaultDailyParamList(id.Value, "产品结构");
            }
            if (model.SalesProfitList == null || model.SalesProfitList.Count == 0)
            {
                model.SalesProfitList = GetDefaultDailyParamList(id.Value, "营收信息");
            }
            if (model.SalesFundList == null || model.SalesFundList.Count == 0)
            {
                model.SalesFundList = GetDefaultSalesFundList(id.Value);
            }
            ViewData["SalesTypeList"] = GetSalesTypeList("国内");
            return View(model);
        }

        #region GetDefaultParams
        public List<CompanySalesDailyFund> GetDefaultSalesFundList(int dailyId)
        {
            List<CompanySalesDailyFund> result = new List<CompanySalesDailyFund>();
            var paramDicts = from p in db.ParamDict
                             where p.ParamName == "B2C平台"
                             select p;
            foreach (ParamDict paramDict in paramDicts)
            {
                CompanySalesDailyFund item = new CompanySalesDailyFund();
                item.CompanySalesDailyId = dailyId;
                item.Id = 0;
                item.FreezeFund = 0;
                item.WorkingFund = 0;
                item.NeededFund = 0;
                item.SalesSource = paramDict.SubItemName;

                result.Add(item);
            }
            return result;
        }

        public List<CompanySalesDailyParam> GetDefaultDailyParamList(int dailyId, string paramName)
        {
            List<CompanySalesDailyParam> result = new List<Models.CompanySalesDailyParam>();
            var paramDicts = from p in db.ParamDict
                             where p.ParamName == paramName
                             select p;
            foreach (ParamDict paramDict in paramDicts)
            {
                CompanySalesDailyParam companyBusinessDailyParam = new CompanySalesDailyParam();
                companyBusinessDailyParam.CompanySalesDailyId = dailyId;
                companyBusinessDailyParam.Id = 0;
                companyBusinessDailyParam.ParamName = paramName;
                companyBusinessDailyParam.SubParamItem = paramDict.SubItemName;
                companyBusinessDailyParam.ItemAmount = 0;

                result.Add(companyBusinessDailyParam);
            }
            return result;
        }

        public List<CompanySalesDailyProductDesp> GetDefaultSalesProductDesp(int dailyId)
        {
            List<CompanySalesDailyProductDesp> result = new List<CompanySalesDailyProductDesp>();
            var paramDicts = from p in db.ParamDict
                             where p.ParamName == "B2C平台"
                             select p;
            foreach (ParamDict paramDict in paramDicts)
            {
                CompanySalesDailyProductDesp item = new CompanySalesDailyProductDesp();
                item.CompanySalesDailyId = dailyId;
                item.Id = 0;
                item.SalesCount = 0;
                item.SalesProduct = "官网";
                item.SalesSource = paramDict.SubItemName;

                result.Add(item);
            }
            return result;
        }

        public List<CompanySalesDailySalesSource> GetDefaultSalesSource(int dailyId)
        {
            List<CompanySalesDailySalesSource> result = new List<CompanySalesDailySalesSource>();
            var paramDicts = from p in db.ParamDict
                             where p.ParamName == "B2C平台"
                             select p;
            foreach (ParamDict paramDict in paramDicts)
            {
                CompanySalesDailySalesSource item = new CompanySalesDailySalesSource();
                item.CompanySalesDailyId = dailyId;
                item.EmployeeCount = 0;
                item.EmployeePayment = 0;
                item.Id = 0;
                item.SaleSource = paramDict.SubItemName;
                item.TicketCount = 0;

                result.Add(item);
            }
            return result;
        }
        #endregion
        // POST: CompanySalesDailies/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyId,CompanyName,ManagerName,ManagerPhone,SalesType,CreateUserName,CreateTime,SalesLogDate")] CompanySalesDaily model,
            List<CompanySalesDailySalesSource> salesSourceList, List<CompanySalesDailyProductDesp> salesProductDespList, List<CompanySalesDailyParam> salesProductPercentList,
            List<CompanySalesDailyParam> salesProfitList, List<CompanySalesDailyFund> salesFundList)
        {
            if (ModelState.IsValid)
            {
                CompanySalesDaily dailyItem = db.CompanySalesDaily.FirstOrDefault(p => p.Id == model.Id);
                dailyItem.ManagerName = model.ManagerName;
                dailyItem.ManagerPhone = model.ManagerPhone;
                dailyItem.SalesLogDate = model.SalesLogDate;
                await db.SaveChangesAsync();    //看看保存运营记录，保存相关的具体运营信息

                UpdateCompanySalesDailySalesSource(model.Id, salesSourceList);
                UpdateCompanySalesDailyProductDesp(model.Id, salesProductDespList);
                UpdateCompanySalesDailyFund(model.Id, salesFundList);

                List<CompanySalesDailyParam> updateItems = new List<CompanySalesDailyParam>();
                updateItems.AddRange(salesProductPercentList);
                updateItems.AddRange(salesProfitList);
                UpdateCompanySalesDailyParam(model.Id, updateItems);

                return RedirectToAction("Index", new { companyId = model.CompanyId });
            }
            ViewData["SalesTypeList"] = GetSalesTypeList("国内");
            return View(model);
        }

        public void UpdateCompanySalesDailyParam(int dailyId, List<CompanySalesDailyParam> items)
        {
            string sql = "Delete From CompanySalesDailyParam Where CompanySalesDailyId=@CompanySalesDailyId";
            SqlParameter[] paras = new SqlParameter[] {
                     new SqlParameter("@CompanySalesDailyId",dailyId)
                    };
            db.Database.ExecuteSqlCommand(sql, paras);

            db.CompanySalesDailyParam.AddRange(items);
            db.SaveChanges();
        }

        public void UpdateCompanySalesDailyFund(int dailyId, List<CompanySalesDailyFund> items)
        {
            string sql = "Delete From CompanySalesDailyFund Where CompanySalesDailyId=@CompanySalesDailyId";
            SqlParameter[] paras = new SqlParameter[] {
                     new SqlParameter("@CompanySalesDailyId",dailyId)
                    };
            db.Database.ExecuteSqlCommand(sql, paras);

            db.CompanySalesDailyFund.AddRange(items);
            db.SaveChanges();
        }

        public void UpdateCompanySalesDailyProductDesp(int dailyId, List<CompanySalesDailyProductDesp> items)
        {
            string sql = "Delete From CompanySalesDailyProductDesp Where CompanySalesDailyId=@CompanySalesDailyId";
            SqlParameter[] paras = new SqlParameter[] {
                     new SqlParameter("@CompanySalesDailyId",dailyId)
                    };
            db.Database.ExecuteSqlCommand(sql, paras);

            db.CompanySalesDailyProductDesp.AddRange(items);
            db.SaveChanges();
        }

        public void UpdateCompanySalesDailySalesSource(int dailyId, List<CompanySalesDailySalesSource> items)
        {
            string sql = "Delete From CompanySalesDailySalesSource Where CompanySalesDailyId=@CompanySalesDailyId";
            SqlParameter[] paras = new SqlParameter[] {
                     new SqlParameter("@CompanySalesDailyId",dailyId)
                    };            
            db.Database.ExecuteSqlCommand(sql, paras);

            db.CompanySalesDailySalesSource.AddRange(items);
            db.SaveChanges();
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
