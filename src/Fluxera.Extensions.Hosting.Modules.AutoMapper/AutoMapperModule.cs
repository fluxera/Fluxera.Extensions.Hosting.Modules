using System;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Fluxera.Extensions.DependencyInjection;
using Fluxera.Extensions.Hosting.Modules.AutoMapper.Contributors;
using Fluxera.Extensions.Hosting.Modules.Configuration;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Fluxera.Extensions.Hosting.Modules.AutoMapper
{
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
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			// Configure the mapping profiles as services.
			MappingProfileContributorList contributorList = context.Services.GetObject<MappingProfileContributorList>();
			foreach(IMappingProfileContributor contributor in contributorList)
			{
				contributor.ConfigureProfileServices(context);
			}
		}

		/// <inheritdoc />
		public override void PreConfigure(IApplicationInitializationContext context)
		{
			context.Log("Initialize(AutoMapper)", serviceProvider =>
			{
				IOptions<AutoMapperOptions> optionsService = serviceProvider.GetRequiredService<IOptions<AutoMapperOptions>>();
				AutoMapperOptions options = optionsService.Value;

				// Configure the mapping profiles.
				MappingProfileContributorList contributorList = context.ServiceProvider.GetObject<MappingProfileContributorList>();
				foreach(IMappingProfileContributor contributor in contributorList)
				{
					contributor.ConfigureProfiles(options, context);
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
