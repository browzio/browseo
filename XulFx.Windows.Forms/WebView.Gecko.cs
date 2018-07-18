using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.GUI;
using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Interop;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Diagnostics;
using Gecko.IO;

namespace Gecko.Windows
{
    //partial class WebView // : IWebView
    //{

    //    protected GeckoWebNavigation WebNav
    //    {
    //        get
    //        {
    //            return this.Window.QueryInterface<nsIWebNavigation>().Wrap(GeckoWebNavigation.Create);
    //        }
    //    }

    //    private void CreateContentView()
    //    {
    //        IntPtr hwndView = this.Handle;

    //        _contentView = GetOrCreateGlobalView("_contentView", out _contentViewElement);
    //        //Debug.Assert(_contentView != null, "_contentView is null");
    //        if (_contentView == null)
    //            _contentView = _widget.View;

    //        nsIDocShell docShell = null;
    //        nsIWebProgress progress = null;
    //        try
    //        {
    //            docShell = _widget.View.QueryInterface<nsIDocShell>();

    //            progress = Xpcom.QueryInterface<nsIWebProgress>(docShell);
    //            progress.AddProgressListener(_wbGlue, (uint)(nsIWebProgressConsts.NOTIFY_STATE_ALL | nsIWebProgressConsts.NOTIFY_ALL));
    //        }
    //        finally
    //        {
    //            Xpcom.FreeComObject(ref progress);
    //            Xpcom.FreeComObject(ref docShell);
    //        }

    //        _eventTarget = _contentView.QueryInterface<nsIDocShell>().GetChromeEventHandlerAttribute().Wrap(GeckoDOMEventTarget.Create);
    //        if (_eventTarget != null)
    //        {
    //            foreach (string eventName in this.HandledDOMEvents)
    //            {
    //                _eventTarget.AddEventListener(eventName, _wbGlue, true, true, 2);
    //            }
    //        }
    //    }



    //    protected void AssertNotPreview()
    //    {
    //        if (_previewMode)
    //            throw new InvalidOperationException();
    //    }

    //    #region IWebView

    //    [BrowsableAttribute(false)]
    //    public virtual GeckoWindow Window
    //    {
    //        get
    //        {
    //            if (this.Handle != IntPtr.Zero) // create control
    //                return _contentView;
    //            return null;
    //        }
    //    }

    //    #endregion

    //}
}
