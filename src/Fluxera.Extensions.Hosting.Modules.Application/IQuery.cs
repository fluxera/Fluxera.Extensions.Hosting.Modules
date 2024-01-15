namespace Fluxera.Extensions.Hosting.Modules.Application
{
	using JetBrains.Annotations;
	using MediatR;

	/// <summary>
	///     Marker interface to represent a query.
	/// </summary>
	/// <typeparam name="TResult"></typeparam>
	[PublicAPI]
	public interface IQuery<out TResult> : IRequest<TResult>
	{
	}
}
