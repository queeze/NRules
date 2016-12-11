using System;
using System.Collections.Generic;

namespace RuleBuilder.Core.Model
{
    public class ContextClass
    {
        public Type Type { get; set; }

        public string Name { get; set; }

        public ICollection<ContextClass> Properties { get; set; } = new HashSet<ContextClass>();

        public ContextClass Parent { get; set; }
    }
}
