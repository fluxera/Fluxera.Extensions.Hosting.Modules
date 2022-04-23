namespace Fluxera.Extensions.Hosting.Modules.Configuration
{
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	/// <inheritdoc />
	[PublicAPI]
	public abstract class ConfigureOptionsContributorBase<TOptions> : IConfigureOptionsContributor
		where TOptions : class, new()
	{
		/// <inheritdoc />
		public abstract string SectionName { get; }

		/// <inheritdoc />
		public void Configure(IServiceConfigurationContext context)
		{
			string optionsTypeName = typeof(TOptions).Name;
			IConfigurationSection section = context.Configuration.GetSection(ConfigurationSectionUtil.GetSectionName(this.SectionName));
			TOptions options = section.Get<TOptions>() ?? new TOptions();

			// Configure as IOptions<T>
			context.Log($"Configure({optionsTypeName})",
				services => services.Configure<TOptions>(section));

			// Add as ObjectAccessor that is available during configure services pipeline.
			context.Log($"AddObjectAccessor({optionsTypeName})",
				services => services.AddObjectAccessor(options, ObjectAccessorLifetime.ConfigureServices));

			this.AdditionalConfigure(context, options);
		}

		/// <summary>
		///     Performs additional options configuration.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="createdOptions"></param>
		protected virtual void AdditionalConfigure(IServiceConfigurationContext context, TOptions createdOptions)
		{
		}
	}
}
