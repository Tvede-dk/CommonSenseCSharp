using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class HashSet {
    public static void AddAll<T>(this HashSet<T> hashset, IEnumerable<T> items) {
        items.Foreach(hashset.Add);
    }

}
