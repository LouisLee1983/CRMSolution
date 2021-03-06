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
    public class CompanyCertificatesController : Controller
    {
        private OtaCrmModel db = new OtaCrmModel();

        // GET: CompanyCertificates
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Index(int? companyId)
        {
            var model = from cbd in db.CompanyCertificate
                        select cbd;
            if (companyId.HasValue)
            {
                model = model.Where(p => p.CompanyId == companyId.Value);
                ViewBag.CompanyId = companyId.Value;
                ViewBag.CompanyName = db.OtaCompany.FirstOrDefault(p => p.Id == companyId.Value).CompanyName;
            }

            return View(await model.ToListAsync());
        }

        // GET: CompanyCertificates/Details/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyCertificate companyCertificate = await db.CompanyCertificate.FindAsync(id);
            if (companyCertificate == null)
            {
                return HttpNotFound();
            }
            return View(companyCertificate);
        }

        // GET: CompanyCertificates/Create
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult Create(int? companyId)
        {
            var model = new CompanyCertificate();
            if (companyId.HasValue)
            {
                model.CompanyId = companyId.Value;
                var company = db.OtaCompany.FirstOrDefault(p => p.Id == companyId.Value);
                if (company != null)
                {
                    model.CompanyName = company.CompanyName;
                }
                model.CreateUserName = User.Identity.Name;
                model.CertificateName = "";
                model.CreateTime = DateTime.Now;
                model.PictureUrl = ""; //取上一个记录得数据
            }
            ViewData["CertificateNameList"] = GetCertificateNameList("营业执照");

            return PartialView("_PartialCertificateUpload",model);
        }

        private List<SelectListItem> GetCertificateNameList(string defaultValue)
        {
            var bussinessTypes = from p in db.ParamDict
                                 where p.ParamName == "资质"
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

        // POST: CompanyCertificates/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult Create([Bind(Include = "Id,CompanyId,CertificateName,CompanyName,PictureUrl,CreateTime,CreateUserName")] CompanyCertificate companyCertificate, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                //上传图片先
                string pathForSaving = Server.MapPath("~/CompanyImages/Certificates/" + companyCertificate.CompanyName);
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        string fileName = companyCertificate.CompanyName + "_" + companyCertificate.CertificateName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        string fileExtension = Path.GetExtension(imageFile.FileName);
                        imageFile.SaveAs(Path.Combine(pathForSaving, fileName + fileExtension));
                        companyCertificate.PictureUrl = fileName + fileExtension;   //保存图片名
                        db.CompanyCertificate.Add(companyCertificate);
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = string.Format("File upload failed: {0}", ex.Message);
                    }
                }

                return RedirectToAction("Index", new { companyId = companyCertificate.CompanyId });
            }
            ViewData["CertificateNameList"] = GetCertificateNameList("营业执照");

            return View(companyCertificate);
        }

        // GET: CompanyCertificates/Edit/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyCertificate companyCertificate = await db.CompanyCertificate.FindAsync(id);
            if (companyCertificate == null)
            {
                return HttpNotFound();
            }
            return View(companyCertificate);
        }

        // POST: CompanyCertificates/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CompanyId,CertificateName,CompanyName,PictureUrl,CreateTime,CreateUserName")] CompanyCertificate companyCertificate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyCertificate).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(companyCertificate);
        }

        // GET: CompanyCertificates/Delete/5
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyCertificate companyCertificate = await db.CompanyCertificate.FindAsync(id);
            if (companyCertificate == null)
            {
                return HttpNotFound();
            }
            return View(companyCertificate);
        }

        // POST: CompanyCertificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompanyCertificate companyCertificate = await db.CompanyCertificate.FindAsync(id);
            int companyId = companyCertificate.CompanyId;
            db.CompanyCertificate.Remove(companyCertificate);
            await db.SaveChangesAsync();
            return RedirectToAction("Index",new { companyId=companyId});
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
