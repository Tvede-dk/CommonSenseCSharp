using CommonSenseCSharp.datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class NaturalSort {

    public static NonNullList<T> SortNatural<T>(this NonNullList<T> list, Func<T, string> extractor) {
        return list.FlatMap(x => { return new internalStructure<T>(x, extractor(x)); }).sortInternalStructure();
    }

    public static NonNullList<string> SortNatural(this IEnumerable<string> list) {
        return list.FlatMap(x => { return new internalStructure<string>(x, x); }).sortInternalStructure();
    }
    private static NonNullList<T> sortInternalStructure<T>(this NonNullList<internalStructure<T>> newLst) {
        newLst.Sort((lhs, rhs) => lhs.CompareTo(rhs));
        return newLst.FlatMap(x => x.GetObj());
    }

    private class internalStructure<T> {

        private T obj;

        private string value;

        private readonly NonNullList<TypeContent> order = new NonNullList<TypeContent>();

        public internalStructure(T orgObj, string value) {
            this.obj = orgObj;
            this.value = value;
            parseValue();
        }

        private void parseValue() {
            var builder = new StringBuilder();
            int startIndex = 0;
            var isString = true;
            for (int i = 0; i < value.Length; i++) {
                if (char.IsWhiteSpace(value, i)) {
                    continue;
                }
                if (char.IsNumber(value, i) == isString) { //iff diffferent, then store the current and change the type.
                    order.Add(new TypeContent(isString, startIndex, i, this));
                    isString = !isString;
                    builder.Clear();
                    startIndex = i;
                }
                builder.Append(value[i]);
            }

            if (builder.Length > 0) {
                order.Add(new TypeContent(isString, startIndex, value.Length, this));
            }


        }

        public int CompareTo(internalStructure<T> rhs) {
            var minSize = Math.Min(order.Count, rhs.order.Count);
            var res = minSize.PerformEachTimeUntil(i => order[i].CompareTo(rhs.order[i]), i => i != 0, 0);
            return res != 0 ? res : order.Count.CompareTo(rhs.order.Count); //if res != 0, return res, otherwise whichever is the smallest of the 2 order lists.
        }

        public T GetObj() {
            return obj;
        }

        struct TypeContent {
            public bool isNumber { get; set; }

            public string stringValue {
                get {
                    return structure.value.Substring(startIndex, endIndex - startIndex);
                }
            }
            private internalStructure<T> structure;
            private int startIndex;
            private int endIndex;

            public TypeContent(bool isString, int startIndex, int endIndex, internalStructure<T> structure) {
                this.isNumber = !isString;
                this.startIndex = startIndex;
                this.endIndex = endIndex;
                this.structure = structure;
            }

            public int CompareTo(TypeContent other) {
                if (isNumber) {
                    return other.isNumber ? int.Parse(stringValue).CompareTo(int.Parse(other.stringValue)) : 1; //if number, compare as such, else the other one wins
                } else {
                    return other.isNumber ? -1 : string.Compare(stringValue, other.stringValue, true); // if number,  we win (as string), otherwise, compare both as strings.
                }
            }
        }
    }
}
