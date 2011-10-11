using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaconGame
{
    public interface IGameMainForm
    {
        ListView FileView { get; }
        ListView QuestionView { get; }

        ComboBox CorrectAnswer { get; }

        string QuestionText { get; set; }
        string Answer1Text { get; set; }
        string Answer2Text { get; set; }
        string Answer3Text { get; set; }
        string Answer4Text { get; set; }
    }
}
