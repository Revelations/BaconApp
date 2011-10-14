using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaconBuilder.View;
using BaconFeedback;
using BaconGame;

namespace BaconSplash
{
	public partial class Launcher : Form
	{
		public Launcher()
		{
			InitializeComponent();
		}

		private void btnInfo_Click(object sender, EventArgs e)
		{
			MainWindow m = new MainWindow();
			m.ShowDialog();
		}

		private void btnGame_Click(object sender, EventArgs e)
		{
			GameMainForm g = new GameMainForm();
			g.ShowDialog();
		}

		private void btnFeedback_Click(object sender, EventArgs e)
		{
			FeedbackMainForm f = new FeedbackMainForm();
			f.ShowDialog();
		}
	}
}
