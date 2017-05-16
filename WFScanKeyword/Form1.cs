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
        }
    }
}
