namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using JetBrains.Annotations;
	using MadEyeMatt.Results;
	using MediatR;

	/// <summary>
	///		A marker interface for application commands with non-value result.
	/// </summary>
	[PublicAPI]
	public interface IApplicationCommand<T> : IRequest<Result<T>>
	{
	}

	/// <summary>
	///		A marker interface for application commands with value result.
	/// </summary>
	[PublicAPI]
	public interface IApplicationCommand : IRequest<Result>
	{
	}
}
