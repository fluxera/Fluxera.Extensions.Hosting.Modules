namespace Fluxera.Extensions.Hosting.Modules.Messaging.AmazonSQS
{
	using System;
	using System.Linq;
	using Fluxera.Guards;

	internal sealed class AmazonSqsConnectionString
	{
		public AmazonSqsConnectionString(string connectionString)
		{
			Guard.Against.NullOrWhiteSpace(connectionString);

			// Host=localhost;AccessKey=guest;SecretKey=guest
			string[] connectionStringParts = connectionString.Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

			foreach(string connectionStringPart in connectionStringParts)
			{
				if(connectionStringPart.StartsWith("Host"))
				{
					this.Host = connectionStringPart.Split("=").LastOrDefault() ?? "localhost";
					continue;
				}

				if(connectionStringPart.StartsWith("AccessKey"))
				{
					this.AccessKey = connectionStringPart.Split("=").LastOrDefault() ?? string.Empty;
					continue;
				}

				if(connectionStringPart.StartsWith("SecretKey"))
				{
					this.SecretKey = connectionStringPart.Split("=").LastOrDefault() ?? string.Empty;
				}
			}
		}

		public string Host { get; } = string.Empty;

		public string AccessKey { get; } = string.Empty;

		public string SecretKey { get; } = string.Empty;

		public static implicit operator string(AmazonSqsConnectionString connectionString)
		{
			return connectionString?.ToString();
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"Host={this.Host};AccessKey={this.AccessKey};SecretKey={this.SecretKey}";
		}
	}
}
