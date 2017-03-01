using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CrmWebApp.Models;

namespace CrmWebApp
{
    /// <summary>
    /// ApiWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class ApiWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void InsertAgentGradeOprations(List<AgentGradeOperation> itemList)
        {
            //计算每一条具体的当天票量，用当前的减去比它日期早一天的票量            
            OtaCrmModel db = new OtaCrmModel();
            db.AgentGradeOperation.AddRange(itemList);
            db.SaveChangesAsync();
        }
    }
}
