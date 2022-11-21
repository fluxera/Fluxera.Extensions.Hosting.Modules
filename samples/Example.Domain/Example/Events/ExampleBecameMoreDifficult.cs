namespace Example.Domain.Example.Events
{
	using Fluxera.Entity.DomainEvents;
	using global::Example.Domain.Shared.Example;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ExampleBecameMoreDifficult : IDomainEvent
	{
		public ExampleBecameMoreDifficult(ExampleId exampleID, string exampleName)
		{
			this.ExampleID = exampleID;
			this.ExampleName = exampleName;
		}

		public ExampleId ExampleID { get; }

		public string ExampleName { get; }
	}
}
