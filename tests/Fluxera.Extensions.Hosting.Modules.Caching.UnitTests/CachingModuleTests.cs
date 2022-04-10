namespace Fluxera.Extensions.Hosting.Modules.Caching.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.Caching.Distributed;
	using Microsoft.Extensions.Caching.Memory;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	public class CachingModuleTests : StartupModuleTestBase<CachingModule>
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
		public void ShouldConfigureCaching()
		{
			IOptions<CachingOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<CachingOptions>>();
			options.Value.DistributedCache.AbsoluteExpiration.Should().Be(DateTimeOffset.Parse("12:00:00"));
			options.Value.DistributedCache.AbsoluteExpirationRelativeToNow.Should().Be(TimeSpan.Parse("00:30:00"));
			options.Value.DistributedCache.SlidingExpiration.Should().Be(TimeSpan.Parse("00:15:00"));
		}

		[Test]
		public void ShouldAddMemoryCache()
		{
			IMemoryCache memoryCache = this.ApplicationLoader.ServiceProvider.GetRequiredService<IMemoryCache>();
			memoryCache.Should().NotBeNull();
			memoryCache.Should().BeOfType<MemoryCache>();
		}

		[Test]
		public void ShouldAddDistributedCache()
		{
			IDistributedCache distributedCache = this.ApplicationLoader.ServiceProvider.GetRequiredService<IDistributedCache>();
			distributedCache.Should().NotBeNull();
			distributedCache.Should().BeOfType<MemoryDistributedCache>();
		}
	}
}
