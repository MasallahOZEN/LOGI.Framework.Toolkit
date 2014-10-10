using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace LOGI.Framework.Toolkit.Core.Reflection
{
    public static class PropertyUtil
    {
        public static string GetPropertyName<TObject, TProp>(this TObject type,
                                                       Expression<Func<TObject, TProp>> propertyRefExpr)
        {
            return GetPropertyNameCore(propertyRefExpr.Body);
        }

        public static string GetName<TObject, TProp>(Expression<Func<TObject, TProp>> propertyRefExpr)
        {
            return GetPropertyNameCore(propertyRefExpr.Body);
        }

        public static Tuple<string,T> GetNameWithValue<T>(Expression<Func<T>> e,bool withValue=false)
        {
            var member = (MemberExpression)e.Body;
            string propertyName = member.Member.Name;
            if (withValue)
            {
                T value = e.Compile()();
                return new Tuple<string, T>(propertyName, value);
            }
            else
            {
                return new Tuple<string, T>(propertyName, default(T));
            }

        }

        private static string GetPropertyNameCore(Expression propertyRefExpr)
        {
            var retrunValue = "";
            if (propertyRefExpr == null)
                throw new ArgumentNullException("propertyRefExpr", "propertyRefExpr is null.");

            MemberExpression memberExpr = propertyRefExpr as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyRefExpr as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                    memberExpr = unaryExpr.Operand as MemberExpression;
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                retrunValue = memberExpr.Member.Name;
                return retrunValue;
            }

            throw new ArgumentException(string.Format("No property reference expression was found.Detail:[{0}]", propertyRefExpr.ToString()),
                             "propertyRefExpr");
        }
    }

}
