namespace Fluxera.Extensions.Hosting.Modules.Domain
{
	using Fluxera.Extensions.Hosting.Modules.Messaging;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables the domain.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(MessagingModule))]
	public sealed class DomainModule : IModule
	{
	}
}
