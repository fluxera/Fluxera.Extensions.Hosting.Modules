namespace ExampleService
{
	using Example.HttpApi;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi;
	using JetBrains.Annotations;

	[PublicAPI]
	[DependsOn(typeof(ExampleHttpApiModule))]
	public sealed class ExampleServiceModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
		}

		/// <inheritdoc />
		public override void Configure(IApplicationInitializationContext context)
		{
			// Configure the HTTP request pipeline.
			if(context.Environment.IsDevelopment())
			{
				context.UseSwaggerUI();
			}

			context.UseHttpsRedirection();

			context.UseRouting();

			context.UseEndpoints();
		}
	}
}
