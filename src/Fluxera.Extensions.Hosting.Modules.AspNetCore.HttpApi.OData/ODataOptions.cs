namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options of the OData module.
	/// </summary>
	[PublicAPI]
	public sealed class ODataOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="ODataOptions" /> type.
		/// </summary>
		public ODataOptions()
		{
			this.Batching = new ODataBatchOptions();
		}

		/// <summary>
		///     Gets or sets the batching options.
		/// </summary>
		public ODataBatchOptions Batching { get; set; }
	}
}
