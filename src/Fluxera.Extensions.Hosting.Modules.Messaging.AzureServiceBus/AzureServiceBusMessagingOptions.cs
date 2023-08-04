namespace Fluxera.Extensions.Hosting.Modules.Messaging.AzureServiceBus
{
	using Fluxera.Extensions.DataManagement;
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the Azure ServiceBus messaging module.
	/// </summary>
	[PublicAPI]
	public sealed class AzureServiceBusMessagingOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="AzureServiceBusMessagingOptions" /> type.
		/// </summary>
		public AzureServiceBusMessagingOptions()
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
