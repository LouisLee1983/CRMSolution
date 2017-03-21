using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrmWebApp.Models
{
    public class CompanyEditViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = "公司名")]
        public string CompanyName { get; set; }
        
        [Display(Name = "销售")]
        public string SalesUserName { get; set; }

        [Display(Name = "状态")]
        public string BusinessStatus { get; set; }

        [Display(Name = "业务")]
        public string BusinessRange { get; set; }
    }
}