using System.ComponentModel;
using System.Windows.Forms;

namespace Common
{
	public interface ISyncDialog
	{
		ProgressBar ProgressBar { get; }

		BackgroundWorker Worker { get; }
	}
}
