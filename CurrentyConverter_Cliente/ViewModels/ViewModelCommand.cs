using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Inputs windows
using System.Windows.Input;

namespace CurrentyConverter_Cliente.ViewModels
{
	public class ViewModelCommand : ICommand
	{
		//Fields
		private readonly Action<object> _executeAction; //Sirve para encapsular un metodo como un parametro
		private readonly Predicate<object> _canExecuteAction; //Esto es para saber si se puede ejecutar o no, permite desactivar o activar el control

		//Constructors
		public ViewModelCommand(Action<object> executeAction)
		{
			_executeAction = executeAction;
			_canExecuteAction = null;
		}
		public ViewModelCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
		{
			_executeAction = executeAction;
			_canExecuteAction = canExecuteAction;
		}


		//Events
		public event EventHandler CanExecuteChanged //Detecta si la ejecucion del comando a cambiado
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }


		}

		//Methods
		public bool CanExecute(object parameter)
		{
			
			return _canExecuteAction == null ? true : _canExecuteAction(parameter);
			
		}

		public void Execute(object parameter)
		{

			_executeAction(parameter);
		}
	}
}
