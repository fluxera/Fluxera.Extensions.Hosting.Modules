namespace Fluxera.Extensions.Hosting.Modules.Configuration
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the modular hosting infrastructure.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class HostingOptions
	{
		/// <summary>
		///     Gets or sets the application name.
		/// </summary>
		public string AppName { get; internal set; }

		/// <summary>
		///     Gets or sets the application version.
		/// </summary>
		public Version Version { get; internal set; }
	}
}
