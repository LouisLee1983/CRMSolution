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
using System.IO;

namespace CrmWebApp.Controllers
{
    public class CompanyMediasController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanyMedias
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.CompanyMedia.ToListAsync());
        }

        // GET: CompanyMedias/Details/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMedia companyMedia = await db.CompanyMedia.FindAsync(id);
            if (companyMedia == null)
            {
                return HttpNotFound();
            }
            return View(companyMedia);
        }

        // GET: CompanyMedias/Create
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult Create(int outerId, string mediaFor)
        {
            var model = new CompanyMedia();
            model.Id = 0;
            model.MediaFor = mediaFor;
            model.MediaName = "附件";
            model.MediaUrl = "";
            model.OuterKeyId = outerId;

            return PartialView("_PartialAddCompanyMedia", model);
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

        // POST: CompanyMedias/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult Create(CompanyMedia companyMedia)
        {
            if (ModelState.IsValid)
            {
                //上传图片先
                string pathForSaving = Server.MapPath("~/CompanyImages/Meetings/" + companyMedia.OuterKeyId);
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        List<CompanyMedia> insertList = new List<CompanyMedia>();
                        var imageFiles = Request.Files;
                        for (int i = 0; i < imageFiles.Count; i++)
                        {
                            HttpPostedFileBase imageFile = imageFiles[i];

                            CompanyMedia insertItem = new CompanyMedia();
                            insertItem.MediaFor = companyMedia.MediaFor;
                            insertItem.MediaName = companyMedia.MediaName + i.ToString();
                            insertItem.OuterKeyId = companyMedia.OuterKeyId;
                            insertItem.MediaUrl = "";

                            string fileName = insertItem.OuterKeyId + "_" + insertItem.MediaName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                            string fileExtension = Path.GetExtension(imageFile.FileName);
                            imageFile.SaveAs(Path.Combine(pathForSaving, fileName + fileExtension));

                            insertItem.MediaUrl = fileName + fileExtension;   //保存图片名
                            insertList.Add(insertItem);
                        }
                        db.CompanyMedia.AddRange(insertList);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = string.Format("File upload failed: {0}", ex.Message);
                    }
                }

                return RedirectToAction("Edit", "CompanyMeetings", new { id = companyMedia.OuterKeyId });
            }

            return View(companyMedia);
        }

        // GET: CompanyMedias/Edit/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMedia companyMedia = await db.CompanyMedia.FindAsync(id);
            if (companyMedia == null)
            {
                return HttpNotFound();
            }
            return View(companyMedia);
        }

        // POST: CompanyMedias/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,OuterKeyId,MediaFor,MediaName,MediaUrl")] CompanyMedia companyMedia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyMedia).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyMedia);
        }

        // GET: CompanyMedias/Delete/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyMedia companyMedia = await db.CompanyMedia.FindAsync(id);
            if (companyMedia == null)
            {
                return HttpNotFound();
            }
            return View(companyMedia);
        }

        // POST: CompanyMedias/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyMedia companyMedia = await db.CompanyMedia.FindAsync(id);
            db.CompanyMedia.Remove(companyMedia);
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
