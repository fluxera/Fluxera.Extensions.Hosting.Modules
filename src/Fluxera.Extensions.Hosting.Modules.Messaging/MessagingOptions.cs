namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using JetBrains.Annotations;

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
	}
}
