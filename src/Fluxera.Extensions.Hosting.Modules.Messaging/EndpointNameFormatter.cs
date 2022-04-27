namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using System;
	using MassTransit;

	/// <summary>
	///     Formats the endpoint names using kebab-case (dashed snake case)
	///     SubmitOrderConsumer -&gt; submit-order
	///     OrderState -&gt; order-state
	///     UpdateCustomerActivity -&gt; update-customer-execute, update-customer-compensate
	/// </summary>
	internal sealed class EndpointNameFormatter : IEndpointNameFormatter
	{
		private readonly string globalSubscriptionId;
		private readonly IEndpointNameFormatter innerFormatter;

		public EndpointNameFormatter(string globalSubscriptionId)
		{
			this.innerFormatter = KebabCaseEndpointNameFormatter.Instance;
			this.globalSubscriptionId = globalSubscriptionId ?? string.Empty;
		}

		public string TemporaryEndpoint(string tag)
		{
			string temporaryEndpoint = this.innerFormatter.TemporaryEndpoint(tag);
			return temporaryEndpoint;
		}

		public string Consumer<T>() where T : class, IConsumer
		{
			string name = GetConsumerName(typeof(T), this.innerFormatter.Consumer<T>());
			return this.SanitizeName(name);
		}

		public string Message<T>() where T : class
		{
			string name = GetMessageName(typeof(T), this.innerFormatter.Message<T>());
			return this.SanitizeName(name);
		}

		public string Saga<T>() where T : class, ISaga
		{
			string name = GetSagaName(typeof(T), this.innerFormatter.Saga<T>());
			return this.SanitizeName(name);
		}

		public string ExecuteActivity<T, TArguments>()
			where T : class, IExecuteActivity<TArguments>
			where TArguments : class
		{
			string name =
				GetActivityName(typeof(T), this.innerFormatter.ExecuteActivity<T, TArguments>());
			return this.SanitizeName(name);
		}

		public string CompensateActivity<T, TLog>() where T : class, ICompensateActivity<TLog> where TLog : class
		{
			string name =
				GetActivityName(typeof(T), this.innerFormatter.CompensateActivity<T, TLog>());
			return this.SanitizeName(name);
		}

		public string SanitizeName(string name)
		{
			string sanitizedName = name;

			if(!string.IsNullOrWhiteSpace(this.globalSubscriptionId))
			{
				sanitizedName = $"{name}__{this.globalSubscriptionId}";
			}

			return sanitizedName;
		}

		/// <inheritdoc />
		public string Separator => this.innerFormatter.Separator;

		private static string GetConsumerName(Type type, string consumerName)
		{
			if(type.IsGenericType)
			{
				type = type.GetGenericArguments()[0];
			}

			string name = $"{GetNamespaceName(type)}.{consumerName}";
			return name;
		}

		private static string GetMessageName(Type type, string messageName)
		{
			if(type.IsGenericType)
			{
				type = type.GetGenericArguments()[0];
			}

			string name = $"{GetNamespaceName(type)}.{messageName}";
			return name;
		}

		private static string GetSagaName(Type type, string sagaName)
		{
			string name = $"{GetNamespaceName(type)}.{sagaName}";
			return name;
		}

		private static string GetActivityName(Type type, string activityName)
		{
			string name = $"{GetNamespaceName(type)}.{activityName}";
			return name;
		}

		private static string GetNamespaceName(Type type)
		{
			return (type.Namespace ?? string.Empty).ToLowerInvariant();
		}
	}
}
