namespace RuleBuilder.Core.Model
{
    using System;
    using System.Collections.Generic;

    using Commands.Policies;

    public class Policy
    {
        private readonly List<CommandBase> _commands = new List<CommandBase>();
        private readonly HashSet<Rule> _rules;
        private readonly HashSet<Fact> _facts;
        private readonly HashSet<Policy> _children;
        private string _name;
        private string _description;
        private int _version;
        private DataContext _dataContext;

        public Policy()
        {
            _rules = new HashSet<Rule>();
            _facts = new HashSet<Fact>();
            _children = new HashSet<Policy>();
            Rules = new ReadOnlyHashSet<Rule>(_rules);
            Facts = new ReadOnlyHashSet<Fact>(_facts);
            Children = new ReadOnlyHashSet<Policy>(_children);
            ProcessCommand(new CreatePolicyCommand());
        }

        public Policy(string name)
            : this()
        {
            ProcessCommand(new CreatePolicyCommand());
            ProcessCommand(new UpdatePolicyNameCommand(_version, name));
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    ProcessCommand(new UpdatePolicyNameCommand(Version, value));
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int Version => _version;

        public Policy Parent { get; internal set; }

        public bool IsDeleted { get; set; }

        public DataContext DataContext
        {
            get
            {
                if (_dataContext != null)
                {
                    return _dataContext;
                }

                return FindParentDataContext(Parent);
            }

            set
            {
                ProcessCommand(new AddDataContextToPolicyCommand(_version, value));
            }
        }

        public IReadOnlyCollection<Rule> Rules { get; }

        public IReadOnlyCollection<Fact> Facts { get; }

        public IReadOnlyCollection<Policy> Children { get; }

        public void AddRule(Rule rule)
        {
            if (!_rules.Contains(rule))
            {
                ProcessCommand(new AddRuleToPolicyCommand(_version, rule));
            }
        }

        public void AddFact(Fact fact)
        {
            if (!_facts.Contains(fact))
            {
                ProcessCommand(new AddFactToPolicyCommand(_version, fact));
            }
        }

        public void AddPolicy(Policy policy)
        {
            if (!_children.Contains(policy))
            {
                ProcessCommand(new AddPolicyToPolicyCommand(_version, policy));
            }
        }

        internal static readonly Type PolicyCommandTypeType = typeof(PolicyCommandType);

        internal void ProcessCommand(CommandBase command)
        {
            var commandType = (PolicyCommandType)Enum.Parse(
                PolicyCommandTypeType, 
                Enum.GetName(PolicyCommandTypeType, command.CommandType));

            switch (commandType)
            {
                case PolicyCommandType.Create:
                    _commands.Clear();
                    _version = 0;
                    var createCommand = (CreatePolicyCommand) command;
                    createCommand.CheckVersion(_version);
                    break;
                case PolicyCommandType.UpdateName:
                    var updateNameCommand = (UpdatePolicyNameCommand) command;
                    updateNameCommand.CheckVersion(_version);
                    _name = updateNameCommand.Name;
                    break;
                case PolicyCommandType.UpdateDescription:
                    var updateDescriptionCommand = (UpdatePolicyDescriptionCommand) command;
                    updateDescriptionCommand.CheckVersion(_version);
                    _description = updateDescriptionCommand.Description;
                    break;
                case PolicyCommandType.AddRule:
                    var addRuleCommand = (AddRuleToPolicyCommand)command;
                    addRuleCommand.CheckVersion(_version);
                    _rules.Add(addRuleCommand.Rule);
                    break;
                case PolicyCommandType.AddFact:
                    var addFactCommand = (AddFactToPolicyCommand)command;
                    addFactCommand.CheckVersion(_version);
                    _facts.Add(addFactCommand.Fact);
                    break;
                case PolicyCommandType.AddPolicy:
                    var addPolicyCommand = (AddPolicyToPolicyCommand)command;
                    addPolicyCommand.CheckVersion(_version);
                    addPolicyCommand.Policy.Parent = this;
                    _children.Add(addPolicyCommand.Policy);
                    break;
                case PolicyCommandType.AddDataContext:
                    var addDataContextCommand = (AddDataContextToPolicyCommand)command;
                    addDataContextCommand.CheckVersion(_version);
                    _dataContext = addDataContextCommand.DataContext;
                    break;
            }

            ++_version;
            _commands.Add(command);
        }

        internal void ProcessCommands(IEnumerable<CommandBase> commands)
        {
            foreach (var command in commands)
            {
                ProcessCommand(command);
            }
        }

        private DataContext FindParentDataContext(Policy policy)
        {
            if (policy != null && policy._dataContext == null)
            {
                return FindParentDataContext(policy.Parent);
            }

            return policy?.DataContext;
        }
    }
}
