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

			// Host=localhost;Username=guest;Password=guest;UseSsl=true
			string[] connectionStringParts = connectionString.Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

			foreach(string connectionStringPart in connectionStringParts)
			{
				if(connectionStringPart.StartsWith("Host", StringComparison.InvariantCultureIgnoreCase))
				{
					this.Host = connectionStringPart.Split("=").LastOrDefault() ?? "localhost";
					continue;
				}

				if(connectionStringPart.StartsWith("Username", StringComparison.InvariantCultureIgnoreCase))
				{
					this.Username = connectionStringPart.Split("=").LastOrDefault() ?? "guest";
					continue;
				}

				if(connectionStringPart.StartsWith("Password", StringComparison.InvariantCultureIgnoreCase))
				{
					this.Password = connectionStringPart.Split("=").LastOrDefault() ?? "guest";
					continue;
				}

				if(connectionStringPart.StartsWith("UseSsl", StringComparison.InvariantCultureIgnoreCase))
				{
					this.UseSsl = bool.Parse(connectionStringPart.Split("=").LastOrDefault() ?? "false");
					continue;
				}
			}
		}

		public string Host { get; } = "localhost";

		public string Username { get; } = "guest";

		public string Password { get; } = "guest";

		public bool UseSsl { get; }

		public static implicit operator string(RabbitMqConnectionString connectionString)
		{
			return connectionString?.ToString();
		}

		/// <inheritdoc />
		public override string ToString()
		{
			string connectionString = this.UseSsl 
				? $"amqps://{this.Username}:{this.Password}@{this.Host}" 
				: $"amqp://{this.Username}:{this.Password}@{this.Host}";

			return connectionString;
		}
	}
}
