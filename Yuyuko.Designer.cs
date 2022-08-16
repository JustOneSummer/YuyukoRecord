namespace YuyukoRecord
{
    partial class Yuyuko
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Yuyuko));
            this.label1 = new System.Windows.Forms.Label();
            this.labelServer = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelGameTime = new System.Windows.Forms.Label();
            this.labelDataStatus = new System.Windows.Forms.Label();
            this.dataGridViewOne = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.SheZhiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetGamePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateSorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ColorTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReplayPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReLoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExeLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelMyOne = new System.Windows.Forms.Label();
            this.labelMyTwo = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOne)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前服务器：";
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelServer.ForeColor = System.Drawing.SystemColors.Highlight;
            this.labelServer.Location = new System.Drawing.Point(125, 37);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(44, 22);
            this.labelServer.TabIndex = 1;
            this.labelServer.Text = "N/A";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(1209, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "开始时间：";
            // 
            // labelGameTime
            // 
            this.labelGameTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGameTime.AutoSize = true;
            this.labelGameTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelGameTime.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.labelGameTime.Location = new System.Drawing.Point(1305, 37);
            this.labelGameTime.Name = "labelGameTime";
            this.labelGameTime.Size = new System.Drawing.Size(163, 21);
            this.labelGameTime.TabIndex = 3;
            this.labelGameTime.Text = "2020-08-13 14:23:59";
            // 
            // labelDataStatus
            // 
            this.labelDataStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDataStatus.AutoSize = true;
            this.labelDataStatus.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDataStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelDataStatus.Location = new System.Drawing.Point(1085, 37);
            this.labelDataStatus.Name = "labelDataStatus";
            this.labelDataStatus.Size = new System.Drawing.Size(118, 21);
            this.labelDataStatus.TabIndex = 4;
            this.labelDataStatus.Text = "等待开始游戏...";
            // 
            // dataGridViewOne
            // 
            this.dataGridViewOne.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewOne.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridViewOne.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewOne.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewOne.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOne.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridViewOne.Location = new System.Drawing.Point(12, 81);
            this.dataGridViewOne.Name = "dataGridViewOne";
            this.dataGridViewOne.ReadOnly = true;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewOne.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewOne.RowTemplate.Height = 23;
            this.dataGridViewOne.Size = new System.Drawing.Size(1460, 878);
            this.dataGridViewOne.TabIndex = 5;
            this.dataGridViewOne.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewOne_CellMouseDown);
            this.dataGridViewOne.SelectionChanged += new System.EventHandler(this.dataGridViewOne_SelectionChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyToolStripMenuItem,
            this.OpenViewToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.CopyToolStripMenuItem.Text = "复制战绩";
            this.CopyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // OpenViewToolStripMenuItem
            // 
            this.OpenViewToolStripMenuItem.Name = "OpenViewToolStripMenuItem";
            this.OpenViewToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.OpenViewToolStripMenuItem.Text = "查看战绩";
            this.OpenViewToolStripMenuItem.Click += new System.EventHandler(this.OpenViewToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SheZhiToolStripMenuItem,
            this.ReplayPathToolStripMenuItem,
            this.ReLoadToolStripMenuItem,
            this.DefaultToolStripMenuItem,
            this.InfoToolStripMenuItem,
            this.ExeLogToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1484, 27);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // SheZhiToolStripMenuItem
            // 
            this.SheZhiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetGamePathToolStripMenuItem,
            this.UpdateSorToolStripMenuItem,
            this.ColorTemplateToolStripMenuItem,
            this.ImageToolStripMenuItem});
            this.SheZhiToolStripMenuItem.Name = "SheZhiToolStripMenuItem";
            this.SheZhiToolStripMenuItem.Size = new System.Drawing.Size(47, 23);
            this.SheZhiToolStripMenuItem.Text = "设置";
            // 
            // SetGamePathToolStripMenuItem
            // 
            this.SetGamePathToolStripMenuItem.Name = "SetGamePathToolStripMenuItem";
            this.SetGamePathToolStripMenuItem.Size = new System.Drawing.Size(156, 24);
            this.SetGamePathToolStripMenuItem.Text = "设置游戏路径";
            this.SetGamePathToolStripMenuItem.Click += new System.EventHandler(this.SetGamePathToolStripMenuItem_Click);
            // 
            // UpdateSorToolStripMenuItem
            // 
            this.UpdateSorToolStripMenuItem.Name = "UpdateSorToolStripMenuItem";
            this.UpdateSorToolStripMenuItem.Size = new System.Drawing.Size(156, 24);
            this.UpdateSorToolStripMenuItem.Text = "更新资源文件";
            this.UpdateSorToolStripMenuItem.Click += new System.EventHandler(this.UpdateSorToolStripMenuItem_Click);
            // 
            // ColorTemplateToolStripMenuItem
            // 
            this.ColorTemplateToolStripMenuItem.Name = "ColorTemplateToolStripMenuItem";
            this.ColorTemplateToolStripMenuItem.Size = new System.Drawing.Size(156, 24);
            this.ColorTemplateToolStripMenuItem.Text = "颜色模板";
            // 
            // ImageToolStripMenuItem
            // 
            this.ImageToolStripMenuItem.Name = "ImageToolStripMenuItem";
            this.ImageToolStripMenuItem.Size = new System.Drawing.Size(156, 24);
            this.ImageToolStripMenuItem.Text = "自定义图片";
            this.ImageToolStripMenuItem.Click += new System.EventHandler(this.ImageToolStripMenuItem_Click);
            // 
            // ReplayPathToolStripMenuItem
            // 
            this.ReplayPathToolStripMenuItem.Name = "ReplayPathToolStripMenuItem";
            this.ReplayPathToolStripMenuItem.Size = new System.Drawing.Size(123, 23);
            this.ReplayPathToolStripMenuItem.Text = "打开replay文件夹";
            this.ReplayPathToolStripMenuItem.Click += new System.EventHandler(this.ReplayPathToolStripMenuItem_Click);
            // 
            // ReLoadToolStripMenuItem
            // 
            this.ReLoadToolStripMenuItem.Name = "ReLoadToolStripMenuItem";
            this.ReLoadToolStripMenuItem.Size = new System.Drawing.Size(73, 23);
            this.ReLoadToolStripMenuItem.Text = "重新加载";
            this.ReLoadToolStripMenuItem.Click += new System.EventHandler(this.ReLoadToolStripMenuItem_Click);
            // 
            // DefaultToolStripMenuItem
            // 
            this.DefaultToolStripMenuItem.Name = "DefaultToolStripMenuItem";
            this.DefaultToolStripMenuItem.Size = new System.Drawing.Size(99, 23);
            this.DefaultToolStripMenuItem.Text = "恢复窗口大小";
            this.DefaultToolStripMenuItem.Click += new System.EventHandler(this.DefaultToolStripMenuItem_Click);
            // 
            // InfoToolStripMenuItem
            // 
            this.InfoToolStripMenuItem.Name = "InfoToolStripMenuItem";
            this.InfoToolStripMenuItem.Size = new System.Drawing.Size(47, 23);
            this.InfoToolStripMenuItem.Text = "关于";
            this.InfoToolStripMenuItem.Click += new System.EventHandler(this.InfoToolStripMenuItem_Click);
            // 
            // ExeLogToolStripMenuItem
            // 
            this.ExeLogToolStripMenuItem.Name = "ExeLogToolStripMenuItem";
            this.ExeLogToolStripMenuItem.Size = new System.Drawing.Size(73, 23);
            this.ExeLogToolStripMenuItem.Text = "程序日志";
            this.ExeLogToolStripMenuItem.Click += new System.EventHandler(this.ExeLogToolStripMenuItem_Click);
            // 
            // labelMyOne
            // 
            this.labelMyOne.AutoSize = true;
            this.labelMyOne.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMyOne.ForeColor = System.Drawing.Color.Green;
            this.labelMyOne.Location = new System.Drawing.Point(475, 53);
            this.labelMyOne.Name = "labelMyOne";
            this.labelMyOne.Size = new System.Drawing.Size(69, 25);
            this.labelMyOne.TabIndex = 7;
            this.labelMyOne.Text = "我方：";
            this.labelMyOne.DoubleClick += new System.EventHandler(this.labelMyOne_DoubleClick);
            // 
            // labelMyTwo
            // 
            this.labelMyTwo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMyTwo.AutoSize = true;
            this.labelMyTwo.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMyTwo.ForeColor = System.Drawing.Color.Firebrick;
            this.labelMyTwo.Location = new System.Drawing.Point(787, 53);
            this.labelMyTwo.Name = "labelMyTwo";
            this.labelMyTwo.Size = new System.Drawing.Size(69, 25);
            this.labelMyTwo.TabIndex = 8;
            this.labelMyTwo.Text = "敌方：";
            this.labelMyTwo.DoubleClick += new System.EventHandler(this.labelMyTwo_DoubleClick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Yuyuko
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 971);
            this.Controls.Add(this.labelMyTwo);
            this.Controls.Add(this.labelMyOne);
            this.Controls.Add(this.dataGridViewOne);
            this.Controls.Add(this.labelDataStatus);
            this.Controls.Add(this.labelGameTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelServer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Yuyuko";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "yuyuko";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Yuyuko_FormClosed);
            this.Load += new System.EventHandler(this.Yuyuko_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOne)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelGameTime;
        private System.Windows.Forms.Label labelDataStatus;
        private System.Windows.Forms.DataGridView dataGridViewOne;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem SheZhiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReplayPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReLoadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetGamePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExeLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DefaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UpdateSorToolStripMenuItem;
        private System.Windows.Forms.Label labelMyOne;
        private System.Windows.Forms.Label labelMyTwo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ColorTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImageToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
    }
}

