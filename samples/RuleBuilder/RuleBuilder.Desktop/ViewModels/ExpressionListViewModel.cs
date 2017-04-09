using Prism.Mvvm;
using RuleBuilder.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using Expression = System.Linq.Expressions.Expression;

namespace RuleBuilder.Desktop.Controls
{
    public class ExpressionListViewModel : BindableBase
    {
        private readonly string _valueName;
        private IEnumerable<PropertyInfo> _availableProperties;
        private Type _objectType;
        private ComparisonOperators _compareOperator = ComparisonOperators.Equal;
        private CombinationOperators _combineOperator = CombinationOperators.And;
        private PropertyInfo _propertyInfo;
        private object _value2;

        public ExpressionListViewModel(string valueName)
        {
            _valueName = valueName;
        }

        public IEnumerable<PropertyInfo> AvailableProperties
        {
            get { return _availableProperties; }
            set { SetProperty(ref _availableProperties, value); }
        }

        public Array AvailableCompareOperators
        {
            get
            {
                var operators = from o in Enum.GetValues(typeof(ComparisonOperators)).Cast<ComparisonOperators>()
                                select o;
                return PropertyType == typeof(string) ?
                    operators.Where(x => x != ComparisonOperators.Contains).ToArray()
                    : operators.ToArray();
            }
        }

        public Array AvailableCombinationOperators => Enum.GetValues(typeof(CombinationOperators));

        public Type ObjectType
        {
            get { return _objectType; }

            set
            {
                if (value == null)
                {
                    AvailableProperties = null;
                }
                else
                {
                    AvailableProperties = from p in value.GetProperties()
                                          where GetSupportedTypes().Contains(p.PropertyType) &&
                                          // in other words, where attribute is NOT applied
                                          p.GetCustomAttributes(typeof(DoNotFilterOnAttribute), true).Length == 0
                                          select p;
                }

                SetProperty(ref _objectType, value);
            }
        }

        public ComparisonOperators CompareOperator
        {
            get { return _compareOperator; }
            set { SetProperty(ref _compareOperator, value); }
        }

        public CombinationOperators CombineOperator
        {
            get { return _combineOperator; }
            set { SetProperty(ref _combineOperator, value); }
        }

        public PropertyInfo PropertyInfo
        {
            get { return _propertyInfo; }

            set
            {
                _propertyInfo = value;
                RaisePropertyChanged(nameof(PropertyInfo));
                RaisePropertyChanged(nameof(PropertyType));
                RaisePropertyChanged(nameof(PropertyName));
                RaisePropertyChanged(nameof(AvailableCompareOperators));
                RaisePropertyChanged(nameof(Value2));
                RaisePropertyChanged(nameof(ValueControl));
            }
        }

        public Type PropertyType => PropertyInfo?.PropertyType ?? typeof(string);

        public string PropertyName => PropertyInfo?.Name;

        public dynamic Value
        {
            get { return Value2.Value; }
            set
            {
                if (!object.Equals(Value2.Value, value))
                {
                    Value2.Value = value;
                    RaisePropertyChanged(nameof(Value));
                }
            }
        }

        public dynamic Value2
        {
            get
            {
                Type specificType = typeof(Variant<>).MakeGenericType(PropertyType);

                if (_value2 == null || _value2.GetType() != specificType)
                {
                    _value2 = Activator.CreateInstance(specificType);
                }

                return _value2;
            }
        }

        public Control ValueControl
        {
            get
            {
                Binding binding = new Binding("Value2.Value") { Source = this };

                if (PropertyType == typeof(DateTime) || PropertyType == typeof(DateTime?))
                {
                    DatePicker dp = new DatePicker();
                    dp.SetBinding(DatePicker.SelectedDateProperty, binding);
                    return dp;
                }

                TextBox tb = new TextBox();
                tb.SetBinding(TextBox.TextProperty, binding);
                return tb;
            }
        }

        public static object CreateGeneric(Type generic, Type innerType, params object[] args)
        {
            Type specificType = generic.MakeGenericType(innerType);
            return Activator.CreateInstance(specificType, args);
        }

        public LambdaExpression MakeExpression(ParameterExpression paramExpr = null)
        {
            if (paramExpr == null) paramExpr = Expression.Parameter(ObjectType, _valueName);

            var callExpr = Expression.MakeMemberAccess(paramExpr, PropertyInfo);
            var valueExpr = Expression.Constant(Value, PropertyType);
            Expression expr;

            if (CompareOperator == ComparisonOperators.Contains)
            {
                expr = Expression.Call(callExpr, PropertyType.GetMethod("Contains"), valueExpr);
            }
            else
            {
                expr = Expression.MakeBinary((ExpressionType)CompareOperator, callExpr, valueExpr);
            }

            return Expression.Lambda(expr, paramExpr);
        }

        public static IEnumerable<Type> GetSupportedTypes()
        {
            return new[] {
                typeof(DateTime),   typeof(DateTime?),
                typeof(long),       typeof(long?),
                typeof(short),      typeof(short?),
                typeof(ulong),      typeof(ulong?),
                typeof(ushort),     typeof(ushort?),
                typeof(float),      typeof(float?),
                typeof(double),     typeof(double?),
                typeof(decimal),    typeof(decimal?),
                typeof(bool),       typeof(bool?),
                typeof(int),        typeof(int?),
                typeof(uint),       typeof(uint),
                typeof(string)
            };
        }
    }
}
