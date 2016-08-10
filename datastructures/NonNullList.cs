﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSenseCSharp.datastructures {


    /// <summary>
    /// a proxy class that basically discards all null related queries on this list, thus making sure every element inside is NOT null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NonNullList<T> : List<T> {

        public NonNullList(T[] variable) {
            this.AddRange(variable);
        }

        public NonNullList() {

        }

        public NonNullList(IEnumerable<T> input) {
            this.AddRange(input);
        }

        public NonNullList(int capacity) : base(capacity) {
           
        }

        /// <summary>
        /// adds a single item to this list (iff not null)
        /// </summary>
        /// <param name="item">the item to add, iff null then it does nothing </param>
        public new void Add(T item) {
            if (item != null) {
                base.Add(item);
            }
        }

        public void Add(params T[] items ) {
            items.Foreach(Add);
        }

        /// <summary>
        /// adds a collection of items to this list (iff not nulls)
        /// </summary>
        /// <param name="collection"></param>
        public new void AddRange(IEnumerable<T> collection) {
            collection.FlatForeach(base.Add);
        }
        [Pure]
        public NonNullList<T> GetSafeRange(int index, int count) {
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
        public new bool Contains(T item) {
            return item == null ? false : base.Contains(item);
        }




    }
}