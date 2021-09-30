using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static void Extend<T>(this T model, T newModel)
        {
            var modelType = typeof(T);

            var enumerableType = typeof(IEnumerable<>);
            var listType = typeof(List<>);

            foreach (var property in modelType.GetProperties())
            {
                var propertyType = property.PropertyType;

                var isListType = false;
                var isEnumerableType = false; 
                var genericArguments = propertyType.GetGenericArguments();
                var addRangeMethod = property.PropertyType.GetMethod("AddRange");
                if (propertyType.IsArray || propertyType.IsGenericType
                    && (isEnumerableType = propertyType == enumerableType.MakeGenericType(genericArguments))
                    || (isListType = propertyType == listType.MakeGenericType(genericArguments)))
                {
                    var newValues = property.GetValue(newModel);

                    if (isListType)
                    {
                        addRangeMethod?.Invoke(model, new[] { newValues });
                    }
                    else if (isEnumerableType)
                    {
                        var modelValue = property.GetValue(model);

                        var list = Activator.CreateInstance(listType.MakeGenericType(genericArguments), modelValue);
                        addRangeMethod.Invoke(list, new[] { newValues });
                        property.SetValue(model, list);
                    }
                }
            }
        }

        public static void ResolveDependencies(this object value, IModuleServiceProvider moduleServiceProvider)
        {
            var valueType = value.GetType();
            var properties = valueType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var propertyOrField in properties)
            {
                if(propertyOrField.GetCustomAttribute<ResolveAttribute>() == null)
                {
                    continue;
                }

                var service = moduleServiceProvider.GetService(propertyOrField.PropertyType);
                propertyOrField.SetValue(value, service);
            }
        }

        public static void Set<T, TProperty, TValue>(this T value, Expression<Func<T, TProperty>> getProperty, TValue newValue)
        {
            PropertyInfo propertyInfo;
            Expression body = getProperty;
            if (body is LambdaExpression expression)
            {
                body = expression.Body;
            }
            switch (body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    propertyInfo = (PropertyInfo)((MemberExpression)body).Member;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            propertyInfo.SetValue(value, newValue);
        }
    }

}
