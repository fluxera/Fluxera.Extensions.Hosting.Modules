namespace Fluxera.Extensions.Hosting.Modules.Domain
{
	using Fluxera.Extensions.Common;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables the domain.
	/// </summary>
	[PublicAPI]
	public sealed class DomainModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Log("AddDateTimeOffsetProvider",
				services => services.AddDateTimeOffsetProvider());
		}
	}
}
