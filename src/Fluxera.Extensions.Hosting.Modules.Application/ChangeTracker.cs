namespace Fluxera.Extensions.Hosting.Modules.Application
{
	using System.Collections.Generic;
	using System.Dynamic;
	using System.Runtime.CompilerServices;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	/// <summary>
	///     A change tracker implementation.
	/// </summary>
	/// <typeparam name="TDto"></typeparam>
	[PublicAPI]
	public sealed class ChangeTracker<TDto> : IChangeTracker
		where TDto : class, IPatchableEntityDto
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ChangeTracker{TDto}" /> class.
		/// </summary>
		public ChangeTracker()
		{
			this.ChangedProperties = new Dictionary<string, object>();
		}

		/// <summary>
		///     Gets the changed properties.
		/// </summary>
		/// <value>
		///     The changed properties.
		/// </value>
		private IDictionary<string, object> ChangedProperties { get; }

		/// <inheritdoc />
		public int Count => this.ChangedProperties.Count;

		/// <inheritdoc />
		public TValue Get<TValue>([CallerMemberName] string propertyName = null)
		{
			propertyName = Guard.Against.NullOrWhiteSpace(propertyName);

			if(this.ChangedProperties.ContainsKey(propertyName))
			{
				return (TValue)this.ChangedProperties[propertyName];
			}

			return default;
		}

		/// <inheritdoc />
		public void SetIfChanged<TValue>(TValue newValue, [CallerMemberName] string propertyName = null)
		{
			propertyName = Guard.Against.NullOrWhiteSpace(propertyName);

			TValue oldValue = this.Get<TValue>(propertyName);
			if(EqualityComparer<TValue>.Default.Equals(oldValue, newValue))
			{
				return;
			}

			if(!this.ChangedProperties.ContainsKey(propertyName))
			{
				this.ChangedProperties.Add(propertyName, newValue);
			}
			else
			{
				this.ChangedProperties[propertyName] = newValue;
			}
		}

		/// <inheritdoc />
		object IChangeTracker.GetChangesObject()
		{
			ExpandoObject expandoObject = new ExpandoObject();
			ICollection<KeyValuePair<string, object>> collection = expandoObject;

			foreach(KeyValuePair<string, object> item in this.ChangedProperties)
			{
				collection.Add(item);
			}

			dynamic dynamicObject = expandoObject;
			return dynamicObject;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"ChangeTracker<{typeof(TDto).Name}>";
		}
	}
}
