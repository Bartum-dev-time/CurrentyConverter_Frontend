using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Ensamblado de componentes
using System.ComponentModel;

namespace CurrentyConverter_Cliente.ViewModels
{

	/// <summary>
	/// notifica a la vista (View) cuando una propiedad ha cambiado, lo que facilita la actualización automática de la UI.
	/// </summary>
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public void OnPropertyChanged(String propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
