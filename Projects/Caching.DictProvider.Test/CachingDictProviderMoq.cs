using System;
using System.Collections.Generic;
using Xunit;
using LOGI.Framework.Toolkit.Core.Caching.Provider.VelocityProvider;
using LOGI.Framework.Toolkit.Core.Client;
using LOGI.Framework.Toolkit.Foundation.Caching;

namespace LOGI.Framework.Caching.DictProvider.Test
{
    public class CachingDictProviderMoq
    {
        [Fact]
        public void InitializeProxiesInstanceNotNull()
        {
            Assert.NotNull(Proxies.Instance);
        }

        [Fact]
        public void InitializeCacheInstanceNotNull()
        {
            var aaa = Proxies.Instance.Get<ICacheWrapper>();

            var aaaa = Proxies.Instance.CacheWrapper;

            var abc = aaaa.Save("Test", "123");

            var xyz = aaaa.Get<string>("Test");

            Assert.NotNull(aaa);

        }

        [Fact]
        public void CacheInstanceSave()
        {
            Assert.DoesNotThrow(()=>Proxies.Instance.CacheWrapper.Save("test", 1));
            Assert.Equal(1, Proxies.Instance.CacheWrapper.Get<int>("test"));
        }

        [Fact]
        public void CacheInstanceGet()
        {
            Assert.Equal(1, Proxies.Instance.CacheWrapper.Get<int>("test"));
        }
    }
}
