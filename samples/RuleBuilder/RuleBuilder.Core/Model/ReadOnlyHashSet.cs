using System.Collections;
using System.Collections.Generic;

namespace RuleBuilder.Core.Model
{
    public class ReadOnlyHashSet<T> : IReadOnlyCollection<T>
    {
        private readonly HashSet<T> _items;

        public ReadOnlyHashSet(HashSet<T> items)
        {
            _items = items;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _items.Count;
    }
}