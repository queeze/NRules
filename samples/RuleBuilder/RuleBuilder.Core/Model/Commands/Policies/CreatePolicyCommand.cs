namespace RuleBuilder.Core.Model.Commands.Policies
{
    public class CreatePolicyCommand : CommandBase
    {
        public CreatePolicyCommand()
            : base((int)PolicyCommandType.Create)
        {
            ExpectedVersion = 0;
        }
    }
}