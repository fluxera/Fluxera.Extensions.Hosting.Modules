namespace Fluxera.Extensions.Hosting.Modules.Configuration
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     An options class for the modular hosting infrastructure.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class HostingOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="HostingOptions" /> type.
		/// </summary>
		public HostingOptions()
		{
			this.Modules = new ModulesOptionsDictionary();
		}

		/// <summary>
		///     Gets or sets the application name.
		/// </summary>
		public string? AppName { get; set; }

		/// <summary>
		///     Gets or sets the application version.
		/// </summary>
		public Version? Version { get; set; }

		/// <summary>
		///     Gets or sets the modules app settings.
		/// </summary>
		public ModulesOptionsDictionary Modules { get; set; }
	}
}
