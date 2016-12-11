using System.Collections.Generic;
using System.Threading.Tasks;

namespace RuleBuilder.Core.Storage
{
    public interface IRepository<T>
        where T : new()
    {
        Task<IList<T>> Load(int skip, int take);

        Task Delete(T item, bool hard = true);

        Task SaveOrUpdate(T item);
    }
}