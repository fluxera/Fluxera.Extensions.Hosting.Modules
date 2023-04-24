namespace Fluxera.Extensions.Hosting.Modules.Messaging.RabbitMQ
{
	using System;
	using System.Linq;
	using Fluxera.Guards;

	internal sealed class RabbitMqConnectionString
	{
		public RabbitMqConnectionString(string connectionString)
		{
			Guard.Against.NullOrWhiteSpace(connectionString);

			// Host=localhost;Username=guest;Password=guest
			string[] connectionStringParts = connectionString.Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

			foreach(string connectionStringPart in connectionStringParts)
			{
				if(connectionStringPart.StartsWith("Host"))
				{
					this.Host = connectionStringPart.Split("=").LastOrDefault() ?? "localhost";
					continue;
				}

				if(connectionStringPart.StartsWith("Username"))
				{
					this.Username = connectionStringPart.Split("=").LastOrDefault() ?? "guest";
					continue;
				}

				if(connectionStringPart.StartsWith("Password"))
				{
					this.Password = connectionStringPart.Split("=").LastOrDefault() ?? "guest";
				}
			}
		}

		public string Host { get; } = "localhost";

		public string Username { get; } = "guest";

		public string Password { get; } = "guest";

		public static implicit operator string(RabbitMqConnectionString connectionString)
		{
			return connectionString?.ToString();
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"amqps://{this.Username}:{this.Password}@{this.Host}";
		}
	}
}
