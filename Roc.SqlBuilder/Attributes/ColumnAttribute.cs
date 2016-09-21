using System;

namespace Roc.SqlBuilder
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
    }
}
