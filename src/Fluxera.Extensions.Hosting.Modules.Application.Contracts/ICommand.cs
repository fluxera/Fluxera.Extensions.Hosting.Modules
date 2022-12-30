namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using FluentResults;
	using JetBrains.Annotations;
	using MediatR;

	/// <summary>
	///     Marker interface to represent a command.
	/// </summary>
	[PublicAPI]
	public interface ICommand : IRequest<Result>
	{
	}

	/// <summary>
	///     Marker interface to represent a command with a result.
	/// </summary>
	[PublicAPI]
	public interface ICommand<TResult> : IRequest<Result<TResult>>
	{
	}
}
