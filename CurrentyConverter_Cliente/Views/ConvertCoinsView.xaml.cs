using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CurrentyConverter_Cliente.Views
{
	/// <summary>
	/// Interaction logic for ConvertCoinsView.xaml
	/// </summary>
	public partial class ConvertCoinsView : Window
	{
		public ConvertCoinsView()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Permite mover la ventana desde cualquier lugar de la ventana
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
				DragMove();
		}

		/// <summary>
		/// Evento para minimizar la ventana
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnMinimize_Click(Object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}


		/// <summary>
		/// Evento para cerrar la ventana
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClose_Click(Object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
