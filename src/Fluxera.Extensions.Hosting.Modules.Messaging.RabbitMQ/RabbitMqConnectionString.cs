namespace Fluxera.Extensions.Hosting.Modules.Messaging.RabbitMQ
{
	using System.Linq;
	using Fluxera.Guards;

	internal sealed class RabbitMqConnectionString
	{
		public RabbitMqConnectionString(string connectionString)
		{
			Guard.Against.NullOrWhiteSpace(connectionString, nameof(connectionString));

			// host=localhost;username=guest;password=guest
			string[] connectionStringParts = connectionString.Split(";");

			foreach(string connectionStringPart in connectionStringParts)
			{
				if(connectionStringPart.StartsWith("host"))
				{
					this.Host = connectionStringPart.Split("=").LastOrDefault() ?? "localhost";
					continue;
				}

				if(connectionStringPart.StartsWith("username"))
				{
					this.Username = connectionStringPart.Split("=").LastOrDefault() ?? "guest";
					continue;
				}

				if(connectionStringPart.StartsWith("password"))
				{
					this.Password = connectionStringPart.Split("=").LastOrDefault() ?? "guest";
				}
			}
		}

		public string Host { get; } = "localhost";

		public string Username { get; } = "guest";

		public string Password { get; } = "guest";
	}
}
