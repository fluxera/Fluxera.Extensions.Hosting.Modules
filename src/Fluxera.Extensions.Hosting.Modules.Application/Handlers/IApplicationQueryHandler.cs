namespace Fluxera.Extensions.Hosting.Modules.Application.Handlers
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using global::MediatR;
	using JetBrains.Annotations;
	using MadEyeMatt.Results;

	///  <summary>
	/// 	A contract for application command handlers with value result.
	///  </summary>
	///  <typeparam name="TQuery"></typeparam>
	///  <typeparam name="TValue"></typeparam>
	[PublicAPI]
	public interface IApplicationQueryHandler<in TQuery, TValue> : IRequestHandler<TQuery, Result<TValue>>
		where TQuery : class, IApplicationQuery<TValue>
	{
		/// <summary>
		///		Handles an application event.
		/// </summary>
		/// <param name="query"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<Result<TValue>> HandleAsync(TQuery query, CancellationToken cancellationToken);

		/// <inheritdoc />
		Task<Result<TValue>> IRequestHandler<TQuery, Result<TValue>>.Handle(TQuery query, CancellationToken cancellationToken)
		{
			return this.HandleAsync(query, cancellationToken);
		}
	}
}
