namespace Fluxera.Extensions.Hosting.Modules.ODataClient.UnitTests
{
	using Fluxera.Extensions.OData;

	public class Person : IODataEntity<string>
	{
		/// <inheritdoc />
		public string ID { get; set; }
	}
}
