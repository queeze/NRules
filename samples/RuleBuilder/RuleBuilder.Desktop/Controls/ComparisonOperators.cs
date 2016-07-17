using System.Linq.Expressions;

namespace RuleBuilder.Desktop.Controls
{
    public enum ComparisonOperators
    {
        Equal = ExpressionType.Equal,
        NotEqual = ExpressionType.NotEqual,
        LessThan = ExpressionType.LessThan,
        GreaterThan = ExpressionType.GreaterThan,
        LessThanOrEqual = ExpressionType.LessThanOrEqual,
        GreaterThanOrEqual = ExpressionType.GreaterThanOrEqual,
        Contains
    }

}
