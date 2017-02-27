using CrmWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrmWebApp.Controllers
{
    public class ChartsController : Controller
    {
        // GET: Charts
        [Authorize(Roles = "SalesDirector,OtaSales")]
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult GetMeetCountChart()
        {
            SimpleChartModel chartModel = new SimpleChartModel();
            chartModel.ContainerId = "meetChart";
            chartModel.Title = "沟通频率";
            chartModel.YName = "次数";
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
                chartModel.YSeriesList.Add(item.count);
            }
            DotNet.Highcharts.Highcharts chart = GetChart(chartModel);
            return PartialView("_PartialChartView", chart);
        }

        public PartialViewResult GetCompanyCountChart()
        {
            SimpleChartModel chartModel = new SimpleChartModel();
            chartModel.ContainerId = "companyCountChart";
            chartModel.Title = "客户数量";
            chartModel.YName = "数量";
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
                chartModel.YSeriesList.Add(item.count);
            }
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
            DotNet.Highcharts.Options.Series ss = new DotNet.Highcharts.Options.Series();
            ss.Data = new DotNet.Highcharts.Helpers.Data(chartModel.YSeriesList.ToArray());
            ss.Name = chartModel.YName;
            chart.SetSeries(ss);

            return chart;
        }
    }
}