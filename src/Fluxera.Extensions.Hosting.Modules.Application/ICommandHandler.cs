namespace Fluxera.Extensions.Hosting.Modules.Application
{
	using FluentResults;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;
	using MediatR;

	/// <summary>
	///     Defines a handler for a command.
	/// </summary>
	/// <typeparam name="TCommand">The type of command being handled.</typeparam>
	[PublicAPI]
	public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
		where TCommand : ICommand
	{
	}

	/// <summary>
	///     Defines a handler for a command.
	/// </summary>
	/// <typeparam name="TCommand">The type of command being handled.</typeparam>
	/// <typeparam name="TResult">The type of the result of the command.</typeparam>
	[PublicAPI]
	public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>>
		where TCommand : ICommand<TResult>
	{
	}
}
