namespace Fluxera.Extensions.Hosting.Modules.AutoMapper
{
	using System;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.AutoMapper.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using global::AutoMapper;
	using global::AutoMapper.Extensions.ExpressionMapping;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     A module that enables AutoMapper.
	/// </summary>
	/// <remarks>
	///     https://github.com/AutoMapper/AutoMapper/blob/master/docs/Expression-Translation-(UseAsDataSource).md
	///     https://github.com/AutoMapper/AutoMapper/blob/master/docs/Queryable-Extensions.md
	/// </remarks>
	[PublicAPI]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class AutoMapperModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add an object accessor for the mapper instance that is created lazy at runtime.
			context.Log("AddObjectAccessor(IMapper)",
				services => services.AddObjectAccessor<IMapper>(ObjectAccessorLifetime.Application));

			// Add a factory methods to get the mapper instance out of the object accessor.
			context.Log("AddMapperFactory",
				services => services.AddTransient(serviceProvider => serviceProvider.GetObject<IMapper>()));

			// Add the contributor list.
			context.Log("AddObjectAccessor(MappingProfileContributorList)",
				services => services.AddObjectAccessor(new MappingProfileContributorList(), ObjectAccessorLifetime.Configure));
		}

		/// <inheritdoc />
		public override void PreConfigure(IApplicationInitializationContext context)
		{
			context.Log("Initialize(AutoMapper)", serviceProvider =>
			{
				IOptions<AutoMapperOptions> optionsService = serviceProvider.GetRequiredService<IOptions<AutoMapperOptions>>();
				AutoMapperOptions options = optionsService.Value;

				// Configure the module options.
				MappingProfileContributorList contributorList = context.ServiceProvider.GetObject<MappingProfileContributorList>();
				foreach(IMappingProfileContributor contributor in contributorList)
				{
					contributor.ConfigureProfiles(options);
				}

				MapperConfiguration configuration = new MapperConfiguration(cfg =>
				{
					cfg.ConstructServicesUsing(serviceProvider.GetRequiredService);

					cfg.AddExpressionMapping();

					AutoMapperConfigurationContext ctx = new AutoMapperConfigurationContext(cfg, serviceProvider);
					foreach(Action<AutoMapperConfigurationContext> configurator in options.Configurators)
					{
						configurator(ctx);
					}
				});

				ValidateAll(configuration, options, serviceProvider);

				// Create the mapper instance and set it to its object assessor.
				IObjectAccessor<IMapper> accessor = serviceProvider.GetRequiredService<IObjectAccessor<IMapper>>();
				accessor.Value = configuration.CreateMapper();
			});
		}

		private static void ValidateAll(IConfigurationProvider config, AutoMapperOptions options, IServiceProvider serviceProvider)
		{
			foreach(Type profileType in options.ValidatingProfiles)
			{
				Profile profile = (Profile)(Activator.CreateInstance(profileType) ?? serviceProvider.GetService(profileType));
				if(profile != null)
				{
					config.AssertConfigurationIsValid();
				}
				else
				{
					throw new InvalidOperationException($"Could not create an instance of the AutoMapper profile '{profileType}'.");
				}
			}
		}
	}
}
