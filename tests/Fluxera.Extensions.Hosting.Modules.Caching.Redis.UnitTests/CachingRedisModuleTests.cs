namespace Fluxera.Extensions.Hosting.Modules.Caching.Redis.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.Caching.Distributed;
	using Microsoft.Extensions.Caching.StackExchangeRedis;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	[TestFixture]
	public class CachingRedisModuleTests : StartupModuleTestBase<CachingRedisModule>
	{
		[SetUp]
		public void SetUp()
		{
			this.StartApplication();
		}

		[TearDown]
		public void TearDown()
		{
			this.StopApplication();
		}

		[Test]
		public void ShouldAddDistributedRedisCache()
		{
			IDistributedCache distributedCache = this.ApplicationLoader.ServiceProvider.GetRequiredService<IDistributedCache>();
			distributedCache.Should().NotBeNull();
			distributedCache.Should().BeOfType<RedisCache>();
		}

		[Test]
		public void ShouldConfigureCachingRedisOptions()
		{
			IOptions<CachingRedisOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<CachingRedisOptions>>();
			options.Value.ConnectionStringName.Should().Be("RedisServer");
			options.Value.InstanceName.Should().Be("RedisInstance");

			options.Value.ConnectionStrings.Should().NotBeNullOrEmpty();
			options.Value.ConnectionStrings[options.Value.ConnectionStringName].Should().Be("localhost");
		}
	}
}
