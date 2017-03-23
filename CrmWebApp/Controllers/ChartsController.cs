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
using System.Web.Helpers;
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

        //日均票量分客户数
        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public PartialViewResult GetCompanyTicketRangeChart()
        {
            SimpleChartModel chartModel = new SimpleChartModel();
            //chartModel.Height = 500;
            //chartModel.ContainerId = "companyTicketRangeChart";
            //chartModel.Title = "客户票量";
            //chartModel.SeriesList = new List<YSeries>();

            //YSeries series = new YSeries();
            //series.YSeriesList = new List<object>();
            //series.YName = "数量";

            //chartModel.YTitle = DateTime.Today.ToString("yyyy-MM-dd");
            //chartModel.ValueSuffix = "个";
            ////读取公司的表，group by 销售名
            //OtaCrmModel db = new OtaCrmModel();
            //var ss = from i in db.AgentGradeOperation
            //         group i by i.SalesUserName
            //             into g
            //         select new { count = g.Count(), userName = g.Key };
            //foreach (var item in ss)
            //{
            //    chartModel.XList.Add(item.userName);
            //    series.YSeriesList.Add(item.count);
            //}
            //chartModel.SeriesList.Add(series);

            DotNet.Highcharts.Highcharts chart = GetChart(chartModel);
            return PartialView("_PartialChartView", chart);
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
            chartModel.Height = 500;
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

        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public PartialViewResult GetMyCompanyStatusChart()
        {
            AccountController ac = new AccountController();
            string realName = ac.GetRealName(User.Identity.Name);
            SimpleChartModel chartModel = new SimpleChartModel();
            chartModel.ChartType = DotNet.Highcharts.Enums.ChartTypes.Pie;
            chartModel.ContainerId = "myCompanyStatusChart";
            chartModel.Title = realName+ "客户状态";

            YSeries series = new YSeries();
            series.YName = "数量";

            chartModel.ValueSuffix = "个";
            //读取公司的表，group by 销售名
            OtaCrmModel db = new OtaCrmModel();
            var ss = from i in db.OtaCompany
                     where i.BusinessStatus != null && i.SalesUserName == realName
                     group i by i.BusinessStatus
                         into g
                     select new { count = g.Count(), businessStatus = g.Key };
            List<object[]> pieDataList = new List<object[]>();
            foreach (var item in ss)
            {
                object[] pieData = new object[] { item.businessStatus, item.count };
                pieDataList.Add(pieData);
            }
            chartModel.pieDataList = pieDataList;

            DotNet.Highcharts.Highcharts chart = GetPieChart(chartModel);
            return PartialView("_PartialChartView", chart);
        }

        [Authorize(Roles = "SalesDirector,OtaSales,AreaManager,Admin")]
        public PartialViewResult GetCompanyStatusChart()
        {
            SimpleChartModel chartModel = new SimpleChartModel();
            chartModel.ChartType = DotNet.Highcharts.Enums.ChartTypes.Pie;
            chartModel.ContainerId = "companyStatusChart";
            chartModel.Title = "客户状态";

            YSeries series = new YSeries();
            series.YName = "数量";

            chartModel.ValueSuffix = "个";
            //读取公司的表，group by 销售名
            OtaCrmModel db = new OtaCrmModel();
            var ss = from i in db.OtaCompany
                     where i.BusinessStatus != null
                     group i by i.BusinessStatus
                         into g
                     select new { count = g.Count(), businessStatus = g.Key };
            List<object[]> pieDataList = new List<object[]>();
            foreach (var item in ss)
            {
                object[] pieData = new object[] { item.businessStatus, item.count };
                pieDataList.Add(pieData);
            }
            chartModel.pieDataList = pieDataList;

            DotNet.Highcharts.Highcharts chart = GetPieChart(chartModel);
            return PartialView("_PartialChartView", chart);
        }

        public DotNet.Highcharts.Highcharts GetPieChart(SimpleChartModel chartModel)
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
            tooltip.PointFormat = "{point.name}: <b>{point.y}</b>";
            chart.SetTooltip(tooltip);

            //饼图
            DotNet.Highcharts.Options.PlotOptions plotOptions = new DotNet.Highcharts.Options.PlotOptions();
            plotOptions.Pie = new DotNet.Highcharts.Options.PlotOptionsPie();
            plotOptions.Pie.AllowPointSelect = true;
            plotOptions.Pie.Cursor = DotNet.Highcharts.Enums.Cursors.Pointer;
            plotOptions.Pie.ShowInLegend = true;
            plotOptions.Pie.DataLabels = new DotNet.Highcharts.Options.PlotOptionsPieDataLabels();
            plotOptions.Pie.DataLabels.Enabled = true;
            plotOptions.Pie.DataLabels.Format = "<b>{point.name}</b>: {point.y}个";
            chart.SetPlotOptions(plotOptions);

            //设置表现值
            List<DotNet.Highcharts.Options.Series> ssList = new List<DotNet.Highcharts.Options.Series>();
            DotNet.Highcharts.Options.Series ss = new DotNet.Highcharts.Options.Series();
            ss.Type = DotNet.Highcharts.Enums.ChartTypes.Pie;
            ss.Name = chartModel.Title;
            ss.Data = new DotNet.Highcharts.Helpers.Data(chartModel.pieDataList.ToArray());
            ssList.Add(ss);
            chart.SetSeries(ssList.ToArray());

            return chart;
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