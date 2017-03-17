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
            this.buttonGetAllPageCmsReponse = new System.Windows.Forms.Button();
            this.buttonSaveCmsData = new System.Windows.Forms.Button();
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
            this.tabControlMain.Location = new System.Drawing.Point(12, 12);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1251, 824);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageAgentGrade
            // 
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
            this.tabPageAgentGrade.Location = new System.Drawing.Point(4, 25);
            this.tabPageAgentGrade.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageAgentGrade.Name = "tabPageAgentGrade";
            this.tabPageAgentGrade.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageAgentGrade.Size = new System.Drawing.Size(1243, 795);
            this.tabPageAgentGrade.TabIndex = 0;
            this.tabPageAgentGrade.Text = "评分";
            this.tabPageAgentGrade.UseVisualStyleBackColor = true;
            // 
            // buttonGetAllAgentGradeData
            // 
            this.buttonGetAllAgentGradeData.Location = new System.Drawing.Point(764, 38);
            this.buttonGetAllAgentGradeData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGetAllAgentGradeData.Name = "buttonGetAllAgentGradeData";
            this.buttonGetAllAgentGradeData.Size = new System.Drawing.Size(75, 22);
            this.buttonGetAllAgentGradeData.TabIndex = 12;
            this.buttonGetAllAgentGradeData.Text = "获取所有";
            this.buttonGetAllAgentGradeData.UseVisualStyleBackColor = true;
            this.buttonGetAllAgentGradeData.Click += new System.EventHandler(this.buttonGetAllData_Click);
            // 
            // buttonSaveAgentGradeData
            // 
            this.buttonSaveAgentGradeData.Location = new System.Drawing.Point(864, 38);
            this.buttonSaveAgentGradeData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSaveAgentGradeData.Name = "buttonSaveAgentGradeData";
            this.buttonSaveAgentGradeData.Size = new System.Drawing.Size(75, 22);
            this.buttonSaveAgentGradeData.TabIndex = 11;
            this.buttonSaveAgentGradeData.Text = "储存";
            this.buttonSaveAgentGradeData.UseVisualStyleBackColor = true;
            this.buttonSaveAgentGradeData.Click += new System.EventHandler(this.buttonSaveData_Click);
            // 
            // buttonParseAgentGradeJson
            // 
            this.buttonParseAgentGradeJson.Location = new System.Drawing.Point(600, 38);
            this.buttonParseAgentGradeJson.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonParseAgentGradeJson.Name = "buttonParseAgentGradeJson";
            this.buttonParseAgentGradeJson.Size = new System.Drawing.Size(75, 22);
            this.buttonParseAgentGradeJson.TabIndex = 10;
            this.buttonParseAgentGradeJson.Text = "转换json";
            this.buttonParseAgentGradeJson.UseVisualStyleBackColor = true;
            this.buttonParseAgentGradeJson.Click += new System.EventHandler(this.buttonParseJson_Click);
            // 
            // buttonGetAgentGradeCookie
            // 
            this.buttonGetAgentGradeCookie.Location = new System.Drawing.Point(557, 9);
            this.buttonGetAgentGradeCookie.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGetAgentGradeCookie.Name = "buttonGetAgentGradeCookie";
            this.buttonGetAgentGradeCookie.Size = new System.Drawing.Size(75, 22);
            this.buttonGetAgentGradeCookie.TabIndex = 9;
            this.buttonGetAgentGradeCookie.Text = "获取Cookie";
            this.buttonGetAgentGradeCookie.UseVisualStyleBackColor = true;
            this.buttonGetAgentGradeCookie.Click += new System.EventHandler(this.buttonGetCookie_Click);
            // 
            // textBoxAgentGradePageIndex
            // 
            this.textBoxAgentGradePageIndex.Location = new System.Drawing.Point(412, 35);
            this.textBoxAgentGradePageIndex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxAgentGradePageIndex.Name = "textBoxAgentGradePageIndex";
            this.textBoxAgentGradePageIndex.Size = new System.Drawing.Size(100, 25);
            this.textBoxAgentGradePageIndex.TabIndex = 8;
            this.textBoxAgentGradePageIndex.Text = "1";
            // 
            // buttonGetAgentGradeOnpageResponse
            // 
            this.buttonGetAgentGradeOnpageResponse.Location = new System.Drawing.Point(517, 38);
            this.buttonGetAgentGradeOnpageResponse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGetAgentGradeOnpageResponse.Name = "buttonGetAgentGradeOnpageResponse";
            this.buttonGetAgentGradeOnpageResponse.Size = new System.Drawing.Size(75, 22);
            this.buttonGetAgentGradeOnpageResponse.TabIndex = 7;
            this.buttonGetAgentGradeOnpageResponse.Text = "获取数据";
            this.buttonGetAgentGradeOnpageResponse.UseVisualStyleBackColor = true;
            this.buttonGetAgentGradeOnpageResponse.Click += new System.EventHandler(this.buttonGetResponse_Click);
            // 
            // panelAgentGrade
            // 
            this.panelAgentGrade.Controls.Add(this.webBrowserAgentGrade);
            this.panelAgentGrade.Location = new System.Drawing.Point(11, 68);
            this.panelAgentGrade.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelAgentGrade.Name = "panelAgentGrade";
            this.panelAgentGrade.Size = new System.Drawing.Size(1227, 539);
            this.panelAgentGrade.TabIndex = 6;
            // 
            // webBrowserAgentGrade
            // 
            this.webBrowserAgentGrade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserAgentGrade.Location = new System.Drawing.Point(0, 0);
            this.webBrowserAgentGrade.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.webBrowserAgentGrade.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserAgentGrade.Name = "webBrowserAgentGrade";
            this.webBrowserAgentGrade.Size = new System.Drawing.Size(1227, 539);
            this.webBrowserAgentGrade.TabIndex = 0;
            // 
            // textBoxAgentGradeCookie
            // 
            this.textBoxAgentGradeCookie.Location = new System.Drawing.Point(11, 35);
            this.textBoxAgentGradeCookie.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxAgentGradeCookie.Name = "textBoxAgentGradeCookie";
            this.textBoxAgentGradeCookie.Size = new System.Drawing.Size(400, 25);
            this.textBoxAgentGradeCookie.TabIndex = 5;
            // 
            // textBoxAgentGradeResult
            // 
            this.textBoxAgentGradeResult.Location = new System.Drawing.Point(11, 612);
            this.textBoxAgentGradeResult.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxAgentGradeResult.Multiline = true;
            this.textBoxAgentGradeResult.Name = "textBoxAgentGradeResult";
            this.textBoxAgentGradeResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxAgentGradeResult.Size = new System.Drawing.Size(1225, 176);
            this.textBoxAgentGradeResult.TabIndex = 4;
            // 
            // buttonGoAgentGradeUrl
            // 
            this.buttonGoAgentGradeUrl.Location = new System.Drawing.Point(476, 8);
            this.buttonGoAgentGradeUrl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGoAgentGradeUrl.Name = "buttonGoAgentGradeUrl";
            this.buttonGoAgentGradeUrl.Size = new System.Drawing.Size(75, 22);
            this.buttonGoAgentGradeUrl.TabIndex = 2;
            this.buttonGoAgentGradeUrl.Text = "前往";
            this.buttonGoAgentGradeUrl.UseVisualStyleBackColor = true;
            this.buttonGoAgentGradeUrl.Click += new System.EventHandler(this.buttonGoUrl_Click);
            // 
            // textBoxAgentGradeUrl
            // 
            this.textBoxAgentGradeUrl.Location = new System.Drawing.Point(69, 8);
            this.textBoxAgentGradeUrl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxAgentGradeUrl.Name = "textBoxAgentGradeUrl";
            this.textBoxAgentGradeUrl.Size = new System.Drawing.Size(400, 25);
            this.textBoxAgentGradeUrl.TabIndex = 1;
            this.textBoxAgentGradeUrl.Text = "http://fuwu.corp.qunar.com/callcenterLogin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "URL";
            // 
            // tabPageCms
            // 
            this.tabPageCms.Controls.Add(this.buttonGetAllPageCmsReponse);
            this.tabPageCms.Controls.Add(this.buttonSaveCmsData);
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
            this.tabPageCms.Location = new System.Drawing.Point(4, 25);
            this.tabPageCms.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageCms.Name = "tabPageCms";
            this.tabPageCms.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageCms.Size = new System.Drawing.Size(1243, 795);
            this.tabPageCms.TabIndex = 1;
            this.tabPageCms.Text = "合同管理";
            this.tabPageCms.UseVisualStyleBackColor = true;
            // 
            // buttonGetAllPageCmsReponse
            // 
            this.buttonGetAllPageCmsReponse.Location = new System.Drawing.Point(763, 37);
            this.buttonGetAllPageCmsReponse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGetAllPageCmsReponse.Name = "buttonGetAllPageCmsReponse";
            this.buttonGetAllPageCmsReponse.Size = new System.Drawing.Size(75, 22);
            this.buttonGetAllPageCmsReponse.TabIndex = 24;
            this.buttonGetAllPageCmsReponse.Text = "获取所有";
            this.buttonGetAllPageCmsReponse.UseVisualStyleBackColor = true;
            this.buttonGetAllPageCmsReponse.Click += new System.EventHandler(this.buttonGetAllPageCmsReponse_Click);
            // 
            // buttonSaveCmsData
            // 
            this.buttonSaveCmsData.Location = new System.Drawing.Point(859, 37);
            this.buttonSaveCmsData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSaveCmsData.Name = "buttonSaveCmsData";
            this.buttonSaveCmsData.Size = new System.Drawing.Size(75, 22);
            this.buttonSaveCmsData.TabIndex = 23;
            this.buttonSaveCmsData.Text = "储存";
            this.buttonSaveCmsData.UseVisualStyleBackColor = true;
            this.buttonSaveCmsData.Click += new System.EventHandler(this.buttonSaveCmsData_Click);
            // 
            // buttonGenerateCmsData
            // 
            this.buttonGenerateCmsData.Location = new System.Drawing.Point(599, 37);
            this.buttonGenerateCmsData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGenerateCmsData.Name = "buttonGenerateCmsData";
            this.buttonGenerateCmsData.Size = new System.Drawing.Size(75, 22);
            this.buttonGenerateCmsData.TabIndex = 22;
            this.buttonGenerateCmsData.Text = "转换json";
            this.buttonGenerateCmsData.UseVisualStyleBackColor = true;
            this.buttonGenerateCmsData.Click += new System.EventHandler(this.buttonGenerateCmsData_Click);
            // 
            // buttonGetCmsCookies
            // 
            this.buttonGetCmsCookies.Location = new System.Drawing.Point(556, 8);
            this.buttonGetCmsCookies.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGetCmsCookies.Name = "buttonGetCmsCookies";
            this.buttonGetCmsCookies.Size = new System.Drawing.Size(75, 22);
            this.buttonGetCmsCookies.TabIndex = 21;
            this.buttonGetCmsCookies.Text = "获取Cookie";
            this.buttonGetCmsCookies.UseVisualStyleBackColor = true;
            this.buttonGetCmsCookies.Click += new System.EventHandler(this.buttonGetCmsCookies_Click);
            // 
            // textBoxCmsPageIndex
            // 
            this.textBoxCmsPageIndex.Location = new System.Drawing.Point(411, 34);
            this.textBoxCmsPageIndex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxCmsPageIndex.Name = "textBoxCmsPageIndex";
            this.textBoxCmsPageIndex.Size = new System.Drawing.Size(100, 25);
            this.textBoxCmsPageIndex.TabIndex = 20;
            this.textBoxCmsPageIndex.Text = "1";
            // 
            // buttonGetCmsOnepageData
            // 
            this.buttonGetCmsOnepageData.Location = new System.Drawing.Point(516, 37);
            this.buttonGetCmsOnepageData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGetCmsOnepageData.Name = "buttonGetCmsOnepageData";
            this.buttonGetCmsOnepageData.Size = new System.Drawing.Size(75, 22);
            this.buttonGetCmsOnepageData.TabIndex = 19;
            this.buttonGetCmsOnepageData.Text = "获取数据";
            this.buttonGetCmsOnepageData.UseVisualStyleBackColor = true;
            this.buttonGetCmsOnepageData.Click += new System.EventHandler(this.buttonGetCmsOnepageData_Click);
            // 
            // panelCms
            // 
            this.panelCms.Controls.Add(this.webBrowserCms);
            this.panelCms.Location = new System.Drawing.Point(10, 67);
            this.panelCms.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelCms.Name = "panelCms";
            this.panelCms.Size = new System.Drawing.Size(1227, 539);
            this.panelCms.TabIndex = 18;
            // 
            // webBrowserCms
            // 
            this.webBrowserCms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserCms.Location = new System.Drawing.Point(0, 0);
            this.webBrowserCms.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.webBrowserCms.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserCms.Name = "webBrowserCms";
            this.webBrowserCms.Size = new System.Drawing.Size(1227, 539);
            this.webBrowserCms.TabIndex = 0;
            // 
            // textBoxCmsCookies
            // 
            this.textBoxCmsCookies.Location = new System.Drawing.Point(10, 34);
            this.textBoxCmsCookies.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxCmsCookies.Name = "textBoxCmsCookies";
            this.textBoxCmsCookies.Size = new System.Drawing.Size(400, 25);
            this.textBoxCmsCookies.TabIndex = 17;
            // 
            // textBoxCmsResult
            // 
            this.textBoxCmsResult.Location = new System.Drawing.Point(10, 611);
            this.textBoxCmsResult.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxCmsResult.Multiline = true;
            this.textBoxCmsResult.Name = "textBoxCmsResult";
            this.textBoxCmsResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCmsResult.Size = new System.Drawing.Size(1225, 176);
            this.textBoxCmsResult.TabIndex = 16;
            // 
            // buttonGoCmsUrl
            // 
            this.buttonGoCmsUrl.Location = new System.Drawing.Point(475, 7);
            this.buttonGoCmsUrl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGoCmsUrl.Name = "buttonGoCmsUrl";
            this.buttonGoCmsUrl.Size = new System.Drawing.Size(75, 22);
            this.buttonGoCmsUrl.TabIndex = 15;
            this.buttonGoCmsUrl.Text = "前往";
            this.buttonGoCmsUrl.UseVisualStyleBackColor = true;
            this.buttonGoCmsUrl.Click += new System.EventHandler(this.buttonGoCmsUrl_Click);
            // 
            // textBoxCmsUrl
            // 
            this.textBoxCmsUrl.Location = new System.Drawing.Point(68, 7);
            this.textBoxCmsUrl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxCmsUrl.Name = "textBoxCmsUrl";
            this.textBoxCmsUrl.Size = new System.Drawing.Size(400, 25);
            this.textBoxCmsUrl.TabIndex = 14;
            this.textBoxCmsUrl.Text = "http://cms.qunar.com/cms/queryFlightCompany.do?method=listCompany&companyType=0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "URL";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1275, 848);
            this.Controls.Add(this.tabControlMain);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.Button buttonSaveCmsData;
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
    }
}

