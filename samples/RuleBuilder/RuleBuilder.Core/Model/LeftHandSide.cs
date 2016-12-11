using System.Collections.Generic;

namespace RuleBuilder.Core.Model
{
    public class LeftHandSide
    {
        public LeftHandSide()
        {
            Conditions = new HashSet<Condition>();
        }

        public string Type { get; set; }

        public string Name { get; set; }

        public ICollection<Condition> Conditions { get; }
    }
}
