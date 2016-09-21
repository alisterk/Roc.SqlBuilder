using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roc.SqlBuilder;
using Roc.TEST.Entity;

namespace Roc.TEST
{
    class Sqlite3Test
    {
        public void TEST()
        {
            string s = @"data source=C:\AAAAAAA\DB\Sqlite3\TEST.db;cache size=4000";
            string p = "System.Data.SQLite.EF6";
            DBHelper.SetConnection(s, p);

            Common com = new Common(SqlAdapter.Sqlite3);

            var sql = com.GetSqlLamLinq();

            string sqlText = sql.SqlString;
            string parameterString = com.GetParameterString(sql.Parameters);

            Console.WriteLine("SQL Text: " + sqlText);
            Console.WriteLine("Parameter Text: " + parameterString);

            Console.Read();
        }
    }
}
