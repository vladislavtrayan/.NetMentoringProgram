using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Task1.CustomExceptions;
using Task1.DoNotChange;

namespace Task1
{
    public class Container
    {
        private readonly Dictionary<Type, Type> _map = new Dictionary<Type, Type>();

        public void AddAssembly(Assembly assembly)
        {
            var mappedTypes = assembly.GetTypes().Where(t =>
                Attribute.IsDefined(t, typeof(ExportAttribute)) ||
                Attribute.IsDefined(t, typeof(ImportConstructorAttribute)) ||
                t.GetProperties().Any(p => Attribute.IsDefined(p, typeof(ImportAttribute))) ||
                t.GetFields().Any(f => Attribute.IsDefined(f, typeof(ImportAttribute))));

            foreach (var type in mappedTypes)
                if (type.GetCustomAttributes(typeof(ExportAttribute)).FirstOrDefault() == null)
                    AddType(type);
                else if ((type.GetCustomAttributes(typeof(ExportAttribute)).FirstOrDefault() as ExportAttribute)
                         .Contract != null)
                    AddType(type,
                        (type.GetCustomAttributes(typeof(ExportAttribute)).First() as ExportAttribute).Contract);
                else
                    AddType(type);
        }

        public void AddType(Type type)
        {
            _map.Add(type, type);
        }

        public void AddType(Type type, Type baseType)
        {
            AddType(type);
            _map.Add(baseType, type);
        }

        public T Get<T>()
        {
            return (T)Resolve(typeof(T));
        }

        private object Resolve(Type type)
        {
            Type resolvedType;
            try
            {
                resolvedType = _map[type];
            }
            catch(Exception resolveException)
            {
                throw new TypeIsNotRegisteredExeption("Unable to find type : " + type, resolveException);
            }

            var props = resolvedType.GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(ImportAttribute)));
            var fields = resolvedType.GetFields().Where(
                field => Attribute.IsDefined(field, typeof(ImportAttribute)));

            object resolvedObject;
            if (Attribute.IsDefined(resolvedType, typeof(ImportConstructorAttribute)))
            {
                var ctor = resolvedType.GetConstructors().First();
                var ctorParameters = ctor.GetParameters();   
                var parameters = new List<object>();

                foreach (var p in ctorParameters)
                {
                    parameters.Add(Resolve(p.ParameterType));
                }
                resolvedObject = ctor.Invoke(parameters.ToArray());
            }
            else
            {
                resolvedObject = Activator.CreateInstance(resolvedType); 
            }
            foreach (var prop in props)
            {
                prop.SetValue(resolvedObject,Resolve(prop.PropertyType));
            }
            foreach (var field in fields)
            {
                field.SetValue(resolvedObject,Resolve(field.FieldType));
            }
            return resolvedObject;
        }
    }
}