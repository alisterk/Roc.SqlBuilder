using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using Dapper;

namespace Roc.TEST
{
    public class DBHelper
    {
        private static string connectionString = string.Empty;
        private static string provider = string.Empty;

        static DBHelper()
        {

        }

        public static void SetConnection(string s, string p)
        {
            connectionString = s;
            provider = p;
        }

        public static IDbConnection GetConnection()
        {
            var factory = DbProviderFactories.GetFactory(provider);
            var connection = factory.CreateConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }

        public static int Excute(string sql, object parameter)
        {
            var connection = GetConnection();
            return connection.Execute(sql, parameter);
        }

        public static IEnumerable<T> Query<T>(string sql, object parameter)
        {
            var connection = GetConnection();
            return connection.Query<T>(sql, parameter);
        }

        public static T Get<T>(string sql, object parameter)
        {
            var connection = GetConnection();
            return connection.Query<T>(sql, parameter).FirstOrDefault();
        }
    }
}
