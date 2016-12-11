using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using RuleBuilder.Core.Model;
using RuleBuilder.Presentation.ViewModels;

namespace RuleBuilder.Presentation.Services
{
    public class ViewModelServices
    {
        public PoliciesViewModel CreatePoliciesViewModel(IEnumerable<Policy> policies)
        {
            var result = new PoliciesViewModel();
            Temp(policies).ToList().ForEach(result.Items.Add);
            return result;
        }

        private IEnumerable<NodeViewModel> Temp(IEnumerable<Policy> policies)
        {
            List<NodeViewModel> result = new List<NodeViewModel>();

            foreach (var policy in policies)
            {
                var nodeModel = new NodeViewModel(policy.Name, policy, (int)PoliciesTreeNodeTypes.Policy);

                var rulesNode = new NodeViewModel("Rules", policy, (int) PoliciesTreeNodeTypes.Rules);
                foreach (var rule in policy.Rules)
                {
                    rulesNode.Children.Add(new NodeViewModel(rule.Name, rule, (int) PoliciesTreeNodeTypes.Rule));
                }
                nodeModel.Children.Add(rulesNode);

                var factsNode = new NodeViewModel("Facts", policy, (int) PoliciesTreeNodeTypes.Facts);
                foreach (var fact in policy.Facts)
                {
                    factsNode.Children.Add(new NodeViewModel(fact.Name, fact, (int)PoliciesTreeNodeTypes.Fact));
                }
                nodeModel.Children.Add(factsNode);

                Temp(policy.Children).ToList().ForEach(x => nodeModel.Children.Add(x));
                result.Add(nodeModel);
            }

            return result;
        }
    }

    public enum PoliciesTreeNodeTypes
    {
        Policy = 1,
        Rules = 2,
        Facts = 3,
        Rule = 4,
        Fact = 5
    }
}
