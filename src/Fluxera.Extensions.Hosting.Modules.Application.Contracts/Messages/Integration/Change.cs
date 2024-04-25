namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Integration
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A class holding information about the change of a property.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class Change
	{
		/// <summary>
		///     The path to the property in the original type.
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		///     The value before the change.
		/// </summary>
		public object BeforeUpdateValue { get; set; }

		/// <summary>
		///     The value after the change.
		/// </summary>
		public object AfterUpdateValue { get; set; }

		/// <summary>
		///     The type of the changed property.
		/// </summary>
		public Type DataType { get; set; }
	}
}
