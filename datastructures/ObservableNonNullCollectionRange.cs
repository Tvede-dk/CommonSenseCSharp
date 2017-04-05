using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CommonSenseCSharp.datastructures
{
    public class ObservableNonNullCollectionRange<T> : ObservableCollectionRange<T>
    {

        public new void Add(T item)
        {
            if (item != null)
            {
                base.Add(item);
            }
        }
        protected override void InsertItem(int index, T item)
        {
            if (item != null)
            {
                base.InsertItem(index, item);
            }
        }

        protected override void SetItem(int index, T item)
        {
            if (item != null)
            {
                base.SetItem(index, item);
            }
        }
        public ObservableNonNullCollectionRange()
        {

        }

        #region range features
        public new void AddAll(IEnumerable<T> list) => base.AddAll(list.Filter(x => x != null));

        public new void ClearAndAddAll(IEnumerable<T> list)
        {
            Items.Clear();
            AddAll(list);
        }

        #endregion


    }
}
