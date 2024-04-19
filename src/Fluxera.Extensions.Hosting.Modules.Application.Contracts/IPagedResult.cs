namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using JetBrains.Annotations;

	/// <summary>
	///     This interface is defined to standardize to return a page of items to clients.
	/// </summary>
	/// <typeparam name="T">Type of the items in the <see cref="IListResult{T}.Items" /> list</typeparam>
	[PublicAPI]
	public interface IPagedResult<out T> : IListResult<T>, IHasTotalCount
	{
	}
}
