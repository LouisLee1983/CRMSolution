using CrmWebApp.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Svg;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CrmWebApp.Controllers
{
    public class ChartsController : Controller
    {
        // GET: Charts
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public ActionResult Index()
        {
            return View();
        }

        //HighCharts 导出图片 svg
        //filename type width scale svg
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Export(FormCollection fc)
        {
            string tType = fc["type"];
            string tSvg = fc["svg"];
            string tFileName = fc["filename"];
            if (string.IsNullOrEmpty(tFileName))
                tFileName = "chart";
            MemoryStream tData = new MemoryStream(Encoding.UTF8.GetBytes(tSvg));
            MemoryStream tStream = new MemoryStream();
            string tTmp = new Random().Next().ToString();
            string tExt = "";
            string tTypeString = "";

            switch (tType)
            {
                case "image/png":
                    tTypeString = "-m image/png";
                    tExt = "png";
                    break;
                case "image/jpeg":
                    tTypeString = "-m image/jpeg";
                    tExt = "jpg";
                    break;
                case "application/pdf":
                    tTypeString = "-m application/pdf";
                    tExt = "pdf";
                    break;
                case "image/svg+xml":
                    tTypeString = "-m image/svg+xml";
                    tExt = "svg";
                    break;
            }

            if (tTypeString != "")
            {
                string tWidth = fc["width"];
                Svg.SvgDocument tSvgObj = SvgDocument.Open(tData);

                switch (tExt)
                {
                    case "jpg":
                        tSvgObj.Draw().Save(tStream, ImageFormat.Jpeg);
                        break;
                    case "png":
                        tSvgObj.Draw().Save(tStream, ImageFormat.Png);
                        break;
                    case "pdf":
                        PdfWriter tWriter = null;
                        Document tDocumentPdf = null;
                        try
                        {
                            tSvgObj.Draw().Save(tStream, ImageFormat.Png);
                            tDocumentPdf = new Document(new iTextSharp.text.Rectangle((float)tSvgObj.Width, (float)tSvgObj.Height));
                            tDocumentPdf.SetMargins(0.0f, 0.0f, 0.0f, 0.0f);
                            iTextSharp.text.Image tGraph = iTextSharp.text.Image.GetInstance(tStream.ToArray());
                            tGraph.ScaleToFit((float)tSvgObj.Width, (float)tSvgObj.Height);

                            tStream = new MemoryStream();
                            tWriter = PdfWriter.GetInstance(tDocumentPdf, tStream);
                            tDocumentPdf.Open();
                            tDocumentPdf.NewPage();
                            tDocumentPdf.Add(tGraph);
                            tDocumentPdf.Close();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            tDocumentPdf.Close();
                            tWriter.Close();
                            tData.Dispose();
                            tData.Close();

                        }
                        break;
                    case "svg":
                        tStream = tData;
                        break;
                }
            }
            return File(tStream.ToArray(), tType, tFileName);
        }

        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public PartialViewResult GetCompanyTicketCountChart(string companyName)
        {
            //调用30天的数据
            DateTime startDate = DateTime.Today.AddMonths(-1);
            OtaCrmModel db = new OtaCrmModel();
            var a = from b in db.AgentGradeOperation
                    where b.agentName == companyName && b.statDate >= startDate
                    orderby b.statDate, b.agentDomain
                    select b;

            SimpleChartModel chartModel = new SimpleChartModel();
            chartModel.ChartType = DotNet.Highcharts.Enums.ChartTypes.Line;
            chartModel.Width = 600;
            chartModel.ContainerId = "companyTicketChart";
            chartModel.Title = "票量";

            chartModel.SeriesList = new List<YSeries>();

            //分开域名，
            Dictionary<string, int> agoTicketCountDict = new Dictionary<string, int>();
            List<string> domainList = new List<string>();
            foreach (AgentGradeOperation item in a)
            {
                if (!domainList.Contains(item.agentDomain))
                {
                    domainList.Add(item.agentDomain);
                }
                string key = item.agentDomain + item.statDate.Value.ToString("yyyyMMdd");
                if (!agoTicketCountDict.ContainsKey(key))
                {
                    agoTicketCountDict.Add(key, item.CurDateTicketCount.Value);
                }
            }

            TimeSpan ts = DateTime.Today - startDate;
            for (int i = 0; i < ts.Days; i++)
            {
                DateTime curDate = startDate.AddDays(i);
                string xValue = curDate.ToString("MMdd");
                chartModel.XList.Add(xValue);
            }
            //每个域名按天统计数据，加入series
            foreach (string agentDomain in domainList)
            {
                YSeries series = new YSeries();
                series.YSeriesList = new List<object>();
                series.YName = agentDomain;
                for (int i = 0; i < ts.Days; i++)
                {
                    DateTime curDate = startDate.AddDays(i);
                    string xValue = curDate.ToString("yyyyMMdd");
                    string key = agentDomain + xValue;
                    int ticketCount = 0;
                    if (agoTicketCountDict.ContainsKey(key))
                    {
                        ticketCount = agoTicketCountDict[key];
                    }
                    series.YSeriesList.Add(ticketCount);
                }
                chartModel.SeriesList.Add(series);
            }

            chartModel.ValueSuffix = "张";

            DotNet.Highcharts.Highcharts chart = GetChart(chartModel);
            return PartialView("_PartialChartView", chart);
        }
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public PartialViewResult GetMeetCountChart()
        {
            SimpleChartModel chartModel = new SimpleChartModel();
            chartModel.ContainerId = "meetChart";
            chartModel.Title = "沟通频率";

            chartModel.SeriesList = new List<YSeries>();

            YSeries series = new YSeries();
            series.YSeriesList = new List<object>();
            series.YName = "次数";

            chartModel.ValueSuffix = "次";
            //读取meeting表，按照时间为上一周
            DateTime startWeek = DateTime.Now.AddDays(1 - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d")));
            startWeek = startWeek.AddDays(-7);
            DateTime endWeek = startWeek.AddDays(6);
            chartModel.YTitle = startWeek.ToString("yyyyMMdd") + "-" + endWeek.ToString("yyyyMMdd");

            OtaCrmModel db = new OtaCrmModel();
            var ss = from i in db.CompanyMeeting
                     where i.MeetDate >= startWeek && i.MeetDate <= endWeek
                     group i by i.CreateUserName
                         into g
                     select new { count = g.Count(), userName = g.Key };
            foreach (var item in ss)
            {
                chartModel.XList.Add(item.userName);
                series.YSeriesList.Add(item.count);
            }
            chartModel.SeriesList.Add(series);

            DotNet.Highcharts.Highcharts chart = GetChart(chartModel);
            return PartialView("_PartialChartView", chart);
        }
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public PartialViewResult GetCompanyCountChart()
        {
            SimpleChartModel chartModel = new SimpleChartModel();
            chartModel.ContainerId = "companyCountChart";
            chartModel.Title = "客户数量";
            chartModel.SeriesList = new List<YSeries>();

            YSeries series = new YSeries();
            series.YSeriesList = new List<object>();
            series.YName = "数量";

            chartModel.YTitle = DateTime.Today.ToString("yyyy-MM-dd");
            chartModel.ValueSuffix = "个";
            //读取公司的表，group by 销售名
            OtaCrmModel db = new OtaCrmModel();
            var ss = from i in db.OtaCompany
                     group i by i.SalesUserName
                         into g
                     select new { count = g.Count(), userName = g.Key };
            foreach (var item in ss)
            {
                chartModel.XList.Add(item.userName);
                series.YSeriesList.Add(item.count);
            }
            chartModel.SeriesList.Add(series);

            DotNet.Highcharts.Highcharts chart = GetChart(chartModel);
            return PartialView("_PartialChartView", chart);
        }

        public DotNet.Highcharts.Highcharts GetChart(SimpleChartModel chartModel)
        {
            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts(chartModel.ContainerId);

            //初始化
            DotNet.Highcharts.Options.Chart chartOption = new DotNet.Highcharts.Options.Chart();
            chartOption.DefaultSeriesType = chartModel.ChartType;
            chartOption.Width = chartModel.Width;
            chartOption.Height = chartModel.Height;
            chart.InitChart(chartOption);

            //设置标题
            DotNet.Highcharts.Options.Title title = new DotNet.Highcharts.Options.Title();
            title.Align = DotNet.Highcharts.Enums.HorizontalAligns.Center;
            title.Text = chartModel.Title;
            chart.SetTitle(title);

            //提示
            DotNet.Highcharts.Options.Tooltip tooltip = new DotNet.Highcharts.Options.Tooltip();
            tooltip.ValueSuffix = chartModel.ValueSuffix;
            chart.SetTooltip(tooltip);

            DotNet.Highcharts.Options.PlotOptions plotOptions = new DotNet.Highcharts.Options.PlotOptions();
            plotOptions.Bar = new DotNet.Highcharts.Options.PlotOptionsBar();
            plotOptions.Bar.DataLabels = new DotNet.Highcharts.Options.PlotOptionsBarDataLabels();
            plotOptions.Bar.DataLabels.Enabled = true;
            chart.SetPlotOptions(plotOptions);

            //横坐标
            DotNet.Highcharts.Options.XAxis xs = new DotNet.Highcharts.Options.XAxis();
            xs.Categories = chartModel.XList.ToArray();
            xs.Title = new DotNet.Highcharts.Options.XAxisTitle();
            xs.Title.Text = chartModel.XTitle;
            chart.SetXAxis(xs);

            //纵坐标
            DotNet.Highcharts.Options.YAxis ys = new DotNet.Highcharts.Options.YAxis();
            ys.Title = new DotNet.Highcharts.Options.YAxisTitle();
            ys.Title.Text = chartModel.YTitle;
            chart.SetYAxis(ys);

            //设置表现值
            List<DotNet.Highcharts.Options.Series> ssList = new List<DotNet.Highcharts.Options.Series>();
            foreach (YSeries yserires in chartModel.SeriesList)
            {
                DotNet.Highcharts.Options.Series ss = new DotNet.Highcharts.Options.Series();
                ss.Data = new DotNet.Highcharts.Helpers.Data(yserires.YSeriesList.ToArray());
                ss.Name = yserires.YName;
                ssList.Add(ss);
            }
            chart.SetSeries(ssList.ToArray());

            return chart;
        }
    }
}