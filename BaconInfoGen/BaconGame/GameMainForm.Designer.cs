namespace BaconGame
{
    partial class GameMainForm
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
		[System.Diagnostics.DebuggerNonUserCode, System.CodeDom.Compiler.GeneratedCode("Winform Designer", "VS2010 SP1")]
		private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameMainForm));
			this.splitter = new System.Windows.Forms.Splitter();
			this.splitter4 = new System.Windows.Forms.Splitter();
			this.splitter3 = new System.Windows.Forms.Splitter();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.fileView = new System.Windows.Forms.ListView();
			this.fileHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.toolStripFile = new System.Windows.Forms.ToolStripMenuItem();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label6 = new System.Windows.Forms.Label();
			this.textBoxAnswer4 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxAnswer3 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxAnswer2 = new System.Windows.Forms.TextBox();
			this.textBoxAnswer1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxQuestion = new System.Windows.Forms.TextBox();
			this.comboBoxCorrectAnswer = new System.Windows.Forms.ComboBox();
			this.splitter5 = new System.Windows.Forms.Splitter();
			this.questionView = new System.Windows.Forms.ListView();
			this.numberHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.questionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.answerHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripAdd = new System.Windows.Forms.ToolStripButton();
			this.toolStripDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSync = new System.Windows.Forms.ToolStripButton();
			this.toolStripExit = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitter
			// 
			this.splitter.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitter.Location = new System.Drawing.Point(170, 32);
			this.splitter.Name = "splitter";
			this.splitter.Size = new System.Drawing.Size(8, 500);
			this.splitter.TabIndex = 12;
			this.splitter.TabStop = false;
			this.splitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter_SplitterMoved);
			// 
			// splitter4
			// 
			this.splitter4.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitter4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter4.Enabled = false;
			this.splitter4.Location = new System.Drawing.Point(8, 532);
			this.splitter4.Name = "splitter4";
			this.splitter4.Size = new System.Drawing.Size(768, 8);
			this.splitter4.TabIndex = 11;
			this.splitter4.TabStop = false;
			// 
			// splitter3
			// 
			this.splitter3.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter3.Enabled = false;
			this.splitter3.Location = new System.Drawing.Point(8, 24);
			this.splitter3.Name = "splitter3";
			this.splitter3.Size = new System.Drawing.Size(768, 8);
			this.splitter3.TabIndex = 10;
			this.splitter3.TabStop = false;
			// 
			// splitter2
			// 
			this.splitter2.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter2.Enabled = false;
			this.splitter2.Location = new System.Drawing.Point(776, 24);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(8, 516);
			this.splitter2.TabIndex = 9;
			this.splitter2.TabStop = false;
			// 
			// splitter1
			// 
			this.splitter1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitter1.Enabled = false;
			this.splitter1.Location = new System.Drawing.Point(0, 24);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(8, 516);
			this.splitter1.TabIndex = 8;
			this.splitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.fileView);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(8, 32);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(162, 500);
			this.panel1.TabIndex = 13;
			// 
			// fileView
			// 
			this.fileView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.fileView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileHeader});
			this.fileView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fileView.FullRowSelect = true;
			this.fileView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.fileView.HideSelection = false;
			this.fileView.Location = new System.Drawing.Point(0, 0);
			this.fileView.MultiSelect = false;
			this.fileView.Name = "fileView";
			this.fileView.Size = new System.Drawing.Size(162, 500);
			this.fileView.SmallImageList = this.imageList;
			this.fileView.TabIndex = 0;
			this.fileView.UseCompatibleStateImageBehavior = false;
			this.fileView.View = System.Windows.Forms.View.Details;
			this.fileView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.fileView_ItemSelectionChanged);
			// 
			// fileHeader
			// 
			this.fileHeader.Text = "Question Files";
			this.fileHeader.Width = 162;
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "file_info.ico");
			this.imageList.Images.SetKeyName(1, "file_edit.ico");
			this.imageList.Images.SetKeyName(2, "file.ico");
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 540);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(784, 22);
			this.statusStrip1.TabIndex = 14;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripFile});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(784, 24);
			this.menuStrip.TabIndex = 15;
			this.menuStrip.Text = "menuStrip1";
			// 
			// toolStripFile
			// 
			this.toolStripFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripExit});
			this.toolStripFile.Name = "toolStripFile";
			this.toolStripFile.Size = new System.Drawing.Size(37, 20);
			this.toolStripFile.Text = "&File";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.tableLayoutPanel1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(178, 229);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(598, 303);
			this.panel2.TabIndex = 16;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.textBoxAnswer4, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.textBoxAnswer3, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.textBoxAnswer2, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.textBoxAnswer1, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.textBoxQuestion, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxCorrectAnswer, 1, 5);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.18182F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.18182F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.18182F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.18182F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(598, 303);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Right;
			this.label6.Location = new System.Drawing.Point(18, 259);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(79, 44);
			this.label6.TabIndex = 11;
			this.label6.Text = "Correct Answer";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxAnswer4
			// 
			this.textBoxAnswer4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxAnswer4.Location = new System.Drawing.Point(103, 215);
			this.textBoxAnswer4.Multiline = true;
			this.textBoxAnswer4.Name = "textBoxAnswer4";
			this.textBoxAnswer4.Size = new System.Drawing.Size(492, 41);
			this.textBoxAnswer4.TabIndex = 10;
			this.textBoxAnswer4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
			this.textBoxAnswer4.Leave += new System.EventHandler(this.textBoxQuestion_Leave);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Right;
			this.label5.Location = new System.Drawing.Point(31, 212);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(66, 47);
			this.label5.TabIndex = 9;
			this.label5.Text = "Answer Four";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxAnswer3
			// 
			this.textBoxAnswer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxAnswer3.Location = new System.Drawing.Point(103, 168);
			this.textBoxAnswer3.Multiline = true;
			this.textBoxAnswer3.Name = "textBoxAnswer3";
			this.textBoxAnswer3.Size = new System.Drawing.Size(492, 41);
			this.textBoxAnswer3.TabIndex = 8;
			this.textBoxAnswer3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
			this.textBoxAnswer3.Leave += new System.EventHandler(this.textBoxQuestion_Leave);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Right;
			this.label4.Location = new System.Drawing.Point(24, 165);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(73, 47);
			this.label4.TabIndex = 7;
			this.label4.Text = "Answer Three";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Right;
			this.label3.Location = new System.Drawing.Point(31, 118);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(66, 47);
			this.label3.TabIndex = 6;
			this.label3.Text = "Answer Two";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxAnswer2
			// 
			this.textBoxAnswer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxAnswer2.Location = new System.Drawing.Point(103, 121);
			this.textBoxAnswer2.Multiline = true;
			this.textBoxAnswer2.Name = "textBoxAnswer2";
			this.textBoxAnswer2.Size = new System.Drawing.Size(492, 41);
			this.textBoxAnswer2.TabIndex = 5;
			this.textBoxAnswer2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
			this.textBoxAnswer2.Leave += new System.EventHandler(this.textBoxQuestion_Leave);
			// 
			// textBoxAnswer1
			// 
			this.textBoxAnswer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxAnswer1.Location = new System.Drawing.Point(103, 74);
			this.textBoxAnswer1.Multiline = true;
			this.textBoxAnswer1.Name = "textBoxAnswer1";
			this.textBoxAnswer1.Size = new System.Drawing.Size(492, 41);
			this.textBoxAnswer1.TabIndex = 3;
			this.textBoxAnswer1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
			this.textBoxAnswer1.Leave += new System.EventHandler(this.textBoxQuestion_Leave);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Right;
			this.label2.Location = new System.Drawing.Point(32, 71);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 47);
			this.label2.TabIndex = 2;
			this.label2.Text = "Answer One";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Right;
			this.label1.Location = new System.Drawing.Point(24, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 71);
			this.label1.TabIndex = 0;
			this.label1.Text = "Question Text";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxQuestion
			// 
			this.textBoxQuestion.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxQuestion.Location = new System.Drawing.Point(103, 3);
			this.textBoxQuestion.Multiline = true;
			this.textBoxQuestion.Name = "textBoxQuestion";
			this.textBoxQuestion.Size = new System.Drawing.Size(492, 65);
			this.textBoxQuestion.TabIndex = 1;
			this.textBoxQuestion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
			this.textBoxQuestion.Leave += new System.EventHandler(this.textBoxQuestion_Leave);
			// 
			// comboBoxCorrectAnswer
			// 
			this.comboBoxCorrectAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxCorrectAnswer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCorrectAnswer.FormattingEnabled = true;
			this.comboBoxCorrectAnswer.Location = new System.Drawing.Point(103, 270);
			this.comboBoxCorrectAnswer.Name = "comboBoxCorrectAnswer";
			this.comboBoxCorrectAnswer.Size = new System.Drawing.Size(492, 21);
			this.comboBoxCorrectAnswer.TabIndex = 12;
			this.comboBoxCorrectAnswer.SelectedIndexChanged += new System.EventHandler(this.comboBoxCorrectAnswer_SelectedIndexChanged);
			// 
			// splitter5
			// 
			this.splitter5.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitter5.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter5.Enabled = false;
			this.splitter5.Location = new System.Drawing.Point(178, 221);
			this.splitter5.Name = "splitter5";
			this.splitter5.Size = new System.Drawing.Size(598, 8);
			this.splitter5.TabIndex = 17;
			this.splitter5.TabStop = false;
			// 
			// questionView
			// 
			this.questionView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.questionView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.numberHeader,
            this.questionHeader,
            this.answerHeader});
			this.questionView.Dock = System.Windows.Forms.DockStyle.Top;
			this.questionView.FullRowSelect = true;
			this.questionView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.questionView.HideSelection = false;
			this.questionView.Location = new System.Drawing.Point(178, 80);
			this.questionView.MultiSelect = false;
			this.questionView.Name = "questionView";
			this.questionView.Size = new System.Drawing.Size(598, 141);
			this.questionView.SmallImageList = this.imageList;
			this.questionView.TabIndex = 18;
			this.questionView.UseCompatibleStateImageBehavior = false;
			this.questionView.View = System.Windows.Forms.View.Details;
			this.questionView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.questionView_ItemSelectionChanged);
			// 
			// numberHeader
			// 
			this.numberHeader.Text = "#";
			this.numberHeader.Width = 50;
			// 
			// questionHeader
			// 
			this.questionHeader.Text = "Question Text";
			this.questionHeader.Width = 350;
			// 
			// answerHeader
			// 
			this.answerHeader.Text = "Correct Answer";
			this.answerHeader.Width = 250;
			// 
			// toolStrip
			// 
			this.toolStrip.AutoSize = false;
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripAdd,
            this.toolStripDelete,
            this.toolStripSeparator3,
            this.toolStripSync});
			this.toolStrip.Location = new System.Drawing.Point(178, 32);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip.Size = new System.Drawing.Size(598, 48);
			this.toolStrip.TabIndex = 19;
			// 
			// toolStripAdd
			// 
			this.toolStripAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripAdd.Image")));
			this.toolStripAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripAdd.Margin = new System.Windows.Forms.Padding(4, 1, 4, 2);
			this.toolStripAdd.Name = "toolStripAdd";
			this.toolStripAdd.Size = new System.Drawing.Size(36, 45);
			this.toolStripAdd.ToolTipText = "Add Question - Creates a new blank quesition in the currently selected file.";
			this.toolStripAdd.Click += new System.EventHandler(this.toolStripAdd_Click);
			// 
			// toolStripDelete
			// 
			this.toolStripDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDelete.Image")));
			this.toolStripDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDelete.Margin = new System.Windows.Forms.Padding(4, 1, 4, 2);
			this.toolStripDelete.Name = "toolStripDelete";
			this.toolStripDelete.Size = new System.Drawing.Size(36, 45);
			this.toolStripDelete.ToolTipText = "Delete Question - Deletes the selected question.";
			this.toolStripDelete.Click += new System.EventHandler(this.toolStripDelete_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
			// 
			// toolStripSync
			// 
			this.toolStripSync.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripSync.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSync.Image")));
			this.toolStripSync.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripSync.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSync.Margin = new System.Windows.Forms.Padding(4, 1, 4, 2);
			this.toolStripSync.Name = "toolStripSync";
			this.toolStripSync.Size = new System.Drawing.Size(36, 45);
			this.toolStripSync.ToolTipText = "Synchronise - Uploads question files to the server.";
			this.toolStripSync.Click += new System.EventHandler(this.toolStripSync_Click);
			// 
			// toolStripExit
			// 
			this.toolStripExit.Name = "toolStripExit";
			this.toolStripExit.Size = new System.Drawing.Size(152, 22);
			this.toolStripExit.Text = "E&xit";
			this.toolStripExit.Click += new System.EventHandler(this.toolStripExit_Click);
			// 
			// GameMainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.splitter5);
			this.Controls.Add(this.questionView);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.splitter);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.splitter4);
			this.Controls.Add(this.splitter3);
			this.Controls.Add(this.splitter2);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip);
			this.MainMenuStrip = this.menuStrip;
			this.MinimumSize = new System.Drawing.Size(800, 600);
			this.Name = "GameMainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Game Creator";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameMainForm_FormClosing);
			this.Shown += new System.EventHandler(this.GameMainForm_Shown);
			this.panel1.ResumeLayout(false);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Splitter splitter;
        private System.Windows.Forms.Splitter splitter4;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ListView fileView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter5;
        private System.Windows.Forms.ListView questionView;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripSync;
        private System.Windows.Forms.ColumnHeader fileHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxAnswer4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxAnswer3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAnswer2;
        private System.Windows.Forms.TextBox textBoxAnswer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxQuestion;
        private System.Windows.Forms.ComboBox comboBoxCorrectAnswer;
        private System.Windows.Forms.ColumnHeader numberHeader;
        private System.Windows.Forms.ToolStripMenuItem toolStripFile;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ColumnHeader questionHeader;
        private System.Windows.Forms.ColumnHeader answerHeader;
        private System.Windows.Forms.ToolStripButton toolStripAdd;
		private System.Windows.Forms.ToolStripMenuItem toolStripExit;
    }
}

