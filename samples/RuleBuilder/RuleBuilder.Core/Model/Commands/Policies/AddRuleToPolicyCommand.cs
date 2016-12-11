namespace RuleBuilder.Core.Model.Commands.Policies
{
    public class AddRuleToPolicyCommand : CommandBase
    {
        public AddRuleToPolicyCommand(int expectedVersion, Rule rule)
            : base((int)PolicyCommandType.AddRule)
        {
            ExpectedVersion = expectedVersion;
            Rule = rule;
        }

        public Rule Rule { get; internal set; }
    }
}