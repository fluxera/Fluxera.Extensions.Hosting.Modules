namespace Fluxera.Extensions.Hosting.Modules.Messaging.AmazonSQS
{
	using Fluxera.Extensions.DataManagement;
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the AmazonSQS messaging module.
	/// </summary>
	[PublicAPI]
	public sealed class AmazonSqsMessagingOptions
	{
		/// <summary>
		///     Creates new instance of the <see cref="AmazonSqsMessagingOptions" /> type.
		/// </summary>
		public AmazonSqsMessagingOptions()
		{
			this.ConnectionStrings = new ConnectionStrings();
		}

		/// <summary>
		///     Gets or sets a value indicating whether the in-memory outbox is enabled.
		/// </summary>
		/// <value>
		///     <c>true</c> if the in-memory outbox is enabled; otherwise, <c>false</c>.
		/// </value>
		public bool InMemoryOutboxEnabled { get; set; } = true;

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
