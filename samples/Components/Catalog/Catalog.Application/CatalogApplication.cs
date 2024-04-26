namespace Catalog.Application
{
	using System.Reflection;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class CatalogApplication
	{
		public static Assembly Assembly => Assembly.GetExecutingAssembly();
	}
}
