namespace Fluxera.Extensions.Hosting.Modules.AutoMapper
{
	using System;
	using global::AutoMapper;

	internal sealed class AutoMapperConfigurationContext
	{
		public AutoMapperConfigurationContext(
			IMapperConfigurationExpression mapperConfigurationExpression,
			IServiceProvider serviceProvider)
		{
			this.MapperConfiguration = mapperConfigurationExpression;
			this.ServiceProvider = serviceProvider;
		}

		public IMapperConfigurationExpression MapperConfiguration { get; }

		public IServiceProvider ServiceProvider { get; }
	}
}
