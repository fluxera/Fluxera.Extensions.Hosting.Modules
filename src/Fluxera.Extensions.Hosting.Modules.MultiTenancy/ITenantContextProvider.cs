namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a service that provides the current tenant context.
	/// </summary>
	[PublicAPI]
	public interface ITenantContextProvider
	{
		/// <summary>
		///     Gets the current tenant context. Fails with an exception if the context could not be found.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		TenantContext GetTenantContext();

		/// <summary>
		///     Tries to get the current tenant context. Returns <c>false</c> if the context could not be found.
		/// </summary>
		/// <param name="tenantContext"></param>
		/// <returns></returns>
		bool TryGetTenantContext(out TenantContext tenantContext);
	}
}
