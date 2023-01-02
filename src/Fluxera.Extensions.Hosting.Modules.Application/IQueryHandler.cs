namespace Fluxera.Extensions.Hosting.Modules.Application
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;
	using MediatR;

	/// <summary>
	///     Defines a handler for a query.
	/// </summary>
	/// <typeparam name="TQuery">The type of query being handled.</typeparam>
	/// <typeparam name="TResult">The type of result of the query.</typeparam>
	[PublicAPI]
	public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
		where TQuery : IQuery<TResult>
	{
	}
}
