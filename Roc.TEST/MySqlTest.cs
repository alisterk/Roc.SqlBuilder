using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roc.SqlBuilder;

namespace Roc.TEST
{
    class MySqlTest
    {
        public void TEST()
        {
            string s = @"data source=localhost;Database=test;Password=sa1234!;User ID=root;";
            string p = "MySql.Data.MySqlClient";
            DBHelper.SetConnection(s, p);

            Common com = new Common(SqlAdapter.MySql);

            var sql = com.GetSqlLamQueryPage();

            string sqlText = sql.SqlString;
            string parameterString = com.GetParameterString(sql.Parameters);

            Console.WriteLine("SQL Text: " + sqlText);
            Console.WriteLine("Parameter Text: " + parameterString);
            //Console.WriteLine("Split: " + sql.SplitColumns[0]);

            Console.Read();
        }
    }
}
