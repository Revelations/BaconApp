namespace BaconBuilder.Component
{
    sealed partial class MapBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
		[System.Diagnostics.DebuggerNonUserCode, System.CodeDom.Compiler.GeneratedCode("Winform Designer", "VS2010 SP1")]
		private void InitializeComponent()
        {
			this.canvas = new BaconBuilder.Component.DbPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnZoomReset = new System.Windows.Forms.Button();
			this.btnZoomOut = new System.Windows.Forms.Button();
			this.btnZoomIn = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// canvas
			// 
			this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.canvas.Location = new System.Drawing.Point(0, 0);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(300, 360);
			this.canvas.TabIndex = 2;
			canvas.Paint += CanvasPaint;
			canvas.MouseUp += CanvasMouseUp;
			canvas.MouseDown += CanvasMouseDown;
			canvas.MouseMove += CanvasMouseMove;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnZoomReset);
			this.panel1.Controls.Add(this.btnZoomOut);
			this.panel1.Controls.Add(this.btnZoomIn);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 360);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(300, 40);
			this.panel1.TabIndex = 1;
			// 
			// btnZoomReset
			// 
			this.btnZoomReset.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnZoomReset.Location = new System.Drawing.Point(111, 14);
			this.btnZoomReset.Name = "btnZoomReset";
			this.btnZoomReset.Size = new System.Drawing.Size(80, 23);
			this.btnZoomReset.TabIndex = 5;
			this.btnZoomReset.Text = "Reset Zoom";
			this.btnZoomReset.UseVisualStyleBackColor = true;
			this.btnZoomReset.Click += ZoomReset;
			// 
			// btnZoomOut
			// 
			this.btnZoomOut.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnZoomOut.Location = new System.Drawing.Point(211, 14);
			this.btnZoomOut.Name = "btnZoomOut";
			this.btnZoomOut.Size = new System.Drawing.Size(80, 23);
			this.btnZoomOut.TabIndex = 4;
			this.btnZoomOut.Text = "Zoom Out";
			this.btnZoomOut.UseVisualStyleBackColor = true;
			this.btnZoomOut.Click += ZoomOut;
			// 
			// btnZoomIn
			// 
			this.btnZoomIn.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnZoomIn.Location = new System.Drawing.Point(11, 14);
			this.btnZoomIn.Name = "btnZoomIn";
			this.btnZoomIn.Size = new System.Drawing.Size(80, 23);
			this.btnZoomIn.TabIndex = 3;
			this.btnZoomIn.Text = "Zoom In";
			this.btnZoomIn.UseVisualStyleBackColor = true;
			this.btnZoomIn.Click += ZoomIn;
			// 
			// MapBox
			// 
			this.AutoSize = true;
			this.Controls.Add(this.canvas);
			this.Controls.Add(this.panel1);
			this.Name = "MapBox";
			this.Size = new System.Drawing.Size(300, 400);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private BaconBuilder.Component.DbPanel canvas;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnZoomReset;
		private System.Windows.Forms.Button btnZoomOut;
		private System.Windows.Forms.Button btnZoomIn;

	}
}
