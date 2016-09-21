using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roc.SqlBuilder.Adapter
{
    interface ISqlAdapter
    {
        //string LeftToken { get; }
        //string RightToken { get; }
        //string ParamPrefix { get; }
        bool SupportParameter { get; }

        string Query(SqlEntity entity);
        string QueryPage(SqlEntity entity);
        string Insert(bool key, SqlEntity entity);
        string Update(SqlEntity entity);
        string Delete(SqlEntity entity);

        string Table(string tableName);
        string Field(string filedName);
        string Field(string tableName, string fieldName);
        string Parameter(string parameterId);
    }
}
