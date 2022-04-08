namespace Fluxera.Extensions.Hosting.Modules.DataManagement
{
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     A default implementation of the <see cref="IConnectionStringResolver" /> contract
	///     that resolves the connection strings from the configuration.
	/// </summary>
	[PublicAPI]
	public class DefaultConnectionStringResolver : IConnectionStringResolver
	{
		/// <summary>
		///     Creates a new instance of the <see cref="DefaultConnectionStringResolver" /> type.
		/// </summary>
		/// <param name="optionsWrapper"></param>
		public DefaultConnectionStringResolver(IOptions<DataManagementOptions> optionsWrapper)
		{
			this.Options = optionsWrapper.Value;
		}

		/// <summary>
		///     Gets the options of the module.
		/// </summary>
		protected DataManagementOptions Options { get; }

		/// <inheritdoc />
		public string ResolveConnectionString(string name)
		{
			// Get module specific value if provided.
			if(!name.IsNullOrEmpty())
			{
				string connectionString = this.Options.ConnectionStrings.GetOrDefault(name);
				if(!string.IsNullOrWhiteSpace(connectionString))
				{
					return connectionString;
				}
			}

			// Get default value.
			return this.Options.ConnectionStrings.Default;
		}
	}
}
