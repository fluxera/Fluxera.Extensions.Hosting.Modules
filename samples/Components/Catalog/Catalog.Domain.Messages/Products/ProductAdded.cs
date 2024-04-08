namespace Catalog.Domain.Messages.Products
{
	using Fluxera.Extensions.Hosting.Modules.Domain.Messages;
	using JetBrains.Annotations;

	/// <summary>
	///     An event message for notifying than an example was added.
	/// </summary>
	[PublicAPI]
	public sealed class ProductAdded : ItemAdded
	{
	}
}
