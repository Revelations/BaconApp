using System.Windows.Forms;

namespace BaconBuilder.View
{
	partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printAllModifiedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeDirectory = new System.Windows.Forms.TreeView();
            this.pnlDirectories = new System.Windows.Forms.Panel();
            this.flpDirectory = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.pnlContentEditor = new System.Windows.Forms.Panel();
            this.pnlTextEditor = new System.Windows.Forms.Panel();
            this.rtbContents = new System.Windows.Forms.RichTextBox();
            this.flpPreview = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.toolContents = new System.Windows.Forms.ToolStrip();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.tlpDataFields = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.lblX = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.btnMapPreview = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbImage = new System.Windows.Forms.ToolStripButton();
            this.tsbBold = new System.Windows.Forms.ToolStripButton();
            this.tsbItalics = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.pnlDirectories.SuspendLayout();
            this.flpDirectory.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.pnlContentEditor.SuspendLayout();
            this.pnlTextEditor.SuspendLayout();
            this.flpPreview.SuspendLayout();
            this.toolContents.SuspendLayout();
            this.tlpDataFields.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDatabaseToolStripMenuItem,
            this.toolStripSeparator1,
            this.printToolStripMenuItem,
            this.printAllModifiedToolStripMenuItem,
            this.printAllToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openDatabaseToolStripMenuItem
            // 
            this.openDatabaseToolStripMenuItem.Name = "openDatabaseToolStripMenuItem";
            this.openDatabaseToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.openDatabaseToolStripMenuItem.Text = "Open Database...";
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.printToolStripMenuItem.Text = "Print...";
            // 
            // printAllModifiedToolStripMenuItem
            // 
            this.printAllModifiedToolStripMenuItem.Name = "printAllModifiedToolStripMenuItem";
            this.printAllModifiedToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.printAllModifiedToolStripMenuItem.Text = "Print All Modified...";
            // 
            // printAllToolStripMenuItem
            // 
            this.printAllToolStripMenuItem.Name = "printAllToolStripMenuItem";
            this.printAllToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.printAllToolStripMenuItem.Text = "Print All...";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.optionsToolStripMenuItem.Text = "&Options...";
            // 
            // treeDirectory
            // 
            this.treeDirectory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDirectory.Location = new System.Drawing.Point(0, 0);
            this.treeDirectory.Name = "treeDirectory";
            this.treeDirectory.Size = new System.Drawing.Size(160, 496);
            this.treeDirectory.TabIndex = 0;
            // 
            // pnlDirectories
            // 
            this.pnlDirectories.BackColor = System.Drawing.SystemColors.Control;
            this.pnlDirectories.Controls.Add(this.treeDirectory);
            this.pnlDirectories.Controls.Add(this.flpDirectory);
            this.pnlDirectories.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDirectories.Location = new System.Drawing.Point(0, 0);
            this.pnlDirectories.Name = "pnlDirectories";
            this.pnlDirectories.Size = new System.Drawing.Size(160, 525);
            this.pnlDirectories.TabIndex = 8;
            // 
            // flpDirectory
            // 
            this.flpDirectory.AutoSize = true;
            this.flpDirectory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpDirectory.Controls.Add(this.btnAddFile);
            this.flpDirectory.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpDirectory.Location = new System.Drawing.Point(0, 496);
            this.flpDirectory.Name = "flpDirectory";
            this.flpDirectory.Size = new System.Drawing.Size(160, 29);
            this.flpDirectory.TabIndex = 2;
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(3, 3);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(150, 23);
            this.btnAddFile.TabIndex = 1;
            this.btnAddFile.Text = "Add Files";
            this.btnAddFile.UseVisualStyleBackColor = true;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlContentEditor);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitter1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlDirectories);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(792, 527);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(792, 573);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // pnlContentEditor
            // 
            this.pnlContentEditor.AutoSize = true;
            this.pnlContentEditor.BackColor = System.Drawing.SystemColors.Control;
            this.pnlContentEditor.Controls.Add(this.pnlTextEditor);
            this.pnlContentEditor.Controls.Add(this.splitter3);
            this.pnlContentEditor.Controls.Add(this.tlpDataFields);
            this.pnlContentEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContentEditor.Location = new System.Drawing.Point(168, 0);
            this.pnlContentEditor.Name = "pnlContentEditor";
            this.pnlContentEditor.Size = new System.Drawing.Size(622, 525);
            this.pnlContentEditor.TabIndex = 9;
            // 
            // pnlTextEditor
            // 
            this.pnlTextEditor.Controls.Add(this.rtbContents);
            this.pnlTextEditor.Controls.Add(this.flpPreview);
            this.pnlTextEditor.Controls.Add(this.toolContents);
            this.pnlTextEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTextEditor.Location = new System.Drawing.Point(0, 66);
            this.pnlTextEditor.Name = "pnlTextEditor";
            this.pnlTextEditor.Size = new System.Drawing.Size(622, 459);
            this.pnlTextEditor.TabIndex = 12;
            // 
            // rtbContents
            // 
            this.rtbContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbContents.Location = new System.Drawing.Point(0, 25);
            this.rtbContents.Name = "rtbContents";
            this.rtbContents.Size = new System.Drawing.Size(622, 405);
            this.rtbContents.TabIndex = 9;
            this.rtbContents.Text = "";
            // 
            // flpPreview
            // 
            this.flpPreview.AutoSize = true;
            this.flpPreview.Controls.Add(this.btnPreview);
            this.flpPreview.Controls.Add(this.btnPrintPreview);
            this.flpPreview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpPreview.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpPreview.Location = new System.Drawing.Point(0, 430);
            this.flpPreview.Name = "flpPreview";
            this.flpPreview.Size = new System.Drawing.Size(622, 29);
            this.flpPreview.TabIndex = 11;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(469, 3);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(150, 23);
            this.btnPreview.TabIndex = 10;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Location = new System.Drawing.Point(313, 3);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(150, 23);
            this.btnPrintPreview.TabIndex = 11;
            this.btnPrintPreview.Text = "Print Preview";
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            // 
            // toolContents
            // 
            this.toolContents.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbImage,
            this.tsbBold,
            this.tsbItalics});
            this.toolContents.Location = new System.Drawing.Point(0, 0);
            this.toolContents.Name = "toolContents";
            this.toolContents.Size = new System.Drawing.Size(622, 25);
            this.toolContents.TabIndex = 8;
            this.toolContents.Text = "toolStrip1";
            // 
            // splitter3
            // 
            this.splitter3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter3.Enabled = false;
            this.splitter3.Location = new System.Drawing.Point(0, 58);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(622, 8);
            this.splitter3.TabIndex = 13;
            this.splitter3.TabStop = false;
            // 
            // tlpDataFields
            // 
            this.tlpDataFields.AutoSize = true;
            this.tlpDataFields.ColumnCount = 5;
            this.tlpDataFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpDataFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDataFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpDataFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDataFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDataFields.Controls.Add(this.btnMapPreview, 4, 1);
            this.tlpDataFields.Controls.Add(this.txtY, 3, 1);
            this.tlpDataFields.Controls.Add(this.lblY, 2, 1);
            this.tlpDataFields.Controls.Add(this.txtX, 1, 1);
            this.tlpDataFields.Controls.Add(this.lblX, 0, 1);
            this.tlpDataFields.Controls.Add(this.txtTitle, 1, 0);
            this.tlpDataFields.Controls.Add(this.lblTitle, 0, 0);
            this.tlpDataFields.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpDataFields.Location = new System.Drawing.Point(0, 0);
            this.tlpDataFields.Name = "tlpDataFields";
            this.tlpDataFields.RowCount = 2;
            this.tlpDataFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDataFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDataFields.Size = new System.Drawing.Size(622, 58);
            this.tlpDataFields.TabIndex = 9;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(3, 3);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(44, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title:";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtY
            // 
            this.txtY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtY.Location = new System.Drawing.Point(281, 32);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(172, 20);
            this.txtY.TabIndex = 5;
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblX.Location = new System.Drawing.Point(3, 32);
            this.lblX.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(44, 16);
            this.lblX.TabIndex = 1;
            this.lblX.Text = "X:";
            this.lblX.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblY.Location = new System.Drawing.Point(231, 32);
            this.lblY.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(44, 16);
            this.lblY.TabIndex = 2;
            this.lblY.Text = "Y:";
            this.lblY.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtX
            // 
            this.txtX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtX.Location = new System.Drawing.Point(53, 32);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(172, 20);
            this.txtX.TabIndex = 4;
            // 
            // txtTitle
            // 
            this.tlpDataFields.SetColumnSpan(this.txtTitle, 3);
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTitle.Location = new System.Drawing.Point(53, 3);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(400, 20);
            this.txtTitle.TabIndex = 3;
            // 
            // btnMapPreview
            // 
            this.btnMapPreview.Location = new System.Drawing.Point(464, 32);
            this.btnMapPreview.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.btnMapPreview.Name = "btnMapPreview";
            this.btnMapPreview.Size = new System.Drawing.Size(150, 23);
            this.btnMapPreview.TabIndex = 6;
            this.btnMapPreview.Text = "Map Preview";
            this.btnMapPreview.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter1.Location = new System.Drawing.Point(160, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(8, 525);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // tsbImage
            // 
            this.tsbImage.Image = ((System.Drawing.Image)(resources.GetObject("tsbImage.Image")));
            this.tsbImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImage.Name = "tsbImage";
            this.tsbImage.Size = new System.Drawing.Size(60, 22);
            this.tsbImage.Text = "Image";
            // 
            // tsbBold
            // 
            this.tsbBold.Image = ((System.Drawing.Image)(resources.GetObject("tsbBold.Image")));
            this.tsbBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBold.Name = "tsbBold";
            this.tsbBold.Size = new System.Drawing.Size(51, 22);
            this.tsbBold.Text = "Bold";
            // 
            // tsbItalics
            // 
            this.tsbItalics.Image = ((System.Drawing.Image)(resources.GetObject("tsbItalics.Image")));
            this.tsbItalics.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbItalics.Name = "tsbItalics";
            this.tsbItalics.Size = new System.Drawing.Size(57, 22);
            this.tsbItalics.Text = "Italics";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(173, 6);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(173, 6);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.toolStripContainer1);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlDirectories.ResumeLayout(false);
            this.pnlDirectories.PerformLayout();
            this.flpDirectory.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.pnlContentEditor.ResumeLayout(false);
            this.pnlContentEditor.PerformLayout();
            this.pnlTextEditor.ResumeLayout(false);
            this.pnlTextEditor.PerformLayout();
            this.flpPreview.ResumeLayout(false);
            this.toolContents.ResumeLayout(false);
            this.toolContents.PerformLayout();
            this.tlpDataFields.ResumeLayout(false);
            this.tlpDataFields.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private MenuStrip menuStrip1;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem openDatabaseToolStripMenuItem;
		private TreeView treeDirectory;
		private Panel pnlDirectories;
		private ToolStripContainer toolStripContainer1;
		private Panel pnlContentEditor;
		private Splitter splitter1;
		private Splitter splitter3;
		private Panel pnlTextEditor;
        private ToolStrip toolContents;
		private ToolStripMenuItem printToolStripMenuItem;
		private ToolStripMenuItem printAllModifiedToolStripMenuItem;
		private ToolStripMenuItem printAllToolStripMenuItem;
		private TableLayoutPanel tlpDataFields;
		private Label lblTitle;
		private TextBox txtY;
		private Label lblX;
		private Label lblY;
		private TextBox txtX;
		private TextBox txtTitle;
		private Button btnMapPreview;
		private ToolStripMenuItem editToolStripMenuItem;
		private ToolStripMenuItem viewToolStripMenuItem;
		private ToolStripMenuItem toolsToolStripMenuItem;
		private ToolStripMenuItem optionsToolStripMenuItem;
        private RichTextBox rtbContents;
        private Button btnAddFile;
        private Button btnPreview;
        private FlowLayoutPanel flpPreview;
        private FlowLayoutPanel flpDirectory;
        private Button btnPrintPreview;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripButton tsbImage;
        private ToolStripButton tsbBold;
        private ToolStripButton tsbItalics;
        private StatusStrip statusStrip1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator1;

	}
}

