using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Forms
{
    public interface IMainForm : IView
    {
        event Action ButtonPressed;
        string TextBox { get; set; }
        void ShowMessage(string message);
    }
}
