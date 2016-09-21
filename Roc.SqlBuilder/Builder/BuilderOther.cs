using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roc.SqlBuilder;

namespace Roc.SqlBuilder
{
    partial class Builder
    {
        public void Join(string originalTableName, string joinTableName, string leftField, string rightField)
        {
            var joinString = string.Format("JOIN {0} ON {1} = {2}",
                                           _adapter.Table(joinTableName),
                                           _adapter.Field(originalTableName, leftField),
                                           _adapter.Field(joinTableName, rightField));
            _tableNames.Add(joinTableName);
            _joinExpressions.Add(joinString);
            _splitColumns.Add(rightField);
        }

        public void OrderBy(string tableName, string fieldName, bool desc = false)
        {
            var order = _adapter.Field(tableName, fieldName);
            if (desc) order += " DESC";

            _sortList.Add(order);
        }

        public void Select(string tableName)
        {
            var selectionString = string.Format("{0}.*", _adapter.Table(tableName));
            _selectionList.Add(selectionString);
        }

        public void Select(string tableName, string fieldName)
        {
            _selectionList.Add(_adapter.Field(tableName, fieldName));
        }

        public void Select(string tableName, string fieldName, SelectFunction selectFunction, string aliasName)
        {
            string name = string.IsNullOrEmpty(aliasName) ? fieldName : aliasName;
            name = _adapter.Field(name);
            var selectionString = string.Format("{0}({1}) AS {2}", selectFunction.ToString(), _adapter.Field(tableName, fieldName), name);
            _selectionList.Add(selectionString);
        }

        public void GroupBy(string tableName, string fieldName)
        {
            _groupingList.Add(_adapter.Field(tableName, fieldName));
        }
    }
}
