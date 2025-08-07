using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TastyCore.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetTypesWithCustomAttribute<T>(this Assembly assembly) where T : Attribute
        {
            return assembly
                .GetTypes()
                .Where(t => t.GetCustomAttribute<T>() != null);
        }
    }
}