namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using MediatR;

	/// <summary>
	///     Marker interface to represent a query.
	/// </summary>
	/// <typeparam name="TResult">The result type.</typeparam>
	public interface IQuery<out TResult> : IRequest<TResult>
	{
	}
}
