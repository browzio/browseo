using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WTF.WPF.tests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            new WTFForm().Show();
            //var ffexePath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";

            //var info = new ProcessStartInfo
            //{
            //    CreateNoWindow = true,
            //    UseShellExecute = false,
            //    Arguments = "-no-remote -ProfileManager",
            //    FileName = ffexePath
            //};
            //Process process = Process.Start(info);

            //var handle = process.MainWindowHandle;
            //var form = HwndSource.FromHwnd(handle);

            ////WindowInteropHelper windowHwnd = new WindowInteropHelper(host);
            ////host.Children.Add(form);
            ////process.WaitForExit();
        }
    }
}
