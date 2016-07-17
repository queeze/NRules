using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using System.Windows.Data;
using System.Windows.Controls;

namespace RuleBuilder.Desktop.Controls
{
    public class ExpressionListViewModel : ViewModelBase 
    {
        private IEnumerable<PropertyInfo> _availableProperties;
        private Type _objectType;
        private ComparisonOperators _compareOperator = ComparisonOperators.Equal;
        private CombinationOperators _combineOperator = CombinationOperators.And;
        private PropertyInfo _propertyInfo;
        private object _value2;

        public IEnumerable<PropertyInfo> AvailableProperties
        {
            get { return _availableProperties; }
            set
            {
                _availableProperties = value;
                OnPropertyChanged(nameof(AvailableProperties));
            }
        }

        public Array AvailableCompareOperators
        {
            get 
            {
                if (PropertyType == typeof(string))
                {
                    return new ComparisonOperators[] { ComparisonOperators.Equal, ComparisonOperators.NotEqual, ComparisonOperators.Contains };
                }
                else
                {
                    var operators = from o in Enum.GetValues(typeof(ComparisonOperators)).Cast<ComparisonOperators>()
                                    where o != ComparisonOperators.Contains // exclude "Contains" as only works for String
                                    select o;
                    return operators.ToArray();
                }
            }
        }

        public Array AvailableCombinationOperators
        {
            get { return Enum.GetValues(typeof(CombinationOperators)); }
        }

	    public Type ObjectType
	    {
		    get { return _objectType;}

            set
            { 
                _objectType = value;

                if (value == null)
                {
                    AvailableProperties = null;
                }
                else
                {
                    AvailableProperties = from p in value.GetProperties()
                                          where GetSupportedTypes().Contains(p.PropertyType) &&
                                          p.GetCustomAttributes(typeof(DoNotFilterOnAttribute), true).Length == 0 // in other words, where attribute is NOT applied
                                          select p;
                }

                OnPropertyChanged(nameof(ObjectType));
            }
	    }
	
        public ComparisonOperators CompareOperator
        {
            get { return _compareOperator; }

            set
            {
                _compareOperator = value;
                OnPropertyChanged(nameof(CompareOperator));
            }
        }

        public CombinationOperators CombineOperator
        {
            get { return _combineOperator; }

            set
            {
                _combineOperator = value;
                OnPropertyChanged(nameof(CombineOperator));
            }
        }

        public PropertyInfo PropertyInfo
        {
            get { return _propertyInfo; }

            set
            {
                _propertyInfo = value;
                OnPropertyChanged(nameof(PropertyInfo));
                OnPropertyChanged(nameof(PropertyType));
                OnPropertyChanged(nameof(PropertyName));
                OnPropertyChanged(nameof(AvailableCompareOperators));
                OnPropertyChanged(nameof(Value2));
                OnPropertyChanged(nameof(ValueControl));
            }
        }

 	    public Type PropertyType
	    {
            get
            {
                return PropertyInfo == null? typeof(string) : PropertyInfo.PropertyType;
            }
	    }

        public string PropertyName
        {
            get
            {
                return PropertyInfo == null ? null : PropertyInfo.Name;
            }
        }

        public dynamic Value
        {
            get { return Value2.Value; }
            set
            {
                if (!Object.Equals(Value2.Value, value))
                {
                    Value2.Value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        public dynamic Value2
        {
            get
            {
                Type specificType = typeof(Variant<>).MakeGenericType(new Type[] { PropertyType });

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
                Binding binding = new Binding("Value2.Value");
                binding.Source = this;

                if (PropertyType == typeof(DateTime) || PropertyType == typeof(DateTime?))
                {
                    DatePicker dp = new DatePicker();
                    dp.SetBinding(DatePicker.SelectedDateProperty, binding);
                    return dp;
                }
                else
                {
                    TextBox tb = new TextBox();
                    tb.SetBinding(TextBox.TextProperty, binding);
                    return tb;
                }
            }
        }

        public static object CreateGeneric(Type generic, Type innerType, params object[] args)
        {
            Type specificType = generic.MakeGenericType(new Type[] { innerType });
            return Activator.CreateInstance(specificType, args);
        }

        public LambdaExpression MakeExpression(ParameterExpression paramExpr = null)
        {
            if(paramExpr == null) paramExpr = Expression.Parameter(ObjectType, "left");
            
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
            
            return Expression.Lambda( expr, paramExpr);
        }

        public static IEnumerable<Type> GetSupportedTypes()
        {
            return new Type[] {
                typeof(DateTime), typeof(DateTime?),
                typeof(long), typeof(long?),
                typeof(short), typeof(short?),
                typeof(ulong), typeof(ulong?),
                typeof(ushort), typeof(ushort?),
                typeof(float), typeof(float?),
                typeof(double), typeof(double?),
                typeof(decimal), typeof(decimal?),
                typeof(bool), typeof(bool?),
                typeof(int), typeof(int?),
                typeof(uint), typeof(uint),
                typeof(string)
            };
        }
    }
}
