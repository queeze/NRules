using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace RuleBuilder.Desktop.Controls
{
    public class ExpressionCollection : ObservableCollection<ExpressionListViewModel>
    {
        private Type _type;
        private string _valueName;
        public ExpressionCollection(string valueName)
        {
            _valueName = valueName;
        }

        public Type Type
        {
            get { return _type; }

            set
            {
                _type = value;

                foreach (ExpressionListViewModel c in this)
                {
                    c.ObjectType = value;
                }
            }
        }

        public LambdaExpression CompleteExpression
        {
            get
            {
                var paramExp = Expression.Parameter(Type, _valueName);

                if (Count == 0)
                {
                    return Expression.Lambda(Expression.Constant(true), paramExp);
                }

                LambdaExpression lambda1 = this.First().MakeExpression(paramExp);
                var ret = lambda1.Body;
                foreach (var c in this.Skip(1))
                {
                    ret = Expression.MakeBinary((ExpressionType)c.CombineOperator, ret, c.MakeExpression(paramExp).Body);
                }

                return Expression.Lambda(ret, paramExp);
            }
        }

        public Expression<Func<T, bool>> GetCompleteExpression<T>()
        {
            return CompleteExpression as Expression<Func<T, bool>>;
        }

        protected override void InsertItem(int index, ExpressionListViewModel item)
        {
            base.InsertItem(index, item);
            if (item.ObjectType == null) item.ObjectType = Type;
        }
    }
}
