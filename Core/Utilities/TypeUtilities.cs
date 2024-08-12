using System;
using System.Collections.Generic;
using System.Linq;

namespace iCare
{
    public static class TypeUtilities
    {
        public static IEnumerable<Type> GetAllTypesThatImplementInterface<T>() where T : class
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(T).IsAssignableFrom(type) && !type.IsInterface);
        }
    }
}