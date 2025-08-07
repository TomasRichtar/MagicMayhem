using System;
using System.Collections.Generic;
using System.Linq;

using TastyCore.Attributes;
using TastyCore.Extensions;

namespace TastyCore.Patterns.ServiceLocator
{
    public static class ReflectionService
    {
        public static IEnumerable<Type> GetAllAutoRegisteredServices()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypesWithCustomAttribute<AutoRegisteredServiceAttribute>())
                .Where(service => typeof(IRegistrable).IsAssignableFrom(service));
        }
    }
}