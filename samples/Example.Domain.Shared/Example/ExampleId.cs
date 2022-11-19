namespace Example.Domain.Shared.Example
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
