using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaconFeedback
{
    public partial class FeedbackMainForm : Form
    {
        public ListView FolderView { get { return folderView; } }
        public ListView FileView { get { return fileView; } }

        private FeedbackPresenter _presenter;

        public FeedbackMainForm()
        {
            InitializeComponent();

            _presenter = new FeedbackPresenter(this);

            _presenter.PopulateFolderView();
            _presenter.PopulateFileView("Herp");
        }
    }
}
