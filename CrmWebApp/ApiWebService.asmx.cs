using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CrmWebApp.Models;
using System.Security.Cryptography;

namespace CrmWebApp
{
    /// <summary>
    /// ApiWebService 的摘要说明
    /// </summary>
    /// 
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class ApiWebService : System.Web.Services.WebService
    {
        private string md5hashstr = "asdfghjklzxcvbnm";
        [WebMethod]
        public void InsertCompanyCms(List<CompanyCmsData> itemList, string md5Key)
        {
            if (md5Key == GetMd5Str(DateTime.Now.ToString("yyyyMMddHHmm00") + md5hashstr))
            {
                OtaCrmModel db = new OtaCrmModel();
                db.CompanyCmsData.AddRange(itemList);
                db.SaveChangesAsync();
            }
        }

        [WebMethod]
        public void InsertAgentGradeOprations(List<AgentGradeOperation> itemList, string md5Key)
        {
            if (md5Key == GetMd5Str(DateTime.Now.ToString("yyyyMMddHHmm00")+md5hashstr))
            {
                //计算每一条具体的当天票量，用当前的减去比它日期早一天的票量            
                OtaCrmModel db = new OtaCrmModel();
                db.AgentGradeOperation.AddRange(itemList);
                db.SaveChangesAsync();
            }
        }

        public string GetMd5Str(string str)
        {
            string result = "";
            MD5 md = MD5.Create();
            byte[] bytes = md.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            foreach (byte b in bytes)
            {
                result += b.ToString();
            }
            return result;
        }
    }
}
