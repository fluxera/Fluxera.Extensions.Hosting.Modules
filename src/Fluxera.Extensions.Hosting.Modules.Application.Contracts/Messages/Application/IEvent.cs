namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Application
{
	using JetBrains.Annotations;
	using MediatR;

	/// <summary>
	///		A marker interface for application events.
	/// </summary>
	[PublicAPI]
	public interface IEvent : INotification
	{
	}
}
