namespace RuleBuilder.Core.Model.Commands.Policies
{
    public class AddFactToPolicyCommand : CommandBase
    {
        public AddFactToPolicyCommand(int expectedVersion, Fact fact)
            : base((int)PolicyCommandType.AddFact)
        {
            ExpectedVersion = expectedVersion;
            Fact = fact;
        }

        public Fact Fact { get; internal set; }
    }
}