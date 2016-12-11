namespace RuleBuilder.Core.Model.Commands.Policies
{
    public class AddDataContextToPolicyCommand : CommandBase
    {
        public AddDataContextToPolicyCommand(int expectedVersion, DataContext dataContext)
            : base((int)PolicyCommandType.AddDataContext)
        {
            ExpectedVersion = expectedVersion;
            DataContext = dataContext;
        }

        public DataContext DataContext { get; internal set; }
    }
}