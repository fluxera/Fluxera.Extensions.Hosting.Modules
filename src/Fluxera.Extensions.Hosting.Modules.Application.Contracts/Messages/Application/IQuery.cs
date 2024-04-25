namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Application
{
	using JetBrains.Annotations;
	using MadEyeMatt.Results;
	using MediatR;

	/// <summary>
	///		A marker interface for application queries.
	/// </summary>
	[PublicAPI]
	public interface IQuery<T> : IRequest<Result<T>>
	{
	}
}
