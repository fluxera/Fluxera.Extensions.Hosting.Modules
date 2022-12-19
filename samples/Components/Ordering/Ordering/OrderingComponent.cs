namespace Ordering
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using JetBrains.Annotations;
	using Ordering.Application;
	using Ordering.HttpApi;
	using Ordering.MessagingApi;

	[PublicAPI]
	[DependsOn<OrderingHttpApiModule>]
	[DependsOn<OrderingMessagingApiModule>]
	[DependsOn<OrderingApplicationModule>]
	public sealed class OrderingComponent : IModule
	{
	}
}
