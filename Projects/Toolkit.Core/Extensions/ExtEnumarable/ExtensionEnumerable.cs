using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using LOGI.Framework.Toolkit.Core.Common.Comparers;
using LOGI.Framework.Toolkit.Core.Components;

namespace LOGI.Framework.Toolkit.Core.Extensions.ExtEnumarable
{

    namespace PredicateBuilders
    {
        public abstract class ExpressionVisitor
        {
            protected ExpressionVisitor()
            {

            }

            protected virtual Expression Visit(Expression exp)
            {

                if (exp == null)

                    return exp;

                switch (exp.NodeType)
                {

                    case ExpressionType.Negate:

                    case ExpressionType.NegateChecked:

                    case ExpressionType.Not:

                    case ExpressionType.Convert:

                    case ExpressionType.ConvertChecked:

                    case ExpressionType.ArrayLength:

                    case ExpressionType.Quote:

                    case ExpressionType.TypeAs:

                        return this.VisitUnary((UnaryExpression)exp);

                    case ExpressionType.Add:

                    case ExpressionType.AddChecked:

                    case ExpressionType.Subtract:

                    case ExpressionType.SubtractChecked:

                    case ExpressionType.Multiply:

                    case ExpressionType.MultiplyChecked:

                    case ExpressionType.Divide:

                    case ExpressionType.Modulo:

                    case ExpressionType.And:

                    case ExpressionType.AndAlso:

                    case ExpressionType.Or:

                    case ExpressionType.OrElse:

                    case ExpressionType.LessThan:

                    case ExpressionType.LessThanOrEqual:

                    case ExpressionType.GreaterThan:

                    case ExpressionType.GreaterThanOrEqual:

                    case ExpressionType.Equal:

                    case ExpressionType.NotEqual:

                    case ExpressionType.Coalesce:

                    case ExpressionType.ArrayIndex:

                    case ExpressionType.RightShift:

                    case ExpressionType.LeftShift:

                    case ExpressionType.ExclusiveOr:

                        return this.VisitBinary((BinaryExpression)exp);

                    case ExpressionType.TypeIs:

                        return this.VisitTypeIs((TypeBinaryExpression)exp);

                    case ExpressionType.Conditional:

                        return this.VisitConditional((ConditionalExpression)exp);

                    case ExpressionType.Constant:

                        return this.VisitConstant((ConstantExpression)exp);

                    case ExpressionType.Parameter:

                        return this.VisitParameter((ParameterExpression)exp);

                    case ExpressionType.MemberAccess:

                        return this.VisitMemberAccess((MemberExpression)exp);

                    case ExpressionType.Call:

                        return this.VisitMethodCall((MethodCallExpression)exp);

                    case ExpressionType.Lambda:

                        return this.VisitLambda((LambdaExpression)exp);

                    case ExpressionType.New:

                        return this.VisitNew((NewExpression)exp);

                    case ExpressionType.NewArrayInit:

                    case ExpressionType.NewArrayBounds:

                        return this.VisitNewArray((NewArrayExpression)exp);

                    case ExpressionType.Invoke:

                        return this.VisitInvocation((InvocationExpression)exp);

                    case ExpressionType.MemberInit:

                        return this.VisitMemberInit((MemberInitExpression)exp);

                    case ExpressionType.ListInit:

                        return this.VisitListInit((ListInitExpression)exp);

                    default:

                        throw new Exception(string.Format("Unhandled expression type: '{0}'", exp.NodeType));

                }

            }



            protected virtual MemberBinding VisitBinding(MemberBinding binding)
            {

                switch (binding.BindingType)
                {

                    case MemberBindingType.Assignment:

                        return this.VisitMemberAssignment((MemberAssignment)binding);

                    case MemberBindingType.MemberBinding:

                        return this.VisitMemberMemberBinding((MemberMemberBinding)binding);

                    case MemberBindingType.ListBinding:

                        return this.VisitMemberListBinding((MemberListBinding)binding);

                    default:

                        throw new Exception(string.Format("Unhandled binding type '{0}'", binding.BindingType));

                }

            }



            protected virtual ElementInit VisitElementInitializer(ElementInit initializer)
            {

                ReadOnlyCollection<Expression> arguments = this.VisitExpressionList(initializer.Arguments);

                if (arguments != initializer.Arguments)
                {

                    return Expression.ElementInit(initializer.AddMethod, arguments);

                }

                return initializer;

            }



            protected virtual Expression VisitUnary(UnaryExpression u)
            {

                Expression operand = this.Visit(u.Operand);

                if (operand != u.Operand)
                {

                    return Expression.MakeUnary(u.NodeType, operand, u.Type, u.Method);

                }

                return u;

            }



            protected virtual Expression VisitBinary(BinaryExpression b)
            {

                Expression left = this.Visit(b.Left);

                Expression right = this.Visit(b.Right);

                Expression conversion = this.Visit(b.Conversion);

                if (left != b.Left || right != b.Right || conversion != b.Conversion)
                {

                    if (b.NodeType == ExpressionType.Coalesce && b.Conversion != null)

                        return Expression.Coalesce(left, right, conversion as LambdaExpression);

                    else

                        return Expression.MakeBinary(b.NodeType, left, right, b.IsLiftedToNull, b.Method);

                }

                return b;

            }



            protected virtual Expression VisitTypeIs(TypeBinaryExpression b)
            {

                Expression expr = this.Visit(b.Expression);

                if (expr != b.Expression)
                {

                    return Expression.TypeIs(expr, b.TypeOperand);

                }

                return b;

            }



            protected virtual Expression VisitConstant(ConstantExpression c)
            {

                return c;

            }



            protected virtual Expression VisitConditional(ConditionalExpression c)
            {

                Expression test = this.Visit(c.Test);

                Expression ifTrue = this.Visit(c.IfTrue);

                Expression ifFalse = this.Visit(c.IfFalse);

                if (test != c.Test || ifTrue != c.IfTrue || ifFalse != c.IfFalse)
                {

                    return Expression.Condition(test, ifTrue, ifFalse);

                }

                return c;

            }



            protected virtual Expression VisitParameter(ParameterExpression p)
            {

                return p;

            }



            protected virtual Expression VisitMemberAccess(MemberExpression m)
            {

                Expression exp = this.Visit(m.Expression);

                if (exp != m.Expression)
                {

                    return Expression.MakeMemberAccess(exp, m.Member);

                }

                return m;

            }



            protected virtual Expression VisitMethodCall(MethodCallExpression m)
            {

                Expression obj = this.Visit(m.Object);

                IEnumerable<Expression> args = this.VisitExpressionList(m.Arguments);

                if (obj != m.Object || args != m.Arguments)
                {

                    return Expression.Call(obj, m.Method, args);

                }

                return m;

            }



            protected virtual ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original)
            {

                List<Expression> list = null;

                for (int i = 0, n = original.Count; i < n; i++)
                {

                    Expression p = this.Visit(original[i]);

                    if (list != null)
                    {

                        list.Add(p);

                    }

                    else if (p != original[i])
                    {

                        list = new List<Expression>(n);

                        for (int j = 0; j < i; j++)
                        {

                            list.Add(original[j]);

                        }

                        list.Add(p);

                    }

                }

                if (list != null)
                {

                    return list.AsReadOnly();

                }

                return original;

            }



            protected virtual MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
            {

                Expression e = this.Visit(assignment.Expression);

                if (e != assignment.Expression)
                {

                    return Expression.Bind(assignment.Member, e);

                }

                return assignment;

            }



            protected virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
            {

                IEnumerable<MemberBinding> bindings = this.VisitBindingList(binding.Bindings);

                if (bindings != binding.Bindings)
                {

                    return Expression.MemberBind(binding.Member, bindings);

                }

                return binding;

            }



            protected virtual MemberListBinding VisitMemberListBinding(MemberListBinding binding)
            {

                IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(binding.Initializers);

                if (initializers != binding.Initializers)
                {

                    return Expression.ListBind(binding.Member, initializers);

                }

                return binding;

            }



            protected virtual IEnumerable<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> original)
            {

                List<MemberBinding> list = null;

                for (int i = 0, n = original.Count; i < n; i++)
                {

                    MemberBinding b = this.VisitBinding(original[i]);

                    if (list != null)
                    {

                        list.Add(b);

                    }

                    else if (b != original[i])
                    {

                        list = new List<MemberBinding>(n);

                        for (int j = 0; j < i; j++)
                        {

                            list.Add(original[j]);

                        }

                        list.Add(b);

                    }

                }

                if (list != null)

                    return list;

                return original;

            }



            protected virtual IEnumerable<ElementInit> VisitElementInitializerList(ReadOnlyCollection<ElementInit> original)
            {

                List<ElementInit> list = null;

                for (int i = 0, n = original.Count; i < n; i++)
                {

                    ElementInit init = this.VisitElementInitializer(original[i]);

                    if (list != null)
                    {

                        list.Add(init);

                    }

                    else if (init != original[i])
                    {

                        list = new List<ElementInit>(n);

                        for (int j = 0; j < i; j++)
                        {

                            list.Add(original[j]);

                        }

                        list.Add(init);

                    }

                }

                if (list != null)

                    return list;

                return original;

            }



            protected virtual Expression VisitLambda(LambdaExpression lambda)
            {

                Expression body = this.Visit(lambda.Body);

                if (body != lambda.Body)
                {

                    return Expression.Lambda(lambda.Type, body, lambda.Parameters);

                }

                return lambda;

            }



            protected virtual NewExpression VisitNew(NewExpression nex)
            {

                IEnumerable<Expression> args = this.VisitExpressionList(nex.Arguments);

                if (args != nex.Arguments)
                {

                    if (nex.Members != null)

                        return Expression.New(nex.Constructor, args, nex.Members);

                    else

                        return Expression.New(nex.Constructor, args);

                }

                return nex;

            }



            protected virtual Expression VisitMemberInit(MemberInitExpression init)
            {

                NewExpression n = this.VisitNew(init.NewExpression);

                IEnumerable<MemberBinding> bindings = this.VisitBindingList(init.Bindings);

                if (n != init.NewExpression || bindings != init.Bindings)
                {

                    return Expression.MemberInit(n, bindings);

                }

                return init;

            }



            protected virtual Expression VisitListInit(ListInitExpression init)
            {

                NewExpression n = this.VisitNew(init.NewExpression);

                IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(init.Initializers);

                if (n != init.NewExpression || initializers != init.Initializers)
                {

                    return Expression.ListInit(n, initializers);

                }

                return init;

            }



            protected virtual Expression VisitNewArray(NewArrayExpression na)
            {

                IEnumerable<Expression> exprs = this.VisitExpressionList(na.Expressions);

                if (exprs != na.Expressions)
                {

                    if (na.NodeType == ExpressionType.NewArrayInit)
                    {

                        return Expression.NewArrayInit(na.Type.GetElementType(), exprs);

                    }

                    else
                    {

                        return Expression.NewArrayBounds(na.Type.GetElementType(), exprs);

                    }

                }

                return na;

            }



            protected virtual Expression VisitInvocation(InvocationExpression iv)
            {

                IEnumerable<Expression> args = this.VisitExpressionList(iv.Arguments);

                Expression expr = this.Visit(iv.Expression);

                if (args != iv.Arguments || expr != iv.Expression)
                {

                    return Expression.Invoke(expr, args);

                }

                return iv;

            }

        }

        class ParameterRebinder : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> map;

            public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;
                if (map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }
                return base.VisitParameter(p);
            }
        }

        public static class ExpressionExtensions
        {
            public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
            {
                // build parameter map (from parameters of second to parameters of first)
                var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

                // replace parameters in the second lambda expression with parameters from the first
                var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

                // apply composition of lambda expression bodies to parameters from the first expression
                return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
            }

            public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
            {
                return first.Compose(second, Expression.And);
            }

            public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
            {
                return first.Compose(second, Expression.Or);
            }
        }

        public class PredicateBuilder
        {
            public static Expression<Func<T, bool>> True<T>() { return f => true; }
            public static Expression<Func<T, bool>> False<T>() { return f => false; }
        }
    }

    ///<summary>
    /// LOGI.Framework.Toolkit.Core.Extensions
    ///</summary>
    public static class Extensions
    {
        #region IEnumerable
        #region IEnumerable CopyToDataTable
        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source)
        {
            return new ObjectShredder<T>().Shred(source, null, null);
        }

        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source,
                                                    DataTable table, LoadOption? options)
        {
            return new ObjectShredder<T>().Shred(source, table, options);
        }
        #endregion

        #region IEnumerable WhereIf
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, int, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        //public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> condition, Func<TSource, bool> predicate)
        //{
        //    if (condition)
        //        return source.Where(predicate);
        //    else
        //        return source;
        //}

        #endregion

        #region IEnumerable WhereIn
        private static Expression<Func<TElement, bool>> GetWhereInExpression<TElement, TValue>(Expression<Func<TElement, TValue>> propertySelector, IEnumerable<TValue> values)
        {
            ParameterExpression p = propertySelector.Parameters.Single();
            if (!values.Any())
                return e => false;

            var equals = values.Select(value => (Expression)Expression.Equal(propertySelector.Body, Expression.Constant(value, typeof(TValue))));
            var body = equals.Aggregate<Expression>((accumulate, equal) => Expression.Or(accumulate, equal));

            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }

        /// <summary> 
        /// Return the element that the specified property's value is contained in the specifiec values 
        /// </summary> 
        /// <typeparam name="TElement">The type of the element.</typeparam> 
        /// <typeparam name="TValue">The type of the values.</typeparam> 
        /// <param name="source">The source.</param> 
        /// <param name="propertySelector">The property to be tested.</param> 
        /// <param name="values">The accepted values of the property.</param> 
        /// <returns>The accepted elements.</returns> 
        public static IQueryable<TElement> WhereIn<TElement, TValue>(this IQueryable<TElement> source, Expression<Func<TElement, TValue>> propertySelector, params TValue[] values)
        {
            return source.Where(GetWhereInExpression(propertySelector, values));
        }

        /// <summary> 
        /// Return the element that the specified property's value is contained in the specifiec values 
        /// </summary> 
        /// <typeparam name="TElement">The type of the element.</typeparam> 
        /// <typeparam name="TValue">The type of the values.</typeparam> 
        /// <param name="source">The source.</param> 
        /// <param name="propertySelector">The property to be tested.</param> 
        /// <param name="values">The accepted values of the property.</param> 
        /// <returns>The accepted elements.</returns> 
        public static IQueryable<TElement> WhereIn<TElement, TValue>(this IQueryable<TElement> source, Expression<Func<TElement, TValue>> propertySelector, IEnumerable<TValue> values)
        {
            return source.Where(GetWhereInExpression(propertySelector, values));
        }
        #endregion

        #region IEnumerable WhereNotIn
        private static Expression<Func<TElement, bool>> GetWhereNotInExpression<TElement, TValue>(Expression<Func<TElement, TValue>> propertySelector, IEnumerable<TValue> values)
        {
            ParameterExpression p =
                propertySelector.Parameters.Single();

            if (!values.Any())
            {
                return e => true;
            }

            var unequals = values.Select(value =>
                (Expression)Expression.NotEqual(
                    propertySelector.Body,
                    Expression.Constant(value, typeof(TValue))
                )
            );

            var body = unequals.Aggregate<Expression>(
                (accumulate, unequal) => Expression.And(accumulate, unequal));

            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }

        public static IQueryable<TElement> WhereNotIn<TElement, TValue>(this IQueryable<TElement> source, Expression<Func<TElement, TValue>> propertySelector, params TValue[] values)
        {
            return source.Where(GetWhereNotInExpression(
                propertySelector, values));
        }

        public static IQueryable<TElement> WhereNotIn<TElement, TValue>(this IQueryable<TElement> source, Expression<Func<TElement, TValue>> propertySelector, IEnumerable<TValue> values)
        {
            return source.Where(GetWhereNotInExpression(
                propertySelector, values));
        }
        #endregion

        
        #endregion
    }

    /// <summary>
    /// The almost inevitable collection of extra helper methods on
    /// <see cref="IEnumerable{T}"/> to augment the rich set of what
    /// Linq already gives us.
    /// </summary>
    public static class EnumerableExtensions
    {
        //public static IEnumerable<TList> RemoveBy<TList, TListItem>(this IEnumerable<TList> sequence, LambdaComparer<TListItem> lambdaComparer)
        //{
        //    var returnList = new List<TList>();

        //    foreach (var item in sequence)
        //    {
        //        if (lambdaComparer.Equals(item))
        //        {
                    
        //        }
        //    }

        //    return returnList;
        //}

        /// <summary>
        /// Execute the provided <paramref name="action"/> on every item in <paramref name="sequence"/>.
        /// </summary>
        /// <typeparam name="TItem">Type of the items stored in <paramref name="sequence"/></typeparam>
        /// <param name="sequence">Sequence of items to process.</param>
        /// <param name="action">Code to run over each item.</param>
        public static void ForEach<TItem>(this IEnumerable<TItem> sequence, Action<TItem> action)
        {
            foreach (var item in sequence)
            {
                action(item);
            }
        }

        /// <summary>
        /// Create a single string from a sequenc of items, separated by the provided <paramref name="separator"/>,
        /// and with the conversion to string done by the given <paramref name="converter"/>.
        /// </summary>
        /// <remarks>This method does basically the same thing as <see cref="string.Join(string,string[])"/>,
        /// but will work on any sequence of items, not just arrays.</remarks>
        /// <typeparam name="TItem">Type of items in the sequence.</typeparam>
        /// <param name="sequence">Sequence of items to convert.</param>
        /// <param name="separator">Separator to place between the items in the string.</param>
        /// <param name="converter">The conversion function to change TItem -&gt; string.</param>
        /// <returns>The resulting string.</returns>
        public static string JoinStrings<TItem>(this IEnumerable<TItem> sequence, string separator, Func<TItem, string> converter)
        {
            if (sequence==null || sequence.Count()<1)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            sequence.Aggregate(sb, (builder, item) =>
            {
                if (builder.Length > 0)
                {
                    builder.Append(separator);
                }
                builder.Append(converter(item));
                return builder;
            });
            return sb.ToString();
        }

        /// <summary>
        /// Create a single string from a sequenc of items, separated by the provided <paramref name="separator"/>,
        /// and with the conversion to string done by the item's <see cref='object.ToString'/> method.
        /// </summary>
        /// <remarks>This method does basically the same thing as <see cref="string.Join(string,string[])"/>,
        /// but will work on any sequence of items, not just arrays.</remarks>
        /// <typeparam name="TItem">Type of items in the sequence.</typeparam>
        /// <param name="sequence">Sequence of items to convert.</param>
        /// <param name="separator">Separator to place between the items in the string.</param>
        /// <returns>The resulting string.</returns>
        public static string JoinStrings<TItem>(this IEnumerable<TItem> sequence, string separator)
        {
            return sequence.JoinStrings(separator, item => item.ToString());
        }

        public static string ToStringList(this IEnumerable list)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in list)
            {
                sb.AppendFormat("{0}, ", item);
            }

            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }

        public static List<T> Page<T, TResult, TResult1>(this List<T> obj, int page, int pageSize, System.Linq.Expressions.Expression<Func<T, TResult>> keySelector,
        bool asc, out int rowsCount, bool isThenBy, System.Linq.Expressions.Expression<Func<T, TResult1>> thenByClause)
        {
            rowsCount = obj.Count();
            if (asc)
            {
                if (isThenBy)
                    return obj.AsQueryable().OrderBy(keySelector).ThenBy(thenByClause).Skip((page == 0 ? 0 : page - 1) * pageSize)
                                .Take(pageSize).ToList();
                return obj.AsQueryable().OrderBy(keySelector).Skip((page - 1) * pageSize)
                            .Take(pageSize).ToList();
            }
            if (isThenBy)
                return obj.AsQueryable().OrderByDescending(keySelector).ThenByDescending(thenByClause).Skip((page == 0 ? 0 : page - 1) * pageSize)
                            .Take(pageSize).ToList();
            return obj.AsQueryable().OrderByDescending(keySelector).Skip((page - 1) * pageSize)
                        .Take(pageSize).ToList();
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> enumerable, Func<TSource, TKey> keySelector, bool descending)
        {
            if (enumerable == null)
            {
                return null;
            }

            if (descending)
            {
                return enumerable.OrderByDescending(keySelector);
            }

            return enumerable.OrderBy(keySelector);
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> enumerable, Func<TSource, IComparable> keySelector1, Func<TSource, IComparable> keySelector2, params Func<TSource, IComparable>[] keySelectors)
        {
            if (enumerable == null)
            {
                return null;
            }

            IEnumerable<TSource> current = enumerable;

            if (keySelectors != null)
            {
                for (int i = keySelectors.Length - 1; i >= 0; i--)
                {
                    current = current.OrderBy(keySelectors[i]);
                }
            }

            current = current.OrderBy(keySelector2);

            return current.OrderBy(keySelector1);
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> enumerable, bool descending, Func<TSource, IComparable> keySelector, params Func<TSource, IComparable>[] keySelectors)
        {
            if (enumerable == null)
            {
                return null;
            }

            IEnumerable<TSource> current = enumerable;

            if (keySelectors != null)
            {
                for (int i = keySelectors.Length - 1; i >= 0; i--)
                {
                    current = current.OrderBy(keySelectors[i], descending);
                }
            }

            return current.OrderBy(keySelector, descending);
        }

        #region Equals
        /// <summary>
        /// Checks whether a collection is the same as another collection
        /// </summary>
        /// <param name="value">The current instance object</param>
        /// <param name="compareList">The collection to compare with</param>
        /// <param name="comparer">The comparer object to use to compare each item in the collection.  If null uses EqualityComparer(T).Default</param>
        /// <returns>True if the two collections contain all the same items in the same order</returns>
        public static bool IsEqualTo<TSource>(this IEnumerable<TSource> value, IEnumerable<TSource> compareList, IEqualityComparer<TSource> comparer)
        {
            if (value == compareList)
            {
                return true;
            }
            else if (value == null || compareList == null)
            {
                return false;
            }
            else
            {
                if (comparer == null)
                {
                    comparer = EqualityComparer<TSource>.Default;
                }

                IEnumerator<TSource> enumerator1 = value.GetEnumerator();
                IEnumerator<TSource> enumerator2 = compareList.GetEnumerator();

                bool enum1HasValue = enumerator1.MoveNext();
                bool enum2HasValue = enumerator2.MoveNext();

                try
                {
                    while (enum1HasValue && enum2HasValue)
                    {
                        if (!comparer.Equals(enumerator1.Current, enumerator2.Current))
                        {
                            return false;
                        }

                        enum1HasValue = enumerator1.MoveNext();
                        enum2HasValue = enumerator2.MoveNext();
                    }

                    return !(enum1HasValue || enum2HasValue);
                }
                finally
                {
                    if (enumerator1 != null) enumerator1.Dispose();
                    if (enumerator2 != null) enumerator2.Dispose();
                }
            }
        }

        /// <summary>
        /// Checks whether a collection is the same as another collection
        /// </summary>
        /// <param name="value">The current instance object</param>
        /// <param name="compareList">The collection to compare with</param>
        /// <returns>True if the two collections contain all the same items in the same order</returns>
        public static bool IsEqualTo<TSource>(this IEnumerable<TSource> value, IEnumerable<TSource> compareList)
        {
            return IsEqualTo(value, compareList, null);
        }

        /// <summary>
        /// Checks whether a collection is the same as another collection
        /// </summary>
        /// <param name="value">The current instance object</param>
        /// <param name="compareList">The collection to compare with</param>
        /// <returns>True if the two collections contain all the same items in the same order</returns>
        public static bool IsEqualTo(this IEnumerable value, IEnumerable compareList)
        {
            return IsEqualTo<object>(value.OfType<object>(), compareList.OfType<object>());
        }

        #endregion
    }
}
