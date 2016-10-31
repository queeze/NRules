using System;
using System.Linq.Expressions;
using NRules;
using NRules.RuleModel;
using NRules.RuleModel.Builders;
using RuleBuilder.Desktop.Controls;
using RuleBuilder.Desktop.Domain;
using Prism.Commands;
using Prism.Mvvm;

namespace RuleBuilder.Desktop
{
    public class MainWindowViewModel : BindableBase
    {

        public MainWindowViewModel()
        {
            ExpressionList1 = new ExpressionCollection("customer") { Type = typeof(Customer) };
            ExpressionList1.Add(new ExpressionListViewModel("customer"));
            RunTestCommand = new DelegateCommand(OnRunTest);
        }

        public ExpressionCollection ExpressionList1 { get; set; }

        public DelegateCommand RunTestCommand { get; } 

        private void OnRunTest()
        {
            NRules.RuleModel.Builders.RuleBuilder builder = new NRules.RuleModel.Builders.RuleBuilder();
            builder.Name("TestRule");
            PatternBuilder lhsCustomerPattern = builder
                .LeftHandSide()
                .Pattern(typeof(Customer), "customer");
            ActionGroupBuilder rhsCustomerPattern = builder
                .RightHandSide();
            Expression<Action<IContext>> action = context => Console.WriteLine(@"Test");
            rhsCustomerPattern.Action(action);
            lhsCustomerPattern.Condition(ExpressionList1.CompleteExpression);
            IRuleDefinition rule = builder.Build();
            RuleCompiler compiler = new RuleCompiler();
            ISessionFactory sessionFactory = compiler.Compile(new[] { rule });
            ISession session = sessionFactory.CreateSession();
            session.Insert(new Customer("Test"));
            session.Fire();
        }
    }
}
