namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the OData batching support.
	/// </summary>
	[PublicAPI]
	public sealed class ODataBatchOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="ODataBatchOptions" /> type.
		/// </summary>
		public ODataBatchOptions()
		{
			this.MessageQuotas = new ODataBatchMessageQuotasOptions();
		}

		/// <summary>
		///     Flag, indicating if the batching is enabled.
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		///     Gets or sets the batch message quotas options.
		/// </summary>
		public ODataBatchMessageQuotasOptions MessageQuotas { get; set; }
	}
}
