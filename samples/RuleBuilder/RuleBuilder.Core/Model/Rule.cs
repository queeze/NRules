namespace RuleBuilder.Core.Model
{
    public class Rule
    {
        public Rule()
        {
            LeftHandSide = new LeftHandSide();
            RightHandSide = new RightHandSide();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public int Version { get; set; }

        public string Notes { get; set; }

        public LeftHandSide LeftHandSide { get; }

        public RightHandSide RightHandSide { get; }
    }
}
