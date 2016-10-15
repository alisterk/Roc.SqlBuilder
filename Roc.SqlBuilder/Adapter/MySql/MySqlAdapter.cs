using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roc.SqlBuilder.Adapter
{
    [Serializable]
    class MySqlAdapter : AdapterBase
    {
        public MySqlAdapter()
            : base(SqlConst.LeftTokens[1], SqlConst.RightTokens[1], SqlConst.ParamPrefixs[0])
        {

        }

        public override string QueryPage(SqlEntity entity)
        {
            int pageSize = entity.PageSize;
            int limit = pageSize * (entity.PageNumber - 1);

            return string.Format("SELECT {0} FROM {1} {2} {3} LIMIT {4},{5}", entity.Selection, entity.TableName, entity.Conditions, entity.OrderBy, limit, pageSize);
        }

        public override string Field(string filedName)
        {
            return string.Format("{0}{1}{2}", _leftToken, filedName, _rightToken);
        }

        public override string Field(string tableName, string fieldName)
        {
            return fieldName;//string.Format("{1}", this.Table(tableName), this.Field(fieldName));
        }
    }
}
