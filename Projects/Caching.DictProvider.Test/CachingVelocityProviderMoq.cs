using System;
using System.Collections.Generic;
using Xunit;
using LOGI.Framework.Toolkit.Core.Caching.Provider.VelocityProvider;
using LOGI.Framework.Toolkit.Core.Client;
using LOGI.Framework.Toolkit.Foundation.Caching;

namespace LOGI.Framework.Caching.DictProvider.Test
{
    public class CachingVelocityProviderMoq
    {
        [Fact]
        public void InitializeProxiesInstanceNotNull()
        {
            Assert.NotNull(Proxies.Instance);
        }

        private class MyClass:ICacheWrapper
        {
            public MyClass()
            {
                
            }

            public T Get<T>(string key)
            {
                return default(T);
            }

            public bool Exist(string key)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<T> Find<T>(Func<T, bool> predicate)
            {
                throw new NotImplementedException();
            }

            public bool Save<T>(string key, T data, TimeSpan? timeout, bool overriteIfExist)
            {
                throw new NotImplementedException();
            }

            public bool Remove(string key)
            {
                throw new NotImplementedException();
            }

            public bool Remove<T>(Func<T, bool> predicate)
            {
                throw new NotImplementedException();
            }

            public bool Clear()
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void Initialize_RegisterTest()
        {
            var velocityCacheWrapper = Proxies.Instance.VelocityCacheWrapper;
            Assert.NotNull(velocityCacheWrapper);

            Proxies.Instance.Register<ICacheWrapper, MyClass>("Velocity", removeIfExists: true);

            velocityCacheWrapper = Proxies.Instance.VelocityCacheWrapper;

            var aaa = velocityCacheWrapper.Get<string>("a");

            Assert.Null(velocityCacheWrapper);
        }

        [Fact]
        public void Initialize_VelocityCacheInstanceNotNull()
        {
            var velocityCacheWrapper = Proxies.Instance.VelocityCacheWrapper;
            Assert.NotNull(velocityCacheWrapper);
        }

        [Fact]
        public void Initialize_Velocity_NewRegion_CacheInstanceNotNull()
        {
            var velocityCacheWrapper = Proxies.Instance.VelocityCacheWrapper;
            Assert.NotNull(velocityCacheWrapper);

            Proxies.Instance.ReNew<ICacheWrapper>(velocityCacheWrapper.GetType(), "Velocity_WithHSBCRegion", true, "HSBC");

            var newInstance = Proxies.Instance.Get<ICacheWrapper>("Velocity_WithHSBCRegion");

            Assert.NotNull(newInstance);
        }

        [Fact]
        public void Add_VelocityCacheInstanceNotNull()
        {
            string regionName1 = "default";

            var velocityCacheWrapper = Proxies.Instance.VelocityCacheWrapper;

            try
            {

                var cacheWrapperItem = new CacheWrapperItem("key1", "key1 eklendi", new string[] { "key", "test" }) { Region = regionName1 };
                velocityCacheWrapper.Save<CacheWrapperItem>("Key1", cacheWrapperItem, new TimeSpan(0, 0, 0, 10));

                var cacheItem = velocityCacheWrapper.Get<CacheWrapperItem>("Key1");

                velocityCacheWrapper.Save<CacheWrapperItem>("Key1", cacheWrapperItem, new TimeSpan(0, 0, 0, 20), true);

                cacheItem = velocityCacheWrapper.Get<CacheWrapperItem>("Key1");

                Assert.NotNull(velocityCacheWrapper);
            }
            catch (Exception)
            {

            }

        }

        [Fact]
        public void Add_VelocityCacheInstanceNotNull_DiffrentRegions()
        {
            string regionName1 = "default";
            string regionName2 = "HSBC";

            var velocityCacheWrapper = Proxies.Instance.VelocityCacheWrapper;

            #region Region1
            try
            {

                var cacheWrapperItem = new CacheWrapperItem("key1", "key1 eklendi", new string[] {"key", "test"});
                velocityCacheWrapper.Save<CacheWrapperItem>("Key1", cacheWrapperItem, new TimeSpan(0, 0, 0, 10));

                var cacheItem = velocityCacheWrapper.Get<CacheWrapperItem>("Key1");

                velocityCacheWrapper.Save<CacheWrapperItem>("Key1", cacheWrapperItem, new TimeSpan(0, 0, 0, 20), true);

                cacheItem = velocityCacheWrapper.Get<CacheWrapperItem>("Key1");

                Assert.NotNull(velocityCacheWrapper);
            }
            catch (Exception)
            {

            } 
            #endregion
            
            #region Region2
            try
            {
                Proxies.Instance.ReNew<ICacheWrapper>(velocityCacheWrapper.GetType(), "Velocity_WithHSBCRegion", true, regionName2);
                var newInstance = Proxies.Instance.Get<ICacheWrapper>("Velocity_WithHSBCRegion");
                Assert.NotNull(newInstance);

                var cacheItem = newInstance.Get<CacheWrapperItem>("Key1");

                var cacheWrapperItem = new CacheWrapperItem("key1", "key1 eklendi", new string[] {"key", "test"});
                newInstance.Save<CacheWrapperItem>("Key1", cacheWrapperItem, new TimeSpan(0, 0, 0, 10));

                cacheItem = newInstance.Get<CacheWrapperItem>("Key1");

                newInstance.Save<CacheWrapperItem>("Key1", cacheWrapperItem, new TimeSpan(0, 0, 0, 20), true);

                cacheItem = newInstance.Get<CacheWrapperItem>("Key1");

                
            }
            catch (Exception)
            {

            } 
            #endregion

        }

        [Fact]
        public void Add_VelocityCacheInstanceNotNull2()
        {
            
            try
            {
                var cacheWorker = CacheWorker.Instance;

                //var cacheWrapperItem = new CacheWrapperItem("key1", "key1 eklendi", new string[] { "key", "test" });
                //velocityCacheWrapper.Save<CacheWrapperItem>("Key1", cacheWrapperItem, new TimeSpan(0, 0, 0, 10));

                //var cacheItem = velocityCacheWrapper.Get<CacheWrapperItem>("Key1");

                //velocityCacheWrapper.Save<CacheWrapperItem>("Key1", cacheWrapperItem, new TimeSpan(0, 0, 0, 20), true);

                //cacheItem = velocityCacheWrapper.Get<CacheWrapperItem>("Key1");

                //Assert.NotNull(velocityCacheWrapper);
            }
            catch (Exception)
            {

            }

        }

        [Fact]
        public void Get_VelocityCacheInstanceNotNull()
        {
            var velocityCacheWrapper = Proxies.Instance.VelocityCacheWrapper;
            Assert.NotNull(velocityCacheWrapper);

            var cacheWrapperItem = new CacheWrapperItem("key1", "key1 eklendi", new string[] { "key", "test" });
            var cacheItem=velocityCacheWrapper.Get<CacheWrapperItem>("Key1");

            Assert.NotNull(cacheItem);

        }

        [Fact]
        public void Remove_VelocityCacheInstanceNotNull()
        {
            var velocityCacheWrapper = Proxies.Instance.VelocityCacheWrapper;
            Assert.NotNull(velocityCacheWrapper);

            var res=velocityCacheWrapper.Remove("Key1");

            Assert.True(res);

        }
    }
}
