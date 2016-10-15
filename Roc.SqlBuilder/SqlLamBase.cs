using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Roc.SqlBuilder;
using Roc.SqlBuilder.Resolver;
using Roc.SqlBuilder.Adapter;

namespace Roc.SqlBuilder
{
    public abstract class SqlLamBase
    {
        internal Builder _builder;
        internal LambdaResolver _resolver;
        internal SqlType _type;
        internal SqlAdapter _adapter;

        public Builder SqlBuilder { get { return _builder; } }

        public SqlType SqlType { get { return _type; } }

        public SqlLamBase()
        {

        }

        public SqlLamBase(SqlAdapter adater, string tableName)
        {
            _type = SqlType.Query;
            _adapter = adater;
            _builder = new Builder(_type, tableName, GetAdapterInstance(_adapter));
            _resolver = new LambdaResolver(_builder);
        }

        public string SqlString
        {
            get
            {
                return _builder.SqlString();
            }
        }

        public string QueryPage(int pageSize, int? pageNumber = null)
        {
            return _builder.QueryPage(pageSize, pageNumber);
        }

        public IDictionary<string, object> Parameters
        {
            get { return _builder.Parameters; }
        }

        /// <summary>
        /// 主要给Dapper用
        /// </summary>
        public string[] SplitColumns
        {
            get { return _builder.SplitColumns.ToArray(); }
        }

        #region update

        public void Clear()
        {
            _builder.Clear();
        }

        #endregion

        public void SetAdapter(SqlAdapter adapter)
        {
            _builder.Adapter = GetAdapterInstance(adapter);
        }

        private ISqlAdapter GetAdapterInstance(SqlAdapter adapter)
        {
            switch (adapter)
            {
                case SqlAdapter.SqlServer2005:
                    return new SqlserverAdapter();
                case SqlAdapter.Sqlite3:
                    return new Sqlite3Adapter();
                case SqlAdapter.Oracle:
                    return new OracleAdapter();
                case SqlAdapter.MySql:
                    return new MySqlAdapter();
                case SqlAdapter.Postgres:
                    return new PostgresAdapter();
                case SqlAdapter.SqlAnyWhere:
                    return new SqlAnyWhereAdapter();
                default:
                    throw new ArgumentException("The specified Sql Adapter was not recognized");
            }
        }

        //public static SqlAdapter GetAdapterByDb(IDbConnection dbconnection)
        //{
        //    SqlAdapter adapter = SqlAdapter.SqlServer2005;


        //}
    }
}
