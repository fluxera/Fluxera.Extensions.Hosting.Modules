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
	}
}
