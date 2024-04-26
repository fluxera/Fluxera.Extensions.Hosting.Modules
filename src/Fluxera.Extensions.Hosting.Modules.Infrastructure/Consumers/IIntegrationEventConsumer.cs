namespace Fluxera.Extensions.Hosting.Modules.Infrastructure.Consumers
{
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///		Defines a class that is a consumer of an integration event message.
	/// </summary>
	/// <typeparam name="TEvent">The event message type.</typeparam>
	[PublicAPI]
	public interface IIntegrationEventConsumer<in TEvent> : IConsumer<TEvent> 
		where TEvent : class, IIntegrationEvent
	{
		/// <summary>
		///		Consumes the event message.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		Task ConsumeAsync(ConsumeContext<TEvent> context);

		/// <inheritdoc />
		Task IConsumer<TEvent>.Consume(ConsumeContext<TEvent> context)
		{
			return this.ConsumeAsync(context);
		}
	}
}
