namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using JetBrains.Annotations;
	using MadEyeMatt.Results;
	using MediatR;

	/// <summary>
	///		A marker interface for application queries.
	/// </summary>
	[PublicAPI]
	public interface IApplicationQuery<T> : IRequest<Result<T>>
	{
	}
}
