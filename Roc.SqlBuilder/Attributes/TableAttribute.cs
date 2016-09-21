using System;

namespace Roc.SqlBuilder
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
