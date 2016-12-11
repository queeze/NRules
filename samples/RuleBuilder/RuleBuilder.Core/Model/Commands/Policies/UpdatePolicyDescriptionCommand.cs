namespace RuleBuilder.Core.Model.Commands.Policies
{
    public class UpdatePolicyDescriptionCommand : CommandBase
    {
        public UpdatePolicyDescriptionCommand(int version, string description)
            : base((int)PolicyCommandType.UpdateName)
        {
            ExpectedVersion = version;
            Description = description;
        }

        public string Description { get; }
    }
}