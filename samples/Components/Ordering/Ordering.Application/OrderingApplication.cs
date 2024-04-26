namespace Ordering.Application
{
	using System.Reflection;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class OrderingApplication
	{
		public static Assembly Assembly => Assembly.GetExecutingAssembly();
	}
}
