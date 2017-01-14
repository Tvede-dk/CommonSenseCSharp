using System;
using System.Collections.Generic;
using System.Linq;
using CommonSenseCSharp.datastructures;
using JetBrains.Annotations;

public enum DecentStrategy{
    BreadthFirst,
    DepthFirst
}

public static class DecentStrategyExtensions{
}

public static class FunctionalRecursive{
    public static void OnEachDecent<T>(IEnumerable<T> startItems, [NotNull] Action<T> onEachItem,
        [NotNull] Func<T, IReadOnlyList<T>> childrenExtractor, DecentStrategy strategy){
        if (DecentStrategy.DepthFirst == strategy){
        }
        else{
            OnEachBreadthFirstDecent(new SafeQueue<T>(startItems), onEachItem, childrenExtractor);
        }
    }

    public static void OnEachDecent<T>([NotNull] T startItem, [NotNull] Action<T> onEachItem,
        [NotNull] Func<T, IReadOnlyList<T>> childrenExtractor, DecentStrategy strategy){
        if (DecentStrategy.DepthFirst == strategy){
        }
        else{
            OnEachBreadthFirstDecent(new SafeQueue<T>(startItem), onEachItem, childrenExtractor);
        }
    }


    public static void OnEachDepthFirstDecent<T>([NotNull] T startItem, [NotNull] Action<T> onEachItem,
        [NotNull] Func<T, IReadOnlyList<T>> childrenExtractor){
        var order = new SafeStack<T>();
        var missingNodes = new NonNullList<T>(startItem);
        //first construct the traversel.
        //we go from right to left, thus we can create the "regular" DFS .. (this requires one to do it eg on paper to realize this).
        //and since we use a stack, we
        while (missingNodes.Count > 0){
            //first take the next node
            var currentNode = missingNodes.First();
            missingNodes.RemoveAt(0);
            //then we know this node is the last so far (and that the children comes before)
            order.Push(currentNode); //add now since this is reversed.
            //then take all children and add reversely to the list of missing nodes, then do the above over again.
            childrenExtractor(currentNode).FlatForeach(x => missingNodes.Insert(0, x));
        }
        //then its pretty easy to apply
        order.FlatForeach(onEachItem);
    }

    public static void OnEachBreadthFirstDecent<T>([NotNull] SafeQueue<T> itemQueue, [NotNull] Action<T> onEachItem,
        [NotNull] Func<T, IReadOnlyList<T>> childrenExtractor){
        do{
            var currentItem = itemQueue.Dequeue();
            onEachItem(currentItem);
            childrenExtractor(currentItem).FlatForeach(itemQueue.Enqueue);
        } while (itemQueue.Count > 0);
    }
}