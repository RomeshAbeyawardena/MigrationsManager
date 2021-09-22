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
