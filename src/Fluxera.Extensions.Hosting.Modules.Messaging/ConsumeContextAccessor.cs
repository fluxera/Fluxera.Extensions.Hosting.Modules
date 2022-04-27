namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using System.Threading;
	using JetBrains.Annotations;
	using MassTransit;

	[UsedImplicitly]
	internal sealed class ConsumeContextAccessor : IConsumeContextAccessor
	{
		private static readonly AsyncLocal<ConsumeContextHolder> ConsumeContextCurrent = new AsyncLocal<ConsumeContextHolder>();

		public ConsumeContext ConsumeContext
		{
			get => ConsumeContextCurrent.Value?.Context;
			set
			{
				ConsumeContextHolder holder = ConsumeContextCurrent.Value;
				if(holder != null)
				{
					// Clear current ConsumeContextHolder trapped in the AsyncLocals, as its done.
					holder.Context = null;
				}

				if(value != null)
				{
					// Use an object indirection to hold the ConsumeContextHolder in the AsyncLocal,
					// so it can be cleared in all ExecutionContexts when its cleared.
					ConsumeContextCurrent.Value = new ConsumeContextHolder
					{
						Context = value
					};
				}
			}
		}

		private class ConsumeContextHolder
		{
			public ConsumeContext Context;
		}
	}
}
