namespace RuleBuilder.Core.Model
{
    public class Condition
    {
        public CombinationOperators CombinationOperator { get; set; }

        public string Name { get; set; }

        public ComparisonOperators ComparisonOperator { get; set; }
    }
}
