namespace WFSpider
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonParseJson = new System.Windows.Forms.Button();
            this.buttonGetCookie = new System.Windows.Forms.Button();
            this.textBoxPageIndex = new System.Windows.Forms.TextBox();
            this.buttonGetResponse = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.textBoxCookie = new System.Windows.Forms.TextBox();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.buttonGoUrl = new System.Windows.Forms.Button();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonSaveData = new System.Windows.Forms.Button();
            this.buttonGetAllData = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1250, 824);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonGetAllData);
            this.tabPage1.Controls.Add(this.buttonSaveData);
            this.tabPage1.Controls.Add(this.buttonParseJson);
            this.tabPage1.Controls.Add(this.buttonGetCookie);
            this.tabPage1.Controls.Add(this.textBoxPageIndex);
            this.tabPage1.Controls.Add(this.buttonGetResponse);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.textBoxCookie);
            this.tabPage1.Controls.Add(this.textBoxResult);
            this.tabPage1.Controls.Add(this.buttonGoUrl);
            this.tabPage1.Controls.Add(this.textBoxUrl);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1242, 795);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonParseJson
            // 
            this.buttonParseJson.Location = new System.Drawing.Point(600, 37);
            this.buttonParseJson.Name = "buttonParseJson";
            this.buttonParseJson.Size = new System.Drawing.Size(75, 23);
            this.buttonParseJson.TabIndex = 10;
            this.buttonParseJson.Text = "转换json";
            this.buttonParseJson.UseVisualStyleBackColor = true;
            this.buttonParseJson.Click += new System.EventHandler(this.buttonParseJson_Click);
            // 
            // buttonGetCookie
            // 
            this.buttonGetCookie.Location = new System.Drawing.Point(557, 9);
            this.buttonGetCookie.Name = "buttonGetCookie";
            this.buttonGetCookie.Size = new System.Drawing.Size(75, 23);
            this.buttonGetCookie.TabIndex = 9;
            this.buttonGetCookie.Text = "获取Cookie";
            this.buttonGetCookie.UseVisualStyleBackColor = true;
            this.buttonGetCookie.Click += new System.EventHandler(this.buttonGetCookie_Click);
            // 
            // textBoxPageIndex
            // 
            this.textBoxPageIndex.Location = new System.Drawing.Point(412, 35);
            this.textBoxPageIndex.Name = "textBoxPageIndex";
            this.textBoxPageIndex.Size = new System.Drawing.Size(100, 25);
            this.textBoxPageIndex.TabIndex = 8;
            this.textBoxPageIndex.Text = "1";
            // 
            // buttonGetResponse
            // 
            this.buttonGetResponse.Location = new System.Drawing.Point(518, 37);
            this.buttonGetResponse.Name = "buttonGetResponse";
            this.buttonGetResponse.Size = new System.Drawing.Size(75, 23);
            this.buttonGetResponse.TabIndex = 7;
            this.buttonGetResponse.Text = "获取数据";
            this.buttonGetResponse.UseVisualStyleBackColor = true;
            this.buttonGetResponse.Click += new System.EventHandler(this.buttonGetResponse_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.webBrowser1);
            this.panel1.Location = new System.Drawing.Point(10, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1226, 539);
            this.panel1.TabIndex = 6;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1226, 539);
            this.webBrowser1.TabIndex = 0;
            // 
            // textBoxCookie
            // 
            this.textBoxCookie.Location = new System.Drawing.Point(10, 35);
            this.textBoxCookie.Name = "textBoxCookie";
            this.textBoxCookie.Size = new System.Drawing.Size(400, 25);
            this.textBoxCookie.TabIndex = 5;
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(10, 612);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.Size = new System.Drawing.Size(1226, 177);
            this.textBoxResult.TabIndex = 4;
            // 
            // buttonGoUrl
            // 
            this.buttonGoUrl.Location = new System.Drawing.Point(476, 8);
            this.buttonGoUrl.Name = "buttonGoUrl";
            this.buttonGoUrl.Size = new System.Drawing.Size(75, 23);
            this.buttonGoUrl.TabIndex = 2;
            this.buttonGoUrl.Text = "前往";
            this.buttonGoUrl.UseVisualStyleBackColor = true;
            this.buttonGoUrl.Click += new System.EventHandler(this.buttonGoUrl_Click);
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Location = new System.Drawing.Point(69, 7);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(400, 25);
            this.textBoxUrl.TabIndex = 1;
            this.textBoxUrl.Text = "http://fuwu.corp.qunar.com/callcenterLogin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "URL";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1242, 795);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonSaveData
            // 
            this.buttonSaveData.Location = new System.Drawing.Point(682, 36);
            this.buttonSaveData.Name = "buttonSaveData";
            this.buttonSaveData.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveData.TabIndex = 11;
            this.buttonSaveData.Text = "储存";
            this.buttonSaveData.UseVisualStyleBackColor = true;
            this.buttonSaveData.Click += new System.EventHandler(this.buttonSaveData_Click);
            // 
            // buttonGetAllData
            // 
            this.buttonGetAllData.Location = new System.Drawing.Point(764, 37);
            this.buttonGetAllData.Name = "buttonGetAllData";
            this.buttonGetAllData.Size = new System.Drawing.Size(75, 23);
            this.buttonGetAllData.TabIndex = 12;
            this.buttonGetAllData.Text = "获取所有";
            this.buttonGetAllData.UseVisualStyleBackColor = true;
            this.buttonGetAllData.Click += new System.EventHandler(this.buttonGetAllData_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 848);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox textBoxCookie;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Button buttonGoUrl;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonGetCookie;
        private System.Windows.Forms.TextBox textBoxPageIndex;
        private System.Windows.Forms.Button buttonGetResponse;
        private System.Windows.Forms.Button buttonParseJson;
        private System.Windows.Forms.Button buttonGetAllData;
        private System.Windows.Forms.Button buttonSaveData;
    }
}

