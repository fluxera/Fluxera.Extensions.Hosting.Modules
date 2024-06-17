namespace SampleApp.Infrastructure
{
	using System.Reflection;
	using JetBrains.Annotations;

	/// <summary>
	///		Provides the assembly of the Infrastructure module.
	/// </summary>
	[PublicAPI]
	public static class SampleAppInfrastructure
	{
		/// <summary>
		///		Gets the Infrastructure module assembly.
		/// </summary>
		public static Assembly Assembly => Assembly.GetExecutingAssembly();
	}
}
