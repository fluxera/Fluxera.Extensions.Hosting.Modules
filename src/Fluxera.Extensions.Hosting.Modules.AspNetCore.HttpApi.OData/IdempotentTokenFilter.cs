namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Caching;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc.Filters;
	using Microsoft.Extensions.Caching.Distributed;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Primitives;

	[PublicAPI]
	internal sealed class IdempotentTokenFilter : IAsyncActionFilter, IAsyncResultFilter
	{
		private const string ItemKey = "__IdempotentResponseData";

		private readonly IDistributedCache distributedCache;
		private readonly ILogger<IdempotentTokenFilter> logger;

		/// <summary>
		///     Initializes a new instance of the <see cref="IdempotentTokenFilter" /> type.
		/// </summary>
		/// <param name="distributedCache"></param>
		/// <param name="logger"></param>
		public IdempotentTokenFilter(IDistributedCache distributedCache, ILogger<IdempotentTokenFilter> logger)
		{
			this.distributedCache = distributedCache;
			this.logger = logger;
		}

		/// <inheritdoc />
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			HttpRequest request = context.HttpContext.Request;

			if(request.Method == "POST")
			{
				string token = request.Headers["X-Idempotent-Token"].FirstOrDefault();
				if(!string.IsNullOrWhiteSpace(token))
				{
					IdempotentResponseData responseData =
						await this.distributedCache.GetAsJsonAsync<IdempotentResponseData>(token, Encoding.UTF8);
					if(responseData != null)
					{
						this.logger.LogFoundDuplicateRequest(token);

						context.HttpContext.Response.OnStarting(state =>
						{
							HttpContext httpContext = (HttpContext)state;
							IdempotentResponseData data = (IdempotentResponseData)httpContext.Items[ItemKey];

							// Add the response headers.
							foreach((string key, string value) in data?.Headers ?? new Dictionary<string, string>())
							{
								httpContext.Response.Headers[key] = value;
							}

							return Task.CompletedTask;
						}, context.HttpContext);

						context.HttpContext.Items[ItemKey] = responseData;

						context.HttpContext.Response.StatusCode = responseData.StatusCode;

						await context.HttpContext.Response.WriteAsync(responseData.Body);

						await context.HttpContext.Response.CompleteAsync();
					}
					else
					{
						await next();
					}
				}
				else
				{
					await next();
				}
			}
			else
			{
				await next();
			}
		}

		/// <inheritdoc />
		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			if(context.HttpContext.Request.Method == "POST" && !context.HttpContext.Items.ContainsKey(ItemKey))
			{
				string token = context.HttpContext.Request.Headers["X-Idempotent-Token"].FirstOrDefault();
				if(!string.IsNullOrWhiteSpace(token))
				{
					Stream originalBody = context.HttpContext.Response.Body;

					try
					{
						await using(MemoryStream memoryStream = new MemoryStream())
						{
							context.HttpContext.Response.Body = memoryStream;

							await next();

							IdempotentResponseData toStore = new IdempotentResponseData();

							// Read the body data.
							memoryStream.Rewind();
							using(StreamReader reader = new StreamReader(memoryStream))
							{
								toStore.Body = await reader.ReadToEndAsync();

								memoryStream.Position = 0;
								await memoryStream.CopyToAsync(originalBody);
							}

							// Read additional headers.
							foreach((string key, StringValues values) in context.HttpContext.Response.Headers)
							{
								if(key is "Transfer-Encoding" or "Date")
								{
									continue;
								}

								toStore.Headers.Add(key, values.ToString());
							}

							// Read the status code.
							toStore.StatusCode = context.HttpContext.Response.StatusCode;

							this.logger.LogStoringResponseData(token);

							await this.distributedCache.SetAsJsonAsync(token, toStore, Encoding.UTF8);
						}
					}
					finally
					{
						context.HttpContext.Response.Body = originalBody;
					}
				}
				else
				{
					context.HttpContext.Items.Remove(ItemKey);
					await next();
				}
			}
			else
			{
				context.HttpContext.Items.Remove(ItemKey);
				await next();
			}
		}
	}
}
