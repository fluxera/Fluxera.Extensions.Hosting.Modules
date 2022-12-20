namespace Catalog.Domain.Shared.ProductAggregate.Events
{
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages;
	using JetBrains.Annotations;

	/// <summary>
	///     An event message for notifying than an example was added.
	/// </summary>
	[PublicAPI]
	public sealed class ProductAdded : ItemAdded
	{
	}
}
