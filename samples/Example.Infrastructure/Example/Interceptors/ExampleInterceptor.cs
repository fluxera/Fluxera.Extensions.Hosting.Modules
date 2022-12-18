namespace Catalog.Infrastructure.Example.Interceptors
{
	using Catalog.Domain.Example;
	using Catalog.Domain.Shared.Example;
	using Fluxera.Repository.Interception;
	using JetBrains.Annotations;

	/// <summary>
	///     A repository interceptor for the example aggregate root.
	/// </summary>
	[UsedImplicitly]
	public sealed class ExampleInterceptor : InterceptorBase<Example, ExampleId>
	{
	}
}
