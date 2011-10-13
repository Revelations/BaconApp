using System.ComponentModel;
using System.Windows.Forms;

namespace Common
{
	/// <summary>
	/// Interface that any form wishing to be controlled by a sync presenter should implement.
	/// </summary>
	public interface ISyncDialog
	{
		ProgressBar ProgressBar { get; }

		BackgroundWorker Worker { get; }
	}
}
