namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using Fluxera.Results;
	using JetBrains.Annotations;
	using MediatR;

	/// <summary>
	///		A marker interface for application queries.
	/// </summary>
	[PublicAPI]
	public interface IApplicationQuery<T> : IRequest<Result<T>>
	{
	}
}
