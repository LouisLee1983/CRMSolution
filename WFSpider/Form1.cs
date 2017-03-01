using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using WFSpider.CrmWebServiceReference;

namespace WFSpider
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonGoUrl_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(textBoxUrl.Text);
        }

        public string GetZipWebResponseWithCookies(Cookie[] cookies, string url, string encode)
        {
            string result = "";
            CookieContainer cc = new CookieContainer();
            foreach (Cookie coItem in cookies)
            {
                cc.Add(coItem);
            }
            Encoding encoding = Encoding.GetEncoding(encode);
            try
            {
                HttpWebRequest webRequest = HttpWebRequest.Create(url) as HttpWebRequest;
                webRequest.Method = "GET";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Accept = "text/html, application/xhtml+xml, */*";
                webRequest.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
                webRequest.KeepAlive = true;
                webRequest.CookieContainer = cc;

                HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
                Stream streamResponse = webResponse.GetResponseStream();

                System.IO.Compression.GZipStream responseStream = new System.IO.Compression.GZipStream(streamResponse, System.IO.Compression.CompressionMode.Decompress);
                StreamReader streamReader = new StreamReader(responseStream, encoding);
                result = streamReader.ReadToEnd();

                streamResponse.Close();
                webResponse.Close();
            }
            catch (Exception exp)
            {
                result = exp.Message;
            }
            return result;
        }

        private void buttonGetCookie_Click(object sender, EventArgs e)
        {
            textBoxCookie.Text = webBrowser1.Document.Cookie;
        }

        private void buttonGetResponse_Click(object sender, EventArgs e)
        {
            string url = "http://checking.corp.qunar.com/agentEvaluation/query?domain=&limit=50&pageIndex=1&fromMonth=2017-01&toMonth=2017-01&agentName=&agentManager=&type=1&_=1488264078648";
            string domain = "flight.qunar.com";
            List<Cookie> cookieList = GetDomainCookies(domain, textBoxCookie.Text);
            textBoxResult.Text = GetZipWebResponseWithCookies(cookieList.ToArray(), url, "UTF-8");
        }

        public List<Cookie> GetDomainCookies(string domain, string cookieStr)
        {
            List<Cookie> result = new List<Cookie>();
            string cookiePath = "/";
            string cookieDomain = domain;
            char[] ch = { ';' };
            string[] Cookies = cookieStr.Replace(" ", "").Split(ch);
            for (int i = 0; i < Cookies.Length; i++)
            {

                int temp = Cookies[i].IndexOf('=');
                if (temp < 0)
                {
                    System.Net.Cookie c = new System.Net.Cookie(Cookies[i], "", cookiePath, cookieDomain);
                    result.Add(c);
                }
                else
                {
                    System.Net.Cookie c = new System.Net.Cookie(Cookies[i].Substring(0, temp), Cookies[i].Substring(temp + 1, Cookies[i].Length - temp - 1).Replace(",", "%2C"), cookiePath, cookieDomain);
                    result.Add(c);
                }
            }
            return result;
        }

        private void buttonParseJson_Click(object sender, EventArgs e)
        {
            string json = textBoxResult.Text;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            AgentGradeOperationRoot agentGradeOperation = jss.Deserialize(json, typeof(AgentGradeOperationRoot)) as AgentGradeOperationRoot;

        }

        public string[] GetAllFileName(string filePath)
        {
            DirectoryInfo dir = new DirectoryInfo(filePath);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            FileInfo[] finfo = dir.GetFiles();
            List<string> result = new List<string>();
            for (int i = 0; i < finfo.Length; i++)
            {
                result.Add(finfo[i].Name);
            }
            return result.ToArray();
        }

        private void buttonSaveData_Click(object sender, EventArgs e)
        {
            //读取文件夹下面的所有文件
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "AgentGradeOprationJson";
            if (CreateFolderIfNeeded(filePath))
            {
                string[] fileNames = GetAllFileName(filePath);
                Dictionary<string, int> perDayTicketDict = new Dictionary<string, int>();
                List<AgentDetailOperationDatum> agentDetailList = new List<AgentDetailOperationDatum>();
                //一次性读完整理完
                foreach (string fileName in fileNames)
                {
                    string json = ReadTxtFile(Path.Combine(filePath, fileName), false);
                    AgentGradeOperationRoot root = jss.Deserialize(json, typeof(AgentGradeOperationRoot)) as AgentGradeOperationRoot;
                    if (root != null && root.data != null && root.data.data != null && root.data.data.Length > 0)
                    {
                        agentDetailList.AddRange(root.data.data);
                        foreach (AgentDetailOperationDatum item in root.data.data)
                        {
                            DateTime curDate = DateTime.Parse(item.statDate);
                            string perDayKey = item.agentDomain.Trim() + curDate.ToString("yyyyMMdd");
                            if (!perDayTicketDict.ContainsKey(perDayKey))
                            {
                                perDayTicketDict.Add(perDayKey, item.totalTicketNum);
                            }
                        }
                    }
                }
                if (agentDetailList.Count > 0)
                {
                    List<AgentGradeOperation> agoList = GetAgentGradeOperations(agentDetailList, perDayTicketDict);
                    CrmWebServiceReference.ApiWebServiceSoapClient ws = new ApiWebServiceSoapClient();
                    ws.InsertAgentGradeOprations(agoList.ToArray());
                    textBoxResult.Text = "保存成功：" + agoList.Count;
                }
            }
        }

        public List<AgentGradeOperation> GetAgentGradeOperations(List<AgentDetailOperationDatum> agentDetailList, Dictionary<string, int> perDayTicketDict)
        {
            List<AgentGradeOperation> itemList = new List<AgentGradeOperation>();
            foreach (AgentDetailOperationDatum item in agentDetailList)
            {
                DateTime curDate = DateTime.Parse(item.statDate);
                int perDayTicketCount = 0;
                if (curDate.Day > 1)
                {
                    string perDayKey = item.agentDomain.Trim() + curDate.AddDays(-1).ToString("yyyyMMdd");
                    if (perDayTicketDict.ContainsKey(perDayKey))
                    {
                        perDayTicketCount = perDayTicketDict[perDayKey];
                    }
                }
                AgentGradeOperation agentGradeOperation = new AgentGradeOperation();
                agentGradeOperation.agentDomain = item.agentDomain;
                agentGradeOperation.agentManager = item.agentManager;
                agentGradeOperation.agentName = item.agentName;
                agentGradeOperation.complainRate = item.complainRate;
                agentGradeOperation.CreateTime = DateTime.Now;
                agentGradeOperation.CurDateTicketCount = item.totalTicketNum - perDayTicketCount; //需要找前一天计算
                agentGradeOperation.grade = item.grade;
                agentGradeOperation.involuntaryRate = item.involuntaryRate;
                agentGradeOperation.less60minRate = item.less60minRate;
                agentGradeOperation.messageTimeoutRate = item.messageTimeoutRate;
                agentGradeOperation.orderAlterRate = item.orderAlterRate;
                agentGradeOperation.passRate = item.passRate;
                agentGradeOperation.phoneAnswerRate = item.phoneAnswerRate;
                agentGradeOperation.promotion = item.promotion;
                agentGradeOperation.qapassRate = item.qapassRate;
                agentGradeOperation.qualification = item.qualification;
                agentGradeOperation.statDate = curDate;
                agentGradeOperation.statMonth = item.statMonth;
                agentGradeOperation.status = item.status;
                agentGradeOperation.totalScore = item.totalScore;
                agentGradeOperation.totalTicket = item.totalTicket;
                agentGradeOperation.totalTicketNum = item.totalTicketNum;
                agentGradeOperation.voluntaryRate = item.voluntaryRate;
                agentGradeOperation.whiteList = item.whiteList;

                itemList.Add(agentGradeOperation);
            }
            return itemList;
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
        public void DeleteAllFile(string filePath)
        {
            DirectoryInfo dir = new DirectoryInfo(filePath);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            FileInfo[] finfo = dir.GetFiles();
            for (int i = 0; i < finfo.Length; i++)
            {
                File.Delete(filePath + finfo[i].Name);
            }
        }
        public void WriteFile(string dataString, string filePath, bool append)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    fs.Close();
                }
                StreamWriter sw = new StreamWriter(filePath, append);
                sw.Write(dataString);
                sw.Close();
                sw = null;
            }
            catch { }
        }
        public string ReadTxtFile(string filePath, bool createIfNotExist)
        {
            string result = "";
            try
            {
                if (!File.Exists(filePath))
                {
                    if (createIfNotExist)
                    {
                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        fs.Close();
                    }
                    return "";
                }
                StreamReader sr = new StreamReader(filePath);
                result = sr.ReadToEnd();
                sr.Close();
            }
            catch { }
            return result;
        }

        private void buttonGetAllData_Click(object sender, EventArgs e)
        {
            //连取三个月数据，先执行按天的查询，最后结束的时候查一个月的统计，把jason保存到文件，然后再for循环保存
            DateTime curMonth = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-01"));
            string domain = "checking.corp.qunar.com";
            List<Cookie> cookieList = GetDomainCookies(domain, textBoxCookie.Text);
            int limit = 50;
            int pageIndex = 1;
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "AgentGradeOprationJson";
            CreateFolderIfNeeded(filePath);
            DeleteAllFile(filePath);    //清空旧的
            JavaScriptSerializer jss = new JavaScriptSerializer();
            for (int i = 0; i < 2; i++)
            {
                DateTime startDate = curMonth.AddMonths(-i);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                if (endDate > DateTime.Today)
                {
                    endDate = DateTime.Today;
                }
                TimeSpan ts = endDate - startDate;
                for (int j = 0; j <= ts.Days; j++)
                {
                    DateTime curDate = startDate.AddDays(j);
                    //获取第一页，然后获取到totalcount，然后再翻页
                    string firstDayJson = GetDayAgentGradeOprationJson(cookieList.ToArray(), curDate, limit, pageIndex);

                    string fileName = curDate.ToString("yyyyMMdd") + "_" + pageIndex + ".txt";
                    WriteFile(firstDayJson, Path.Combine(filePath, fileName), false);
                    AgentGradeOperationRoot agentGradeOperation = jss.Deserialize(firstDayJson, typeof(AgentGradeOperationRoot)) as AgentGradeOperationRoot;
                    int totalPage = agentGradeOperation.data.totalPage;
                    for (int x = 1; x < totalPage; x++)
                    {
                        int curPageIndex = pageIndex + x;
                        string json = GetDayAgentGradeOprationJson(cookieList.ToArray(), curDate, limit, curPageIndex);
                        fileName = curDate.ToString("yyyyMMdd") + "_" + curPageIndex + ".txt";
                        WriteFile(json, Path.Combine(filePath, fileName), false);
                    }
                }
            }
        }


        public string GetDayAgentGradeOprationJson(Cookie[] cookies, DateTime date, int limit, int pageIndex)
        {
            //先取出第一页，然后再循环翻页，取出的数据保存到文件夹
            string url = "http://checking.corp.qunar.com/agentEvaluation/query?domain=&limit=" + limit
                + "&pageIndex=" + pageIndex
                + "&fromDate=" + date.ToString("yyyy-MM-dd")
                + "&toDate=" + date.ToString("yyyy-MM-dd")
                + "&agentName=&agentManager=&type=0&_=1488264362156";
            return GetZipWebResponseWithCookies(cookies, url, "UTF-8");
        }
    }
}
