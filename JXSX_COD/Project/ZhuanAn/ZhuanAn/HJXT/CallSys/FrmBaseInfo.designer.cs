namespace CallSys
{
    partial class FrmBaseInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaseInfo));
            this.info = new DevComponents.DotNetBar.SuperTabControl();
            this.tabPanelHandler = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.tabItemHandler = new DevComponents.DotNetBar.SuperTabItem();
            this.tabPanelFault = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.tabItemFault = new DevComponents.DotNetBar.SuperTabItem();
            this.tabPanelError = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.tabItemError = new DevComponents.DotNetBar.SuperTabItem();
            ((System.ComponentModel.ISupportInitialize)(this.info)).BeginInit();
            this.info.SuspendLayout();
            this.SuspendLayout();
            // 
            // info
            // 
            this.info.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            // 
            // 
            // 
            this.info.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.info.ControlBox.MenuBox.Name = "";
            this.info.ControlBox.Name = "";
            this.info.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.info.ControlBox.MenuBox,
            this.info.ControlBox.CloseBox});
            this.info.Controls.Add(this.tabPanelFault);
            this.info.Controls.Add(this.tabPanelHandler);
            this.info.Controls.Add(this.tabPanelError);
            this.info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.info.ForeColor = System.Drawing.Color.Black;
            this.info.Location = new System.Drawing.Point(0, 0);
            this.info.Margin = new System.Windows.Forms.Padding(4);
            this.info.Name = "info";
            this.info.ReorderTabsEnabled = true;
            this.info.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.info.SelectedTabIndex = 0;
            this.info.Size = new System.Drawing.Size(1600, 800);
            this.info.TabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.info.TabIndex = 0;
            this.info.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.tabItemHandler,
            this.tabItemError,
            this.tabItemFault});
            // 
            // tabPanelHandler
            // 
            this.tabPanelHandler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPanelHandler.Location = new System.Drawing.Point(0, 31);
            this.tabPanelHandler.Name = "tabPanelHandler";
            this.tabPanelHandler.Size = new System.Drawing.Size(1600, 769);
            this.tabPanelHandler.TabIndex = 0;
            this.tabPanelHandler.TabItem = this.tabItemHandler;
            // 
            // tabItemHandler
            // 
            this.tabItemHandler.AttachedControl = this.tabPanelHandler;
            this.tabItemHandler.GlobalItem = false;
            this.tabItemHandler.Name = "tabItemHandler";
            this.tabItemHandler.Text = "处理人信息维护";
            this.tabItemHandler.Click += new System.EventHandler(this.tabItemHandler_Click);
            // 
            // tabPanelFault
            // 
            this.tabPanelFault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPanelFault.Location = new System.Drawing.Point(0, 31);
            this.tabPanelFault.Margin = new System.Windows.Forms.Padding(0);
            this.tabPanelFault.Name = "tabPanelFault";
            this.tabPanelFault.Size = new System.Drawing.Size(1600, 769);
            this.tabPanelFault.TabIndex = 0;
            this.tabPanelFault.TabItem = this.tabItemFault;
            // 
            // tabItemFault
            // 
            this.tabItemFault.AttachedControl = this.tabPanelFault;
            this.tabItemFault.GlobalItem = false;
            this.tabItemFault.Name = "tabItemFault";
            this.tabItemFault.Text = "故障方案维护";
            this.tabItemFault.Click += new System.EventHandler(this.tabItemFault_Click);
            // 
            // tabPanelError
            // 
            this.tabPanelError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPanelError.Location = new System.Drawing.Point(0, 31);
            this.tabPanelError.Name = "tabPanelError";
            this.tabPanelError.Size = new System.Drawing.Size(1600, 769);
            this.tabPanelError.TabIndex = 0;
            this.tabPanelError.TabItem = this.tabItemError;
            // 
            // tabItemError
            // 
            this.tabItemError.AttachedControl = this.tabPanelError;
            this.tabItemError.GlobalItem = false;
            this.tabItemError.Name = "tabItemError";
            this.tabItemError.Text = "异常信息记录";
            this.tabItemError.Click += new System.EventHandler(this.tabItemError_Click);
            // 
            // FrmBaseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 800);
            this.Controls.Add(this.info);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmBaseInfo";
            this.Text = "信息维护";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.FrmBaseInfo_Activated);
            this.Load += new System.EventHandler(this.FrmBaseInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.info)).EndInit();
            this.info.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperTabControl info;
        private DevComponents.DotNetBar.SuperTabControlPanel tabPanelFault;
        private DevComponents.DotNetBar.SuperTabItem tabItemFault;
        private DevComponents.DotNetBar.SuperTabControlPanel tabPanelError;
        private DevComponents.DotNetBar.SuperTabItem tabItemError;
        private DevComponents.DotNetBar.SuperTabControlPanel tabPanelHandler;
        private DevComponents.DotNetBar.SuperTabItem tabItemHandler;
    }
}