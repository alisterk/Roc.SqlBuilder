using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roc.SqlBuilder;
using Roc.TEST.Entity;

namespace Roc.TEST
{
    public class SqlserverTest
    {
        public static void TEST()
        {
            Common com = new Common(SqlAdapter.SqlServer2005);

            var sql = com.GetSqlLamUpdate();

            string sqlText = sql.SqlString;
            string parameterString = com.GetParameterString(sql.Parameters);

            Console.WriteLine("SQL Text: " + sqlText);
            Console.WriteLine("Parameter Text: " + parameterString);
            //Console.WriteLine("Split: " + sql.SplitColumns[0]);

            Console.Read();
        }
    }
}
