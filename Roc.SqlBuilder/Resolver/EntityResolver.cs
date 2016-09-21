using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Roc.SqlBuilder.Resolver
{
    partial class LambdaResolver
    {
        public void ResolveUpdate<T>(T entity)
        {
            string tableName = GetTableName<T>();
            ResolveParameter<T>(tableName, entity);
        }

        public void ResolveUpdate(Type type, object obj)
        {
            string tableName = GetTableName(type);
            ResolveParameter<object>(tableName, obj);
        }

        public void ResolveInsert<T>(bool key, T entity)
        {
            string tableName = GetTableName<T>();
            ResolveParameter<T>(key, tableName, entity);
        }

        public void ResolveInsert(bool key, Type type, object obj)
        {
            string tableName = GetTableName(type);
            ResolveParameter<object>(key, tableName, obj);
        }

        private void ResolveParameter<T>(string tableName, T entity)
        {
            var ps = GetPropertyInfos<T>(entity);
            foreach (PropertyInfo item in ps)
            {
                object obj = item.GetValue(entity, null);
                _builder.AddSection(tableName, item.Name, _operationDictionary[ExpressionType.Equal], obj);
            }
        }

        private void ResolveParameter<T>(bool key, string tableName, T entity)
        {
            _builder.UpdateInsertKey(key);

            var ps = GetPropertyInfos<T>(entity);
            foreach (PropertyInfo item in ps)
            {
                object obj = item.GetValue(entity, null);
                _builder.AddSection(tableName, item.Name, obj);
            }
        }

        private IEnumerable<PropertyInfo> GetPropertyInfos<T>(T entity)
        {
            Type type = entity.GetType();
            var ps = type.GetProperties().Where(m =>
            {
                var obj = m.GetCustomAttributes(typeof(KeyAttribute), false).FirstOrDefault();
                if (obj != null)
                {
                    KeyAttribute key = obj as KeyAttribute;
                    return !key.Increment;
                }
                return true;
            });
            return ps;
        }
    }
}
