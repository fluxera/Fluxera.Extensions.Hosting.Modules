namespace Catalog.Domain.Messages.Products
{
	using Fluxera.Extensions.Hosting.Modules.Domain.Messages;
	using JetBrains.Annotations;

	/// <summary>
	///     An event message for notifying than an example was removed.
	/// </summary>
	[PublicAPI]
	public sealed class ProductRemoved : ItemRemoved
	{
	}
}
