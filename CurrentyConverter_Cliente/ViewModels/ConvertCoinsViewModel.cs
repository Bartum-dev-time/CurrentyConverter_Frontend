using CurrentyConverter_Cliente.Helpers;
using CurrentyConverter_Cliente.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace CurrentyConverter_Cliente.ViewModels
{
	public class ConvertCoinsViewModel : ViewModelBase
	{

		//Fiels
		private Decimal? _amount;
		private String _currencyInitial;
		private String? _currencyFinal;
		private Decimal? _total;

		private ObservableCollection<String>? _currenciesList;

		private String? _errorMessage;
		private Boolean _isViewVisible = true;



		//Properties
		public Decimal? Amount
		{
			get
			{
				return _amount;
			}
			set
			{
				_amount = value;
				OnPropertyChanged(nameof(Amount));
			}
		}
		public String CurrencyInitial
		{
			get
			{
				return _currencyInitial = "USD";
			}
			set
			{
				_currencyInitial = value;
				OnPropertyChanged(nameof(CurrencyInitial));
			}
		}
		public String? CurrencyFinal
		{
			get
			{
				return _currencyFinal;
			}
			set
			{
				_currencyFinal = value;
				OnPropertyChanged(nameof(CurrencyFinal));
			}
		}
		public Decimal? Total
		{
			get
			{
				return _total;
			}
			set
			{
				_total = value;
				OnPropertyChanged(nameof(Total));
			}
		}

		public ObservableCollection<String>? CurrenciesList
		{
			get { return _currenciesList; }
			set
			{
				_currenciesList = value;
				OnPropertyChanged(nameof(CurrenciesList));
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

		public ICommand ConvertCurrencyCommand { get; }
		public ICommand LoadCurrenciesCommand { get; }


		// Constructor

		public ConvertCoinsViewModel()
		{
			LoadCurrenciesCommand = new ViewModelCommand(LoadCurrencies);
			LoadCurrenciesCommand.Execute(null);// Cargar las monedas al iniciar
			ConvertCurrencyCommand = new ViewModelCommand(ExecuteConvertCommand, CanExecuteConvertCommand);
		}

		private async void LoadCurrencies(object obj)
		{
			try
			{  // URL del servicio Currencies
				String url = "http://localhost:5064/api/Login/currencies";
				var requestData = new { ParametroEjemplo = "valorEjemplo" }; // Puedes pasar parámetros si es necesario

				var currencies = await HttpHelper.PostAsyncCurrencies(url, requestData);



				CurrenciesList = new ObservableCollection<String>(currencies);

				//Seleccionar una moneda por defecto, puedes asignarla a CurrencyFinal
				if (CurrenciesList.Any())
				{
					CurrencyFinal = CurrenciesList.First(); // Seleccionar la primera moneda por defecto
				}

			}
			catch (Exception e)
			{
				ErrorMessage = $"Error al cargar las monedas: {e.Message}";
			}
		}

		private Boolean CanExecuteConvertCommand(object obj)
		{
			Boolean validData;
			if (Amount == null || Amount == 0)
				validData = false;
			else
			{
				validData = true;
			}
			return validData;
		}

		private async void ExecuteConvertCommand(object obj)
		{
			// URL del servicio Login
			String url = "http://localhost:5064/api/Login/convert";

			//Obj para enviar
			var userData = new
			{
				amount = this.Amount,
				finalCurrency = this.CurrencyFinal
			};

			//Realizar solicitud POST
			var response = await HttpHelper.PostAsyncLogin(url, userData);

			if (!String.IsNullOrEmpty(response))
			{
				var apiResponse = JsonConvert.DeserializeObject<Response>(response);

				if (apiResponse != null)
				{
					Decimal? numTotal = Convert.ToDecimal(apiResponse.ResponseText);
					Total = numTotal;
				}

				

			}


		}
	}
}
