using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrmWebApp.Models
{
    public class SimpleChartModel
    {
        public string ContainerId { get; set; }
        //饼图pia，线图line，区域图area
        public DotNet.Highcharts.Enums.ChartTypes ChartType { get; set; }
        //宽度
        public int Width { get; set; }
        //高度
        public int Height { get; set; }
        //图表标题
        public string Title { get; set; }
        //图表子标题
        public string SubTitle { get; set; }
        //度量单位
        public string ValueSuffix { get; set; }
        //横坐标
        public List<string> XList { get; set; }
        //横坐标标题
        public string XTitle { get; set; }
        //纵坐标标题
        public string YTitle { get; set; }
        
        public List<YSeries> SeriesList { get; set; }

        public List<object[]> pieDataList { get; set; }
        
        public SimpleChartModel()
        {
            this.ChartType = DotNet.Highcharts.Enums.ChartTypes.Bar;
            this.Width = 400;
            this.Height = 400;
            this.Title = "";
            this.SubTitle = "";
            this.ValueSuffix = "";
            this.XList = new List<string>();
            this.XTitle = "";
            this.YTitle = "";
            this.SeriesList = new List<YSeries>();
            this.pieDataList = new List<object[]>();
        }
    }

    public class YSeries
    {
        //纵坐标名称
        public string YName { get; set; }
        //纵坐标值
        public List<object> YSeriesList { get; set; }
    }
}