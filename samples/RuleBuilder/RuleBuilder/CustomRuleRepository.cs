using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NRules.RuleModel;
using NRules.RuleModel.Builders;
using Serialize.Linq.Extensions;
using Serialize.Linq.Serializers;

namespace NRules.Samples.RuleBuilder
{
    public class CustomRuleRepository : IRuleRepository
    {
        private readonly IRuleSet _ruleSet = new RuleSet("DefaultRuleSet");

        public IEnumerable<IRuleSet> GetRuleSets()
        {
            return new[] {_ruleSet};
        }

        public void LoadRules()
        {
            var rule = BuildRule();
            _ruleSet.Add(new []{rule});
        }

        

        private string _customerCondition = "{\"__type\":\"LambdaExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":18,\"Type\":{\"GenericArguments\":[{\"Name\":\"NRules.Samples.RuleBuilder.Customer\"},{\"Name\":\"System.Boolean\"}],\"Name\":\"System.Func`2\"},\"Body\":{\"__type\":\"BinaryExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":13,\"Type\":{\"Name\":\"System.Boolean\"},\"Left\":{\"__type\":\"MemberExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":23,\"Type\":{\"Name\":\"System.String\"},\"Expression\":{\"__type\":\"ParameterExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":38,\"Type\":{\"Name\":\"NRules.Samples.RuleBuilder.Customer\"},\"Name\":\"customer\"},\"Member\":{\"DeclaringType\":{\"Name\":\"NRules.Samples.RuleBuilder.Customer\"},\"Signature\":\"System.String Name\"}},\"Method\":{\"DeclaringType\":{\"Name\":\"System.String\"},\"Signature\":\"Boolean op_Equality(System.String, System.String)\"},\"Right\":{\"__type\":\"ConstantExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":9,\"Type\":{\"Name\":\"System.String\"},\"Value\":\"John Do\"}},\"Parameters\":[{\"__type\":\"ParameterExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":38,\"Type\":{\"Name\":\"NRules.Samples.RuleBuilder.Customer\"},\"Name\":\"customer\"}]}";

        private string _orderCondition1 = "{\"__type\":\"LambdaExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":18,\"Type\":{\"GenericArguments\":[{\"Name\":\"NRules.Samples.RuleBuilder.Order\"},{\"Name\":\"NRules.Samples.RuleBuilder.Customer\"},{\"Name\":\"System.Boolean\"}],\"Name\":\"System.Func`3\"},\"Body\":{\"__type\":\"BinaryExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":13,\"Type\":{\"Name\":\"System.Boolean\"},\"Left\":{\"__type\":\"MemberExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":23,\"Type\":{\"Name\":\"NRules.Samples.RuleBuilder.Customer\"},\"Expression\":{\"__type\":\"ParameterExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":38,\"Type\":{\"Name\":\"NRules.Samples.RuleBuilder.Order\"},\"Name\":\"order\"},\"Member\":{\"DeclaringType\":{\"Name\":\"NRules.Samples.RuleBuilder.Order\"},\"Signature\":\"NRules.Samples.RuleBuilder.Customer Customer\"}},\"Method\":{},\"Right\":{\"__type\":\"ParameterExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":38,\"Type\":{\"Name\":\"NRules.Samples.RuleBuilder.Customer\"},\"Name\":\"customer\"}},\"Parameters\":[{\"__type\":\"ParameterExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":38,\"Type\":{\"Name\":\"NRules.Samples.RuleBuilder.Order\"},\"Name\":\"order\"},{\"__type\":\"ParameterExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":38,\"Type\":{\"Name\":\"NRules.Samples.RuleBuilder.Customer\"},\"Name\":\"customer\"}]}";
        private string _orderCondition2 = "{\"__type\":\"LambdaExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":18,\"Type\":{\"GenericArguments\":[{\"Name\":\"NRules.Samples.RuleBuilder.Order\"},{\"Name\":\"System.Boolean\"}],\"Name\":\"System.Func`2\"},\"Body\":{\"__type\":\"BinaryExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":15,\"Type\":{\"Name\":\"System.Boolean\"},\"Left\":{\"__type\":\"MemberExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":23,\"Type\":{\"Name\":\"System.Decimal\"},\"Expression\":{\"__type\":\"ParameterExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":38,\"Type\":{\"Name\":\"NRules.Samples.RuleBuilder.Order\"},\"Name\":\"order\"},\"Member\":{\"DeclaringType\":{\"Name\":\"NRules.Samples.RuleBuilder.Order\"},\"Signature\":\"System.Decimal Amount\"}},\"Method\":{\"DeclaringType\":{\"Name\":\"System.Decimal\"},\"Signature\":\"Boolean op_GreaterThan(System.Decimal, System.Decimal)\"},\"Right\":{\"__type\":\"ConstantExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":9,\"Type\":{\"Name\":\"System.Decimal\"},\"Value\":100.00}},\"Parameters\":[{\"__type\":\"ParameterExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":38,\"Type\":{\"Name\":\"NRules.Samples.RuleBuilder.Order\"},\"Name\":\"order\"}]}";

        private string _action = "{\"__type\":\"LambdaExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":18,\"Type\":{\"GenericArguments\":[{\"Name\":\"NRules.RuleModel.IContext\"},{\"Name\":\"NRules.Samples.RuleBuilder.Customer\"},{\"Name\":\"NRules.Samples.RuleBuilder.Order\"}],\"Name\":\"System.Action`3\"},\"Body\":{\"__type\":\"MethodCallExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":6,\"Type\":{\"Name\":\"System.Void\"},\"Arguments\":[{\"__type\":\"ConstantExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":9,\"Type\":{\"Name\":\"System.String\"},\"Value\":\"Customer {0} has an order in amount of ${1}\"},{\"__type\":\"MemberExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":23,\"Type\":{\"Name\":\"System.String\"},\"Expression\":{\"__type\":\"ParameterExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":38,\"Type\":{\"Name\":\"NRules.Samples.RuleBuilder.Customer\"},\"Name\":\"customer\"},\"Member\":{\"DeclaringType\":{\"Name\":\"NRules.Samples.RuleBuilder.Customer\"},\"Signature\":\"System.String Name\"}},{\"__type\":\"UnaryExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":10,\"Type\":{\"Name\":\"System.Object\"},\"Operand\":{\"__type\":\"MemberExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":23,\"Type\":{\"Name\":\"System.Decimal\"},\"Expression\":{\"__type\":\"ParameterExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":38,\"Type\":{\"Name\":\"NRules.Samples.RuleBuilder.Order\"},\"Name\":\"order\"},\"Member\":{\"DeclaringType\":{\"Name\":\"NRules.Samples.RuleBuilder.Order\"},\"Signature\":\"System.Decimal Amount\"}}}],\"Method\":{\"DeclaringType\":{\"Name\":\"System.Console\"},\"Signature\":\"Void WriteLine(System.String, System.Object, System.Object)\"}},\"Parameters\":[{\"__type\":\"ParameterExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":38,\"Type\":{\"Name\":\"NRules.RuleModel.IContext\"},\"Name\":\"ctx\"},{\"__type\":\"ParameterExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":38,\"Type\":{\"Name\":\"NRules.Samples.RuleBuilder.Customer\"},\"Name\":\"customer\"},{\"__type\":\"ParameterExpressionNode:#Serialize.Linq.Nodes\",\"NodeType\":38,\"Type\":{\"Name\":\"NRules.Samples.RuleBuilder.Order\"},\"Name\":\"order\"}]}";

        private IRuleDefinition BuildRule()
        {
            // Create rule builder
            var builder = new NRules.RuleModel.Builders.RuleBuilder();
            builder.Name("TestRule");
            

            // Build conditions
            PatternBuilder customerPattern = builder.LeftHandSide().Pattern(typeof (Customer), "customer");

            // Can compose expressions at runtime
            ParameterExpression customerParameter = customerPattern.Declaration.ToParameterExpression();

            //LambdaExpression customerCondition = Expression.Lambda(
            //    Expression.Equal(
            //        Expression.Property(customerParameter, "Name"),
            //        Expression.Constant("John Do")),
            //    customerParameter
            //    );


            var jss = new ExpressionSerializer(new JsonSerializer());

            var customerCondition = (LambdaExpression)jss.DeserializeText(_customerCondition);


            customerPattern.Condition(customerCondition); // customerCondition);


            PatternBuilder orderPattern = builder.LeftHandSide().Pattern(typeof (Order), "order");

            

            // Can specify expression at compile time
            //Expression<Func<Order, Customer, bool>> orderCondition1 = 
            //    (order, customer) => order.Customer == customer;
            //Expression<Func<Order, bool>> orderCondition2 = 
            //    order => order.Amount > 100.00m;

            //var j1 = orderCondition2.ToJson();

            //var orderCondition1 = (Expression<Func<Order, Customer, bool>>)jss.DeserializeText(_orderCondition1);
            //var orderCondition2 = (Expression<Func<Order, bool>>)jss.DeserializeText(_orderCondition2);
            var orderCondition1 = (LambdaExpression)jss.DeserializeText(_orderCondition1);
            var orderCondition2 = (LambdaExpression)jss.DeserializeText(_orderCondition2);

            orderPattern.Condition(orderCondition1);
            orderPattern.Condition(orderCondition2);

            // Build actions
            //Expression<Action<IContext, Customer, Order>> action = 
            //    (ctx, customer, order) => Console.WriteLine("Customer {0} has an order in amount of ${1}", customer.Name, order.Amount);

            //var j1 = action.ToJson();

            var action = (LambdaExpression)jss.DeserializeText(_action);


            builder.RightHandSide().Action(action);

            //Build rule model
            return builder.Build();
        }
    }
}