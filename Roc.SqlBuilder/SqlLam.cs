using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Roc.SqlBuilder;
using Roc.SqlBuilder.Resolver;
using Roc.SqlBuilder.Adapter;

namespace Roc.SqlBuilder
{
    public class SqlLam<T> : SqlLamBase
    {
        public SqlLam(SqlAdapter type = SqlAdapter.SqlServer2005)
            : base(type, LambdaResolver.GetTableName<T>())
        {
            //_type = SqlType.Query;
            //GetAdapterInstance(type);
            //_builder = new Builder(_type, LambdaResolver.GetTableName<T>(), _defaultAdapter);
            //_resolver = new LambdaResolver(_builder);
        }

        public SqlLam(Expression<Func<T, bool>> expression)
            : this()
        {
            Where(expression);
        }

        internal SqlLam(Builder builder, LambdaResolver resolver)
        {
            _builder = builder;
            _resolver = resolver;
        }

        #region 修改配置

        public SqlLam<T> UseEntityProperty(bool use)
        {
            _builder.UpdateUseEntityProperty(use);
            return this;
        }

        #endregion

        #region Insert Update Delete 操作

        public SqlLam<T> Insert(T entity, bool key = false)
        {
            _builder.UpdateSqlType(SqlType.Insert);
            _resolver.ResolveInsert<T>(key, entity);
            return this;
        }

        public SqlLam<T> Insert(object obj, bool key = false)
        {
            _builder.UpdateSqlType(SqlType.Insert);
            _resolver.ResolveInsert(key, typeof(T), obj);
            return this;
        }

        public SqlLam<T> Update(T entity)
        {
            _builder.UpdateSqlType(SqlType.Update);
            _resolver.ResolveUpdate<T>(entity);
            return this;
        }

        public SqlLam<T> Update(object obj)
        {
            _builder.UpdateSqlType(SqlType.Update);
            _resolver.ResolveUpdate(typeof(T), obj);
            return this;
        }

        public SqlLam<T> Delete(Expression<Func<T, bool>> expression)
        {
            _builder.UpdateSqlType(SqlType.Delete);
            return And(expression);
        }

        #endregion

        #region 查询条件

        public SqlLam<T> Where(Expression<Func<T, bool>> expression)
        {
            return And(expression);
        }

        public SqlLam<T> And(Expression<Func<T, bool>> expression)
        {
            _builder.And();
            _resolver.ResolveQuery(expression);
            return this;
        }

        public SqlLam<T> Or(Expression<Func<T, bool>> expression)
        {
            _builder.Or();
            _resolver.ResolveQuery(expression);
            return this;
        }

        public SqlLam<T> WhereIsIn(Expression<Func<T, object>> expression, SqlLamBase sqlQuery)
        {
            _builder.And();
            _resolver.QueryByIsIn(false, expression, sqlQuery);
            return this;
        }

        public SqlLam<T> WhereIsIn(Expression<Func<T, object>> expression, IEnumerable<object> values)
        {
            _builder.And();
            _resolver.QueryByIsIn(false, expression, values);
            return this;
        }

        public SqlLam<T> WhereNotIn(Expression<Func<T, object>> expression, SqlLamBase sqlQuery)
        {
            _builder.And();
            _resolver.QueryByIsIn(true, expression, sqlQuery);
            return this;
        }

        public SqlLam<T> WhereNotIn(Expression<Func<T, object>> expression, IEnumerable<object> values)
        {
            _builder.And();
            _resolver.QueryByIsIn(true, expression, values);
            return this;
        }
        #endregion

        #region 排序

        public SqlLam<T> OrderBy(params Expression<Func<T, object>>[] expressions)
        {
            foreach (var expression in expressions)
                _resolver.OrderBy(expression);
            return this;
        }

        public SqlLam<T> OrderByDescending(params Expression<Func<T, object>>[] expressions)
        {
            foreach (var expression in expressions)
                _resolver.OrderBy(expression, true);
            return this;
        }
        #endregion

        #region 查询
        public SqlLam<T> Select(params Expression<Func<T, object>>[] expressions)
        {
            foreach (var expression in expressions)
                _resolver.Select(expression);
            return this;
        }

        public SqlLam<T> Select(params Expression<Func<T, SqlColumnEntity>>[] expressions)
        {
            foreach (var expression in expressions)
                _resolver.Select(expression);
            return this;
        }

        public SqlLam<T> Distinct(Expression<Func<T, object>> expression)
        {
            _resolver.SelectWithFunction(expression, SelectFunction.DISTINCT, "");
            return this;
        }

        #endregion

        #region 聚合

        public SqlLam<T> Count(Expression<Func<T, object>> expression, string aliasName = "count")
        {
            _resolver.SelectWithFunction(expression, SelectFunction.COUNT, aliasName);
            return this;
        }

        public SqlLam<T> Count( string aliasName = "count")
        {
            _resolver.SelectWithFunction<T>(null, SelectFunction.COUNT, aliasName);
            return this;
        }

        public SqlLam<T> Sum(Expression<Func<T, object>> expression, string aliasName = "")
        {
            _resolver.SelectWithFunction(expression, SelectFunction.SUM, aliasName);
            return this;
        }

        public SqlLam<T> Max(Expression<Func<T, object>> expression, string aliasName = "")
        {
            _resolver.SelectWithFunction(expression, SelectFunction.MAX, aliasName);
            return this;
        }

        public SqlLam<T> Min(Expression<Func<T, object>> expression, string aliasName = "")
        {
            _resolver.SelectWithFunction(expression, SelectFunction.MIN, aliasName);
            return this;
        }

        public SqlLam<T> Average(Expression<Func<T, object>> expression, string aliasName = "")
        {
            _resolver.SelectWithFunction(expression, SelectFunction.AVG, aliasName);
            return this;
        }

        #endregion

        #region 连接 Join

        public SqlLam<TResult> Join<T2, TKey, TResult>(SqlLam<T2> joinQuery,
            Expression<Func<T, TKey>> primaryKeySelector,
            Expression<Func<T2, TKey>> foreignKeySelector,
            Func<T, T2, TResult> selection)
        {
            var query = new SqlLam<TResult>(_builder, _resolver);
            _resolver.Join<T, T2, TKey>(primaryKeySelector, foreignKeySelector);
            return query;
        }

        public SqlLam<T2> Join<T2>(Expression<Func<T, T2, bool>> expression)
        {
            var joinQuery = new SqlLam<T2>(_builder, _resolver);
            _resolver.Join(expression);
            return joinQuery;
        }

        #endregion

        #region 分组

        public SqlLam<T> GroupBy(Expression<Func<T, object>> expression)
        {
            _resolver.GroupBy(expression);
            return this;
        }

        #endregion
    }
}
