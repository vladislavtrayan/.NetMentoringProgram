using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Presentation.Forms;
using Core;

namespace Presentation.Presenters
{
    public class MainFormPresenter : AbstractPresenter<IMainForm>
    {
        public MainFormPresenter(IKernel kernel, IMainForm view) : base(kernel, view)
        {
            view.ButtonPressed += ShowMessage;
        }

        private void ShowMessage()
        {
            if(!string.IsNullOrEmpty(View.TextBox))
                View.ShowMessage(StringFormatter.AddCurrentTimeToString(View.TextBox));
            else
                View.ShowMessage("Pls enter your name!");
        }
    }
}
