using CrmWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrmWebApp.Controllers
{
    public class SelectCityViewController : Controller
    {
        // GET: SelectCityView
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void ChangeSalesServeArea(string userName, string province, string city)
        {
            OtaCrmModel db = new OtaCrmModel();
            var salesServeArea = (from s in db.SalesServeArea
                                  where s.UserName == userName && s.Province == province && s.City == city
                                  select s).FirstOrDefault();
            if (salesServeArea == null)
            {
                SalesServeArea newItem = new SalesServeArea();
                newItem.City = city;
                newItem.Province = province;
                newItem.UserName = userName;
                db.SalesServeArea.Add(newItem);
                db.SaveChanges();
            }
            else
            {
                db.SalesServeArea.Remove(salesServeArea);
                db.SaveChanges();
            }
        }

        public ActionResult EditSalesServeAreaCitys(string userName)
        {
            //根据用户名找出salesservearea
            OtaCrmModel db = new OtaCrmModel();
            var salesServeAreas = from s in db.SalesServeArea
                                  where s.UserName == userName
                                  select s.Province + "-" + s.City;
            List<string> selectedCityList = salesServeAreas.ToList();
            //把省市都读取出来，然后匹配好
            var sprovinces = from sp in db.S_Province
                             select sp;
            var scitys = from sc in db.S_City
                         select sc;

            Dictionary<long, List<S_City>> scitydict = new Dictionary<long, List<S_City>>();
            foreach (S_City scity in scitys)
            {
                if (!scity.ProvinceID.HasValue)
                {
                    continue;
                }
                long provinceId = scity.ProvinceID.Value;
                if (!scitydict.ContainsKey(provinceId))
                {
                    scitydict.Add(provinceId, new List<S_City>());
                }
                scitydict[provinceId].Add(scity);
            }

            List<Province> pl = new List<Province>();
            foreach (S_Province spItem in sprovinces)
            {
                List<S_City> scList = new List<S_City>();
                if (scitydict.ContainsKey(spItem.ProvinceID))
                {
                    scList.AddRange(scitydict[spItem.ProvinceID]);
                }
                Province p = new Province(spItem, scList);
                if (selectedCityList.Contains(p.ProvinceName + "-"))
                {
                    p.Selected = true;
                }
                else
                {
                    //设置provincecity的选中
                    foreach (ProvinceCity provinceCity in p.CityList)
                    {
                        if (selectedCityList.Contains(p.ProvinceName + "-" + provinceCity.CityName))
                        {
                            provinceCity.Selected = true;
                        }
                    }
                }
                pl.Add(p);
            }

            //生成并赋值一个SelectCityViewModel
            SelectCityViewModel model = new SelectCityViewModel(0, userName, pl);
            ViewBag.UserName = userName;

            return PartialView("_PartialSelectCityView", model);
        }
    }
}