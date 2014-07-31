using System;
using System.Collections.Generic;

namespace JobRecord
{
    public class ServiceLocator
    {
        public static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void AddService<T>(T t)
        {
            _services.Add(typeof(T), t);
        }

        public static T GetService<T>()
        {
            return (T)_services[typeof(T)];
        }
    }
}
