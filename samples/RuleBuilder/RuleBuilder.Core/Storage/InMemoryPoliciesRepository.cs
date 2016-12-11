namespace RuleBuilder.Core.Storage
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Model;

    public class InMemoryPoliciesRepository : IPoliciesRepository
    {
        private readonly List<Policy> _policies = new List<Policy>();

        public Task<IList<Policy>> Load(int skip, int take)
        {
            return Task.Factory
                .StartNew<IList<Policy>>(() => _policies.Skip(skip).Take(take).ToList());
        }

        public Task Delete(Policy item, bool hard = true)
        {
            return Task.Factory.StartNew(() =>
            {
                var itemToDelete = _policies.FirstOrDefault(x => x == item);
                if (itemToDelete != null)
                {
                    if (hard)
                    {
                        _policies.Remove(itemToDelete);
                    }
                    else
                    {
                        itemToDelete.IsDeleted = true;
                    }
                }
            });
        }

        public Task SaveOrUpdate(Policy item)
        {
            return Task.Factory.StartNew(() =>
            {
                var existingItem = _policies.FirstOrDefault(x => x == item);
                if (item == null)
                {
                    _policies.Add(existingItem);
                }
            });
        }
    }
}