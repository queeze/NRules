namespace RuleBuilder.Core.Model.Commands.Policies
{
    public class AddPolicyToPolicyCommand : CommandBase
    {
        public AddPolicyToPolicyCommand(int expectedVersion, Policy policy)
            : base((int)PolicyCommandType.AddPolicy)
        {
            ExpectedVersion = expectedVersion;
            Policy = policy;
        }

        public Policy Policy { get; internal set; }
    }
}