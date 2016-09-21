using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roc.TEST
{
    public class Program
    {
        public static void Main()
        {
            //OracleTest test = new OracleTest();
            //test.TEST();

            MySqlTest test = new MySqlTest();
            test.TEST();
        }
    }
}
