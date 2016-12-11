using System.Collections.Generic;

namespace RuleBuilder.Presentation.ViewModels
{
    public class NodeViewModel 
    {
        public NodeViewModel()
        {
                
        }

        public NodeViewModel(string header, object item, int type)
        {
            Header = header;
            Item = item;
            Type = type;
        }

        public ICollection<NodeViewModel> Children { get; } = new List<NodeViewModel>();

        public string Header { get; set; }

        public object Item { get; set; }

        public int Type { get; set; }
    }
}
