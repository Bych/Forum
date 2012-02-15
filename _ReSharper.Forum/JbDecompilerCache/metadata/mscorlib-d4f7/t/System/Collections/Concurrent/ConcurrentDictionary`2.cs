// Type: System.Collections.Concurrent.ConcurrentDictionary`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Collections.Concurrent
{
    [ComVisible(false)]
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof (Mscorlib_DictionaryDebugView<,>))]
    [Serializable]
    [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
    public class ConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary, ICollection, IEnumerable
    {
        public ConcurrentDictionary();
        public ConcurrentDictionary(int concurrencyLevel, int capacity);
        public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection);
        public ConcurrentDictionary(IEqualityComparer<TKey> comparer);
        public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer);
        public ConcurrentDictionary(int concurrencyLevel, IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public ConcurrentDictionary(int concurrencyLevel, int capacity, IEqualityComparer<TKey> comparer);

        public bool IsEmpty { get; }

        #region IDictionary Members

        void IDictionary.Add(object key, object value);
        bool IDictionary.Contains(object key);
        IDictionaryEnumerator IDictionary.GetEnumerator();
        void IDictionary.Remove(object key);
        void ICollection.CopyTo(Array array, int index);
        bool IDictionary.IsFixedSize { get; }
        bool IDictionary.IsReadOnly { get; }

        ICollection IDictionary.Keys { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        ICollection IDictionary.Values { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        object IDictionary.this[object key] { get; set; }
        bool ICollection.IsSynchronized { get; }
        object ICollection.SyncRoot { get; }

        #endregion

        #region IDictionary<TKey,TValue> Members

        public bool ContainsKey(TKey key);
        public bool TryGetValue(TKey key, out TValue value);
        public void Clear();
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index);
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator();
        void IDictionary<TKey, TValue>.Add(TKey key, TValue value);
        bool IDictionary<TKey, TValue>.Remove(TKey key);
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair);
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair);
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair);

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        IEnumerator IEnumerable.GetEnumerator();

        public TValue this[TKey key] { get; set; }
        public int Count { get; }

        public ICollection<TKey> Keys { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public ICollection<TValue> Values { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly { get; }

        #endregion

        public bool TryAdd(TKey key, TValue value);
        public bool TryRemove(TKey key, out TValue value);
        public bool TryUpdate(TKey key, TValue newValue, TValue comparisonValue);
        public KeyValuePair<TKey, TValue>[] ToArray();
        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory);
        public TValue GetOrAdd(TKey key, TValue value);
        public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory);
        public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory);
    }
}
