using System.IO;
using System.Web.UI;
using System.Windows.Forms;

namespace BaconBuilder.View
{
	public partial class MainWindow : Form
	{
		public MainWindow()
		{
			InitializeComponent();
		}

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {

            StringWriter stringWriter = new StringWriter();

            using (HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter))
            {
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Html);

                htmlWriter.RenderEndTag();
            }
        }
	}
}
