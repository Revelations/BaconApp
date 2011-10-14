namespace BaconSplash
{
	partial class Launcher
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.btnInfo = new System.Windows.Forms.Button();
			this.btnGame = new System.Windows.Forms.Button();
			this.btnFeedback = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.pictureBox, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnInfo, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.btnGame, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.btnFeedback, 0, 4);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 462);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// pictureBox
			// 
			this.pictureBox.BackColor = System.Drawing.Color.White;
			this.pictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox.BackgroundImage")));
			this.pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox.Location = new System.Drawing.Point(3, 3);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(378, 225);
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			// 
			// btnInfo
			// 
			this.btnInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnInfo.Location = new System.Drawing.Point(20, 264);
			this.btnInfo.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
			this.btnInfo.Name = "btnInfo";
			this.btnInfo.Size = new System.Drawing.Size(344, 49);
			this.btnInfo.TabIndex = 1;
			this.btnInfo.Text = "Information Editor";
			this.btnInfo.UseVisualStyleBackColor = true;
			this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
			// 
			// btnGame
			// 
			this.btnGame.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGame.Location = new System.Drawing.Point(20, 333);
			this.btnGame.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
			this.btnGame.Name = "btnGame";
			this.btnGame.Size = new System.Drawing.Size(344, 49);
			this.btnGame.TabIndex = 2;
			this.btnGame.Text = "Trivia Editor";
			this.btnGame.UseVisualStyleBackColor = true;
			this.btnGame.Click += new System.EventHandler(this.btnGame_Click);
			// 
			// btnFeedback
			// 
			this.btnFeedback.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnFeedback.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFeedback.Location = new System.Drawing.Point(20, 402);
			this.btnFeedback.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
			this.btnFeedback.Name = "btnFeedback";
			this.btnFeedback.Size = new System.Drawing.Size(344, 50);
			this.btnFeedback.TabIndex = 3;
			this.btnFeedback.Text = "Feedback Viewer";
			this.btnFeedback.UseVisualStyleBackColor = true;
			this.btnFeedback.Click += new System.EventHandler(this.btnFeedback_Click);
			// 
			// Launcher
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 462);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.Name = "Launcher";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Application Launcher";
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Button btnInfo;
		private System.Windows.Forms.Button btnGame;
		private System.Windows.Forms.Button btnFeedback;
	}
}

