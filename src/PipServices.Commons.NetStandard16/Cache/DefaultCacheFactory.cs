﻿using PipServices.Commons.Build;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Cache
{
    public class DefaultCacheFactory : IFactory, IDescriptable
    {
        private static readonly Descriptor ThisDescriptor = new Descriptor("pip-services-commons", "factory", "cache", "*", "1.0");
        private static readonly Descriptor NullCacheDescriptor = new Descriptor("pip-services-common", "cache", "null", "*", "1.0");
        private static readonly Descriptor MemoryCacheDescriptor = new Descriptor("pip-services-common", "cache", "memory", "*", "1.0");

        public bool CanCreate(object locator)
        {
            var descriptor = locator as Descriptor;

            if (descriptor == null)
                return false;

            if (descriptor.Match(NullCacheDescriptor))
                return true;

            if (descriptor.Match(MemoryCacheDescriptor))
                return true;

            return false;
        }

        public object Create(object locator)
        {
            var descriptor = locator as Descriptor;

            if (descriptor == null)
                return null;

            if (descriptor.Match(NullCacheDescriptor))
                return new NullCache();

            if (descriptor.Match(MemoryCacheDescriptor))
                return new MemoryCache(descriptor.Name);

            return null;
        }

        public Descriptor GetDescriptor()
        {
            return ThisDescriptor;
        }
    }
}