using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoA
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
