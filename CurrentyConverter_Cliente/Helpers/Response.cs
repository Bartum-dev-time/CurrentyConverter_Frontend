using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrentyConverter_Cliente.Helpers
{
	public class Response
	{
		public EState State { get; set; }
		public String? MessageText { get; set; }
		public Object? ResponseText { get; set; }


		// Método para establecer datos dinámicos en ResponseText
		public void SetResponseText(Object data)
		{
			ResponseText = data;

		}
	}

	public enum EState
	{
		Aceptado,
		Rechazado,
		Abortado
	}
}
