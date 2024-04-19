namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared
{
	using Fluxera.StronglyTypedId;
	using JetBrains.Annotations;

	/// <summary>
	///		A strongly-typed ID for a tenant.
	/// </summary>
	[PublicAPI]
	public sealed class TenantId : StronglyTypedId<TenantId, string>
	{
		/// <inheritdoc />
		public TenantId(string value) 
			: base(value)
		{
		}
	}
}
