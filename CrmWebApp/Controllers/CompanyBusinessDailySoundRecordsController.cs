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
using System.IO;

namespace CrmWebApp.Controllers
{
    public class CompanyBusinessDailySoundRecordsController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanyBusinessDailySoundRecords
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Index(int? dailyId)
        {
            var model = from cbd in db.CompanyBusinessDailySoundRecord
                        select cbd;
            if (dailyId.HasValue)
            {
                model = model.Where(p => p.CompanyBusinessDailyId == dailyId.Value);
                ViewBag.DailyId = dailyId.Value;
            }

            return View(await model.ToListAsync());
        }

        // GET: CompanyBusinessDailySoundRecords/Details/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailySoundRecord companyBusinessDailySoundRecord = await db.CompanyBusinessDailySoundRecord.FindAsync(id);
            if (companyBusinessDailySoundRecord == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailySoundRecord);
        }

        // GET: CompanyBusinessDailySoundRecords/Create
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult Create(int? dailyId)
        {
            var model = new CompanyBusinessDailySoundRecord();
            if (dailyId.HasValue)
            {
                model.CompanyBusinessDailyId = dailyId.Value;
                model.SoundRecordName = "场地"; //取上一个记录得数据
                model.SoundRecordUrl = "";
            }

            return PartialView("_PartialBusinessDailySoundRecordUpload", model);
        }

        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }

        // POST: CompanyBusinessDailySoundRecords/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompanyBusinessDailySoundRecord companyBusinessDailySoundRecord)
        {
            if (ModelState.IsValid)
            {
                //上传图片先
                string pathForSaving = Server.MapPath("~/CompanyImages/BussinessDailies/" + companyBusinessDailySoundRecord.CompanyBusinessDailyId);
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        List<CompanyBusinessDailySoundRecord> insertList = new List<CompanyBusinessDailySoundRecord>();
                        var imageFiles = Request.Files;
                        for (int i = 0; i < imageFiles.Count; i++)
                        {
                            HttpPostedFileBase imageFile = imageFiles[i];
                            CompanyBusinessDailySoundRecord insertItem = new CompanyBusinessDailySoundRecord();
                            insertItem.CompanyBusinessDailyId = companyBusinessDailySoundRecord.CompanyBusinessDailyId;
                            insertItem.SoundRecordName = companyBusinessDailySoundRecord.SoundRecordName + i.ToString();

                            string fileName = insertItem.CompanyBusinessDailyId + "_" + insertItem.SoundRecordName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                            string fileExtension = Path.GetExtension(imageFile.FileName);
                            imageFile.SaveAs(Path.Combine(pathForSaving, fileName + fileExtension));

                            insertItem.SoundRecordUrl = fileName + fileExtension;   //保存图片名
                            insertList.Add(insertItem);
                        }
                        db.CompanyBusinessDailySoundRecord.AddRange(insertList);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = string.Format("File upload failed: {0}", ex.Message);
                    }
                }

                return RedirectToAction("Edit", "CompanyBusinessDailies", new { id = companyBusinessDailySoundRecord.CompanyBusinessDailyId });
            }

            return View(companyBusinessDailySoundRecord);
        }

        // GET: CompanyBusinessDailySoundRecords/Edit/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailySoundRecord companyBusinessDailySoundRecord = await db.CompanyBusinessDailySoundRecord.FindAsync(id);
            if (companyBusinessDailySoundRecord == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailySoundRecord);
        }

        // POST: CompanyBusinessDailySoundRecords/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyBusinessDailyId,SoundRecordName,SoundRecordUrl")] CompanyBusinessDailySoundRecord companyBusinessDailySoundRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyBusinessDailySoundRecord).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyBusinessDailySoundRecord);
        }

        // GET: CompanyBusinessDailySoundRecords/Delete/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailySoundRecord companyBusinessDailySoundRecord = await db.CompanyBusinessDailySoundRecord.FindAsync(id);
            if (companyBusinessDailySoundRecord == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailySoundRecord);
        }

        // POST: CompanyBusinessDailySoundRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyBusinessDailySoundRecord companyBusinessDailySoundRecord = await db.CompanyBusinessDailySoundRecord.FindAsync(id);
            db.CompanyBusinessDailySoundRecord.Remove(companyBusinessDailySoundRecord);
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
