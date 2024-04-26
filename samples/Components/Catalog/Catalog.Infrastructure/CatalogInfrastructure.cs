namespace Catalog.Infrastructure
{
	using JetBrains.Annotations;
	using System.Reflection;

	[PublicAPI]
	public static class CatalogInfrastructure
	{
		public static Assembly Assembly => Assembly.GetExecutingAssembly();
	}
}
