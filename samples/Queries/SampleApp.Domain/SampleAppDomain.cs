namespace SampleApp.Domain
{
	using System.Reflection;
	using JetBrains.Annotations;

	/// <summary>
	///		Provides the assembly of the Domain module.
	/// </summary>
	[PublicAPI]
	public static class SampleAppDomain
	{
		/// <summary>
		///		Gets the Domain module assembly.
		/// </summary>
		public static Assembly Assembly => Assembly.GetExecutingAssembly();
	}
}
