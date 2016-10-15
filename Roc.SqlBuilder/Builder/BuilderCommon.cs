using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roc.SqlBuilder
{
  
    partial class Builder
    {
        public void BeginExpression()
        {
            _conditions.Add("(");
        }

        public void EndExpression()
        {
            _conditions.Add(")");
        }

        public void And()
        {
            if (_conditions.Count > 0)
                _conditions.Add(" AND ");
        }

        public void Or()
        {
            if (_conditions.Count > 0)
                _conditions.Add(" OR ");
        }

        public void Not()
        {
            _conditions.Add(" NOT ");
        }
    }
}
