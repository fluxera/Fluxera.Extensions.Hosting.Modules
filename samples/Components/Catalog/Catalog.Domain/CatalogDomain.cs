namespace Catalog.Domain
{
	using System.Reflection;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class CatalogDomain
	{
		public static Assembly Assembly => Assembly.GetExecutingAssembly();
	}
}
