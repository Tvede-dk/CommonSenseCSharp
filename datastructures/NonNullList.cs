using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace CommonSenseCSharp.datastructures{
    /// <summary>
    /// a proxy class that basically discards all null related queries on this list, thus making sure every element inside is NOT null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NonNullList<T> : List<T>{
        public NonNullList([NotNull] T[] variable){
            AddRange(variable);
        }

        public NonNullList(){
        }

        public NonNullList([CanBeNull] IEnumerable<T> input){
            this.AddRange(input);
        }

        public NonNullList(int capacity) : base(capacity){
        }

        public NonNullList(T initalItem){
            Add(initalItem);
        }

        /// <summary>
        /// adds a single item to this list (iff not null)
        /// </summary>
        /// <param name="item">the item to add, iff null then it does nothing </param>
        public new void Add([CanBeNull] T item){
            if (item != null){
                base.Add(item);
            }
        }

        public void Add([CanBeNull] params T[] items){
            items?.Foreach(Add);
        }

        [NotNull]
        public NonNullList<T> RemoveLast(){
            if (Count > 0){
                RemoveAt(Count - 1);
            }
            return this;
        }

        [NotNull]
        public NonNullList<T> RemoveFirst(){
            if (Count > 0){
                RemoveAt(0);
            }
            return this;
        }

        /// <summary>
        /// adds a collection of items to this list (iff not nulls)
        /// </summary>
        /// <param name="collection"></param>
        public new void AddRange([CanBeNull] IEnumerable<T> collection){
            collection?.FlatForeach(Add);
        }

        /// <summary>
        /// adds a collection of items to this list (iff not nulls)
        /// </summary>
        /// <param name="collection"></param>
        public void AddRange([NotNull] params T[] collection){
            collection?.FlatForeach(Add);
        }

        [NotNull]
        public NonNullList<T> AddRangeFluent([CanBeNull] IEnumerable<T> collection){
            AddRange(collection);
            return this;
        }

        [Pure]
        public NonNullList<T> GetSafeRange(int index, int count){
            var res = new NonNullList<T>();
            index.PerformTimes(count, x => res.Add(this[x]));
            return res;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [Pure]
        public new bool Contains([CanBeNull] T item){
            return item != null && base.Contains(item);
        }

        [NotNull]
        public static NonNullList<T> CreateFrom([NotNull] params T[] items){
            return items.FlatMap(x => x);
        }

        [NotNull]
        public NonNullList<T> Limit([PositiveIntRange] int limitListCount){
            if (Count > limitListCount && limitListCount > 0){
                return GetSafeRange(0, limitListCount);
            }
            return this;
        }

        public void Repeate([PositiveIntRange] int toSize){
            if (toSize <= Count || Count == 0){
                return;
            }
            for (var i = Count; i < toSize; i++){
                Add(this.First());
            }
        }

        public void AddOrRemove([NotNull] T item, bool shouldAdd) {
            if (shouldAdd) {
                Add(item);
            } else {
                Remove(item);
            }
        }
    }
}