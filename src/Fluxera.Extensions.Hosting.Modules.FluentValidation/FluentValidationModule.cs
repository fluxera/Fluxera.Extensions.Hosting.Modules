namespace Fluxera.Extensions.Hosting.Modules.FluentValidation
{
	using System.Linq;
	using System.Reflection;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Validation;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables validation.
	/// </summary>
	[PublicAPI]
	public sealed class FluentValidationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Get the module container.
			IModuleContainer moduleContainer = context.Services.GetObject<IModuleContainer>();

			// Get all distinct module assemblies.
			Assembly[] assemblies = moduleContainer.Modules.Select(x => x.Assembly).Distinct().ToArray();

			context.Log("AddValidation", services =>
			{
				// Add all available validators from the modules.
				services.AddValidation(assemblies);
			});
		}
	}
}
