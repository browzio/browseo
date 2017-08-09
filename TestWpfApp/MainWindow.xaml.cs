using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWpfApp
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void btnGo_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				pageView.Navigate("http://localhost:12111/geckofx/");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnPrint_Click(object sender, RoutedEventArgs e)
		{
			pageView.PrintPreview(pageView2.Window);
		}

		private void OnTitled(object sender, RoutedEventArgs e)
		{
			this.Title = pageView.DocumentTitle;
		}

		private void btnBack_Click(object sender, RoutedEventArgs e)
		{
			pageView.GoBack();
		}

		private void btnForward_Click(object sender, RoutedEventArgs e)
		{
			pageView.GoForward();
		}

		private void txtAddress_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				try
				{
					pageView.Navigate(txtAddress.Text);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			
		}

	}
}
