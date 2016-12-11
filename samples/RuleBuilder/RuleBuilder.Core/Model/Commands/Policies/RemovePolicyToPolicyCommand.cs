namespace RuleBuilder.Core.Model.Commands.Policies
{
    public class RemovePolicyToPolicyCommand : CommandBase
    {
        public RemovePolicyToPolicyCommand(int expectedVersion, Policy policy)
            : base((int)PolicyCommandType.RemovePolicy)
        {
            ExpectedVersion = expectedVersion;
            Policy = policy;
        }

        public Policy Policy { get; internal set; }
    }
}