using System;
using System.Runtime.InteropServices;

namespace BaconBuilder.View
{
	class MyRichTextBox : System.Windows.Forms.RichTextBox
	{
		public void BeginUpdate()
		{
			SendMessage(this.Handle, WM_SETREDRAW, (IntPtr)0, IntPtr.Zero);
		}
		public void EndUpdate()
		{
			SendMessage(this.Handle, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
		}
		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
		private const int WM_SETREDRAW = 0x0b;
	}

}
