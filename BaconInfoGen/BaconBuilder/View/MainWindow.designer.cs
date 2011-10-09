
using System.Windows.Forms;

namespace BaconBuilder.View
{
	partial class MainWindow
	{
		const int splitterWidth = 4;
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
		[System.Diagnostics.DebuggerNonUserCode]
		[System.CodeDom.Compiler.GeneratedCode("Winform Designer", "VS2010 SP1")]
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSync = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printAllModifiedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlDirectories = new System.Windows.Forms.Panel();
            this.listViewContents = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRemoveFile = new System.Windows.Forms.Button();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.pnlContentEditor = new System.Windows.Forms.Panel();
            this.pnlTextEditor = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.HTMLEditor = new System.Windows.Forms.WebBrowser();
            this.flpPreview = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.toolContents = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbImage = new System.Windows.Forms.ToolStripButton();
            this.tsbAudio = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBold = new System.Windows.Forms.ToolStripButton();
            this.tsbItalics = new System.Windows.Forms.ToolStripButton();
            this.btn_Underline = new System.Windows.Forms.ToolStripButton();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.tlpDataFields = new System.Windows.Forms.TableLayoutPanel();
            this.txtY = new System.Windows.Forms.NumericUpDown();
            this.lblY = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.NumericUpDown();
            this.lblX = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitter5 = new System.Windows.Forms.Splitter();
            this.splitter6 = new System.Windows.Forms.Splitter();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.mapBox = new BaconBuilder.Component.MapBox();
            this.menuStrip.SuspendLayout();
            this.pnlDirectories.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.pnlContentEditor.SuspendLayout();
            this.pnlTextEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flpPreview.SuspendLayout();
            this.toolContents.SuspendLayout();
            this.tlpDataFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtX)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(792, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSync,
            this.toolStripSeparator2,
            this.printToolStripMenuItem,
            this.printAllModifiedToolStripMenuItem,
            this.printAllToolStripMenuItem,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStripSync
            // 
            this.toolStripSync.Name = "toolStripSync";
            this.toolStripSync.Size = new System.Drawing.Size(169, 22);
            this.toolStripSync.Text = "Sync With Server...";
            this.toolStripSync.Click += new System.EventHandler(this.toolStripSync_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(166, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.printToolStripMenuItem.Text = "Print...";
            // 
            // printAllModifiedToolStripMenuItem
            // 
            this.printAllModifiedToolStripMenuItem.Name = "printAllModifiedToolStripMenuItem";
            this.printAllModifiedToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.printAllModifiedToolStripMenuItem.Text = "Print All Modified...";
            // 
            // printAllToolStripMenuItem
            // 
            this.printAllToolStripMenuItem.Name = "printAllToolStripMenuItem";
            this.printAllToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.printAllToolStripMenuItem.Text = "Print All...";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(166, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.optionsToolStripMenuItem.Text = "&Options...";
            // 
            // pnlDirectories
            // 
            this.pnlDirectories.BackColor = System.Drawing.SystemColors.Control;
            this.pnlDirectories.Controls.Add(this.listViewContents);
            this.pnlDirectories.Controls.Add(this.tableLayoutPanel1);
            this.pnlDirectories.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDirectories.Location = new System.Drawing.Point(8, 8);
            this.pnlDirectories.Name = "pnlDirectories";
            this.pnlDirectories.Size = new System.Drawing.Size(160, 509);
            this.pnlDirectories.TabIndex = 8;
            // 
            // listViewContents
            // 
            this.listViewContents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewContents.FullRowSelect = true;
            this.listViewContents.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewContents.HideSelection = false;
            this.listViewContents.Location = new System.Drawing.Point(0, 0);
            this.listViewContents.MultiSelect = false;
            this.listViewContents.Name = "listViewContents";
            this.listViewContents.Size = new System.Drawing.Size(160, 443);
            this.listViewContents.SmallImageList = this.imageList;
            this.listViewContents.TabIndex = 3;
            this.listViewContents.UseCompatibleStateImageBehavior = false;
            this.listViewContents.View = System.Windows.Forms.View.Details;
            this.listViewContents.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewContents_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Files";
            this.columnHeader1.Width = 138;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "html_icon.png");
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnRemoveFile, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAddFile, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 443);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(160, 66);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // btnRemoveFile
            // 
            this.btnRemoveFile.Location = new System.Drawing.Point(3, 36);
            this.btnRemoveFile.Name = "btnRemoveFile";
            this.btnRemoveFile.Size = new System.Drawing.Size(154, 27);
            this.btnRemoveFile.TabIndex = 3;
            this.btnRemoveFile.Text = "Remove File";
            this.btnRemoveFile.UseVisualStyleBackColor = true;
            this.btnRemoveFile.Click += new System.EventHandler(this.btnRemoveFile_Click);
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(3, 3);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(154, 27);
            this.btnAddFile.TabIndex = 2;
            this.btnAddFile.Text = "Add File";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlContentEditor);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitter1);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlDirectories);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitter2);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitter5);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitter6);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitter4);
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
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip);
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.Location = new System.Drawing.Point(0, 0);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(792, 22);
            this.statusStrip.TabIndex = 0;
            // 
            // pnlContentEditor
            // 
            this.pnlContentEditor.AutoSize = true;
            this.pnlContentEditor.BackColor = System.Drawing.SystemColors.Control;
            this.pnlContentEditor.Controls.Add(this.pnlTextEditor);
            this.pnlContentEditor.Controls.Add(this.splitter3);
            this.pnlContentEditor.Controls.Add(this.tlpDataFields);
            this.pnlContentEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContentEditor.Location = new System.Drawing.Point(176, 8);
            this.pnlContentEditor.Name = "pnlContentEditor";
            this.pnlContentEditor.Size = new System.Drawing.Size(606, 509);
            this.pnlContentEditor.TabIndex = 9;
            // 
            // pnlTextEditor
            // 
            this.pnlTextEditor.Controls.Add(this.splitContainer1);
            this.pnlTextEditor.Controls.Add(this.flpPreview);
            this.pnlTextEditor.Controls.Add(this.toolContents);
            this.pnlTextEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTextEditor.Location = new System.Drawing.Point(0, 34);
            this.pnlTextEditor.Name = "pnlTextEditor";
            this.pnlTextEditor.Size = new System.Drawing.Size(606, 475);
            this.pnlTextEditor.TabIndex = 12;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.HTMLEditor);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mapBox);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.mapBox_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(606, 421);
            this.splitContainer1.SplitterDistance = 303;
            this.splitContainer1.TabIndex = 12;
            // 
            // HTMLEditor
            // 
            this.HTMLEditor.AllowWebBrowserDrop = false;
            this.HTMLEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HTMLEditor.Location = new System.Drawing.Point(0, 0);
            this.HTMLEditor.MinimumSize = new System.Drawing.Size(20, 20);
            this.HTMLEditor.Name = "HTMLEditor";
            this.HTMLEditor.ScriptErrorsSuppressed = true;
            this.HTMLEditor.Size = new System.Drawing.Size(303, 421);
            this.HTMLEditor.TabIndex = 0;
            // 
            // flpPreview
            // 
            this.flpPreview.AutoSize = true;
            this.flpPreview.Controls.Add(this.btnPreview);
            this.flpPreview.Controls.Add(this.btnPrintPreview);
            this.flpPreview.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpPreview.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpPreview.Location = new System.Drawing.Point(0, 446);
            this.flpPreview.Name = "flpPreview";
            this.flpPreview.Size = new System.Drawing.Size(606, 29);
            this.flpPreview.TabIndex = 11;
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(453, 3);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(150, 23);
            this.btnPreview.TabIndex = 10;
            this.btnPreview.Text = "Preview QR Code";
            this.btnPreview.UseVisualStyleBackColor = true;
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Location = new System.Drawing.Point(297, 3);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(150, 23);
            this.btnPrintPreview.TabIndex = 11;
            this.btnPrintPreview.Text = "Print Preview";
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // toolContents
            // 
            this.toolContents.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.tsbImage,
            this.tsbAudio,
            this.toolStripSeparator3,
            this.tsbBold,
            this.tsbItalics,
            this.btn_Underline});
            this.toolContents.Location = new System.Drawing.Point(0, 0);
            this.toolContents.Name = "toolContents";
            this.toolContents.Size = new System.Drawing.Size(606, 25);
            this.toolContents.TabIndex = 8;
            this.toolContents.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.CheckOnClick = true;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripButton1.Text = "Edit";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbImage
            // 
            this.tsbImage.Image = ((System.Drawing.Image)(resources.GetObject("tsbImage.Image")));
            this.tsbImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImage.Name = "tsbImage";
            this.tsbImage.Size = new System.Drawing.Size(57, 22);
            this.tsbImage.Text = "Image";
            // 
            // tsbAudio
            // 
            this.tsbAudio.Image = ((System.Drawing.Image)(resources.GetObject("tsbAudio.Image")));
            this.tsbAudio.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAudio.Name = "tsbAudio";
            this.tsbAudio.Size = new System.Drawing.Size(54, 22);
            this.tsbAudio.Text = "Audio";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbBold
            // 
            this.tsbBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBold.Name = "tsbBold";
            this.tsbBold.Size = new System.Drawing.Size(31, 22);
            this.tsbBold.Text = "Bold";
            // 
            // tsbItalics
            // 
            this.tsbItalics.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbItalics.Name = "tsbItalics";
            this.tsbItalics.Size = new System.Drawing.Size(39, 22);
            this.tsbItalics.Text = "Italics";
            // 
            // btn_Underline
            // 
            this.btn_Underline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_Underline.Image = ((System.Drawing.Image)(resources.GetObject("btn_Underline.Image")));
            this.btn_Underline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Underline.Name = "btn_Underline";
            this.btn_Underline.Size = new System.Drawing.Size(56, 22);
            this.btn_Underline.Text = "Underline";
            this.btn_Underline.Click += new System.EventHandler(this.btn_Underline_Click);
            // 
            // splitter3
            // 
            this.splitter3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter3.Enabled = false;
            this.splitter3.Location = new System.Drawing.Point(0, 26);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(606, 8);
            this.splitter3.TabIndex = 13;
            this.splitter3.TabStop = false;
            // 
            // tlpDataFields
            // 
            this.tlpDataFields.AutoSize = true;
            this.tlpDataFields.ColumnCount = 6;
            this.tlpDataFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpDataFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpDataFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpDataFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpDataFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpDataFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpDataFields.Controls.Add(this.txtY, 5, 0);
            this.tlpDataFields.Controls.Add(this.lblY, 4, 0);
            this.tlpDataFields.Controls.Add(this.txtX, 3, 0);
            this.tlpDataFields.Controls.Add(this.lblX, 2, 0);
            this.tlpDataFields.Controls.Add(this.txtTitle, 1, 0);
            this.tlpDataFields.Controls.Add(this.lblTitle, 0, 0);
            this.tlpDataFields.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpDataFields.Location = new System.Drawing.Point(0, 0);
            this.tlpDataFields.Name = "tlpDataFields";
            this.tlpDataFields.RowCount = 1;
            this.tlpDataFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDataFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tlpDataFields.Size = new System.Drawing.Size(606, 26);
            this.tlpDataFields.TabIndex = 9;
            // 
            // txtY
            // 
            this.txtY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtY.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtY.Location = new System.Drawing.Point(517, 3);
            this.txtY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(86, 20);
            this.txtY.TabIndex = 9;
            this.txtY.ValueChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblY.Location = new System.Drawing.Point(467, 3);
            this.lblY.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(44, 13);
            this.lblY.TabIndex = 7;
            this.lblY.Text = "Y:";
            this.lblY.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtX
            // 
            this.txtX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtX.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtX.Location = new System.Drawing.Point(376, 3);
            this.txtX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(85, 20);
            this.txtX.TabIndex = 8;
            this.txtX.ValueChanged += new System.EventHandler(this.txt_ValueChanged);
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblX.Location = new System.Drawing.Point(326, 3);
            this.lblX.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(44, 13);
            this.lblX.TabIndex = 6;
            this.lblX.Text = "X:";
            this.lblX.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtTitle
            // 
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTitle.Location = new System.Drawing.Point(53, 3);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(267, 20);
            this.txtTitle.TabIndex = 3;
            this.txtTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTitle_KeyDown);
            this.txtTitle.Leave += new System.EventHandler(this.txtTitle_FocusLeft);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(3, 3);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(44, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title:";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter1.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.splitter1.Location = new System.Drawing.Point(168, 8);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(8, 509);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Enabled = false;
            this.splitter2.Location = new System.Drawing.Point(0, 8);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(8, 509);
            this.splitter2.TabIndex = 3;
            this.splitter2.TabStop = false;
            // 
            // splitter5
            // 
            this.splitter5.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter5.Enabled = false;
            this.splitter5.Location = new System.Drawing.Point(782, 8);
            this.splitter5.Name = "splitter5";
            this.splitter5.Size = new System.Drawing.Size(8, 509);
            this.splitter5.TabIndex = 11;
            this.splitter5.TabStop = false;
            // 
            // splitter6
            // 
            this.splitter6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter6.Enabled = false;
            this.splitter6.Location = new System.Drawing.Point(0, 517);
            this.splitter6.Name = "splitter6";
            this.splitter6.Size = new System.Drawing.Size(790, 8);
            this.splitter6.TabIndex = 12;
            this.splitter6.TabStop = false;
            // 
            // splitter4
            // 
            this.splitter4.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter4.Enabled = false;
            this.splitter4.Location = new System.Drawing.Point(0, 0);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(790, 8);
            this.splitter4.TabIndex = 10;
            this.splitter4.TabStop = false;
            // 
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.Visible = false;
            // 
            // mapBox
            // 
            this.mapBox.AutoSize = true;
            this.mapBox.Image = global::BaconBuilder.Properties.Resources.map;
            this.mapBox.Location = new System.Drawing.Point(0, 0);
            this.mapBox.MarkerText = "You are here";
            this.mapBox.Name = "mapBox";
            this.mapBox.Size = new System.Drawing.Size(300, 356);
            this.mapBox.TabIndex = 0;
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
            this.Text = "HTML Content Generator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.pnlDirectories.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flpPreview.ResumeLayout(false);
            this.toolContents.ResumeLayout(false);
            this.toolContents.PerformLayout();
            this.tlpDataFields.ResumeLayout(false);
            this.tlpDataFields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtX)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private MenuStrip menuStrip;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem toolStripSync;
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
        private TextBox txtTitle;
		private ToolStripMenuItem editToolStripMenuItem;
		private ToolStripMenuItem viewToolStripMenuItem;
		private ToolStripMenuItem toolsToolStripMenuItem;
		private ToolStripMenuItem optionsToolStripMenuItem;
		private Button btnPreview;
		private FlowLayoutPanel flpPreview;
		private Button btnPrintPreview;
		private ToolStripMenuItem exitToolStripMenuItem;
		private ToolStripButton tsbImage;
		private ToolStripButton tsbBold;
		private ToolStripButton tsbItalics;
		private StatusStrip statusStrip;
		private ToolStripSeparator toolStripSeparator2;
		private Splitter splitter2;
		private Splitter splitter4;
		private Splitter splitter6;
		private Splitter splitter5;
        private ToolStripSeparator toolStripSeparator3;
		private ListView listViewContents;
		private ImageList imageList;
		private TableLayoutPanel tableLayoutPanel1;
		private Button btnRemoveFile;
		private Button btnAddFile;
		private System.Drawing.Printing.PrintDocument printDocument;
		private PrintPreviewDialog printPreviewDialog;
		private ToolStripButton tsbAudio;
		private ColumnHeader columnHeader1;
		private ToolStripSeparator toolStripSeparator4;
        private SplitContainer splitContainer1;
        private WebBrowser HTMLEditor;
        private ToolStripButton btn_Underline;
		private ToolStripButton toolStripButton1;
		private ToolStripSeparator toolStripSeparator1;
		private NumericUpDown txtY;
		private Label lblY;
		private NumericUpDown txtX;
        private Label lblX;
        private BaconBuilder.Component.MapBox mapBox;

	}
}

