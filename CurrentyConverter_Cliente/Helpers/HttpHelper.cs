using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CurrentyConverter_Cliente.Helpers
{
	public class HttpHelper
	{
		private static readonly HttpClient _httpClient = new HttpClient();
		public static async Task<String> PostAsyncLogin(String url, Object data)
		{
			try
			{
				// Serializar el objeto a JSON
				var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
				// La codifica UTF-8 - Indica al servidor que se envía JSON
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				// Realizar la solicitud POST
				var response = await _httpClient.PostAsync(url, content);

				// Verificar si la solicitud fue exitosa
				response.EnsureSuccessStatusCode();

				// Leer y devolver la respuesta
				return await response.Content.ReadAsStringAsync();
			}
			catch (Exception ex)
			{
				// Manejar errores				
				return $"Error: {ex.Message}";
			}
		}
		public static async Task<List<String>> PostAsyncCurrencies(String url, Object data)
		{
			try
			{
				// Serializar el objeto a JSON
				var json = JsonConvert.SerializeObject(data);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				// Realizar la solicitud POST
				var response = await _httpClient.PostAsync(url, content);

				// Verificar si la solicitud fue exitosa
				response.EnsureSuccessStatusCode();


				// Leer y deserializar la respuesta JSON

				var responseContent = await response.Content.ReadAsStringAsync();
				var currencies = JsonConvert.DeserializeObject<List<String>>(responseContent);
				// Leer y devolver la respuesta
				return currencies ?? new List<String>();
			}
			catch (Exception)
			{
				// Manejar errores			
				throw;
				//return new List<String>();
			}
		}		
	}
}
