using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gecko.GUI;

namespace Gecko.Windows.Services
{
    public interface IWebviewListenerService
    {
        void GloableBrowserDocumentLoaded();
        void GloableViewLoadDone();
        void BuiltWindowCore(GeckoXULWindow _widget);
    }
}
