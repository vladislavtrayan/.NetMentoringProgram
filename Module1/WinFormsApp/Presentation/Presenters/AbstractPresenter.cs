using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Presentation.Forms;

namespace Presentation.Presenters
{
    public abstract class AbstractPresenter<T> where T :IView
    {
        protected IKernel Kernel { get; set; }
        protected T View { get; set; }

        public void Run()
        {
            View.Show();
        }

        public AbstractPresenter(IKernel kernel, T view)
        {
            Kernel = kernel;
            View = view;
        }
    }
}
