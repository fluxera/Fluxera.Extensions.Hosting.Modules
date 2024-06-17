namespace SampleApp.Application
{
	using System.Reflection;
	using JetBrains.Annotations;

	/// <summary>
	///		Provides the assembly of the Application module.
	/// </summary>
	[PublicAPI]
	public static class SampleAppApplication
	{
		/// <summary>
		///		Gets the Application module assembly.
		/// </summary>
		public static Assembly Assembly => Assembly.GetExecutingAssembly();
	}
}
