namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using JetBrains.Annotations;
	using MassTransit;
	using System;

	/// <summary>
	///     The options for the messaging module.
	/// </summary>
	[PublicAPI]
	public sealed class MessagingOptions
	{
		/// <summary>
		///     Gets or sets a value indicating whether validation is enabled.
		/// </summary>
		/// <value>
		///     <c>true</c> if validation is enabled; otherwise, <c>false</c>.
		/// </value>
		public bool ValidationEnabled { get; set; } = true;

		/// <summary>
		///     Gets or sets a value indicating whether authentication is enabled.
		/// </summary>
		/// <value>
		///     <c>true</c> if authentication is enabled; otherwise, <c>false</c>.
		/// </value>
		public bool AuthenticationEnabled { get; set; } = true;

		/// <summary>
		///     Gets or sets a value indicating whether scheduling is enabled.
		/// </summary>
		/// <value>
		///     <c>true</c> if scheduling is enabled; otherwise, <c>false</c>.
		/// </value>
		public bool SchedulerEnabled { get; set; } = true;

		/// <summary>
		///     Gets or sets the signing key used when creating the principal from a JWT access-token.
		/// </summary>
		public string SigningKey { get; set; }

		/// <summary>
		///		If True, the hosted service will not return from StartAsync until the bus has started.
		/// </summary>
		public bool WaitUntilStarted { get; set; }

		/// <summary>
		///		If specified, the timeout will be used with StartAsync to cancel if the timeout is reached
		/// </summary>
		public TimeSpan? StartTimeout { get; set; }

		/// <summary>
		///		If specified, the timeout will be used with StopAsync to cancel if the timeout is reached.
		///		The bus is still stopped, only the wait is canceled.
		/// </summary>
		public TimeSpan? StopTimeout { get; set; }

		/// <summary>
		///		If specified, the timeout will be used to wait for Consumers to complete their work
		///		After this timeout ConsumeContext.CancellationToken will be cancelled <seealso cref="PipeContext.CancellationToken"/>
		/// </summary>
		public TimeSpan? ConsumerStopTimeout { get; set; }
	}
}
