namespace RuleBuilder.Core.Model.Commands.Policies
{
    public class UpdatePolicyNameCommand : CommandBase
    {
        public UpdatePolicyNameCommand(int version, string name)
            : base((int)PolicyCommandType.UpdateName)
        {
            ExpectedVersion = version;
            Name = name;
        }

        public string Name { get; }
    }
}