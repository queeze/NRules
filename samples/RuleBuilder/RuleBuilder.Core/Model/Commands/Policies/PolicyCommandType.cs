namespace RuleBuilder.Core.Model.Commands.Policies
{
    public enum PolicyCommandType
    {
        Create = 0,
        UpdateName = 1,
        UpdateDescription = 2,
        AddRule = 3,
        RemoveRule = 4,
        AddFact = 5,
        RemoveFact = 6,
        AddPolicy = 7,
        RemovePolicy = 8,
        AddDataContext = 9,
        RemoveDataContext = 10,
    }
}