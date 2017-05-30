using System.Collections.Generic;
using JetBrains.Annotations;

namespace CommonSenseCSharp.datastructures{
    public class SafeQueue<T> : Queue<T>{
        public SafeQueue([NotNull] T initialItem) => Enqueue(initialItem);

        public SafeQueue([NotNull] IEnumerable<T> initialItemss) => EnqueueItems(initialItemss);

        public SafeQueue([NotNull] params T[] initialItems) => EnqueueItems(initialItems);

        public void EnqueueItems([NotNull] params T[] items) => items.FlatForeach(Enqueue);

        public void EnqueueItems([NotNull] IEnumerable<T> items) => items.FlatForeach(Enqueue);
    }
}