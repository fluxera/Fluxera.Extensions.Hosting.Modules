namespace SampleApp.Domain.Shared.Customers
{
	using System.Runtime.CompilerServices;
	using Fluxera.Enumeration;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class CustomerState : Enumeration<CustomerState>
	{
		public static readonly CustomerState Legacy = new CustomerState(69);

		public static readonly CustomerState New = new CustomerState(42);

		/// <inheritdoc />
		private CustomerState(int value, [CallerMemberName] string name = null)
			: base(value, name)
		{
		}
	}
}
