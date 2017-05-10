using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using CommonSenseCSharp.interfaces;
using JetBrains.Annotations;

namespace CommonSenseCSharp.datastructures {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableCollectionRange<T> : ObservableCollection<T> {
        #region constructors

        private bool _isSuspended = false;

        public ObservableCollectionRange() {
        }

        public ObservableCollectionRange([NotNull] IEnumerable<T> data) => AddAll(data);

        #endregion

        public void SuspendNotifications([NotNull] Action whileSuspened) {
            _isSuspended = true;
            try {
                whileSuspened();
            } catch (Exception) {
                // ignored
            }
            _isSuspended = false;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            if (_isSuspended) {
                return;
            }
            base.OnCollectionChanged(e);
        }

        #region range features

        public void AddAll([NotNull] IEnumerable<T> list) {
            list.Foreach(Items.Add);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, list));
        }

        public void ClearAndAddAll([NotNull] IEnumerable<T> list) {
            Items.Clear();
            AddAll(list);
        }

        public void RemoveAll([NotNull] IEnumerable<T> listToRemove) {
            listToRemove.Foreach(Items.Remove);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, listToRemove));
        }

        #endregion


        #region Equals and hashcode

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            if (obj is ObservableCollectionRange<T> objLst && objLst.Count == Count) {
                var result = true;
                ArrayUtil.ForEach(this, objLst, (a, b) => result |= a == null || !a.Equals(b));
                return result;
            }
            return false;
        }

        public override int GetHashCode() => base.GetHashCode();

        [CanBeNull]
        public T ElementAt(int index) => Items[index];

        #endregion
    }

    public static class ObservableCollectionRangeExtensions {
        public static ObservableCollectionRange<T> CloneDeep<T>([NotNull] this ObservableCollectionRange<T> obsList) where T : IClone<T> {
            return new ObservableCollectionRange<T>(obsList.FlatMap(x => x));
        }
    }
}