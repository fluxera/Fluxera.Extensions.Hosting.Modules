namespace Catalog.Domain.Example.Events
{
	using Catalog.Domain.Shared.Example;
	using Fluxera.Entity.DomainEvents;
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
