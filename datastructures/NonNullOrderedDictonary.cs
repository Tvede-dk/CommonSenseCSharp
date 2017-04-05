using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;

namespace CommonSenseCSharp.datastructures{
    public interface IOrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>{
        [NotNull]
        TValue this[[PositiveIntRange] int index]{ get; set; }

        new TValue this[[NotNull] TKey key]{ get; set; }

        [PositiveIntRange]
        new int Count{ get; }

        [NotNull]
        new IEnumerable<TKey> Keys{ get; }

        [NotNull]
        new IEnumerable<TValue> Values{ get; }

        new void Add([NotNull] TKey key, [NotNull] TValue value);
        new void Clear();
        void Insert([PositiveIntRange] int index, [NotNull] TKey key, [NotNull] TValue value);
        int IndexOf([NotNull] TKey key);
        bool ContainsValue([NotNull] TValue value);
        bool ContainsValue([NotNull] TValue value, [NotNull] IEqualityComparer<TValue> comparer);
        new bool ContainsKey([NotNull] TKey key);

        [NotNull]
        new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator();

        new bool Remove([NotNull] TKey key);

        void RemoveAt([PositiveIntRange] int index);

        new bool TryGetValue(TKey key, out TValue value);

        [CanBeNull]
        TValue GetValue([NotNull] TKey key);

        void SetValue([NotNull] TKey key, [NotNull] TValue value);

        [NotNull]
        KeyValuePair<TKey, TValue> GetItem([PositiveIntRange] int index);

        void SetItem([PositiveIntRange] int index, [NotNull] TValue value);
    }

    public class NonNullOrderedDictonary<TKey, TValue> : IOrderedDictionary<TKey, TValue>, ICollection{
        #region Fields/Properties

        [NotNull] private KeyedCollection2<TKey, KeyValuePair<TKey, TValue>> _keyedCollection;

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key associated with the value to get or set.</param>
        public TValue this[TKey key]{
            get => GetValue(key);
            set => SetValue(key, value);
        }

        /// <summary>
        /// Gets or sets the value at the specified index.
        /// </summary>
        /// <param name="index">The index of the value to get or set.</param>
        public TValue this[int index]{
            get => GetItem(index).Value;
            set => SetItem(index, value);
        }

        public int Count => _keyedCollection.Count;

        public IEnumerable<TKey> Keys => _keyedCollection.Select(x => x.Key);
        public IEnumerable<TValue> Values => _keyedCollection.Select(x => x.Value);
        private IEqualityComparer<TKey> Comparer{ get; set; }

        #endregion

        #region Constructors

        public NonNullOrderedDictonary() => Initialize();

        public NonNullOrderedDictonary(IEqualityComparer<TKey> comparer) => Initialize(comparer);

        public NonNullOrderedDictonary(IOrderedDictionary<TKey, TValue> dictionary){
            Initialize();
            foreach (var pair in dictionary){
                _keyedCollection.Add(pair);
            }
        }

        public NonNullOrderedDictonary(IOrderedDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer){
            Initialize(comparer);
            foreach (var pair in dictionary){
                _keyedCollection.Add(pair);
            }
        }

        #endregion

        #region Methods

        private void Initialize(IEqualityComparer<TKey> comparer = null){
            Comparer = comparer;
            if (comparer != null){
                _keyedCollection = new KeyedCollection2<TKey, KeyValuePair<TKey, TValue>>(x => x.Key, comparer);
            }
            else{
                _keyedCollection = new KeyedCollection2<TKey, KeyValuePair<TKey, TValue>>(x => x.Key);
            }
        }

        public void Add(TKey key, TValue value) => _keyedCollection.Add(new KeyValuePair<TKey, TValue>(key, value));

        public void Clear() => _keyedCollection.Clear();

        public void Insert(int index, TKey key, TValue value) => _keyedCollection.Insert(index, new KeyValuePair<TKey, TValue>(key, value));

        public int IndexOf(TKey key){
            if (_keyedCollection.Contains(key)){
                return _keyedCollection.IndexOf(_keyedCollection[key]);
            }
            else{
                return -1;
            }
        }

        public bool ContainsValue(TValue value) => Values.Contains(value);

        public bool ContainsValue(TValue value, IEqualityComparer<TValue> comparer) => Values.Contains(value, comparer);

        public bool ContainsKey(TKey key) => _keyedCollection.Contains(key);

        public KeyValuePair<TKey, TValue> GetItem(int index){
            if (index < 0 || index >= _keyedCollection.Count){
                throw new ArgumentException($"The index was outside the bounds of the dictionary: {index}");
            }
            return _keyedCollection[index];
        }

        /// <summary>
        /// Sets the value at the index specified.
        /// </summary>
        /// <param name="index">The index of the value desired</param>
        /// <param name="value">The value to set</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the index specified does not refer to a KeyValuePair in this object
        /// </exception>
        public void SetItem(int index, TValue value){
            if (index < 0 || index >= _keyedCollection.Count){
                throw new ArgumentException($"The index is outside the bounds of the dictionary: {index}");
            }
            var kvp = new KeyValuePair<TKey, TValue>(_keyedCollection[index].Key, value);
            _keyedCollection[index] = kvp;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _keyedCollection.GetEnumerator();

        public bool Remove(TKey key) => _keyedCollection.Remove(key);

        public void RemoveAt(int index){
            if (index < 0 || index >= _keyedCollection.Count){
                throw new ArgumentException($"The index was outside the bounds of the dictionary: {index}");
            }
            _keyedCollection.RemoveAt(index);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key associated with the value to get.</param>
        public TValue GetValue(TKey key){
            if (_keyedCollection.Contains(key) == false){
                throw new ArgumentException($"The given key is not present in the dictionary: {key}");
            }
            var kvp = _keyedCollection[key];
            return kvp.Value;
        }

        /// <summary>
        /// Sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key associated with the value to set.</param>
        /// <param name="value">The the value to set.</param>
        public void SetValue(TKey key, TValue value){
            var kvp = new KeyValuePair<TKey, TValue>(key, value);
            var idx = IndexOf(key);
            if (idx > -1){
                _keyedCollection[idx] = kvp;
            }
            else{
                _keyedCollection.Add(kvp);
            }
        }

        public bool TryGetValue(TKey key, out TValue value){
            if (_keyedCollection.Contains(key)){
                value = _keyedCollection[key].Value;
                return true;
            }
            else{
                value = default(TValue);
                return false;
            }
        }

        #endregion

        #region sorting

        public void SortKeys() => _keyedCollection.SortByKeys();

        public void SortKeys(IComparer<TKey> comparer) => _keyedCollection.SortByKeys(comparer);

        public void SortKeys(Comparison<TKey> comparison) => _keyedCollection.SortByKeys(comparison);

        public void SortValues(){
            var comparer = Comparer<TValue>.Default;
            SortValues(comparer);
        }

        public void SortValues(IComparer<TValue> comparer) => _keyedCollection.Sort((x, y) => comparer.Compare(x.Value, y.Value));

        public void SortValues(Comparison<TValue> comparison) => _keyedCollection.Sort((x, y) => comparison(x.Value, y.Value));

        #endregion

        #region IDictionary<TKey, TValue>

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value) => Add(key, value);

        bool IDictionary<TKey, TValue>.ContainsKey(TKey key) => ContainsKey(key);

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => Keys.ToList();

        bool IDictionary<TKey, TValue>.Remove(TKey key) => Remove(key);

        bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value) => TryGetValue(key, out value);

        ICollection<TValue> IDictionary<TKey, TValue>.Values => Values.ToList();

        TValue IDictionary<TKey, TValue>.this[TKey key]{
            get => this[key];
            set => this[key] = value;
        }

        #endregion

        #region ICollection<KeyValuePair<TKey, TValue>>

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => _keyedCollection.Add(item);

        void ICollection<KeyValuePair<TKey, TValue>>.Clear() => _keyedCollection.Clear();

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => _keyedCollection.Contains(item);

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => _keyedCollection.CopyTo(array, arrayIndex);

        int ICollection<KeyValuePair<TKey, TValue>>.Count => _keyedCollection.Count;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => _keyedCollection.Remove(item);

        #endregion

        #region IEnumerable<KeyValuePair<TKey, TValue>>

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => GetEnumerator();

        #endregion

        #region IEnumerable

        IEnumerator IEnumerable.GetEnumerator() => new DictionaryEnumerator<TKey, TValue>(this);

        #endregion

        #region IOrderedDictionary

        void Insert(int index, object key, object value) => Insert(index, (TKey)key, (TValue)value);

        #endregion

        #region IDictionary

        void Add(object key, object value) => Add((TKey)key, (TValue)value);

        bool Contains(object key) => _keyedCollection.Contains((TKey)key);

        bool IsFixedSize => false;

        bool IsReadOnly => false;

        //  ICollection Keys => (ICollection) this.Keys;

        void Remove(object key) => Remove((TKey)key);

        //ICollection Values => (ICollection) this.Values;

        object this[object key]{
            get => this[(TKey) key];
            set => this[(TKey) key] = (TValue) value;
        }

        #endregion

        #region ICollection

        void ICollection.CopyTo(Array array, int index) => ((ICollection)_keyedCollection).CopyTo(array, index);

        int ICollection.Count => ((ICollection) _keyedCollection).Count;

        bool ICollection.IsSynchronized => ((ICollection) _keyedCollection).IsSynchronized;

        object ICollection.SyncRoot => ((ICollection) _keyedCollection).SyncRoot;

        #endregion
    }

    public class KeyedCollection2<TKey, TItem> : KeyedCollection<TKey, TItem>{
        private const string DelegateNullExceptionMessage = "Delegate passed cannot be null";
        private readonly Func<TItem, TKey> _getKeyForItemDelegate;

        public KeyedCollection2(Func<TItem, TKey> getKeyForItemDelegate)
            : base() => _getKeyForItemDelegate = getKeyForItemDelegate ?? throw new ArgumentNullException(DelegateNullExceptionMessage);

        public KeyedCollection2(Func<TItem, TKey> getKeyForItemDelegate, IEqualityComparer<TKey> comparer)
            : base(comparer) => _getKeyForItemDelegate = getKeyForItemDelegate ?? throw new ArgumentNullException(DelegateNullExceptionMessage);

        protected override TKey GetKeyForItem(TItem item) => _getKeyForItemDelegate(item);

        public void SortByKeys(){
            var comparer = Comparer<TKey>.Default;
            SortByKeys(comparer);
        }

        public void SortByKeys(IComparer<TKey> keyComparer){
            var comparer = new Comparer2<TItem>((x, y) => keyComparer.Compare(GetKeyForItem(x), GetKeyForItem(y)));
            Sort(comparer);
        }

        public void SortByKeys(Comparison<TKey> keyComparison){
            var comparer = new Comparer2<TItem>((x, y) => keyComparison(GetKeyForItem(x), GetKeyForItem(y)));
            Sort(comparer);
        }

        public void Sort(){
            var comparer = Comparer<TItem>.Default;
            Sort(comparer);
        }

        public void Sort(Comparison<TItem> comparison){
            var newComparer = new Comparer2<TItem>(comparison);
            Sort(newComparer);
        }

        public void Sort(IComparer<TItem> comparer){
            var list = Items as List<TItem>;
            list?.Sort(comparer);
        }
    }

    public class Comparer2<T> : Comparer<T>{
        //private readonly Func<T, T, int> _compareFunction;
        private readonly Comparison<T> _compareFunction;

        #region Constructors

        public Comparer2(Comparison<T> comparison) => _compareFunction = comparison ?? throw new ArgumentNullException(nameof(comparison));

        #endregion

        public override int Compare(T arg1, T arg2) => _compareFunction(arg1, arg2);
    }

    /// <summary>
    /// Adapter pattern.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class DictionaryEnumerator<TKey, TValue> : IDictionaryEnumerator, IDisposable{
        [NotNull]
        private readonly IEnumerator<KeyValuePair<TKey, TValue>> _impl;

        public void Dispose() => _impl.Dispose();

        public DictionaryEnumerator(IDictionary<TKey, TValue> value) => _impl = value.GetEnumerator();

        public void Reset() => _impl.Reset();

        public bool MoveNext() => _impl.MoveNext();

        public DictionaryEntry Entry{
            get{
                var pair = _impl.Current;
                return new DictionaryEntry(pair.Key, pair.Value);
            }
        }

        public object Key => _impl.Current.Key;

        public object Value => _impl.Current.Value;

        public object Current => Entry;
    }
}