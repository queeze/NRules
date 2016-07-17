using System.Linq.Expressions;

namespace RuleBuilder.Desktop.Controls
{
    public enum CombinationOperators
    {
        Or = ExpressionType.Or,
        And = ExpressionType.And,
        Xor = ExpressionType.ExclusiveOr,
    }

}
