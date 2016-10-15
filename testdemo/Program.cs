using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iAnywhere.Data.SQLAnywhere;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.DataAccess.Client;

namespace testdemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //var logic=new testmysqllogic();

            ////var allList = logic.Find();

            //var pagelist = logic.FindPage(2, 1);

            var npglogic=new PgTestLogic();

            //var pglist = npglogic.FindPage(2, 1);

            //var pglist2 = npglogic.FindPageAction(2, 1);
            var inlist = npglogic.FindAction();

            Console.ReadLine();
        }
    }
}
