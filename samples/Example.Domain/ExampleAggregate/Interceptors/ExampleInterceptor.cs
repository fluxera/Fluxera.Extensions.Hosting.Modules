namespace Example.Domain.ExampleAggregate.Interceptors
{
	using Example.Domain.ExampleAggregate.Model;
	using Fluxera.Repository.Interception;
	using JetBrains.Annotations;

	/// <summary>
	///     A repository interceptor for the example aggregate root.
	/// </summary>
	[UsedImplicitly]
	public sealed class ExampleInterceptor : InterceptorBase<Example, string>
	{
	}
}
