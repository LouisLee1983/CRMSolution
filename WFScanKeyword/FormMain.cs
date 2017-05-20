using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFScanKeyword
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonGetKeywordArticleCount_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonSearchKeyword_Click(object sender, EventArgs e)
        {
            string keyword = textBoxKeyword.Text.Trim();
            string url = "https://www.baidu.com/s?wd="+keyword;
            webBrowserScan.Navigate(url);

            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);
            HtmlAgilityPack.HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@id=\"container\"]/div[2]/div[1]/div[2]");
            string searchResult = node.InnerText;
            textBoxResult.AppendText(searchResult);
            string searchResultCountStr  = searchResult.Replace("百度为您找到相关结果约", "").Replace("个","").Replace("搜索工具", "").Replace(",","");
            int searchResultCount = int.Parse(searchResultCountStr);
            textBoxResult.AppendText("\r\n" + searchResultCount);
        }
    }
}
