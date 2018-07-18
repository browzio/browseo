using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseoFX.Base.Wpf.FX.ServiceInterface
{
    public interface IWebviewListenerService
    {
        void GloableBrowserDocumentLoaded();
        void GloableViewLoadDone();
    }
}
