namespace Ksu.Cis300.MapViewer
{
    partial class UserInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInterface));
            this.uxButtonBar = new System.Windows.Forms.ToolStrip();
            this.uxOpenMap = new System.Windows.Forms.ToolStripButton();
            this.uxZoomIn = new System.Windows.Forms.ToolStripButton();
            this.uxZoomOut = new System.Windows.Forms.ToolStripButton();
            this.uxMapContainer = new System.Windows.Forms.Panel();
            this.uxOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.uxButtonBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxButtonBar
            // 
            this.uxButtonBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxOpenMap,
            this.uxZoomIn,
            this.uxZoomOut});
            this.uxButtonBar.Location = new System.Drawing.Point(0, 0);
            this.uxButtonBar.Name = "uxButtonBar";
            this.uxButtonBar.Size = new System.Drawing.Size(449, 25);
            this.uxButtonBar.TabIndex = 0;
            this.uxButtonBar.Text = "toolStrip1";
            // 
            // uxOpenMap
            // 
            this.uxOpenMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.uxOpenMap.Image = ((System.Drawing.Image)(resources.GetObject("uxOpenMap.Image")));
            this.uxOpenMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uxOpenMap.Name = "uxOpenMap";
            this.uxOpenMap.Size = new System.Drawing.Size(67, 22);
            this.uxOpenMap.Text = "Open Map";
            this.uxOpenMap.Click += new System.EventHandler(this.uxOpenMap_Click);
            // 
            // uxZoomIn
            // 
            this.uxZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.uxZoomIn.Enabled = false;
            this.uxZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("uxZoomIn.Image")));
            this.uxZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uxZoomIn.Name = "uxZoomIn";
            this.uxZoomIn.Size = new System.Drawing.Size(56, 22);
            this.uxZoomIn.Text = "Zoom In";
            this.uxZoomIn.Click += new System.EventHandler(this.uxZoomIn_Click);
            // 
            // uxZoomOut
            // 
            this.uxZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.uxZoomOut.Enabled = false;
            this.uxZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("uxZoomOut.Image")));
            this.uxZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uxZoomOut.Name = "uxZoomOut";
            this.uxZoomOut.Size = new System.Drawing.Size(66, 22);
            this.uxZoomOut.Text = "Zoom Out";
            this.uxZoomOut.Click += new System.EventHandler(this.uxZoomOut_Click);
            // 
            // uxMapContainer
            // 
            this.uxMapContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxMapContainer.AutoScroll = true;
            this.uxMapContainer.Location = new System.Drawing.Point(12, 28);
            this.uxMapContainer.Name = "uxMapContainer";
            this.uxMapContainer.Size = new System.Drawing.Size(425, 319);
            this.uxMapContainer.TabIndex = 1;
            // 
            // uxOpenDialog
            // 
            this.uxOpenDialog.Filter = "CSV files|*.csv|All files|*.*";
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 359);
            this.Controls.Add(this.uxMapContainer);
            this.Controls.Add(this.uxButtonBar);
            this.MinimumSize = new System.Drawing.Size(225, 150);
            this.Name = "UserInterface";
            this.Text = "Map Viewer";
            this.uxButtonBar.ResumeLayout(false);
            this.uxButtonBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip uxButtonBar;
        private System.Windows.Forms.ToolStripButton uxOpenMap;
        private System.Windows.Forms.ToolStripButton uxZoomIn;
        private System.Windows.Forms.ToolStripButton uxZoomOut;
        private System.Windows.Forms.Panel uxMapContainer;
        private System.Windows.Forms.OpenFileDialog uxOpenDialog;
    }
}

