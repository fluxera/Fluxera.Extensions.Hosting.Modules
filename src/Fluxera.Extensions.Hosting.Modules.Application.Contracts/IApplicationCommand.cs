namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using Fluxera.Results;
	using JetBrains.Annotations;
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
