using Gecko;
using Gecko.DOM;
using Gecko.DOM.HTML;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TestApp
{
	public partial class MainForm : Form
	{
		private WebView pageView;
		private WebView tab2View;
		private string _defaultTitle;

		public MainForm()
		{
			InitializeComponent();
			_defaultTitle = this.Text;
			lblStatus.Text = string.Empty;

			

			pageView = new WebView();
			pageView.Dock = DockStyle.Fill;
			pageView.Navigating += pageView_Navigating;
			pageView.FrameNavigating += pageView_FrameNavigating;
			pageView.Navigated += pageView_Navigated;
			pageView.DocumentCompleted += pageView_DocumentCompleted;
			pageView.StatusTextChanged += pageView_StatusTextChanged;
			pageView.DocumentTitleChanged += pageView_DocumentTitleChanged;
			pageView.CreateWindow += pageView_CreateWindow;

			tab1.Controls.Add(pageView);

			tab2View = new WebView();
			tab2View.Dock = DockStyle.Fill;
			tab2.Controls.Add(tab2View);
		}

		void pageView_CreateWindow(object sender, GeckoCreateWindowEventArgs e)
		{
			tabs.SelectTab(tab2);
			e.Window = tab2View.Window;
		}

		void pageView_DocumentTitleChanged(object sender, EventArgs e)
		{
			string title = pageView.Window.Document.Title;
			if (!string.IsNullOrEmpty(title))
				this.Text = _defaultTitle + ": " + title;
			else
				this.Text = _defaultTitle;
		}


		void pageView_Navigating(object sender, GeckoNavigatingEventArgs e)
		{
			Console.WriteLine("Navigating");
		}

		void pageView_FrameNavigating(object sender, GeckoNavigatingEventArgs e)
		{
			Console.WriteLine("FrameNavigating");
		}

		private void pageView_Navigated(object sender, GeckoNavigatedEventArgs e)
		{
			Console.WriteLine("Navigated");
			txtAddress.Text = e.Url.AbsoluteUri;
		}

		void pageView_DocumentCompleted(object sender, GeckoDocumentCompletedEventArgs e)
		{
			Console.WriteLine("DocumentCompleted");
		}

		void pageView_StatusTextChanged(object sender, EventArgs e)
		{
			lblStatus.Text = pageView.StatusText;
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			try
			{
				//pageView.Navigate("http://localhost:12111/geckofx/");
				pageView.Navigate(txtAddress.Text, GeckoLoadFlags.None, "https://example.com", ReferrerPolicy.UnsafeUrl, null, null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void txtAddress_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode == Keys.Enter)
					pageView.Navigate(txtAddress.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			pageView.PrintPreview();

			//var ds = Xpcom.QueryInterface<nsIDocShell>(pageView.Window.Instance);

			//var wbg = Xpcom.QueryInterface<nsIXULWindow>(ds.GetTreeOwnerAttribute()).GetXULBrowserWindowAttribute();
			//var wb = Xpcom.QueryInterface<IWebView>(wbg);
			//wb = null;
		}

		private void btnGCCollect_Click(object sender, EventArgs e)
		{
			var mem = Xpcom.GetService<nsIMemory>(Contracts.MemoryService);
			mem.HeapMinimize(true);
			Xpcom.FreeComObject(ref mem);

			CacheStorageService.GetService().Clear();
		}

		private void btnTestParser_Click(object sender, EventArgs e)
		{
			using (var parser = GeckoParser.Create())
			{
				var document = (GeckoHTMLDocument)parser.Parse("<!DOCTYPE html><html><head><title>hello page</title></head><body id='idhello'><!-- comment -->Hello, World!<iframe src='http://google.com'></iframe></body></html>");
				string docName = document.Doctype.Name;
				var view = document.DefaultView;

				document = null;
			}
		}
	}
}
