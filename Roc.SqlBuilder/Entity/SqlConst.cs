using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roc.SqlBuilder
{
    [Serializable]
    internal class SqlConst
    {
        public static string[] LeftTokens = new string[] { "[", "`", "\"","" };
        public static string[] RightTokens = new string[] { "]", "`", "\"" ,""};
        public static string[] ParamPrefixs = new string[] { "@", ":", "?", "$" };

        public static string QuerySQLFormatString = @"SELECT {0} FROM {1} {2} {3} {4} {5}";
        public static string InsertSQLFormatString = @"INSERT INTO {0} ({1}) VALUES({2})";
        public static string UpdateSQLFormatString = @"UPDATE {0} SET {1} {2}";
        public static string DeleteSQLFormatString = @"DELETE FROM {0} {1}";

        public static string SqlserverAutoKeySQLString = "SELECT ISNULL(SCOPE_IDENTITY(),0) AS AutoID";
        public static string SqliteAutoKeySQLString = "SELECT last_insert_rowid() AS AutoID;";
    }
}
