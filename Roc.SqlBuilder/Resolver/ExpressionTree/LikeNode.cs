using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roc.SqlBuilder.Resolver
{
    class LikeNode : Node
    {
        public LikeMethod Method { get; set; }
        public MemberNode MemberNode { get; set; }
        public string Value { get; set; }
    }
}
