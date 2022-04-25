namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the OData batch message quotas.
	/// </summary>
	[PublicAPI]
	public sealed class ODataBatchMessageQuotasOptions
	{
		/// <summary>
		///     Gets or sets the maximum depth of nesting allowed when reading or writing recursive payloads.
		/// </summary>
		/// <returns>
		///     The maximum depth of nesting allowed when reading or writing recursive payloads.
		/// </returns>
		public int MaxNestingDepth { get; set; } = 100;

		/// <summary>
		///     Gets or sets the maximum number of operations allowed in a single changeset.
		/// </summary>
		/// <returns>
		///     The maximum number of operations allowed in a single changeset.
		/// </returns>
		public int MaxOperationsPerChangeset { get; set; } = 1000;

		/// <summary>
		///     Gets or sets the maximum number of top level query operations and changesets allowed in a single batch.
		/// </summary>
		/// <returns>
		///     The maximum number of top level query operations and changesets allowed in a single batch.
		/// </returns>
		public int MaxPartsPerBatch { get; set; } = 100;

		/// <summary>
		///     Gets or sets the maximum number of bytes that should be read from the message.
		/// </summary>
		/// <returns>
		///     The maximum number of bytes that should be read from the message.
		/// </returns>
		public int MaxReceivedMessageSize { get; set; } = 1048576;
	}
}
