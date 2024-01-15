namespace Fluxera.Extensions.Hosting.Modules.Application
{
	using JetBrains.Annotations;
	using MadEyeMatt.Results;
	using MediatR;

	/// <summary>
	///     Marker interface to represent a command with a void result.
	/// </summary>
	[PublicAPI]
	public interface ICommand : ICommand<Result>
	{
	}

	/// <summary>
	///     Marker interface to represent a command with a void result.
	/// </summary>
	[PublicAPI]
	public interface ICommand<out TResult> : IRequest<TResult> 
		where TResult : class
	{
	}
}
