//namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.OData
//{
//	using System.IO;
//	using System.Linq;
//	using System.Text;
//	using System.Threading.Tasks;
//	using Microsoft.AspNetCore.Http;
//	using Microsoft.AspNetCore.Mvc.Filters;
//	using Microsoft.Extensions.Caching.Distributed;
//	using Microsoft.Extensions.Logging;
//	using Microsoft.Extensions.Options;
//	using Microsoft.Extensions.Primitives;

//	public sealed class IdempotentTokenFilter : IAsyncActionFilter, IAsyncResultFilter
//	{
//		private const string ItemKey = "__IdempotentResponseData";

//		private readonly IDistributedCache distributedCache;
//		private readonly ILogger<IdempotentTokenFilter> logger;
//		private readonly CorrelationIdOptions options;

//		public IdempotentTokenFilter(
//			IDistributedCache distributedCache,
//			IOptions<CorrelationIdOptions> options,
//			ILogger<IdempotentTokenFilter> logger)
//		{
//			this.distributedCache = distributedCache;
//			this.options = options.Value;
//			this.logger = logger;
//		}

//		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//		{
//			HttpRequest request = context.HttpContext.Request;

//			if(request.Method == "POST")
//			{
//				string token = request.Headers["X-Idempotent-Token"].FirstOrDefault();
//				if(!string.IsNullOrWhiteSpace(token))
//				{
//					IdempotentResponseData responseData =
//						await this.distributedCache.GetAsJsonAsync<IdempotentResponseData>(token, Encoding.UTF8);
//					if(responseData != null)
//					{
//						this.logger.LogDebug("Found duplicate request, acquired data from cache: {IdempotentToken}",
//							token);

//						context.HttpContext.Response.OnStarting(state =>
//						{
//							HttpContext httpContext = (HttpContext)state;
//							IdempotentResponseData data = (IdempotentResponseData)httpContext.Items[ItemKey];

//							// Add the response headers.
//							foreach((string key, string value) in data.Headers)
//							{
//								httpContext.Response.Headers[key] = value;
//							}

//							return Task.CompletedTask;
//						}, context.HttpContext);

//						context.HttpContext.Items[ItemKey] = responseData;

//						context.HttpContext.Response.StatusCode = responseData.StatusCode;

//						await context.HttpContext.Response.WriteAsync(responseData.Body);

//						await context.HttpContext.Response.CompleteAsync();
//					}
//					else
//					{
//						await next();
//					}
//				}
//				else
//				{
//					await next();
//				}
//			}
//			else
//			{
//				await next();
//			}
//		}

//		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
//		{
//			if((context.HttpContext.Request.Method == "POST") &&
//			   !context.HttpContext.Items.ContainsKey(ItemKey))
//			{
//				string token = context.HttpContext.Request.Headers["X-Idempotent-Token"].FirstOrDefault();
//				if(!string.IsNullOrWhiteSpace(token))
//				{
//					Stream originalBody = context.HttpContext.Response.Body;

//					try
//					{
//						await using(MemoryStream memoryStream = new MemoryStream())
//						{
//							context.HttpContext.Response.Body = memoryStream;

//							await next();

//							IdempotentResponseData toStore = new IdempotentResponseData();

//							// Read the body data.
//							memoryStream.Rewind();
//							using(StreamReader reader = new StreamReader(memoryStream))
//							{
//								toStore.Body = await reader.ReadToEndAsync();

//								memoryStream.Position = 0;
//								await memoryStream.CopyToAsync(originalBody);
//							}

//							// Read additional headers.
//							foreach((string key, StringValues values) in context.HttpContext.Response.Headers)
//							{
//								if((key == "Transfer-Encoding") || (key == "Date") || (key == this.options.ResponseHeader))
//								{
//									continue;
//								}

//								toStore.Headers.Add(key, values.ToString());
//							}

//							// Read the status code.
//							toStore.StatusCode = context.HttpContext.Response.StatusCode;

//							this.logger.LogDebug("Storing response data in cache: {IdempotentToken}", token);

//							await this.distributedCache.SetAsJsonAsync(token, toStore, Encoding.UTF8);
//						}
//					}
//					finally
//					{
//						context.HttpContext.Response.Body = originalBody;
//					}
//				}
//				else
//				{
//					context.HttpContext.Items.Remove(ItemKey);
//					await next();
//				}
//			}
//			else
//			{
//				context.HttpContext.Items.Remove(ItemKey);
//				await next();
//			}
//		}
//	}
//}


