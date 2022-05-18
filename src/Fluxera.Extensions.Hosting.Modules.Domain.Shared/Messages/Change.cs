namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using JetBrains.Annotations;

	[PublicAPI]
	[Serializable]
	public sealed class Change
	{
		[Required]
		public string Path { get; set; }

		[Required]
		public object BeforeUpdateValue { get; set; }

		[Required]
		public object AfterUpdateValue { get; set; }

		[Required]
		public Type DataType { get; set; }

		public bool IsNumeric { get; set; }
	}
}
