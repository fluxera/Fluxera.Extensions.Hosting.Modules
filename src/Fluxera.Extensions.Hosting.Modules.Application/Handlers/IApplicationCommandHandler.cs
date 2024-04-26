namespace Fluxera.Extensions.Hosting.Modules.Application.Handlers
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;
	using MadEyeMatt.Results;
	using MediatR;

	/// <summary>
	///		A contract for application command handlers with non-value result.
	/// </summary>
	/// <typeparam name="TCommand"></typeparam>
	[PublicAPI]
	public interface IApplicationCommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
		where TCommand : class, IApplicationCommand
	{
		/// <summary>
		///		Handles an application event.
		/// </summary>
		/// <param name="command"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<Result> HandleAsync(TCommand command, CancellationToken cancellationToken);

		/// <inheritdoc />
		Task<Result> IRequestHandler<TCommand, Result>.Handle(TCommand command, CancellationToken cancellationToken)
		{
			return this.HandleAsync(command, cancellationToken);
		}
	}

	///  <summary>
	/// 	A contract for application command handlers with value result.
	///  </summary>
	///  <typeparam name="TCommand"></typeparam>
	///  <typeparam name="TValue"></typeparam>
	[PublicAPI]
	public interface IApplicationCommandHandler<in TCommand, TValue> : IRequestHandler<TCommand, Result<TValue>>
		where TCommand : class, IApplicationCommand<TValue>
	{
		/// <summary>
		///		Handles an application event.
		/// </summary>
		/// <param name="command"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<Result<TValue>> HandleAsync(TCommand command, CancellationToken cancellationToken);

		/// <inheritdoc />
		Task<Result<TValue>> IRequestHandler<TCommand, Result<TValue>>.Handle(TCommand command, CancellationToken cancellationToken)
		{
			return this.HandleAsync(command, cancellationToken);
		}
	}
}
