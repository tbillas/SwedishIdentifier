using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Billas.Identifier
{
    public class PersonIdentifierTypeLoader
    {
        private static readonly List<TypeInfo> LoadedTypes;

        static PersonIdentifierTypeLoader()
        {
            LoadedTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.DefinedTypes)
                .Where(typeInfo => !typeInfo.IsAbstract && typeof(IPersonIdentifier).IsAssignableFrom(typeInfo.AsType()))
                .ToList();
        }

        public TypeInfo FindType(string oid)
        {
            return LoadedTypes.Find(type =>
            {
                var typeOid = type.GetField("Oid", BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
                return typeOid != null && Equals(typeOid, oid);
            });
        }

        public ConstructorInfo GetConstructorWithStringParameter(TypeInfo type)
        {
            return type.DeclaredConstructors.FirstOrDefault(ctor =>
            {
                var parameters = ctor.GetParameters();
                return ctor.IsPublic && parameters.Length == 1 && parameters[0].ParameterType == typeof(string);
            });
        }

        public ConstructorInfo GetConstructorWithFormatterParameter(TypeInfo type, out Type formatterType)
        {
            foreach (var ctor in type.DeclaredConstructors)
            {
                var parameters = ctor.GetParameters();
                if (parameters.Length == 1 && parameters[0].ParameterType.IsSubclassOf(typeof(PersonIdentifierFormatter)))
                {
                    formatterType = parameters[0].ParameterType;
                    return ctor;
                }
            }

            formatterType = null;
            return null;
        }
    }
}