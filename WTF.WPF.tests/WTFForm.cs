using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WTF.WPF.tests
{
    [SerializableAttribute]
    public partial class WTFForm : Form
    {
        public WTFForm()
        {
            InitializeComponent();
            this.Load += WTFForm_Load;
        }

       
        [SecurityCriticalAttribute]
        [ComVisibleAttribute(true)]
        [SecurityPermissionAttribute(SecurityAction.InheritanceDemand,Flags = SecurityPermissionFlag.Infrastructure)]
        private void WTFForm_Load(object sender, EventArgs e)
        {
            var ffexePath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";

            var info = new ProcessStartInfo
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                Arguments = "",
                FileName = ffexePath,
            };
            Process process = new Process();
            process.StartInfo = info;
            process.Start();

            //process.CreateObjRef
            var handle = process.MainModule.BaseAddress;
            var ffObj = System.Runtime.InteropServices.Marshal.GetObjectForIUnknown(handle);
            var form = Control.FromHandle(process.MainWindowHandle);
            this.Controls.Add(form);

            //WindowInteropHelper windowHwnd = new WindowInteropHelper(host);
            //host.Children.Add(form);
            //process.WaitForExit();
        }
    }
}
