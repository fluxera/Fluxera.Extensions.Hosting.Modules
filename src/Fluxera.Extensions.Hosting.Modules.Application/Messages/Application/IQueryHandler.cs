namespace Fluxera.Extensions.Hosting.Modules.Application.Messages.Application
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Application;
	using JetBrains.Annotations;
	using MadEyeMatt.Results;
	using MediatR;

	///  <summary>
	/// 	A contract for application command handlers with value result.
	///  </summary>
	///  <typeparam name="TQuery"></typeparam>
	///  <typeparam name="TValue"></typeparam>
	[PublicAPI]
	public interface IQueryHandler<in TQuery, TValue> : IRequestHandler<TQuery, Result<TValue>>
		where TQuery : class, IQuery<TValue>
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
