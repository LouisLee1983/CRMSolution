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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonSearchKeyword = new System.Windows.Forms.Button();
            this.tabControlMain.SuspendLayout();
            this.tabPageScanKeyword.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxKeyword
            // 
            this.textBoxKeyword.Location = new System.Drawing.Point(15, 17);
            this.textBoxKeyword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxKeyword.Name = "textBoxKeyword";
            this.textBoxKeyword.Size = new System.Drawing.Size(170, 21);
            this.textBoxKeyword.TabIndex = 0;
            this.textBoxKeyword.Text = "九寨沟";
            // 
            // buttonGetKeywordArticleCount
            // 
            this.buttonGetKeywordArticleCount.Location = new System.Drawing.Point(304, 17);
            this.buttonGetKeywordArticleCount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonGetKeywordArticleCount.Name = "buttonGetKeywordArticleCount";
            this.buttonGetKeywordArticleCount.Size = new System.Drawing.Size(99, 18);
            this.buttonGetKeywordArticleCount.TabIndex = 1;
            this.buttonGetKeywordArticleCount.Text = "获取文章总数";
            this.buttonGetKeywordArticleCount.UseVisualStyleBackColor = true;
            this.buttonGetKeywordArticleCount.Click += new System.EventHandler(this.buttonGetKeywordArticleCount_Click);
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(18, 481);
            this.textBoxResult.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.Size = new System.Drawing.Size(691, 74);
            this.textBoxResult.TabIndex = 2;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageScanKeyword);
            this.tabControlMain.Controls.Add(this.tabPage2);
            this.tabControlMain.Location = new System.Drawing.Point(9, 54);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(704, 422);
            this.tabControlMain.TabIndex = 3;
            // 
            // tabPageScanKeyword
            // 
            this.tabPageScanKeyword.Controls.Add(this.webBrowserScan);
            this.tabPageScanKeyword.Location = new System.Drawing.Point(4, 22);
            this.tabPageScanKeyword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPageScanKeyword.Name = "tabPageScanKeyword";
            this.tabPageScanKeyword.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPageScanKeyword.Size = new System.Drawing.Size(696, 396);
            this.tabPageScanKeyword.TabIndex = 0;
            this.tabPageScanKeyword.Text = "关键词扫描";
            this.tabPageScanKeyword.UseVisualStyleBackColor = true;
            // 
            // webBrowserScan
            // 
            this.webBrowserScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserScan.Location = new System.Drawing.Point(2, 2);
            this.webBrowserScan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.webBrowserScan.MinimumSize = new System.Drawing.Size(15, 16);
            this.webBrowserScan.Name = "webBrowserScan";
            this.webBrowserScan.Size = new System.Drawing.Size(692, 392);
            this.webBrowserScan.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Size = new System.Drawing.Size(696, 396);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonSearchKeyword
            // 
            this.buttonSearchKeyword.Location = new System.Drawing.Point(195, 19);
            this.buttonSearchKeyword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSearchKeyword.Name = "buttonSearchKeyword";
            this.buttonSearchKeyword.Size = new System.Drawing.Size(56, 18);
            this.buttonSearchKeyword.TabIndex = 4;
            this.buttonSearchKeyword.Text = "搜索";
            this.buttonSearchKeyword.UseVisualStyleBackColor = true;
            this.buttonSearchKeyword.Click += new System.EventHandler(this.buttonSearchKeyword_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 563);
            this.Controls.Add(this.buttonSearchKeyword);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.buttonGetKeywordArticleCount);
            this.Controls.Add(this.textBoxKeyword);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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

