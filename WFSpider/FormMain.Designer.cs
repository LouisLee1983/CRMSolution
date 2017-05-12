namespace WFSpider
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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageAgentGrade = new System.Windows.Forms.TabPage();
            this.textBoxAgentLastDate = new System.Windows.Forms.TextBox();
            this.buttonGetAllAgentGradeData = new System.Windows.Forms.Button();
            this.buttonSaveAgentGradeData = new System.Windows.Forms.Button();
            this.buttonParseAgentGradeJson = new System.Windows.Forms.Button();
            this.buttonGetAgentGradeCookie = new System.Windows.Forms.Button();
            this.textBoxAgentGradePageIndex = new System.Windows.Forms.TextBox();
            this.buttonGetAgentGradeOnpageResponse = new System.Windows.Forms.Button();
            this.panelAgentGrade = new System.Windows.Forms.Panel();
            this.webBrowserAgentGrade = new System.Windows.Forms.WebBrowser();
            this.textBoxAgentGradeCookie = new System.Windows.Forms.TextBox();
            this.textBoxAgentGradeResult = new System.Windows.Forms.TextBox();
            this.buttonGoAgentGradeUrl = new System.Windows.Forms.Button();
            this.textBoxAgentGradeUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageCms = new System.Windows.Forms.TabPage();
            this.textBoxCmsTotalcount = new System.Windows.Forms.TextBox();
            this.buttonGenerateCmsDetail = new System.Windows.Forms.Button();
            this.buttonGetCmsDataDetail = new System.Windows.Forms.Button();
            this.textBoxCmsId = new System.Windows.Forms.TextBox();
            this.buttonGetAllPageCmsReponse = new System.Windows.Forms.Button();
            this.buttonGenerateCmsData = new System.Windows.Forms.Button();
            this.buttonGetCmsCookies = new System.Windows.Forms.Button();
            this.textBoxCmsPageIndex = new System.Windows.Forms.TextBox();
            this.buttonGetCmsOnepageData = new System.Windows.Forms.Button();
            this.panelCms = new System.Windows.Forms.Panel();
            this.webBrowserCms = new System.Windows.Forms.WebBrowser();
            this.textBoxCmsCookies = new System.Windows.Forms.TextBox();
            this.textBoxCmsResult = new System.Windows.Forms.TextBox();
            this.buttonGoCmsUrl = new System.Windows.Forms.Button();
            this.textBoxCmsUrl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControlMain.SuspendLayout();
            this.tabPageAgentGrade.SuspendLayout();
            this.panelAgentGrade.SuspendLayout();
            this.tabPageCms.SuspendLayout();
            this.panelCms.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageAgentGrade);
            this.tabControlMain.Controls.Add(this.tabPageCms);
            this.tabControlMain.Location = new System.Drawing.Point(9, 10);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(938, 659);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageAgentGrade
            // 
            this.tabPageAgentGrade.Controls.Add(this.textBoxAgentLastDate);
            this.tabPageAgentGrade.Controls.Add(this.buttonGetAllAgentGradeData);
            this.tabPageAgentGrade.Controls.Add(this.buttonSaveAgentGradeData);
            this.tabPageAgentGrade.Controls.Add(this.buttonParseAgentGradeJson);
            this.tabPageAgentGrade.Controls.Add(this.buttonGetAgentGradeCookie);
            this.tabPageAgentGrade.Controls.Add(this.textBoxAgentGradePageIndex);
            this.tabPageAgentGrade.Controls.Add(this.buttonGetAgentGradeOnpageResponse);
            this.tabPageAgentGrade.Controls.Add(this.panelAgentGrade);
            this.tabPageAgentGrade.Controls.Add(this.textBoxAgentGradeCookie);
            this.tabPageAgentGrade.Controls.Add(this.textBoxAgentGradeResult);
            this.tabPageAgentGrade.Controls.Add(this.buttonGoAgentGradeUrl);
            this.tabPageAgentGrade.Controls.Add(this.textBoxAgentGradeUrl);
            this.tabPageAgentGrade.Controls.Add(this.label1);
            this.tabPageAgentGrade.Location = new System.Drawing.Point(4, 22);
            this.tabPageAgentGrade.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageAgentGrade.Name = "tabPageAgentGrade";
            this.tabPageAgentGrade.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageAgentGrade.Size = new System.Drawing.Size(930, 633);
            this.tabPageAgentGrade.TabIndex = 0;
            this.tabPageAgentGrade.Text = "评分";
            this.tabPageAgentGrade.UseVisualStyleBackColor = true;
            // 
            // textBoxAgentLastDate
            // 
            this.textBoxAgentLastDate.Location = new System.Drawing.Point(538, 30);
            this.textBoxAgentLastDate.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAgentLastDate.Name = "textBoxAgentLastDate";
            this.textBoxAgentLastDate.Size = new System.Drawing.Size(76, 21);
            this.textBoxAgentLastDate.TabIndex = 13;
            this.textBoxAgentLastDate.Text = "2017-03-01";
            // 
            // buttonGetAllAgentGradeData
            // 
            this.buttonGetAllAgentGradeData.Enabled = false;
            this.buttonGetAllAgentGradeData.Location = new System.Drawing.Point(618, 30);
            this.buttonGetAllAgentGradeData.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGetAllAgentGradeData.Name = "buttonGetAllAgentGradeData";
            this.buttonGetAllAgentGradeData.Size = new System.Drawing.Size(82, 18);
            this.buttonGetAllAgentGradeData.TabIndex = 12;
            this.buttonGetAllAgentGradeData.Text = "3获取所有";
            this.buttonGetAllAgentGradeData.UseVisualStyleBackColor = true;
            this.buttonGetAllAgentGradeData.Click += new System.EventHandler(this.buttonGetAllData_Click);
            // 
            // buttonSaveAgentGradeData
            // 
            this.buttonSaveAgentGradeData.Enabled = false;
            this.buttonSaveAgentGradeData.Location = new System.Drawing.Point(704, 30);
            this.buttonSaveAgentGradeData.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSaveAgentGradeData.Name = "buttonSaveAgentGradeData";
            this.buttonSaveAgentGradeData.Size = new System.Drawing.Size(56, 18);
            this.buttonSaveAgentGradeData.TabIndex = 11;
            this.buttonSaveAgentGradeData.Text = "储存";
            this.buttonSaveAgentGradeData.UseVisualStyleBackColor = true;
            this.buttonSaveAgentGradeData.Click += new System.EventHandler(this.buttonSaveData_Click);
            // 
            // buttonParseAgentGradeJson
            // 
            this.buttonParseAgentGradeJson.Enabled = false;
            this.buttonParseAgentGradeJson.Location = new System.Drawing.Point(478, 30);
            this.buttonParseAgentGradeJson.Margin = new System.Windows.Forms.Padding(2);
            this.buttonParseAgentGradeJson.Name = "buttonParseAgentGradeJson";
            this.buttonParseAgentGradeJson.Size = new System.Drawing.Size(56, 18);
            this.buttonParseAgentGradeJson.TabIndex = 10;
            this.buttonParseAgentGradeJson.Text = "转换json";
            this.buttonParseAgentGradeJson.UseVisualStyleBackColor = true;
            this.buttonParseAgentGradeJson.Click += new System.EventHandler(this.buttonParseJson_Click);
            // 
            // buttonGetAgentGradeCookie
            // 
            this.buttonGetAgentGradeCookie.Enabled = false;
            this.buttonGetAgentGradeCookie.Location = new System.Drawing.Point(418, 7);
            this.buttonGetAgentGradeCookie.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGetAgentGradeCookie.Name = "buttonGetAgentGradeCookie";
            this.buttonGetAgentGradeCookie.Size = new System.Drawing.Size(93, 18);
            this.buttonGetAgentGradeCookie.TabIndex = 9;
            this.buttonGetAgentGradeCookie.Text = "2获取Cookie";
            this.buttonGetAgentGradeCookie.UseVisualStyleBackColor = true;
            this.buttonGetAgentGradeCookie.Click += new System.EventHandler(this.buttonGetCookie_Click);
            // 
            // textBoxAgentGradePageIndex
            // 
            this.textBoxAgentGradePageIndex.Location = new System.Drawing.Point(309, 28);
            this.textBoxAgentGradePageIndex.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAgentGradePageIndex.Name = "textBoxAgentGradePageIndex";
            this.textBoxAgentGradePageIndex.Size = new System.Drawing.Size(76, 21);
            this.textBoxAgentGradePageIndex.TabIndex = 8;
            this.textBoxAgentGradePageIndex.Text = "1";
            // 
            // buttonGetAgentGradeOnpageResponse
            // 
            this.buttonGetAgentGradeOnpageResponse.Enabled = false;
            this.buttonGetAgentGradeOnpageResponse.Location = new System.Drawing.Point(388, 30);
            this.buttonGetAgentGradeOnpageResponse.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGetAgentGradeOnpageResponse.Name = "buttonGetAgentGradeOnpageResponse";
            this.buttonGetAgentGradeOnpageResponse.Size = new System.Drawing.Size(86, 18);
            this.buttonGetAgentGradeOnpageResponse.TabIndex = 7;
            this.buttonGetAgentGradeOnpageResponse.Text = "获取数据";
            this.buttonGetAgentGradeOnpageResponse.UseVisualStyleBackColor = true;
            this.buttonGetAgentGradeOnpageResponse.Click += new System.EventHandler(this.buttonGetResponse_Click);
            // 
            // panelAgentGrade
            // 
            this.panelAgentGrade.Controls.Add(this.webBrowserAgentGrade);
            this.panelAgentGrade.Location = new System.Drawing.Point(8, 54);
            this.panelAgentGrade.Margin = new System.Windows.Forms.Padding(2);
            this.panelAgentGrade.Name = "panelAgentGrade";
            this.panelAgentGrade.Size = new System.Drawing.Size(920, 431);
            this.panelAgentGrade.TabIndex = 6;
            // 
            // webBrowserAgentGrade
            // 
            this.webBrowserAgentGrade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserAgentGrade.Location = new System.Drawing.Point(0, 0);
            this.webBrowserAgentGrade.Margin = new System.Windows.Forms.Padding(2);
            this.webBrowserAgentGrade.MinimumSize = new System.Drawing.Size(15, 16);
            this.webBrowserAgentGrade.Name = "webBrowserAgentGrade";
            this.webBrowserAgentGrade.Size = new System.Drawing.Size(920, 431);
            this.webBrowserAgentGrade.TabIndex = 0;
            // 
            // textBoxAgentGradeCookie
            // 
            this.textBoxAgentGradeCookie.Location = new System.Drawing.Point(8, 28);
            this.textBoxAgentGradeCookie.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAgentGradeCookie.Name = "textBoxAgentGradeCookie";
            this.textBoxAgentGradeCookie.Size = new System.Drawing.Size(301, 21);
            this.textBoxAgentGradeCookie.TabIndex = 5;
            // 
            // textBoxAgentGradeResult
            // 
            this.textBoxAgentGradeResult.Location = new System.Drawing.Point(8, 490);
            this.textBoxAgentGradeResult.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAgentGradeResult.Multiline = true;
            this.textBoxAgentGradeResult.Name = "textBoxAgentGradeResult";
            this.textBoxAgentGradeResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxAgentGradeResult.Size = new System.Drawing.Size(920, 142);
            this.textBoxAgentGradeResult.TabIndex = 4;
            // 
            // buttonGoAgentGradeUrl
            // 
            this.buttonGoAgentGradeUrl.Location = new System.Drawing.Point(357, 6);
            this.buttonGoAgentGradeUrl.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGoAgentGradeUrl.Name = "buttonGoAgentGradeUrl";
            this.buttonGoAgentGradeUrl.Size = new System.Drawing.Size(56, 18);
            this.buttonGoAgentGradeUrl.TabIndex = 2;
            this.buttonGoAgentGradeUrl.Text = "1前往";
            this.buttonGoAgentGradeUrl.UseVisualStyleBackColor = true;
            this.buttonGoAgentGradeUrl.Click += new System.EventHandler(this.buttonGoUrl_Click);
            // 
            // textBoxAgentGradeUrl
            // 
            this.textBoxAgentGradeUrl.Location = new System.Drawing.Point(52, 6);
            this.textBoxAgentGradeUrl.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAgentGradeUrl.Name = "textBoxAgentGradeUrl";
            this.textBoxAgentGradeUrl.Size = new System.Drawing.Size(301, 21);
            this.textBoxAgentGradeUrl.TabIndex = 1;
            this.textBoxAgentGradeUrl.Text = "http://fuwu.corp.qunar.com/callcenterLogin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "URL";
            // 
            // tabPageCms
            // 
            this.tabPageCms.Controls.Add(this.textBoxCmsTotalcount);
            this.tabPageCms.Controls.Add(this.buttonGenerateCmsDetail);
            this.tabPageCms.Controls.Add(this.buttonGetCmsDataDetail);
            this.tabPageCms.Controls.Add(this.textBoxCmsId);
            this.tabPageCms.Controls.Add(this.buttonGetAllPageCmsReponse);
            this.tabPageCms.Controls.Add(this.buttonGenerateCmsData);
            this.tabPageCms.Controls.Add(this.buttonGetCmsCookies);
            this.tabPageCms.Controls.Add(this.textBoxCmsPageIndex);
            this.tabPageCms.Controls.Add(this.buttonGetCmsOnepageData);
            this.tabPageCms.Controls.Add(this.panelCms);
            this.tabPageCms.Controls.Add(this.textBoxCmsCookies);
            this.tabPageCms.Controls.Add(this.textBoxCmsResult);
            this.tabPageCms.Controls.Add(this.buttonGoCmsUrl);
            this.tabPageCms.Controls.Add(this.textBoxCmsUrl);
            this.tabPageCms.Controls.Add(this.label2);
            this.tabPageCms.Location = new System.Drawing.Point(4, 22);
            this.tabPageCms.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageCms.Name = "tabPageCms";
            this.tabPageCms.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageCms.Size = new System.Drawing.Size(930, 633);
            this.tabPageCms.TabIndex = 1;
            this.tabPageCms.Text = "合同管理";
            this.tabPageCms.UseVisualStyleBackColor = true;
            // 
            // textBoxCmsTotalcount
            // 
            this.textBoxCmsTotalcount.Location = new System.Drawing.Point(622, 26);
            this.textBoxCmsTotalcount.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCmsTotalcount.Name = "textBoxCmsTotalcount";
            this.textBoxCmsTotalcount.Size = new System.Drawing.Size(47, 21);
            this.textBoxCmsTotalcount.TabIndex = 28;
            this.textBoxCmsTotalcount.Text = "1";
            // 
            // buttonGenerateCmsDetail
            // 
            this.buttonGenerateCmsDetail.Location = new System.Drawing.Point(823, 26);
            this.buttonGenerateCmsDetail.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGenerateCmsDetail.Name = "buttonGenerateCmsDetail";
            this.buttonGenerateCmsDetail.Size = new System.Drawing.Size(91, 18);
            this.buttonGenerateCmsDetail.TabIndex = 27;
            this.buttonGenerateCmsDetail.Text = "转换详情";
            this.buttonGenerateCmsDetail.UseVisualStyleBackColor = true;
            this.buttonGenerateCmsDetail.Click += new System.EventHandler(this.buttonGenerateCmsDetail_Click);
            // 
            // buttonGetCmsDataDetail
            // 
            this.buttonGetCmsDataDetail.Location = new System.Drawing.Point(741, 27);
            this.buttonGetCmsDataDetail.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGetCmsDataDetail.Name = "buttonGetCmsDataDetail";
            this.buttonGetCmsDataDetail.Size = new System.Drawing.Size(72, 18);
            this.buttonGetCmsDataDetail.TabIndex = 26;
            this.buttonGetCmsDataDetail.Text = "获取详情";
            this.buttonGetCmsDataDetail.UseVisualStyleBackColor = true;
            this.buttonGetCmsDataDetail.Click += new System.EventHandler(this.buttonGetCmsDataDetail_Click);
            // 
            // textBoxCmsId
            // 
            this.textBoxCmsId.Location = new System.Drawing.Point(688, 26);
            this.textBoxCmsId.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCmsId.Name = "textBoxCmsId";
            this.textBoxCmsId.Size = new System.Drawing.Size(47, 21);
            this.textBoxCmsId.TabIndex = 25;
            this.textBoxCmsId.Text = "1";
            // 
            // buttonGetAllPageCmsReponse
            // 
            this.buttonGetAllPageCmsReponse.Location = new System.Drawing.Point(681, 6);
            this.buttonGetAllPageCmsReponse.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGetAllPageCmsReponse.Name = "buttonGetAllPageCmsReponse";
            this.buttonGetAllPageCmsReponse.Size = new System.Drawing.Size(66, 18);
            this.buttonGetAllPageCmsReponse.TabIndex = 24;
            this.buttonGetAllPageCmsReponse.Text = "获取所有";
            this.buttonGetAllPageCmsReponse.UseVisualStyleBackColor = true;
            this.buttonGetAllPageCmsReponse.Click += new System.EventHandler(this.buttonGetAllPageCmsReponse_Click);
            // 
            // buttonGenerateCmsData
            // 
            this.buttonGenerateCmsData.Location = new System.Drawing.Point(499, 27);
            this.buttonGenerateCmsData.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGenerateCmsData.Name = "buttonGenerateCmsData";
            this.buttonGenerateCmsData.Size = new System.Drawing.Size(91, 18);
            this.buttonGenerateCmsData.TabIndex = 22;
            this.buttonGenerateCmsData.Text = "转换json";
            this.buttonGenerateCmsData.UseVisualStyleBackColor = true;
            this.buttonGenerateCmsData.Click += new System.EventHandler(this.buttonGenerateCmsData_Click);
            // 
            // buttonGetCmsCookies
            // 
            this.buttonGetCmsCookies.Location = new System.Drawing.Point(417, 6);
            this.buttonGetCmsCookies.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGetCmsCookies.Name = "buttonGetCmsCookies";
            this.buttonGetCmsCookies.Size = new System.Drawing.Size(56, 18);
            this.buttonGetCmsCookies.TabIndex = 21;
            this.buttonGetCmsCookies.Text = "获取Cookie";
            this.buttonGetCmsCookies.UseVisualStyleBackColor = true;
            this.buttonGetCmsCookies.Click += new System.EventHandler(this.buttonGetCmsCookies_Click);
            // 
            // textBoxCmsPageIndex
            // 
            this.textBoxCmsPageIndex.Location = new System.Drawing.Point(308, 27);
            this.textBoxCmsPageIndex.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCmsPageIndex.Name = "textBoxCmsPageIndex";
            this.textBoxCmsPageIndex.Size = new System.Drawing.Size(76, 21);
            this.textBoxCmsPageIndex.TabIndex = 20;
            this.textBoxCmsPageIndex.Text = "1";
            // 
            // buttonGetCmsOnepageData
            // 
            this.buttonGetCmsOnepageData.Location = new System.Drawing.Point(388, 29);
            this.buttonGetCmsOnepageData.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGetCmsOnepageData.Name = "buttonGetCmsOnepageData";
            this.buttonGetCmsOnepageData.Size = new System.Drawing.Size(85, 18);
            this.buttonGetCmsOnepageData.TabIndex = 19;
            this.buttonGetCmsOnepageData.Text = "获取首次数据";
            this.buttonGetCmsOnepageData.UseVisualStyleBackColor = true;
            this.buttonGetCmsOnepageData.Click += new System.EventHandler(this.buttonGetCmsOnepageData_Click);
            // 
            // panelCms
            // 
            this.panelCms.Controls.Add(this.webBrowserCms);
            this.panelCms.Location = new System.Drawing.Point(8, 54);
            this.panelCms.Margin = new System.Windows.Forms.Padding(2);
            this.panelCms.Name = "panelCms";
            this.panelCms.Size = new System.Drawing.Size(920, 431);
            this.panelCms.TabIndex = 18;
            // 
            // webBrowserCms
            // 
            this.webBrowserCms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserCms.Location = new System.Drawing.Point(0, 0);
            this.webBrowserCms.Margin = new System.Windows.Forms.Padding(2);
            this.webBrowserCms.MinimumSize = new System.Drawing.Size(15, 16);
            this.webBrowserCms.Name = "webBrowserCms";
            this.webBrowserCms.Size = new System.Drawing.Size(920, 431);
            this.webBrowserCms.TabIndex = 0;
            // 
            // textBoxCmsCookies
            // 
            this.textBoxCmsCookies.Location = new System.Drawing.Point(8, 27);
            this.textBoxCmsCookies.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCmsCookies.Name = "textBoxCmsCookies";
            this.textBoxCmsCookies.Size = new System.Drawing.Size(301, 21);
            this.textBoxCmsCookies.TabIndex = 17;
            // 
            // textBoxCmsResult
            // 
            this.textBoxCmsResult.Location = new System.Drawing.Point(8, 489);
            this.textBoxCmsResult.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCmsResult.Multiline = true;
            this.textBoxCmsResult.Name = "textBoxCmsResult";
            this.textBoxCmsResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCmsResult.Size = new System.Drawing.Size(920, 142);
            this.textBoxCmsResult.TabIndex = 16;
            // 
            // buttonGoCmsUrl
            // 
            this.buttonGoCmsUrl.Location = new System.Drawing.Point(356, 6);
            this.buttonGoCmsUrl.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGoCmsUrl.Name = "buttonGoCmsUrl";
            this.buttonGoCmsUrl.Size = new System.Drawing.Size(56, 18);
            this.buttonGoCmsUrl.TabIndex = 15;
            this.buttonGoCmsUrl.Text = "前往";
            this.buttonGoCmsUrl.UseVisualStyleBackColor = true;
            this.buttonGoCmsUrl.Click += new System.EventHandler(this.buttonGoCmsUrl_Click);
            // 
            // textBoxCmsUrl
            // 
            this.textBoxCmsUrl.Location = new System.Drawing.Point(51, 6);
            this.textBoxCmsUrl.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCmsUrl.Name = "textBoxCmsUrl";
            this.textBoxCmsUrl.Size = new System.Drawing.Size(301, 21);
            this.textBoxCmsUrl.TabIndex = 14;
            this.textBoxCmsUrl.Text = "http://cms.qunar.com/cms/login.do";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "URL";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 678);
            this.Controls.Add(this.tabControlMain);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormMain";
            this.Text = "客户信息抓取";
            this.tabControlMain.ResumeLayout(false);
            this.tabPageAgentGrade.ResumeLayout(false);
            this.tabPageAgentGrade.PerformLayout();
            this.panelAgentGrade.ResumeLayout(false);
            this.tabPageCms.ResumeLayout(false);
            this.tabPageCms.PerformLayout();
            this.panelCms.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageAgentGrade;
        private System.Windows.Forms.Panel panelAgentGrade;
        private System.Windows.Forms.WebBrowser webBrowserAgentGrade;
        private System.Windows.Forms.TextBox textBoxAgentGradeCookie;
        private System.Windows.Forms.TextBox textBoxAgentGradeResult;
        private System.Windows.Forms.Button buttonGoAgentGradeUrl;
        private System.Windows.Forms.TextBox textBoxAgentGradeUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageCms;
        private System.Windows.Forms.Button buttonGetAgentGradeCookie;
        private System.Windows.Forms.TextBox textBoxAgentGradePageIndex;
        private System.Windows.Forms.Button buttonGetAgentGradeOnpageResponse;
        private System.Windows.Forms.Button buttonParseAgentGradeJson;
        private System.Windows.Forms.Button buttonGetAllAgentGradeData;
        private System.Windows.Forms.Button buttonSaveAgentGradeData;
        private System.Windows.Forms.Button buttonGetAllPageCmsReponse;
        private System.Windows.Forms.Button buttonGenerateCmsData;
        private System.Windows.Forms.Button buttonGetCmsCookies;
        private System.Windows.Forms.TextBox textBoxCmsPageIndex;
        private System.Windows.Forms.Button buttonGetCmsOnepageData;
        private System.Windows.Forms.Panel panelCms;
        private System.Windows.Forms.WebBrowser webBrowserCms;
        private System.Windows.Forms.TextBox textBoxCmsCookies;
        private System.Windows.Forms.TextBox textBoxCmsResult;
        private System.Windows.Forms.Button buttonGoCmsUrl;
        private System.Windows.Forms.TextBox textBoxCmsUrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonGetCmsDataDetail;
        private System.Windows.Forms.TextBox textBoxCmsId;
        private System.Windows.Forms.Button buttonGenerateCmsDetail;
        private System.Windows.Forms.TextBox textBoxCmsTotalcount;
        private System.Windows.Forms.TextBox textBoxAgentLastDate;
    }
}

