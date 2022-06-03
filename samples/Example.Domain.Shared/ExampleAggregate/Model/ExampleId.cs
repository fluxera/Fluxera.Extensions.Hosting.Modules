namespace Example.Domain.Shared.ExampleAggregate.Model
{
	using Fluxera.StronglyTypedId;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ExampleId : StronglyTypedId<ExampleId, string>
	{
		/// <inheritdoc />
		public ExampleId(string value) : base(value)
		{
		}
	}
}
