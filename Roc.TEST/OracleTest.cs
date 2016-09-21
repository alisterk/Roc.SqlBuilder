using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roc.SqlBuilder;

namespace Roc.TEST
{
    class OracleTest
    {
        public void TEST()
        {
            string s = @"data source=(DESCRIPTION =
    (ADDRESS = (PROTOCOL = TCP)(HOST = WIN-0GVVGJ72I07.wks.jnj.com)(PORT = 1521))
    (CONNECT_DATA =
      (SERVER = DEDICATED)
      (SERVICE_NAME = orcl.wks.jnj.com)));Password=TEST123456;User ID=TEST;";
            string p = "Oracle.ManagedDataAccess.Client";
            DBHelper.SetConnection(s, p);

            Common com = new Common(SqlAdapter.Oracle);

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
