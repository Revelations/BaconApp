using System.Windows.Forms;

namespace BaconApp
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.pnlDirectories = new System.Windows.Forms.Panel();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.pnlContentEditor = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.splitter3 = new System.Windows.Forms.Splitter();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.pnlDirectories.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.pnlContentEditor.SuspendLayout();
			this.panel2.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
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
            this.printToolStripMenuItem,
            this.printAllModifiedToolStripMenuItem,
            this.printAllToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// openDatabaseToolStripMenuItem
			// 
			this.openDatabaseToolStripMenuItem.Name = "openDatabaseToolStripMenuItem";
			this.openDatabaseToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.openDatabaseToolStripMenuItem.Text = "Open Database...";
			// 
			// printToolStripMenuItem
			// 
			this.printToolStripMenuItem.Name = "printToolStripMenuItem";
			this.printToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.printToolStripMenuItem.Text = "Print...";
			// 
			// printAllModifiedToolStripMenuItem
			// 
			this.printAllModifiedToolStripMenuItem.Name = "printAllModifiedToolStripMenuItem";
			this.printAllModifiedToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.printAllModifiedToolStripMenuItem.Text = "Print All Modified...";
			// 
			// printAllToolStripMenuItem
			// 
			this.printAllToolStripMenuItem.Name = "printAllToolStripMenuItem";
			this.printAllToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.printAllToolStripMenuItem.Text = "Print All...";
			// 
			// listView1
			// 
			this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(160, 549);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			// 
			// pnlDirectories
			// 
			this.pnlDirectories.BackColor = System.Drawing.Color.Green;
			this.pnlDirectories.Controls.Add(this.listView1);
			this.pnlDirectories.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlDirectories.Location = new System.Drawing.Point(0, 0);
			this.pnlDirectories.Name = "pnlDirectories";
			this.pnlDirectories.Size = new System.Drawing.Size(160, 549);
			this.pnlDirectories.TabIndex = 8;
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlContentEditor);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitter1);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlDirectories);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(792, 549);
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
			this.pnlContentEditor.Controls.Add(this.panel2);
			this.pnlContentEditor.Controls.Add(this.splitter3);
			this.pnlContentEditor.Controls.Add(this.tableLayoutPanel1);
			this.pnlContentEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContentEditor.Location = new System.Drawing.Point(168, 0);
			this.pnlContentEditor.Name = "pnlContentEditor";
			this.pnlContentEditor.Size = new System.Drawing.Size(624, 549);
			this.pnlContentEditor.TabIndex = 9;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.toolStrip1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 60);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(624, 489);
			this.panel2.TabIndex = 12;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(624, 25);
			this.toolStrip1.TabIndex = 8;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(57, 22);
			this.toolStripButton1.Text = "Image";
			// 
			// splitter3
			// 
			this.splitter3.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter3.Enabled = false;
			this.splitter3.Location = new System.Drawing.Point(0, 52);
			this.splitter3.Name = "splitter3";
			this.splitter3.Size = new System.Drawing.Size(624, 8);
			this.splitter3.TabIndex = 13;
			this.splitter3.TabStop = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 5;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.textBox3, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.textBox2, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.button1, 4, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(624, 52);
			this.tableLayoutPanel1.TabIndex = 9;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Title:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textBox3
			// 
			this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox3.Location = new System.Drawing.Point(309, 29);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(200, 20);
			this.textBox3.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 29);
			this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "X:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(259, 29);
			this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Y:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textBox2
			// 
			this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox2.Location = new System.Drawing.Point(53, 29);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(200, 20);
			this.textBox2.TabIndex = 4;
			// 
			// textBox1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.textBox1, 3);
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Location = new System.Drawing.Point(53, 3);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(456, 20);
			this.textBox1.TabIndex = 3;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(520, 29);
			this.button1.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(95, 20);
			this.button1.TabIndex = 6;
			this.button1.Text = "Map Preview";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// splitter1
			// 
			this.splitter1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitter1.Location = new System.Drawing.Point(160, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(8, 549);
			this.splitter1.TabIndex = 9;
			this.splitter1.TabStop = false;
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
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.optionsToolStripMenuItem.Text = "&Options";
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
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ContentPanel.PerformLayout();
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.pnlContentEditor.ResumeLayout(false);
			this.pnlContentEditor.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private MenuStrip menuStrip1;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem openDatabaseToolStripMenuItem;
		private ListView listView1;
		private Panel pnlDirectories;
		private ToolStripContainer toolStripContainer1;
		private Panel pnlContentEditor;
		private Splitter splitter1;
		private Splitter splitter3;
		private Panel panel2;
		private ToolStrip toolStrip1;
		private ToolStripButton toolStripButton1;
		private ToolStripMenuItem printToolStripMenuItem;
		private ToolStripMenuItem printAllModifiedToolStripMenuItem;
		private ToolStripMenuItem printAllToolStripMenuItem;
		private TableLayoutPanel tableLayoutPanel1;
		private Label label1;
		private TextBox textBox3;
		private Label label2;
		private Label label3;
		private TextBox textBox2;
		private TextBox textBox1;
		private Button button1;
		private ToolStripMenuItem editToolStripMenuItem;
		private ToolStripMenuItem viewToolStripMenuItem;
		private ToolStripMenuItem toolsToolStripMenuItem;
		private ToolStripMenuItem optionsToolStripMenuItem;

	}
}

