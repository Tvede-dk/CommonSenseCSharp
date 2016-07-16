using CommonSenseCSharp.datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSenseCSharp.datastructures {
    [Serializable]
    public class ObservableNonNullCollectionRange<T> : ObservableCollectionRange<T> {

        public new void Add(T item) {
            if (item != null) {
                base.Add(item);
            }
        }
        protected override void InsertItem(int index, T item) {
            if (item != null) {
                base.InsertItem(index,item);
            }
        }

        protected override void SetItem(int index, T item) {
            if (item != null) {
                base.SetItem(index,item);
            }
        }

        #region range features
        public new void AddAll(IEnumerable<T> list) {
            base.AddAll(list.Filter(x => x != null));
        }

        public new void ClearAndAddAll(IEnumerable<T> list) {
            base.Items.Clear();
            AddAll(list);
        }

        #endregion

      
    }
}
