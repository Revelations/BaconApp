using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BaconBuilder.View;
using BaconBuilder.Model;

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
			Application.Run(new SplashScreen());
            Application.Run(new MainWindow());
        }
    }
}
