using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace CommonSenseCSharp.datastructures
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public class ObservableCollectionRange<T> : ObservableCollection<T>
    {

        #region constructors

        public ObservableCollectionRange()
        {
        }

        public ObservableCollectionRange([NotNull] IEnumerable<T> data)
        {
            this.AddAll(data);
        }

        #endregion

        #region range features

        public void AddAll([NotNull] IEnumerable<T> list)
        {
            list.Foreach(Items.Add);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, list));
        }

        public void ClearAndAddAll([NotNull] IEnumerable<T> list)
        {
            Items.Clear();
            AddAll(list);
        }

        public void RemoveAll([NotNull] IEnumerable<T> listToRemove)
        {
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
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var objLst = obj as ObservableCollectionRange<T>;
            if (objLst != null && objLst.Count == Count)
            {
                var result = true;
                ArrayUtil.ForEach(this, objLst, (a, b) => result |= a == null || !a.Equals(b));
                return result;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [CanBeNull]
        public T ElementAt(int index)
        {
            return Items[index];
        }

        #endregion
    }
}