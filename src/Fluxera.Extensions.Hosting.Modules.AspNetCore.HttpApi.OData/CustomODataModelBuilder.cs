namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.OData.ModelBuilder;

	/// <summary>
	///     A custom OData model builder that configures some defaults automatically.
	/// </summary>
	[PublicAPI]
	public sealed class CustomODataModelBuilder : ODataConventionModelBuilder
	{
		/// <summary>
		///     Creates a new instance of the <see cref="CustomODataModelBuilder" /> type.
		/// </summary>
		public CustomODataModelBuilder()
		{
			this.ContainerName = "ODataContext";
		}

		/// <inheritdoc />
		public override ComplexTypeConfiguration AddComplexType(Type type)
		{
			ComplexTypeConfiguration x = base.AddComplexType(type);
			x.Namespace = this.Namespace;

			if(x.Name.EndsWith("Dto"))
			{
				x.Name = x.Name[..^3];
			}

			return x;
		}

		/// <inheritdoc />
		public override EntityTypeConfiguration AddEntityType(Type type)
		{
			EntityTypeConfiguration x = base.AddEntityType(type);
			x.Namespace = this.Namespace;

			if(x.Name.EndsWith("Dto"))
			{
				x.Name = x.Name[..^3];
			}

			return x;
		}

		/// <inheritdoc />
		public override EnumTypeConfiguration AddEnumType(Type type)
		{
			EnumTypeConfiguration x = base.AddEnumType(type);
			x.Namespace = this.Namespace;

			if(x.Name.EndsWith("Dto"))
			{
				x.Name = x.Name[..^3];
			}

			return x;
		}

		/// <inheritdoc />
		public override void AddOperation(OperationConfiguration operation)
		{
			operation.Namespace = this.Namespace;
			base.AddOperation(operation);
		}
	}
}
