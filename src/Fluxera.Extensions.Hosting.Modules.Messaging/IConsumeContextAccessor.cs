namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     Provides access to the current <see cref="ConsumeContext" />, if one is available.
	/// </summary>
	/// <remarks>
	///     This interface should be used with caution. It relies on <see cref="System.Threading.AsyncLocal{T}" /> which can
	///     have a negative performance impact on async calls. It also creates a dependency on "ambient state" which can make
	///     testing more difficult.
	/// </remarks>
	[PublicAPI]
	public interface IConsumeContextAccessor
	{
		/// <summary>
		///     Gets or sets the current <see cref="ConsumeContext" />.
		///     Returns <see langword="null" /> if there is no active <see cref="ConsumeContext" />.
		/// </summary>
		ConsumeContext ConsumeContext { get; set; }
	}
}
