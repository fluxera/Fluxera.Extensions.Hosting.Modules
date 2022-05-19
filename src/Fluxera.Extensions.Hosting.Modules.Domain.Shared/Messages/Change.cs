namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages
{
	using System;
	using System.ComponentModel.DataAnnotations;
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
		[Required]
		public string Path { get; set; }

		/// <summary>
		///     The value before the change.
		/// </summary>
		[Required]
		public object BeforeUpdateValue { get; set; }

		/// <summary>
		///     The value after the change.
		/// </summary>
		[Required]
		public object AfterUpdateValue { get; set; }

		/// <summary>
		///     The type of the changed property.
		/// </summary>
		[Required]
		public Type DataType { get; set; }
	}
}
