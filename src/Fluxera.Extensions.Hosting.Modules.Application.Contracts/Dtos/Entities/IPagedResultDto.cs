namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos.Entities
{
	using JetBrains.Annotations;

	/// <summary>
	///     This interface is defined to standardize to return a page of items to clients.
	/// </summary>
	/// <typeparam name="T">Type of the items in the <see cref="IListResultDto{T}.Items" /> list</typeparam>
	[PublicAPI]
	public interface IPagedResultDto<out T> : IListResultDto<T>, IHasTotalCount
	{
	}
}
