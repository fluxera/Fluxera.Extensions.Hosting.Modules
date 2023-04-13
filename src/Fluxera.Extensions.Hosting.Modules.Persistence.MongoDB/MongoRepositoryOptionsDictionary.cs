namespace Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB
{
	using System;
	using System.Collections.Generic;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	/// <summary>
	///     The repository options.
	/// </summary>
	[PublicAPI]
	public sealed class MongoRepositoryOptionsDictionary : Dictionary<string, MongoRepositoryOptions>
	{
		/// <summary>
		///     The default repository name.
		/// </summary>
		public const string DefaultRepositoryName = "Default";

		/// <summary>
		///     Gets the default repository options.
		/// </summary>
		public MongoRepositoryOptions Default
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
		public MongoRepositoryOptions GetOptionsOrDefault(string name)
		{
			return this.GetOrDefault(name)
				?? this.Default
				?? throw new Exception($"The repository '{name}' was not found and there are no default options.");
		}
	}
}
