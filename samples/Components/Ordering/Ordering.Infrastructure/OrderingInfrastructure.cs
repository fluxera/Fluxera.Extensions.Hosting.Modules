namespace Ordering.Infrastructure
{
	using System.Reflection;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class OrderingInfrastructure
	{
		public static Assembly Assembly => Assembly.GetExecutingAssembly();
	}
}
