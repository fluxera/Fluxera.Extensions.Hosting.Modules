﻿namespace Fluxera.Extensions.Hosting.Modules.Messaging.AmazonSQS
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
		///     Gets or sets the name of the connection string to use.
		/// </summary>
		public string ConnectionStringName { get; set; } = "Broker";

		/// <summary>
		///     Gets the connection strings.
		/// </summary>
		public ConnectionStrings ConnectionStrings { get; set; }
	}
}