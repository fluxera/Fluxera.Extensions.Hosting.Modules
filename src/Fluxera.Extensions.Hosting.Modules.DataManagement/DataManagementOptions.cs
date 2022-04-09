namespace Fluxera.Extensions.Hosting.Modules.DataManagement
{
	using Fluxera.Extensions.DataManagement;
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the data management module.
	/// </summary>
	[PublicAPI]
	public sealed class DataManagementOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="DataManagementOptions" /> type.
		/// </summary>
		public DataManagementOptions()
		{
			this.ConnectionStrings = new ConnectionStrings();
		}

		/// <summary>
		///     Gets the connection strings.
		/// </summary>
		public ConnectionStrings ConnectionStrings { get; internal set; }
	}
}
