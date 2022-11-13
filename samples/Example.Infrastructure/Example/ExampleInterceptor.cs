namespace Example.Infrastructure.Example
{
	using Fluxera.Repository.Interception;
	using global::Example.Domain.Example;
	using global::Example.Domain.Shared.Example;
	using JetBrains.Annotations;

	/// <summary>
	///     A repository interceptor for the example aggregate root.
	/// </summary>
	[UsedImplicitly]
	public sealed class ExampleInterceptor : InterceptorBase<Example, ExampleId>
	{
	}
}
