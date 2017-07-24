namespace WFScanKeyword
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxKeyword = new System.Windows.Forms.TextBox();
            this.buttonGetKeywordArticleCount = new System.Windows.Forms.Button();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageScanKeyword = new System.Windows.Forms.TabPage();
            this.webBrowserScan = new System.Windows.Forms.WebBrowser();
            this.tabPageGedi = new System.Windows.Forms.TabPage();
            this.buttonSearchKeyword = new System.Windows.Forms.Button();
            this.webBrowserGedi = new System.Windows.Forms.WebBrowser();
            this.tabPageXXDao = new System.Windows.Forms.TabPage();
            this.tabPageYaoquna = new System.Windows.Forms.TabPage();
            this.tabPageGediIndex = new System.Windows.Forms.TabPage();
            this.tabPageXXDaoIndex = new System.Windows.Forms.TabPage();
            this.tabPageYaoqunaIndex = new System.Windows.Forms.TabPage();
            this.tabPageManager = new System.Windows.Forms.TabPage();
            this.tabPageNewPage = new System.Windows.Forms.TabPage();
            this.webBrowserXXDao = new System.Windows.Forms.WebBrowser();
            this.webBrowserYaoquna = new System.Windows.Forms.WebBrowser();
            this.webBrowserGediIndex = new System.Windows.Forms.WebBrowser();
            this.webBrowserXXdaoIndex = new System.Windows.Forms.WebBrowser();
            this.webBrowserYaoqunaIndex = new System.Windows.Forms.WebBrowser();
            this.webBrowserManager = new System.Windows.Forms.WebBrowser();
            this.webBrowserNewPage = new System.Windows.Forms.WebBrowser();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tabControlMain.SuspendLayout();
            this.tabPageScanKeyword.SuspendLayout();
            this.tabPageGedi.SuspendLayout();
            this.tabPageXXDao.SuspendLayout();
            this.tabPageYaoquna.SuspendLayout();
            this.tabPageGediIndex.SuspendLayout();
            this.tabPageXXDaoIndex.SuspendLayout();
            this.tabPageYaoqunaIndex.SuspendLayout();
            this.tabPageManager.SuspendLayout();
            this.tabPageNewPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxKeyword
            // 
            this.textBoxKeyword.Location = new System.Drawing.Point(6, 23);
            this.textBoxKeyword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxKeyword.Name = "textBoxKeyword";
            this.textBoxKeyword.Size = new System.Drawing.Size(168, 25);
            this.textBoxKeyword.TabIndex = 0;
            this.textBoxKeyword.Text = "九寨沟";
            // 
            // buttonGetKeywordArticleCount
            // 
            this.buttonGetKeywordArticleCount.Location = new System.Drawing.Point(131, 52);
            this.buttonGetKeywordArticleCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGetKeywordArticleCount.Name = "buttonGetKeywordArticleCount";
            this.buttonGetKeywordArticleCount.Size = new System.Drawing.Size(120, 22);
            this.buttonGetKeywordArticleCount.TabIndex = 1;
            this.buttonGetKeywordArticleCount.Text = "获取文章总数";
            this.buttonGetKeywordArticleCount.UseVisualStyleBackColor = true;
            this.buttonGetKeywordArticleCount.Click += new System.EventHandler(this.buttonGetKeywordArticleCount_Click);
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(5, 23);
            this.textBoxResult.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxResult.Size = new System.Drawing.Size(250, 459);
            this.textBoxResult.TabIndex = 2;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageScanKeyword);
            this.tabControlMain.Controls.Add(this.tabPageGedi);
            this.tabControlMain.Controls.Add(this.tabPageXXDao);
            this.tabControlMain.Controls.Add(this.tabPageYaoquna);
            this.tabControlMain.Controls.Add(this.tabPageGediIndex);
            this.tabControlMain.Controls.Add(this.tabPageXXDaoIndex);
            this.tabControlMain.Controls.Add(this.tabPageYaoqunaIndex);
            this.tabControlMain.Controls.Add(this.tabPageManager);
            this.tabControlMain.Controls.Add(this.tabPageNewPage);
            this.tabControlMain.Location = new System.Drawing.Point(273, 8);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(983, 851);
            this.tabControlMain.TabIndex = 3;
            // 
            // tabPageScanKeyword
            // 
            this.tabPageScanKeyword.Controls.Add(this.webBrowserScan);
            this.tabPageScanKeyword.Location = new System.Drawing.Point(4, 25);
            this.tabPageScanKeyword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageScanKeyword.Name = "tabPageScanKeyword";
            this.tabPageScanKeyword.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageScanKeyword.Size = new System.Drawing.Size(931, 499);
            this.tabPageScanKeyword.TabIndex = 0;
            this.tabPageScanKeyword.Text = "关键词扫描";
            this.tabPageScanKeyword.UseVisualStyleBackColor = true;
            // 
            // webBrowserScan
            // 
            this.webBrowserScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserScan.Location = new System.Drawing.Point(3, 2);
            this.webBrowserScan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.webBrowserScan.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserScan.Name = "webBrowserScan";
            this.webBrowserScan.Size = new System.Drawing.Size(925, 495);
            this.webBrowserScan.TabIndex = 0;
            // 
            // tabPageGedi
            // 
            this.tabPageGedi.Controls.Add(this.webBrowserGedi);
            this.tabPageGedi.Location = new System.Drawing.Point(4, 25);
            this.tabPageGedi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageGedi.Name = "tabPageGedi";
            this.tabPageGedi.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageGedi.Size = new System.Drawing.Size(931, 499);
            this.tabPageGedi.TabIndex = 1;
            this.tabPageGedi.Text = "各地后台";
            this.tabPageGedi.UseVisualStyleBackColor = true;
            // 
            // buttonSearchKeyword
            // 
            this.buttonSearchKeyword.Location = new System.Drawing.Point(180, 26);
            this.buttonSearchKeyword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSearchKeyword.Name = "buttonSearchKeyword";
            this.buttonSearchKeyword.Size = new System.Drawing.Size(75, 22);
            this.buttonSearchKeyword.TabIndex = 4;
            this.buttonSearchKeyword.Text = "搜索";
            this.buttonSearchKeyword.UseVisualStyleBackColor = true;
            this.buttonSearchKeyword.Click += new System.EventHandler(this.buttonSearchKeyword_Click);
            // 
            // webBrowserGedi
            // 
            this.webBrowserGedi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserGedi.Location = new System.Drawing.Point(3, 2);
            this.webBrowserGedi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.webBrowserGedi.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserGedi.Name = "webBrowserGedi";
            this.webBrowserGedi.Size = new System.Drawing.Size(925, 495);
            this.webBrowserGedi.TabIndex = 1;
            // 
            // tabPageXXDao
            // 
            this.tabPageXXDao.Controls.Add(this.webBrowserXXDao);
            this.tabPageXXDao.Location = new System.Drawing.Point(4, 25);
            this.tabPageXXDao.Name = "tabPageXXDao";
            this.tabPageXXDao.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageXXDao.Size = new System.Drawing.Size(931, 499);
            this.tabPageXXDao.TabIndex = 2;
            this.tabPageXXDao.Text = "学习岛后台";
            this.tabPageXXDao.UseVisualStyleBackColor = true;
            // 
            // tabPageYaoquna
            // 
            this.tabPageYaoquna.Controls.Add(this.webBrowserYaoquna);
            this.tabPageYaoquna.Location = new System.Drawing.Point(4, 25);
            this.tabPageYaoquna.Name = "tabPageYaoquna";
            this.tabPageYaoquna.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageYaoquna.Size = new System.Drawing.Size(931, 499);
            this.tabPageYaoquna.TabIndex = 3;
            this.tabPageYaoquna.Text = "要去哪后台";
            this.tabPageYaoquna.UseVisualStyleBackColor = true;
            // 
            // tabPageGediIndex
            // 
            this.tabPageGediIndex.Controls.Add(this.webBrowserGediIndex);
            this.tabPageGediIndex.Location = new System.Drawing.Point(4, 25);
            this.tabPageGediIndex.Name = "tabPageGediIndex";
            this.tabPageGediIndex.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGediIndex.Size = new System.Drawing.Size(931, 499);
            this.tabPageGediIndex.TabIndex = 4;
            this.tabPageGediIndex.Text = "各地首页";
            this.tabPageGediIndex.UseVisualStyleBackColor = true;
            // 
            // tabPageXXDaoIndex
            // 
            this.tabPageXXDaoIndex.Controls.Add(this.webBrowserXXdaoIndex);
            this.tabPageXXDaoIndex.Location = new System.Drawing.Point(4, 25);
            this.tabPageXXDaoIndex.Name = "tabPageXXDaoIndex";
            this.tabPageXXDaoIndex.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageXXDaoIndex.Size = new System.Drawing.Size(931, 499);
            this.tabPageXXDaoIndex.TabIndex = 5;
            this.tabPageXXDaoIndex.Text = "学习岛首页";
            this.tabPageXXDaoIndex.UseVisualStyleBackColor = true;
            // 
            // tabPageYaoqunaIndex
            // 
            this.tabPageYaoqunaIndex.Controls.Add(this.webBrowserYaoqunaIndex);
            this.tabPageYaoqunaIndex.Location = new System.Drawing.Point(4, 25);
            this.tabPageYaoqunaIndex.Name = "tabPageYaoqunaIndex";
            this.tabPageYaoqunaIndex.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageYaoqunaIndex.Size = new System.Drawing.Size(931, 499);
            this.tabPageYaoqunaIndex.TabIndex = 6;
            this.tabPageYaoqunaIndex.Text = "要去哪首页";
            this.tabPageYaoqunaIndex.UseVisualStyleBackColor = true;
            // 
            // tabPageManager
            // 
            this.tabPageManager.Controls.Add(this.webBrowserManager);
            this.tabPageManager.Location = new System.Drawing.Point(4, 25);
            this.tabPageManager.Name = "tabPageManager";
            this.tabPageManager.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageManager.Size = new System.Drawing.Size(931, 499);
            this.tabPageManager.TabIndex = 7;
            this.tabPageManager.Text = "流量管理后台";
            this.tabPageManager.UseVisualStyleBackColor = true;
            // 
            // tabPageNewPage
            // 
            this.tabPageNewPage.Controls.Add(this.webBrowserNewPage);
            this.tabPageNewPage.Location = new System.Drawing.Point(4, 25);
            this.tabPageNewPage.Name = "tabPageNewPage";
            this.tabPageNewPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNewPage.Size = new System.Drawing.Size(975, 822);
            this.tabPageNewPage.TabIndex = 8;
            this.tabPageNewPage.Text = "新页面";
            this.tabPageNewPage.UseVisualStyleBackColor = true;
            // 
            // webBrowserXXDao
            // 
            this.webBrowserXXDao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserXXDao.Location = new System.Drawing.Point(3, 3);
            this.webBrowserXXDao.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.webBrowserXXDao.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserXXDao.Name = "webBrowserXXDao";
            this.webBrowserXXDao.Size = new System.Drawing.Size(925, 493);
            this.webBrowserXXDao.TabIndex = 2;
            // 
            // webBrowserYaoquna
            // 
            this.webBrowserYaoquna.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserYaoquna.Location = new System.Drawing.Point(3, 3);
            this.webBrowserYaoquna.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.webBrowserYaoquna.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserYaoquna.Name = "webBrowserYaoquna";
            this.webBrowserYaoquna.Size = new System.Drawing.Size(925, 493);
            this.webBrowserYaoquna.TabIndex = 2;
            // 
            // webBrowserGediIndex
            // 
            this.webBrowserGediIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserGediIndex.Location = new System.Drawing.Point(3, 3);
            this.webBrowserGediIndex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.webBrowserGediIndex.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserGediIndex.Name = "webBrowserGediIndex";
            this.webBrowserGediIndex.Size = new System.Drawing.Size(925, 493);
            this.webBrowserGediIndex.TabIndex = 2;
            // 
            // webBrowserXXdaoIndex
            // 
            this.webBrowserXXdaoIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserXXdaoIndex.Location = new System.Drawing.Point(3, 3);
            this.webBrowserXXdaoIndex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.webBrowserXXdaoIndex.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserXXdaoIndex.Name = "webBrowserXXdaoIndex";
            this.webBrowserXXdaoIndex.Size = new System.Drawing.Size(925, 493);
            this.webBrowserXXdaoIndex.TabIndex = 2;
            // 
            // webBrowserYaoqunaIndex
            // 
            this.webBrowserYaoqunaIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserYaoqunaIndex.Location = new System.Drawing.Point(3, 3);
            this.webBrowserYaoqunaIndex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.webBrowserYaoqunaIndex.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserYaoqunaIndex.Name = "webBrowserYaoqunaIndex";
            this.webBrowserYaoqunaIndex.Size = new System.Drawing.Size(925, 493);
            this.webBrowserYaoqunaIndex.TabIndex = 2;
            // 
            // webBrowserManager
            // 
            this.webBrowserManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserManager.Location = new System.Drawing.Point(3, 3);
            this.webBrowserManager.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.webBrowserManager.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserManager.Name = "webBrowserManager";
            this.webBrowserManager.Size = new System.Drawing.Size(925, 493);
            this.webBrowserManager.TabIndex = 2;
            // 
            // webBrowserNewPage
            // 
            this.webBrowserNewPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserNewPage.Location = new System.Drawing.Point(3, 3);
            this.webBrowserNewPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.webBrowserNewPage.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserNewPage.Name = "webBrowserNewPage";
            this.webBrowserNewPage.Size = new System.Drawing.Size(969, 816);
            this.webBrowserNewPage.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.textBoxKeyword);
            this.groupBox1.Controls.Add(this.buttonSearchKeyword);
            this.groupBox1.Controls.Add(this.buttonGetKeywordArticleCount);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 351);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "控制";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxResult);
            this.groupBox2.Location = new System.Drawing.Point(5, 362);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(261, 496);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结果";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(6, 53);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 289);
            this.listBox1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(131, 96);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 22);
            this.button1.TabIndex = 6;
            this.button1.Text = "获取文章总数";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(131, 140);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 22);
            this.button2.TabIndex = 7;
            this.button2.Text = "获取文章总数";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(131, 184);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 22);
            this.button3.TabIndex = 8;
            this.button3.Text = "获取文章总数";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(131, 228);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(120, 22);
            this.button4.TabIndex = 9;
            this.button4.Text = "获取文章总数";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1267, 870);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControlMain);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormMain";
            this.Text = "流量网站工具";
            this.tabControlMain.ResumeLayout(false);
            this.tabPageScanKeyword.ResumeLayout(false);
            this.tabPageGedi.ResumeLayout(false);
            this.tabPageXXDao.ResumeLayout(false);
            this.tabPageYaoquna.ResumeLayout(false);
            this.tabPageGediIndex.ResumeLayout(false);
            this.tabPageXXDaoIndex.ResumeLayout(false);
            this.tabPageYaoqunaIndex.ResumeLayout(false);
            this.tabPageManager.ResumeLayout(false);
            this.tabPageNewPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxKeyword;
        private System.Windows.Forms.Button buttonGetKeywordArticleCount;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageScanKeyword;
        private System.Windows.Forms.WebBrowser webBrowserScan;
        private System.Windows.Forms.TabPage tabPageGedi;
        private System.Windows.Forms.Button buttonSearchKeyword;
        private System.Windows.Forms.WebBrowser webBrowserGedi;
        private System.Windows.Forms.TabPage tabPageXXDao;
        private System.Windows.Forms.TabPage tabPageYaoquna;
        private System.Windows.Forms.TabPage tabPageGediIndex;
        private System.Windows.Forms.TabPage tabPageXXDaoIndex;
        private System.Windows.Forms.TabPage tabPageYaoqunaIndex;
        private System.Windows.Forms.TabPage tabPageManager;
        private System.Windows.Forms.TabPage tabPageNewPage;
        private System.Windows.Forms.WebBrowser webBrowserXXDao;
        private System.Windows.Forms.WebBrowser webBrowserYaoquna;
        private System.Windows.Forms.WebBrowser webBrowserGediIndex;
        private System.Windows.Forms.WebBrowser webBrowserXXdaoIndex;
        private System.Windows.Forms.WebBrowser webBrowserYaoqunaIndex;
        private System.Windows.Forms.WebBrowser webBrowserManager;
        private System.Windows.Forms.WebBrowser webBrowserNewPage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}

