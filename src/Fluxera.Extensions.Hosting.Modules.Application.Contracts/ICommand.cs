namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using FluentResults;
	using JetBrains.Annotations;
	using MediatR;

	/// <summary>
	///     Marker interface to represent a command with a void result.
	/// </summary>
	[PublicAPI]
	public interface ICommand : ICommand<Result>
	{
	}

	/// <summary>
	///     Marker interface to represent a command with a result.
	/// </summary>
	[PublicAPI]
	public interface ICommand<out TResult> : IRequest<TResult>
		where TResult : class, IResultBase
	{
	}
}
