using System;
using System.Collections.Generic;

namespace MemoryQueue
{

    public delegate object ServiceFactory(Type serviceType);

    public static class ServiceFactoryExtensions
    {
        public static T GetService<T>(this ServiceFactory factory) => (T)factory(typeof(T));
        public static IEnumerable<T> GetServices<T>(this ServiceFactory factory) => (IEnumerable<T>)factory(typeof(IEnumerable<T>));
    }
}
