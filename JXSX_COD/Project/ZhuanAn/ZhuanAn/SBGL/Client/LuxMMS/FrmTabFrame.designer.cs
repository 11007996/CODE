namespace LuxMMS
{
    partial class FrmTabFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTabFrame));
            this.info = new DevComponents.DotNetBar.SuperTabControl();
            this.tabPanelError = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.tabItemError = new DevComponents.DotNetBar.SuperTabItem();
            this.tabPanelBasic = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.tabItemBasic = new DevComponents.DotNetBar.SuperTabItem();
            this.tabPanelMaintenance = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.tabItemMaintenance = new DevComponents.DotNetBar.SuperTabItem();
            this.tabPanelAsset = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.tabItemAsset = new DevComponents.DotNetBar.SuperTabItem();
            this.tabPanelMachine = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.tabItemMachine = new DevComponents.DotNetBar.SuperTabItem();
            this.tabPanelUser = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.tabItemUser = new DevComponents.DotNetBar.SuperTabItem();
            this.tabPanelFault = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.tabItemFault = new DevComponents.DotNetBar.SuperTabItem();
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
            this.info.Controls.Add(this.tabPanelError);
            this.info.Controls.Add(this.tabPanelBasic);
            this.info.Controls.Add(this.tabPanelMaintenance);
            this.info.Controls.Add(this.tabPanelAsset);
            this.info.Controls.Add(this.tabPanelMachine);
            this.info.Controls.Add(this.tabPanelUser);
            this.info.Controls.Add(this.tabPanelFault);
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
            this.tabItemError,
            this.tabItemFault,
            this.tabItemUser,
            this.tabItemMachine,
            this.tabItemAsset,
            this.tabItemMaintenance,
            this.tabItemBasic});
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
            // tabPanelBasic
            // 
            this.tabPanelBasic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPanelBasic.Location = new System.Drawing.Point(0, 31);
            this.tabPanelBasic.Name = "tabPanelBasic";
            this.tabPanelBasic.Size = new System.Drawing.Size(1600, 769);
            this.tabPanelBasic.TabIndex = 0;
            this.tabPanelBasic.TabItem = this.tabItemBasic;
            this.tabPanelBasic.Visible = false;
            // 
            // tabItemBasic
            // 
            this.tabItemBasic.AttachedControl = this.tabPanelBasic;
            this.tabItemBasic.GlobalItem = false;
            this.tabItemBasic.Name = "tabItemBasic";
            this.tabItemBasic.Text = "基础信息";
            this.tabItemBasic.Click += new System.EventHandler(this.tabItemBasic_Click);
            // 
            // tabPanelMaintenance
            // 
            this.tabPanelMaintenance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPanelMaintenance.Location = new System.Drawing.Point(0, 31);
            this.tabPanelMaintenance.Name = "tabPanelMaintenance";
            this.tabPanelMaintenance.Size = new System.Drawing.Size(1600, 769);
            this.tabPanelMaintenance.TabIndex = 0;
            this.tabPanelMaintenance.TabItem = this.tabItemMaintenance;
            this.tabPanelMaintenance.Visible = false;
            // 
            // tabItemMaintenance
            // 
            this.tabItemMaintenance.AttachedControl = this.tabPanelMaintenance;
            this.tabItemMaintenance.GlobalItem = false;
            this.tabItemMaintenance.Name = "tabItemMaintenance";
            this.tabItemMaintenance.Text = "资产保养与履历";
            this.tabItemMaintenance.Click += new System.EventHandler(this.tabItemMaintenance_Click);
            // 
            // tabPanelAsset
            // 
            this.tabPanelAsset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPanelAsset.Location = new System.Drawing.Point(0, 31);
            this.tabPanelAsset.Name = "tabPanelAsset";
            this.tabPanelAsset.Size = new System.Drawing.Size(1600, 769);
            this.tabPanelAsset.TabIndex = 0;
            this.tabPanelAsset.TabItem = this.tabItemAsset;
            this.tabPanelAsset.Visible = false;
            // 
            // tabItemAsset
            // 
            this.tabItemAsset.AttachedControl = this.tabPanelAsset;
            this.tabItemAsset.GlobalItem = false;
            this.tabItemAsset.Name = "tabItemAsset";
            this.tabItemAsset.Text = "资产清册";
            this.tabItemAsset.Click += new System.EventHandler(this.tabItemAsset_Click);
            // 
            // tabPanelMachine
            // 
            this.tabPanelMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPanelMachine.Location = new System.Drawing.Point(0, 31);
            this.tabPanelMachine.Name = "tabPanelMachine";
            this.tabPanelMachine.Size = new System.Drawing.Size(1600, 769);
            this.tabPanelMachine.TabIndex = 0;
            this.tabPanelMachine.TabItem = this.tabItemMachine;
            this.tabPanelMachine.Visible = false;
            // 
            // tabItemMachine
            // 
            this.tabItemMachine.AttachedControl = this.tabPanelMachine;
            this.tabItemMachine.GlobalItem = false;
            this.tabItemMachine.Name = "tabItemMachine";
            this.tabItemMachine.Text = "设备管理";
            this.tabItemMachine.Click += new System.EventHandler(this.tabItemMachine_Click);
            // 
            // tabPanelUser
            // 
            this.tabPanelUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPanelUser.Location = new System.Drawing.Point(0, 31);
            this.tabPanelUser.Name = "tabPanelUser";
            this.tabPanelUser.Size = new System.Drawing.Size(1600, 769);
            this.tabPanelUser.TabIndex = 0;
            this.tabPanelUser.TabItem = this.tabItemUser;
            this.tabPanelUser.Visible = false;
            // 
            // tabItemUser
            // 
            this.tabItemUser.AttachedControl = this.tabPanelUser;
            this.tabItemUser.GlobalItem = false;
            this.tabItemUser.Name = "tabItemUser";
            this.tabItemUser.Text = "用户信息";
            this.tabItemUser.Click += new System.EventHandler(this.tabItemUser_Click);
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
            this.tabPanelFault.Visible = false;
            // 
            // tabItemFault
            // 
            this.tabItemFault.AttachedControl = this.tabPanelFault;
            this.tabItemFault.GlobalItem = false;
            this.tabItemFault.Name = "tabItemFault";
            this.tabItemFault.Text = "故障方案";
            this.tabItemFault.Click += new System.EventHandler(this.tabItemFault_Click);
            // 
            // FrmTabFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 800);
            this.ControlBox = false;
            this.Controls.Add(this.info);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmTabFrame";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.FrmBaseInfo_Activated);
            this.Load += new System.EventHandler(this.FrmTabFrame_Load);
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
        private DevComponents.DotNetBar.SuperTabControlPanel tabPanelUser;
        private DevComponents.DotNetBar.SuperTabItem tabItemUser;
        private DevComponents.DotNetBar.SuperTabControlPanel tabPanelMachine;
        private DevComponents.DotNetBar.SuperTabItem tabItemMachine;
        private DevComponents.DotNetBar.SuperTabControlPanel tabPanelMaintenance;
        private DevComponents.DotNetBar.SuperTabItem tabItemMaintenance;
        private DevComponents.DotNetBar.SuperTabControlPanel tabPanelAsset;
        private DevComponents.DotNetBar.SuperTabItem tabItemAsset;
        private DevComponents.DotNetBar.SuperTabControlPanel tabPanelBasic;
        private DevComponents.DotNetBar.SuperTabItem tabItemBasic;
    }
}