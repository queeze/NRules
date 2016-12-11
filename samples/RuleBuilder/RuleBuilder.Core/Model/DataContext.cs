namespace RuleBuilder.Core.Model
{
    using System.Collections.Generic;

    public class DataContext
    {
        public ICollection<ContextClass> ContextTypes { get; } = new HashSet<ContextClass>();
    }
}
