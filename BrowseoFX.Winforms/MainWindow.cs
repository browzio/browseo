using Gecko.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gecko;

namespace BrowseoFX.Winforms
{
    public partial class MainWindow : Form
    {
        private WebView pageView;

        public MainWindow()
        {
            InitializeComponent();


            pageView = new WebView();
            pageView.Dock = DockStyle.Fill;
            pageView.Navigating += pageView_Navigating;
            Controls.Add(pageView);
        }

        private void pageView_Navigating(object sender, GeckoNavigatingEventArgs e)
        {
            //using (var obsSvc = Xpcom.GetService2<Gecko.Interfaces.nsIObserverService>(Gecko.Contracts.ObserverService))
            //{
            //    obsSvc.Instance.NotifyObservers(null, "lightweight-theme-changed", null);
            //}
        }
    }
}
