using CurrentyConverter_Cliente.Helpers;
using CurrentyConverter_Cliente.Views;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CurrentyConverter_Cliente.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{

		//Fields
		private String? _username;
		private String? _password;
		private String? _errorMessage;
		private Boolean _isViewVisible = true;

		//Properties
		public String? Username
		{
			get
			{
				return _username;
			}
			set
			{
				_username = value;
				OnPropertyChanged(nameof(Username));
			}
		}

		public String? Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
				OnPropertyChanged(nameof(Password));
			}
		}

		public String? ErrorMessage
		{
			get
			{
				return _errorMessage;
			}
			set
			{
				_errorMessage = value;
				OnPropertyChanged(nameof(ErrorMessage));
			}
		}

		public Boolean IsViewVisible
		{
			get
			{
				return _isViewVisible;
			}
			set
			{
				_isViewVisible = value;
				OnPropertyChanged(nameof(IsViewVisible));
			}
		}

		//-> Commands
		public ICommand LoginCommand { get; }

		//Constructor
		public LoginViewModel()
		{
			LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
		}
		private Boolean CanExecuteLoginCommand(object obj)
		{

			Boolean validData;
			if (String.IsNullOrWhiteSpace(Username) || Username.Length <= 3 || Password == null || Password.Length <= 3)
				validData = false;
			else
			{
				validData = true;
			}
			return validData;
		}

		private async void ExecuteLoginCommand(object obj)
		{

			try
			{
				// URL del servicio Login
				String url = "http://localhost:5064/api/Login";

				//Obj para enviar
				var userData = new
				{
					userName = this.Username,
					passUser = this.Password
				};

				//Realizar solicitud POST
				var response = await HttpHelper.PostAsyncLogin(url, userData);

				if (!String.IsNullOrEmpty(response))
				{
					var apiResponse = JsonConvert.DeserializeObject<Response>(response);

					if (apiResponse?.State == EState.Aceptado)
					{
						// Inicio de sesión exitoso
						MessageBox.Show("Inicio de sesión exitoso", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

						// Abrir nueva ventana
						ConvertCoinsView ConvertidorMondedasView = new ConvertCoinsView();

						ConvertidorMondedasView.Show();

						// Cerrar la ventana de inicio de sesión actual
						if (obj is Window loginWindow)
						{
							loginWindow.Close();
						}
					}
					else
					{
						if (apiResponse != null)
						{
							// Mostrar mensaje de error de la API
							ErrorMessage = apiResponse.MessageText;
						}
					}
				}
				else
				{
					ErrorMessage = "No se recibió respuesta del servidor.";
				}
			}
			catch (Exception e)
			{

				// Mostrar un mensaje de error
				ErrorMessage = $"error {e.Message}";
				//MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
