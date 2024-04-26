namespace Ordering.Domain
{
	using System.Reflection;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class OrderingDomain
	{
		public static Assembly Assembly => Assembly.GetExecutingAssembly();
	}
}
