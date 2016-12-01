using System.Collections.Generic;

public static class HashSet {
    public static void AddAll<T>(this HashSet<T> hashset, IEnumerable<T> items) {
        items.Foreach(hashset.Add);
    }

}
