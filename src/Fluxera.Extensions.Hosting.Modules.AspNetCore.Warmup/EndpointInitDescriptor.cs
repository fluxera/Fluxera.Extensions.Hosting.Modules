namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A descriptor of a endpoint init implementation.
	/// </summary>
	[PublicAPI]
	public sealed class EndpointInitDescriptor
	{
		/// <summary>
		///     Creates a new instance of the <see cref="EndpointInitDescriptor" /> type.
		/// </summary>
		/// <param name="type"></param>
		public EndpointInitDescriptor(Type type)
		{
			this.Type = type;
		}

		/// <summary>
		///     The type of the endpoint init class, that implements <see cref="IEndpointInit" />.
		/// </summary>
		public Type Type { get; }
	}
}
