namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using System;
	using System.Collections.Generic;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	/// <summary>
	///     A tenant persistence options dictionary.
	/// </summary>
	[PublicAPI]
	public sealed class TenantPersistenceOptionsDictionary : Dictionary<string, TenantPersistenceOptions>
	{
		/// <summary>
		///     The default repository name.
		/// </summary>
		public const string DefaultRepositoryName = "Default";

		/// <summary>
		///     Gets the default tenant options.
		/// </summary>
		public TenantPersistenceOptions Default
		{
			get => this.GetOrDefault(DefaultRepositoryName);
			set => this[DefaultRepositoryName] = value;
		}

		/// <summary>
		///     Gets the options for the given name, oder the default options if available.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public TenantPersistenceOptions GetConfigurationOrDefault(string name)
		{
			return this.GetOrDefault(name)
				?? this.Default
				?? throw new Exception($"The repository '{name}' was not found and there is no default configuration.");
		}
	}
}
