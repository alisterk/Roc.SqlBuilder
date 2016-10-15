using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Roc.SqlBuilder;
using Roc.TEST.Entity;

namespace Roc.TEST
{
    class MySqlTest
    {
        public void TEST()
        {
            string s = @"data source=192.168.1.21;Database=test;Password=plus123456;User ID=root;";
            string p = "MySql.Data.MySqlClient";
            DBHelper.SetConnection(s, p);

             var mysqlCon=new MySqlConnection();
            var sqlcon = new SqlConnection();
        


            var conn = DBHelper.GetConnection();

            var contype=conn.GetType();


            Common com = new Common(SqlAdapter.MySql);

            //var sql = com.GetSqlLamQueryPage();

            //string sqlText = sql.SqlString;
            //string parameterString = com.GetParameterString(sql.Parameters);

            var sqllam= new SqlLam<Tbn>(SqlAdapter.MySql).Where(v=>v.name.Contains("c"));

            

            var sql = sqllam.SqlString;
            var param = sqllam.Parameters;
            string parameterString = com.GetParameterString(sqllam.Parameters);
            var list=DBHelper.Query<Tbn>(sql, param);

            Console.WriteLine("SQL Text: " + sql);
            Console.WriteLine("Parameter Text: " + parameterString);
            //Console.WriteLine("Split: " + sql.SplitColumns[0]);

            Console.Read();
        }
    }
}
