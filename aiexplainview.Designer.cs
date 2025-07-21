namespace serial_FFT_plotter
{
    partial class aiexplainview
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.global_view = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.local_view = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalExplainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postiveMagnitudeContibuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMagnitudeContibuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPositiveContibuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showNegativeContibuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localExplainViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.global_view)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.local_view)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(849, 564);
            this.splitContainer1.SplitterDistance = 288;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.global_view);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(825, 273);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Global Explain";
            // 
            // global_view
            // 
            this.global_view.BackColor = System.Drawing.Color.Black;
            this.global_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.global_view.Location = new System.Drawing.Point(3, 16);
            this.global_view.Name = "global_view";
            this.global_view.Size = new System.Drawing.Size(819, 254);
            this.global_view.TabIndex = 0;
            this.global_view.TabStop = false;
            this.global_view.Paint += new System.Windows.Forms.PaintEventHandler(this.global_view_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.local_view);
            this.groupBox2.Location = new System.Drawing.Point(12, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(825, 257);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Local explain";
            // 
            // local_view
            // 
            this.local_view.BackColor = System.Drawing.Color.Black;
            this.local_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.local_view.Location = new System.Drawing.Point(3, 16);
            this.local_view.Name = "local_view";
            this.local_view.Size = new System.Drawing.Size(819, 238);
            this.local_view.TabIndex = 1;
            this.local_view.TabStop = false;
            this.local_view.Paint += new System.Windows.Forms.PaintEventHandler(this.local_view_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(849, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.globalExplainToolStripMenuItem,
            this.localExplainViewToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // globalExplainToolStripMenuItem
            // 
            this.globalExplainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.postiveMagnitudeContibuteToolStripMenuItem,
            this.showMagnitudeContibuteToolStripMenuItem,
            this.showPositiveContibuteToolStripMenuItem,
            this.showNegativeContibuteToolStripMenuItem});
            this.globalExplainToolStripMenuItem.Name = "globalExplainToolStripMenuItem";
            this.globalExplainToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.globalExplainToolStripMenuItem.Text = "Global explain view";
            // 
            // postiveMagnitudeContibuteToolStripMenuItem
            // 
            this.postiveMagnitudeContibuteToolStripMenuItem.Name = "postiveMagnitudeContibuteToolStripMenuItem";
            this.postiveMagnitudeContibuteToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.postiveMagnitudeContibuteToolStripMenuItem.Text = "postive magnitude contribution ";
            this.postiveMagnitudeContibuteToolStripMenuItem.Click += new System.EventHandler(this.postiveMagnitudeContibuteToolStripMenuItem_Click);
            // 
            // showMagnitudeContibuteToolStripMenuItem
            // 
            this.showMagnitudeContibuteToolStripMenuItem.Name = "showMagnitudeContibuteToolStripMenuItem";
            this.showMagnitudeContibuteToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.showMagnitudeContibuteToolStripMenuItem.Text = "negative magnitude contribution ";
            this.showMagnitudeContibuteToolStripMenuItem.Click += new System.EventHandler(this.showMagnitudeContibuteToolStripMenuItem_Click);
            // 
            // showPositiveContibuteToolStripMenuItem
            // 
            this.showPositiveContibuteToolStripMenuItem.Name = "showPositiveContibuteToolStripMenuItem";
            this.showPositiveContibuteToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.showPositiveContibuteToolStripMenuItem.Text = "positive contribution ";
            this.showPositiveContibuteToolStripMenuItem.Click += new System.EventHandler(this.showPositiveContibuteToolStripMenuItem_Click);
            // 
            // showNegativeContibuteToolStripMenuItem
            // 
            this.showNegativeContibuteToolStripMenuItem.Name = "showNegativeContibuteToolStripMenuItem";
            this.showNegativeContibuteToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.showNegativeContibuteToolStripMenuItem.Text = "negative contribution ";
            this.showNegativeContibuteToolStripMenuItem.Click += new System.EventHandler(this.showNegativeContibuteToolStripMenuItem_Click);
            // 
            // localExplainViewToolStripMenuItem
            // 
            this.localExplainViewToolStripMenuItem.Name = "localExplainViewToolStripMenuItem";
            this.localExplainViewToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.localExplainViewToolStripMenuItem.Text = "Local explain view";
            // 
            // aiexplainview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 588);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "aiexplainview";
            this.Text = "aiexplainview";
            this.Load += new System.EventHandler(this.aiexplainview_Load);
            this.SizeChanged += new System.EventHandler(this.aiexplainview_SizeChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.global_view)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.local_view)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox global_view;
        private System.Windows.Forms.PictureBox local_view;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem globalExplainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPositiveContibuteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showNegativeContibuteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMagnitudeContibuteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem postiveMagnitudeContibuteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localExplainViewToolStripMenuItem;
    }
}