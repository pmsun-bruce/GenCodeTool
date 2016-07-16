namespace NFramework.GenCodeTool
{
    partial class ProgressForm
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
            this.GenBar = new System.Windows.Forms.ProgressBar();
            this.GenLogTxt = new System.Windows.Forms.TextBox();
            this.BarPanel = new System.Windows.Forms.Panel();
            this.GenLogPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FinishBtn = new System.Windows.Forms.Button();
            this.BarPanel.SuspendLayout();
            this.GenLogPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GenBar
            // 
            this.GenBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.GenBar.Location = new System.Drawing.Point(12, 12);
            this.GenBar.Name = "GenBar";
            this.GenBar.Size = new System.Drawing.Size(540, 23);
            this.GenBar.TabIndex = 0;
            // 
            // GenLogTxt
            // 
            this.GenLogTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GenLogTxt.Location = new System.Drawing.Point(0, 0);
            this.GenLogTxt.Multiline = true;
            this.GenLogTxt.Name = "GenLogTxt";
            this.GenLogTxt.ReadOnly = true;
            this.GenLogTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.GenLogTxt.Size = new System.Drawing.Size(564, 223);
            this.GenLogTxt.TabIndex = 1;
            // 
            // BarPanel
            // 
            this.BarPanel.Controls.Add(this.GenBar);
            this.BarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarPanel.Location = new System.Drawing.Point(0, 0);
            this.BarPanel.Name = "BarPanel";
            this.BarPanel.Size = new System.Drawing.Size(564, 49);
            this.BarPanel.TabIndex = 2;
            // 
            // GenLogPanel
            // 
            this.GenLogPanel.Controls.Add(this.GenLogTxt);
            this.GenLogPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.GenLogPanel.Location = new System.Drawing.Point(0, 49);
            this.GenLogPanel.Name = "GenLogPanel";
            this.GenLogPanel.Size = new System.Drawing.Size(564, 223);
            this.GenLogPanel.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FinishBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 272);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(564, 31);
            this.panel1.TabIndex = 4;
            // 
            // FinishBtn
            // 
            this.FinishBtn.Enabled = false;
            this.FinishBtn.Location = new System.Drawing.Point(477, 4);
            this.FinishBtn.Name = "FinishBtn";
            this.FinishBtn.Size = new System.Drawing.Size(75, 23);
            this.FinishBtn.TabIndex = 0;
            this.FinishBtn.Text = "完成";
            this.FinishBtn.UseVisualStyleBackColor = true;
            this.FinishBtn.Click += new System.EventHandler(this.FinishBtn_Click);
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 303);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.GenLogPanel);
            this.Controls.Add(this.BarPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "生成进度";
            this.Load += new System.EventHandler(this.ProgressForm_Load);
            this.BarPanel.ResumeLayout(false);
            this.GenLogPanel.ResumeLayout(false);
            this.GenLogPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar GenBar;
        private System.Windows.Forms.TextBox GenLogTxt;
        private System.Windows.Forms.Panel BarPanel;
        private System.Windows.Forms.Panel GenLogPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button FinishBtn;
    }
}