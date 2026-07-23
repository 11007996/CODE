namespace Call
{
    partial class FrmFaultSolutionForTarger
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
            this.labSolution = new System.Windows.Forms.Label();
            this.tbLine = new System.Windows.Forms.TextBox();
            this.labLine = new System.Windows.Forms.Label();
            this.labTotalTimeTitle = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.labTotalTime = new System.Windows.Forms.Label();
            this.tbxFault = new System.Windows.Forms.TextBox();
            this.labFault = new System.Windows.Forms.Label();
            this.tbxSolution = new System.Windows.Forms.TextBox();
            this.labMessage = new System.Windows.Forms.Label();
            this.tbxSolver = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labHandlerTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labSolution
            // 
            this.labSolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labSolution.AutoSize = true;
            this.labSolution.BackColor = System.Drawing.Color.Transparent;
            this.labSolution.Location = new System.Drawing.Point(30, 168);
            this.labSolution.Name = "labSolution";
            this.labSolution.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.labSolution.Size = new System.Drawing.Size(67, 20);
            this.labSolution.TabIndex = 16;
            this.labSolution.Text = "解决方案";
            // 
            // tbLine
            // 
            this.tbLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbLine.Enabled = false;
            this.tbLine.Location = new System.Drawing.Point(103, 12);
            this.tbLine.Name = "tbLine";
            this.tbLine.Size = new System.Drawing.Size(209, 25);
            this.tbLine.TabIndex = 23;
            // 
            // labLine
            // 
            this.labLine.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labLine.AutoSize = true;
            this.labLine.BackColor = System.Drawing.Color.Transparent;
            this.labLine.Location = new System.Drawing.Point(60, 17);
            this.labLine.Name = "labLine";
            this.labLine.Size = new System.Drawing.Size(37, 15);
            this.labLine.TabIndex = 22;
            this.labLine.Text = "产线";
            // 
            // labTotalTimeTitle
            // 
            this.labTotalTimeTitle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labTotalTimeTitle.AutoSize = true;
            this.labTotalTimeTitle.BackColor = System.Drawing.Color.Transparent;
            this.labTotalTimeTitle.Location = new System.Drawing.Point(45, 286);
            this.labTotalTimeTitle.Name = "labTotalTimeTitle";
            this.labTotalTimeTitle.Size = new System.Drawing.Size(52, 15);
            this.labTotalTimeTitle.TabIndex = 30;
            this.labTotalTimeTitle.Text = "总用时";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConfirm.Location = new System.Drawing.Point(520, 279);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(87, 29);
            this.btnConfirm.TabIndex = 31;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // labTotalTime
            // 
            this.labTotalTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labTotalTime.AutoSize = true;
            this.labTotalTime.BackColor = System.Drawing.Color.Transparent;
            this.labTotalTime.Location = new System.Drawing.Point(103, 286);
            this.labTotalTime.Name = "labTotalTime";
            this.labTotalTime.Size = new System.Drawing.Size(45, 15);
            this.labTotalTime.TabIndex = 33;
            this.labTotalTime.Text = "0分钟";
            // 
            // tbxFault
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tbxFault, 3);
            this.tbxFault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxFault.ForeColor = System.Drawing.Color.Black;
            this.tbxFault.Location = new System.Drawing.Point(103, 53);
            this.tbxFault.Multiline = true;
            this.tbxFault.Name = "tbxFault";
            this.tbxFault.Size = new System.Drawing.Size(504, 102);
            this.tbxFault.TabIndex = 36;
            // 
            // labFault
            // 
            this.labFault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labFault.AutoSize = true;
            this.labFault.BackColor = System.Drawing.Color.Transparent;
            this.labFault.Location = new System.Drawing.Point(30, 50);
            this.labFault.Name = "labFault";
            this.labFault.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.labFault.Size = new System.Drawing.Size(67, 20);
            this.labFault.TabIndex = 14;
            this.labFault.Text = "异常内容";
            // 
            // tbxSolution
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tbxSolution, 3);
            this.tbxSolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxSolution.ForeColor = System.Drawing.Color.Black;
            this.tbxSolution.Location = new System.Drawing.Point(103, 171);
            this.tbxSolution.Multiline = true;
            this.tbxSolution.Name = "tbxSolution";
            this.tbxSolution.Size = new System.Drawing.Size(504, 102);
            this.tbxSolution.TabIndex = 40;
            // 
            // labMessage
            // 
            this.labMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMessage.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labMessage, 4);
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(23, 318);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(37, 15);
            this.labMessage.TabIndex = 52;
            this.labMessage.Text = "提示";
            // 
            // tbxSolver
            // 
            this.tbxSolver.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbxSolver.Enabled = false;
            this.tbxSolver.Location = new System.Drawing.Point(398, 12);
            this.tbxSolver.Name = "tbxSolver";
            this.tbxSolver.Size = new System.Drawing.Size(209, 25);
            this.tbxSolver.TabIndex = 53;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.labMessage, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.tbxSolver, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnConfirm, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbLine, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.labTotalTime, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbxSolution, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.labHandlerTitle, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labTotalTimeTitle, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbxFault, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.labFault, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labSolution, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labLine, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(630, 341);
            this.tableLayoutPanel1.TabIndex = 54;
            // 
            // labHandlerTitle
            // 
            this.labHandlerTitle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labHandlerTitle.AutoSize = true;
            this.labHandlerTitle.BackColor = System.Drawing.Color.Transparent;
            this.labHandlerTitle.Location = new System.Drawing.Point(340, 17);
            this.labHandlerTitle.Name = "labHandlerTitle";
            this.labHandlerTitle.Size = new System.Drawing.Size(52, 15);
            this.labHandlerTitle.TabIndex = 27;
            this.labHandlerTitle.Text = "维护人";
            // 
            // FrmFaultSolutionForTarger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Call.Properties.Resources.menu_bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(630, 341);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmFaultSolutionForTarger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "解决方案";
            this.Load += new System.EventHandler(this.FrmFaultSolutionForTarger_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labSolution;
        private System.Windows.Forms.TextBox tbLine;
        private System.Windows.Forms.Label labLine;
        private System.Windows.Forms.Label labTotalTimeTitle;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label labTotalTime;
        private System.Windows.Forms.TextBox tbxFault;
        private System.Windows.Forms.Label labFault;
        private System.Windows.Forms.TextBox tbxSolution;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.TextBox tbxSolver;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labHandlerTitle;
    }
}