using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrmWebApp.Models
{
    public class SelectCityViewModel
    {
        public int OuterId { get; set; }
        public string OuterName { get; set; }

        public List<Province> ProvinceList { get; set; }

        public SelectCityViewModel() { }

        public SelectCityViewModel(int outerId,string outerName,List<Province> pl)
        {
            this.OuterId = outerId;
            this.OuterName = outerName;
            this.ProvinceList = new List<Province>();
            this.ProvinceList.AddRange(pl);
        }
    }

    public class Province
    {
        public long ProvinceID { get; set; }
        
        public string ProvinceName { get; set; }

        public bool Selected { get; set; }

        public List<ProvinceCity> CityList { get; set; }

        public Province() {
            this.Selected = false;
            this.CityList = new List<ProvinceCity>();
        }

        public Province(S_Province p, List<S_City> pcList)
        {
            this.ProvinceID = p.ProvinceID;
            this.ProvinceName = p.ProvinceName;
            this.Selected = false;
            this.CityList = new List<ProvinceCity>();
            foreach (S_City item in pcList)
            {
                ProvinceCity c = new Models.ProvinceCity(item);
                this.CityList.Add(c); 
            }
        }
    }

    public class ProvinceCity
    {
        public long ProvinceID { get; set; }
        public long CityID { get; set; }
        
        public string CityName { get; set; }       
        
        public bool Selected { get; set; }

        public ProvinceCity()
        {
            this.Selected = false;
        }

        public ProvinceCity(S_City c)
        {
            this.ProvinceID = c.ProvinceID.Value;
            this.CityID = c.CityID;
            this.CityName = c.CityName;
            this.Selected = false;
        }
    }
}