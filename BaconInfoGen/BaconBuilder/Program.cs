using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BaconBuilder.View;
using BaconBuilder.Model;
using BaconBuilder.Controller;

namespace BaconBuilder
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			//var model = new BaconModel();
			var view = new MainWindow();
			//var controller = new MainViewController(model, view);
			Application.Run(view);

		}
	}
}
