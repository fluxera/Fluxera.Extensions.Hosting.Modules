namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using System.Collections.Generic;

	internal sealed class IdempotentResponseData
	{
		public IdempotentResponseData()
		{
			this.Headers = new Dictionary<string, string>();
		}

		public string Body { get; set; }

		public IDictionary<string, string> Headers { get; set; }

		public int StatusCode { get; set; }
	}
}
