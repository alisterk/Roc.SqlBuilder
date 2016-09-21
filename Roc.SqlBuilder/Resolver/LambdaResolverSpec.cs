using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Roc.SqlBuilder.Resolver
{
    partial class LambdaResolver
    {
        public void QueryByIsIn<T>(bool isNot, Expression<Func<T, object>> expression, SqlLamBase sqlQuery)
        {
            var fieldName = GetColumnName(expression);
            _builder.AddCondition(isNot, GetTableName<T>(), fieldName, sqlQuery);
        }

        public void QueryByIsIn<T>(bool isNot, Expression<Func<T, object>> expression, IEnumerable<object> values)
        {
            var fieldName = GetColumnName(expression);
            _builder.AddCondition(isNot, GetTableName<T>(), fieldName, values);
        }

        public void Join<T1, T2>(Expression<Func<T1, T2, bool>> expression)
        {
            var joinExpression = GetBinaryExpression(expression.Body);
            var leftExpression = GetMemberExpression(joinExpression.Left);
            var rightExpression = GetMemberExpression(joinExpression.Right);

            Join<T1, T2>(leftExpression, rightExpression);
        }

        public void Join<T1, T2, TKey>(Expression<Func<T1, TKey>> leftExpression, Expression<Func<T2, TKey>> rightExpression)
        {
            Join<T1, T2>(GetMemberExpression(leftExpression.Body), GetMemberExpression(rightExpression.Body));
        }

        public void Join<T1, T2>(MemberExpression leftExpression, MemberExpression rightExpression)
        {
            _builder.Join(GetTableName<T1>(), GetTableName<T2>(), GetColumnName(leftExpression), GetColumnName(rightExpression));
        }

        public void OrderBy<T>(Expression<Func<T, object>> expression, bool desc = false)
        {
            var body = expression.Body;
            if (body.NodeType == ExpressionType.New)
            {
                var arguments = (body as NewExpression).Arguments;
                foreach (MemberExpression memberExp in arguments)
                    OrderBy<T>(memberExp, desc);
            }
            else
            {
                OrderBy<T>(GetMemberExpression(body), desc);
            }
        }

        private void OrderBy<T>(MemberExpression expression, bool desc)
        {
            var fieldName = GetColumnName(GetMemberExpression(expression));
            _builder.OrderBy(GetTableName<T>(), fieldName, desc);
        }

        public void Select<T>(Expression<Func<T, object>> expression)
        {
            Select<T>(expression.Body);
        }

        public void Select<T>(Expression<Func<T, SqlColumnEntity>> expression)
        {
            Select<T>(expression.Body);
        }

        private void Select<T>(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Parameter:
                    _builder.Select(GetTableName(expression.Type));
                    break;
                case ExpressionType.Convert:
                case ExpressionType.MemberAccess:
                    Select<T>(GetMemberExpression(expression));
                    break;
                case ExpressionType.New:
                    foreach (MemberExpression memberExp in (expression as NewExpression).Arguments)
                        Select<T>(memberExp);
                    break;
                default:
                    throw new ArgumentException("Invalid expression");
            }
        }

        private void Select<T>(MemberExpression expression)
        {
            if (expression.Type.IsClass && expression.Type != typeof(String))
            {
                _builder.Select(GetTableName(expression.Type));
            }
            else
                _builder.Select(GetTableName<T>(), GetColumnName(expression));
        }

        public void SelectWithFunction<T>(Expression<Func<T, object>> expression, SelectFunction selectFunction, string aliasName)
        {
            var fieldName = GetColumnName(GetMemberExpression(expression.Body));
            _builder.Select(GetTableName<T>(), fieldName, selectFunction, aliasName);
        }

        public void GroupBy<T>(Expression<Func<T, object>> expression)
        {
            var body = expression.Body;
            if (body.NodeType == ExpressionType.New)
            {
                var arguments = (body as NewExpression).Arguments;
                foreach (MemberExpression memberExp in arguments)
                    GroupBy<T>(memberExp);
            }
            else
            {
                GroupBy<T>(GetMemberExpression(body));
            }
        }
        private void GroupBy<T>(MemberExpression expression)
        {
            var fieldName = GetColumnName(GetMemberExpression(expression));
            _builder.GroupBy(GetTableName<T>(), fieldName);
        }
    }
}
