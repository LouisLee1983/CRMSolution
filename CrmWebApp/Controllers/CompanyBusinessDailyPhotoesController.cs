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
    public class CompanyBusinessDailyPhotoesController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanyBusinessDailyPhotoes
        public async Task<ActionResult> Index(int? dailyId)
        {
            var model = from cbd in db.CompanyBusinessDailyPhoto
                        select cbd;
            if (dailyId.HasValue)
            {
                model = model.Where(p => p.CompanyBusinessDailyId == dailyId.Value);
                ViewBag.DailyId = dailyId.Value;
            }

            return View(await model.ToListAsync());
        }

        // GET: CompanyBusinessDailyPhotoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailyPhoto companyBusinessDailyPhoto = await db.CompanyBusinessDailyPhoto.FindAsync(id);
            if (companyBusinessDailyPhoto == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailyPhoto);
        }

        // GET: CompanyBusinessDailyPhotoes/Create
        public ActionResult Create(int? dailyId)
        {
            var model = new CompanyBusinessDailyPhoto();
            if (dailyId.HasValue)
            {
                model.CompanyBusinessDailyId = dailyId.Value;
                model.PhotoName = "场地"; //取上一个记录得数据
                model.PhotoUrl = "";
            }

            return PartialView("_PartialBusinessDailyPhotoUpload", model);
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

        // POST: CompanyBusinessDailyPhotoes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompanyBusinessDailyPhoto companyBusinessDailyPhoto)
        {
            if (ModelState.IsValid)
            {
                //上传图片先
                string pathForSaving = Server.MapPath("~/CompanyImages/BussinessDailies/" + companyBusinessDailyPhoto.CompanyBusinessDailyId);
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        List<CompanyBusinessDailyPhoto> insertList = new List<CompanyBusinessDailyPhoto>();
                        var imageFiles = Request.Files;
                        for (int i = 0; i < imageFiles.Count; i++)
                        {
                            HttpPostedFileBase imageFile = imageFiles[i];

                            CompanyBusinessDailyPhoto insertItem = new CompanyBusinessDailyPhoto();
                            insertItem.CompanyBusinessDailyId = companyBusinessDailyPhoto.CompanyBusinessDailyId;
                            insertItem.PhotoName = companyBusinessDailyPhoto.PhotoName + i.ToString();                            

                            string fileName = insertItem.CompanyBusinessDailyId + "_" + insertItem.PhotoName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                            string fileExtension = Path.GetExtension(imageFile.FileName);
                            imageFile.SaveAs(Path.Combine(pathForSaving, fileName + fileExtension));

                            insertItem.PhotoUrl = fileName + fileExtension;   //保存图片名
                            insertList.Add(insertItem);
                        }
                        db.CompanyBusinessDailyPhoto.AddRange(insertList);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = string.Format("File upload failed: {0}", ex.Message);
                    }
                }

                return RedirectToAction("Edit","CompanyBusinessDailies", new { id = companyBusinessDailyPhoto.CompanyBusinessDailyId });
            }

            return View(companyBusinessDailyPhoto);
        }

        // GET: CompanyBusinessDailyPhotoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailyPhoto companyBusinessDailyPhoto = await db.CompanyBusinessDailyPhoto.FindAsync(id);
            if (companyBusinessDailyPhoto == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailyPhoto);
        }

        // POST: CompanyBusinessDailyPhotoes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyBusinessDailyId,PhotoUrl,PhotoName")] CompanyBusinessDailyPhoto companyBusinessDailyPhoto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyBusinessDailyPhoto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyBusinessDailyPhoto);
        }

        // GET: CompanyBusinessDailyPhotoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyBusinessDailyPhoto companyBusinessDailyPhoto = await db.CompanyBusinessDailyPhoto.FindAsync(id);
            if (companyBusinessDailyPhoto == null)
            {
                return HttpNotFound();
            }
            return View(companyBusinessDailyPhoto);
        }

        // POST: CompanyBusinessDailyPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyBusinessDailyPhoto companyBusinessDailyPhoto = await db.CompanyBusinessDailyPhoto.FindAsync(id);
            db.CompanyBusinessDailyPhoto.Remove(companyBusinessDailyPhoto);
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
