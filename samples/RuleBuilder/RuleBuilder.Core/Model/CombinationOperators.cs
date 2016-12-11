using System.Linq.Expressions;

namespace RuleBuilder.Core.Model
{
    public enum CombinationOperators
    {
        Or = ExpressionType.Or,
        And = ExpressionType.And,
        Xor = ExpressionType.ExclusiveOr,
    }
}
