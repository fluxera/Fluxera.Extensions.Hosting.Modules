namespace Fluxera.Extensions.Hosting.Modules.Messaging.RabbitMQ
{
	using Fluxera.Extensions.DataManagement;
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the RabbitMQ messaging module.
	/// </summary>
	[PublicAPI]
	public sealed class RabbitMqMessagingOptions
	{
		/// <summary>
		///     Creates new instance of the <see cref="RabbitMqMessagingOptions" /> type.
		/// </summary>
		public RabbitMqMessagingOptions()
		{
			this.ConnectionStrings = new ConnectionStrings();
		}

		/// <summary>
		///     Gets or sets the name of the connection string to use.
		/// </summary>
		public string ConnectionStringName { get; set; } = "Broker";

		/// <summary>
		///     Gets the connection strings.
		/// </summary>
		public ConnectionStrings ConnectionStrings { get; set; }
	}
}
