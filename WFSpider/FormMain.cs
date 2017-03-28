using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using WFSpider.CrmWebService;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Util;

namespace WFSpider
{
    public partial class FormMain : Form
    {
        private string md5hashstr = "asdfghjklzxcvbnm";

        public FormMain()
        {
            InitializeComponent();
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

        private void buttonGoUrl_Click(object sender, EventArgs e)
        {
            webBrowserAgentGrade.Navigate(textBoxAgentGradeUrl.Text);
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
            textBoxAgentGradeCookie.Text = webBrowserAgentGrade.Document.Cookie;
            textBoxAgentLastDate.Text = ReadTxtFile("lastdate.txt",true);
        }

        private void buttonGetResponse_Click(object sender, EventArgs e)
        {
            string url = "http://checking.corp.qunar.com/agentEvaluation/query?domain=&limit=50&pageIndex=1&fromMonth=2017-01&toMonth=2017-01&agentName=&agentManager=&type=1&_=1488264078648";
            string domain = "flight.qunar.com";
            List<Cookie> cookieList = GetDomainCookies(domain, textBoxAgentGradeCookie.Text);
            textBoxAgentGradeResult.Text = GetZipWebResponseWithCookies(cookieList.ToArray(), url, "UTF-8");
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
            string json = textBoxAgentGradeResult.Text;
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
                DateTime startDate = DateTime.Parse(textBoxAgentLastDate.Text);
                startDate = startDate.AddDays(-1);
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
                            if (curDate == startDate)
                            {
                                continue;
                            }
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
                    //需要分批存储
                    List<AgentGradeOperation> agoList = GetAgentGradeOperations(agentDetailList, perDayTicketDict);

                    CrmWebService.ApiWebServiceSoapClient ws = new ApiWebServiceSoapClient();
                    for (int i = 0; i < agoList.Count; i += 1000)
                    {
                        int len = 1000;
                        if (i + len > agoList.Count)
                        {
                            len = agoList.Count - i;
                        }
                        string md5str = GetMd5Str(DateTime.Now.ToString("yyyyMMddHHmm00") + md5hashstr);
                        ws.InsertAgentGradeOprations(agoList.GetRange(i, len).ToArray(), md5str);
                        textBoxAgentGradeResult.AppendText(agoList.Count + ":" + i.ToString() + "\r\n");
                    }

                    textBoxAgentGradeResult.Text = "保存成功：" + agoList.Count;
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
                File.Delete(Path.Combine(filePath, finfo[i].Name));
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
            DateTime lastDate = curMonth.AddMonths(-2);
            string lastDateFilePath = "lastdate.txt";
            string lastDateStr = ReadTxtFile(lastDateFilePath, true);
            if (!string.IsNullOrEmpty(lastDateStr))
            {
                lastDate = DateTime.Parse(lastDateStr);
            }

            string domain = "checking.corp.qunar.com";
            List<Cookie> cookieList = GetDomainCookies(domain, textBoxAgentGradeCookie.Text);
            int limit = 50;
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "AgentGradeOprationJson";
            CreateFolderIfNeeded(filePath);
            DeleteAllFile(filePath);    //清空旧的
            JavaScriptSerializer jss = new JavaScriptSerializer();
            //从2个月前开始发扫
            DateTime startDate = lastDate.AddDays(-1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            for (int i = 0; i < 3; i++)
            {
                startDate = lastDate.AddMonths(i);
                if (startDate > DateTime.Today)
                {
                    break;
                }
                endDate = startDate.AddMonths(1).AddDays(-1);
                if (endDate > DateTime.Today)
                {
                    endDate = DateTime.Today;
                }
                TimeSpan ts = endDate - startDate;
                for (int j = 0; j <= ts.Days; j++)
                {
                    int pageIndex = 1;
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
                    textBoxAgentGradeResult.AppendText(fileName + ":" + totalPage + "\r\n");
                }
            }
            WriteFile(endDate.AddDays(1).ToString("yyyy-MM-dd"), lastDateFilePath, false);

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

        //*******************************cms系统
        private void buttonGoCmsUrl_Click(object sender, EventArgs e)
        {
            string url = textBoxCmsUrl.Text;
            webBrowserCms.Navigate(url);
            textBoxCmsUrl.Text = "http://cms.qunar.com/cms/queryFlightCompany.do?method=listCompany&companyType=0";
        }

        private void buttonGetCmsCookies_Click(object sender, EventArgs e)
        {
            //用新的cookie获取方式
            Uri uri = webBrowserCms.Url;
            string strCookie = FullWebBrowserCookie.GetCookieInternal(uri, false);
            textBoxCmsCookies.Text = strCookie;
        }

        private void buttonGetCmsOnepageData_Click(object sender, EventArgs e)
        {
            string url = "http://cms.qunar.com/cms/queryFlightCompany.do";
            string curPage = textBoxCmsPageIndex.Text;
            string postData = "method=listCompany&companyType=0&name=&websiteName=&phone=&currPage=" + curPage + "&pageSize=30&totalCount=11";
            string domain = "cms.qunar.com";
            //string postStr = "name=&websiteName=&phone=&currPage=1&pageSize=30&totalCount=11";
            List<Cookie> cookieList = GetDomainCookies(domain, textBoxCmsCookies.Text);
            //应该使用post获取的以后每一页，先要根据当前页面分析出总条数和总页数

            textBoxCmsResult.Text = PostCmsWebResponseWithCookies(cookieList.ToArray(), url, postData, "UTF-8");
        }

        public string PostCmsWebResponseWithCookies(Cookie[] cookies, string url, string postData, string encode)
        {
            CookieContainer cc = new CookieContainer();
            foreach (Cookie coItem in cookies)
            {
                cc.Add(coItem);
            }
            string result = "";

            Encoding encoding = Encoding.GetEncoding(encode);
            byte[] strParm = encoding.GetBytes(postData);
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.Timeout = 300000;
                myReq.KeepAlive = true;
                myReq.Method = "POST";
                myReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 10.0; WOW64; Trident/7.0)";
                myReq.ContentType = "application/x-www-form-urlencoded";
                myReq.Accept = "text/html, application/xhtml+xml, image/jxr, */*";
                myReq.Headers.Add("Accept-Language", "zh-Hans-CN, zh-Hans; q=0.5");
                myReq.Headers.Add("Accept-Encoding", "gzip, deflate");
                myReq.CookieContainer = cc;
                myReq.ContentLength = strParm.Length;
                myReq.AllowWriteStreamBuffering = false;

                Stream outStream = myReq.GetRequestStream();
                outStream.Write(strParm, 0, strParm.Length);
                outStream.Close();

                WebResponse myResp = null;
                myResp = myReq.GetResponse();

                myResp.Headers.Add(HttpResponseHeader.Connection, "keep-alive");
                myResp.Headers.Add(HttpResponseHeader.ContentEncoding, "gzip");
                myResp.Headers.Add(HttpResponseHeader.ContentLanguage, "zh-Hans");
                myResp.Headers.Add(HttpResponseHeader.ContentType, "text/html; charset=UTF-8");
                myResp.Headers.Add(HttpResponseHeader.Server, "QWS/1.0");
                myResp.Headers.Add(HttpResponseHeader.TransferEncoding, "chunked");
                myResp.Headers.Add(HttpResponseHeader.Vary, "Accept-Encoding");

                Stream ReceiveStream = myResp.GetResponseStream();

                System.IO.Compression.GZipStream responseStream = new System.IO.Compression.GZipStream(ReceiveStream, System.IO.Compression.CompressionMode.Decompress);
                StreamReader streamReader = new StreamReader(responseStream, encoding);
                result = streamReader.ReadToEnd();

                ReceiveStream.Close();
                myResp.Close();
            }
            catch (WebException exp)
            {
                HttpWebResponse res = (HttpWebResponse)exp.Response;
                StreamReader sr = new StreamReader(res.GetResponseStream(), encoding);
                result = sr.ReadToEnd();
            }
            return result;
        }

        private void buttonGenerateCmsData_Click(object sender, EventArgs e)
        {
            string html = textBoxCmsResult.Text;
            int totalCount = 0;
            List<CompanyCmsData> ccdList = GenerateCCDList(html, out totalCount);
            textBoxCmsTotalcount.Text = totalCount.ToString();
        }

        public List<CrmWebService.CompanyCmsData> GenerateCCDList(string html, out int totalCount)
        {
            List<CrmWebService.CompanyCmsData> ccdList = new List<CompanyCmsData>();

            mshtml.HTMLDocumentClass doc = new mshtml.HTMLDocumentClass();
            doc.designMode = "on";
            doc.IHTMLDocument2_write(html);

            mshtml.IHTMLElement divcnt = doc.getElementById("cnt");
            mshtml.IHTMLElementCollection childrens = (mshtml.IHTMLElementCollection)divcnt.children;
            mshtml.IHTMLTable table = (mshtml.IHTMLTable)childrens.item(2);
            for (int i = 1; i < table.rows.length; i++)
            {
                CompanyCmsData item = new CompanyCmsData();

                mshtml.IHTMLTableRow row = (mshtml.IHTMLTableRow)table.rows.item(i);
                mshtml.IHTMLElement cell = (mshtml.IHTMLElement)row.cells.item(0);
                item.CmsId = int.Parse(cell.innerText.Trim());
                cell = (mshtml.IHTMLElement)row.cells.item(1);
                item.CompanyName = string.IsNullOrEmpty(cell.innerText) ? "" : cell.innerText.Trim();
                cell = (mshtml.IHTMLElement)row.cells.item(2);
                item.TTSStatusDesp = string.IsNullOrEmpty(cell.innerText) ? "" : cell.innerText.Trim();
                cell = (mshtml.IHTMLElement)row.cells.item(3);
                item.ContactPhone = string.IsNullOrEmpty(cell.innerText) ? "" : cell.innerText.Trim();
                cell = (mshtml.IHTMLElement)row.cells.item(5);
                item.SalesName = string.IsNullOrEmpty(cell.innerText) ? "" : cell.innerText.Trim();

                ccdList.Add(item);
            }
            totalCount = 0;
            mshtml.IHTMLElementCollection eles = doc.getElementsByName("totalCount");
            if (eles != null && eles.length > 0)
            {
                totalCount = int.Parse(((mshtml.IHTMLElement)eles.item(0)).getAttribute("value").ToString());
            }

            return ccdList;
        }

        private void buttonGetAllPageCmsReponse_Click(object sender, EventArgs e)
        {
            //计算页数，然后从第一页开始翻页
            int totalCount = int.Parse(textBoxCmsTotalcount.Text);
            int pageCount = totalCount / 30;
            pageCount++;
            List<CompanyCmsData> pagecmsList = new List<CompanyCmsData>();
            for (int i = 1; i <= pageCount; i++)
            {
                string html = GetCmsPageHtml(i, totalCount);
                int temp = 0;
                List<CompanyCmsData> ccdList = GenerateCCDList(html, out temp);
                pagecmsList.AddRange(ccdList);
            }

            string compactStr = "";
            for (int i = 0; i < pagecmsList.Count; i++)
            {
                compactStr += pagecmsList[i].CmsId.Value.ToString() + "^" + pagecmsList[i].CompanyName + "^" + pagecmsList[i].TTSStatusDesp + "^" + pagecmsList[i].SalesName + "\r\n";
            }
            WriteFile(compactStr, "cms.txt", false);


            CrmWebService.ApiWebServiceSoapClient ws = new ApiWebServiceSoapClient();
            //然后每个获取详情
            List<CompanyCmsData> result = new List<CompanyCmsData>();
            //每一个公司进去抓取更加详细的信息
            for (int i = 0; i < pagecmsList.Count; i++)
            {
                //获取详细的html
                string detailhtml = GetCmsDetailHtml(pagecmsList[i].CmsId.Value);
                //把html转成对象
                CompanyCmsData dItem = GenerateCCD(detailhtml);
                if (dItem == null || string.IsNullOrEmpty(dItem.RealAddress))
                {
                    textBoxCmsResult.AppendText(pagecmsList[i].CmsId.Value.ToString() + ":失败\r\n");                    
                }
                else
                {
                    //重新赋值对象
                    dItem.CmsId = pagecmsList[i].CmsId;
                    dItem.CompanyName = pagecmsList[i].CompanyName;
                    dItem.TTSStatusDesp = pagecmsList[i].TTSStatusDesp;
                    dItem.SalesName = pagecmsList[i].SalesName;
                    result.Add(dItem);
                    textBoxCmsResult.AppendText(pagecmsList[i].CmsId.Value.ToString() + ":成功\r\n");
                }

                if (result.Count > 100)
                {
                    string md5str = GetMd5Str(DateTime.Now.ToString("yyyyMMddHHmm00") + md5hashstr);
                    ws.InsertCompanyCms(result.ToArray(), md5str);
                    textBoxCmsResult.AppendText("100");
                    result = new List<CompanyCmsData>();
                }
            }
            if(result.Count > 0)
            {
                string md5str = GetMd5Str(DateTime.Now.ToString("yyyyMMddHHmm00") + md5hashstr);
                ws.InsertCompanyCms(result.ToArray(), md5str);
                textBoxCmsResult.AppendText("100");
                result = new List<CompanyCmsData>();
            }
        }

        public string GetCmsPageHtml(int page, int totalCount)
        {
            string url = "http://cms.qunar.com/cms/queryFlightCompany.do";
            string postData = "method=listCompany&companyType=0&name=&websiteName=&phone=&currPage=" + page + "&pageSize=30&totalCount=" + totalCount;
            string domain = "cms.qunar.com";
            //string postStr = "name=&websiteName=&phone=&currPage=1&pageSize=30&totalCount=11";
            List<Cookie> cookieList = GetDomainCookies(domain, textBoxCmsCookies.Text);
            //应该使用post获取的以后每一页，先要根据当前页面分析出总条数和总页数

            return PostCmsWebResponseWithCookies(cookieList.ToArray(), url, postData, "UTF-8");
        }

        private void buttonSaveCmsData_Click(object sender, EventArgs e)
        {

        }

        private void buttonGetCmsDataDetail_Click(object sender, EventArgs e)
        {
            string id = textBoxCmsId.Text;
            string url = "http://cms.qunar.com/cms/updateCompanyForm.do?id=" + id + "&companyType=0";
            string domain = "cms.qunar.com";
            List<Cookie> cookieList = GetDomainCookies(domain, textBoxCmsCookies.Text);
            string response = GetZipWebResponseWithCookies(cookieList.ToArray(), url, "UTF-8");
            textBoxCmsResult.Text = response;
        }

        private string GetCmsDetailHtml(int id)
        {
            string url = "http://cms.qunar.com/cms/updateCompanyForm.do?id=" + id + "&companyType=0";
            string domain = "cms.qunar.com";
            List<Cookie> cookieList = GetDomainCookies(domain, textBoxCmsCookies.Text);
            string response = GetZipWebResponseWithCookies(cookieList.ToArray(), url, "UTF-8");
            return response;
        }

        private void buttonGenerateCmsDetail_Click(object sender, EventArgs e)
        {
            string html = textBoxCmsResult.Text;
            CompanyCmsData item = GenerateCCD(html);
            MessageBox.Show(item.RealAddress);
        }

        public CompanyCmsData GenerateCCD(string html)
        {
            CompanyCmsData item = new CompanyCmsData();
            mshtml.HTMLDocumentClass doc = new mshtml.HTMLDocumentClass();
            doc.designMode = "on";
            doc.IHTMLDocument2_write(html);

            mshtml.IHTMLElement noteEle = doc.getElementById("note");   //公司简介
            if (noteEle != null)
            {
                item.CompanyDesp = string.IsNullOrEmpty(noteEle.innerText) ? "" : noteEle.innerText.Trim();
            }
            else
            {
                return null;
            }
            mshtml.IHTMLElement ele = doc.getElementById("registeredAddress"); //注册地址
            if (ele.getAttribute("value") != null)
            {
                item.RegisterAddress = string.IsNullOrEmpty(ele.getAttribute("value").ToString()) ? "" : ele.getAttribute("value").ToString();
            }
            ele = doc.getElementById("address"); //地址
            if (ele.getAttribute("value") != null)
            {
                item.RealAddress = string.IsNullOrEmpty(ele.getAttribute("value").ToString()) ? "" : ele.getAttribute("value").ToString();
            }
            ele = doc.getElementById("businessLicense"); //营业执照号
            if (ele.getAttribute("value") != null)
            {
                item.CompanyIdNo = string.IsNullOrEmpty(ele.getAttribute("value").ToString()) ? "" : ele.getAttribute("value").ToString();
            }
            ele = doc.getElementById("email"); //营业执照号
            if (ele.getAttribute("value") != null)
            {
                item.CompanyEmail = string.IsNullOrEmpty(ele.getAttribute("value").ToString()) ? "" : ele.getAttribute("value").ToString();
            }
            ele = doc.getElementById("legalRepresentative"); //法人
            if (ele.getAttribute("value") != null)
            {
                item.LegalPerson = string.IsNullOrEmpty(ele.getAttribute("value").ToString()) ? "" : ele.getAttribute("value").ToString();
            }
            ele = doc.getElementById("contact"); //联系人
            if (ele.getAttribute("value") != null)
            {
                item.ContactPerson = string.IsNullOrEmpty(ele.getAttribute("value").ToString()) ? "" : ele.getAttribute("value").ToString();
            }
            ele = doc.getElementById("phone"); //电话
            if (ele.getAttribute("value") != null)
            {
                item.ContactPhone = string.IsNullOrEmpty(ele.getAttribute("value").ToString()) ? "" : ele.getAttribute("value").ToString();
            }
            ele = doc.getElementById("accountName"); //tts的admin账号
            if (ele.getAttribute("value") != null)
            {
                item.TTSAdminAccount = string.IsNullOrEmpty(ele.getAttribute("value").ToString()) ? "" : ele.getAttribute("value").ToString();
            }

            mshtml.IHTMLElementCollection eles = doc.getElementsByName("promotionNameDomestic");
            if (eles != null && eles.length > 0)
            {
                ele = (mshtml.IHTMLElement)eles.item(0);
                if (ele.getAttribute("value") != null)
                {
                    item.GuoneiWebName = string.IsNullOrEmpty(ele.getAttribute("value").ToString()) ? "" : ele.getAttribute("value").ToString();
                }
            }
            eles = doc.getElementsByName("promotionNameInternational");
            if (eles != null && eles.length > 0)
            {
                ele = (mshtml.IHTMLElement)eles.item(0);
                if (ele.getAttribute("value") != null)
                {
                    item.GuojiWebName = string.IsNullOrEmpty(ele.getAttribute("value").ToString()) ? "" : ele.getAttribute("value").ToString();
                }
            }
            ele = doc.getElementById("bossBackgrouds");   //公司简介
            item.BossBackground = string.IsNullOrEmpty(ele.innerText) ? "" : ele.innerText.Trim();

            return item;
        }
    }
}
