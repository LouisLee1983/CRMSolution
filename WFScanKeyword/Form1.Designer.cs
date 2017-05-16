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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.webBrowserScan = new System.Windows.Forms.WebBrowser();
            this.buttonSearchKeyword = new System.Windows.Forms.Button();
            this.tabControlMain.SuspendLayout();
            this.tabPageScanKeyword.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxKeyword
            // 
            this.textBoxKeyword.Location = new System.Drawing.Point(20, 21);
            this.textBoxKeyword.Name = "textBoxKeyword";
            this.textBoxKeyword.Size = new System.Drawing.Size(225, 25);
            this.textBoxKeyword.TabIndex = 0;
            // 
            // buttonGetKeywordArticleCount
            // 
            this.buttonGetKeywordArticleCount.Location = new System.Drawing.Point(405, 21);
            this.buttonGetKeywordArticleCount.Name = "buttonGetKeywordArticleCount";
            this.buttonGetKeywordArticleCount.Size = new System.Drawing.Size(132, 23);
            this.buttonGetKeywordArticleCount.TabIndex = 1;
            this.buttonGetKeywordArticleCount.Text = "获取文章总数";
            this.buttonGetKeywordArticleCount.UseVisualStyleBackColor = true;
            this.buttonGetKeywordArticleCount.Click += new System.EventHandler(this.buttonGetKeywordArticleCount_Click);
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(24, 601);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.Size = new System.Drawing.Size(920, 91);
            this.textBoxResult.TabIndex = 2;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageScanKeyword);
            this.tabControlMain.Controls.Add(this.tabPage2);
            this.tabControlMain.Location = new System.Drawing.Point(12, 68);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(939, 527);
            this.tabControlMain.TabIndex = 3;
            // 
            // tabPageScanKeyword
            // 
            this.tabPageScanKeyword.Controls.Add(this.webBrowserScan);
            this.tabPageScanKeyword.Location = new System.Drawing.Point(4, 25);
            this.tabPageScanKeyword.Name = "tabPageScanKeyword";
            this.tabPageScanKeyword.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScanKeyword.Size = new System.Drawing.Size(931, 498);
            this.tabPageScanKeyword.TabIndex = 0;
            this.tabPageScanKeyword.Text = "关键词扫描";
            this.tabPageScanKeyword.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(931, 498);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // webBrowserScan
            // 
            this.webBrowserScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserScan.Location = new System.Drawing.Point(3, 3);
            this.webBrowserScan.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserScan.Name = "webBrowserScan";
            this.webBrowserScan.Size = new System.Drawing.Size(925, 492);
            this.webBrowserScan.TabIndex = 0;
            // 
            // buttonSearchKeyword
            // 
            this.buttonSearchKeyword.Location = new System.Drawing.Point(260, 24);
            this.buttonSearchKeyword.Name = "buttonSearchKeyword";
            this.buttonSearchKeyword.Size = new System.Drawing.Size(75, 23);
            this.buttonSearchKeyword.TabIndex = 4;
            this.buttonSearchKeyword.Text = "搜索";
            this.buttonSearchKeyword.UseVisualStyleBackColor = true;
            this.buttonSearchKeyword.Click += new System.EventHandler(this.buttonSearchKeyword_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 704);
            this.Controls.Add(this.buttonSearchKeyword);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.buttonGetKeywordArticleCount);
            this.Controls.Add(this.textBoxKeyword);
            this.Name = "FormMain";
            this.Text = "流量网站工具";
            this.tabControlMain.ResumeLayout(false);
            this.tabPageScanKeyword.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxKeyword;
        private System.Windows.Forms.Button buttonGetKeywordArticleCount;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageScanKeyword;
        private System.Windows.Forms.WebBrowser webBrowserScan;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonSearchKeyword;
    }
}

