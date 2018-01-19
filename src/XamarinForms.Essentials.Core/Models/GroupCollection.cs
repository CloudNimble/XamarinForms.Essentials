using System.Collections.Generic;

namespace XamarinForms.Essentials.Core
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="K">The Type to use for the Key. Could be something like a string, or a more complex type.</typeparam>
    /// <typeparam name="T">The Type to store in the ObservableRangeCollection.</typeparam>
    public class GroupCollection<K, T>: ObservableRangeCollection<T>
    {

        /// <summary>
        /// 
        /// </summary>
        public K Key { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="items"></param>
        public GroupCollection(K key, IEnumerable<T> items)
        {
            Key = key;
            AddRange(items);
        }

    }

}