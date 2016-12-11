using CommonSenseCSharp.datastructures;
using System;
using System.Collections.Generic;
using System.Text;

public static class NaturalSort {

    public static NonNullList<T> SortNatural<T>(this NonNullList<T> list, Func<T, string> extractor) {
        return list.FlatMap(x => { return new InternalStructure<T>(x, extractor(x)); }).SortInternalStructure();
    }

    public static NonNullList<string> SortNatural(this IEnumerable<string> list) {
        return list?.FlatMap(x => { return new InternalStructure<string>(x, x); }).SortInternalStructure() ?? new NonNullList<string>();
    }
    private static NonNullList<T> SortInternalStructure<T>(this NonNullList<InternalStructure<T>> newLst) {
        newLst.Sort((lhs, rhs) => lhs.CompareTo(rhs));
        return newLst.FlatMap(x => x.GetObj());
    }

    private class InternalStructure<T> {

        private readonly T _obj;

        private readonly string _value;

        private readonly NonNullList<TypeContent> _order = new NonNullList<TypeContent>();

        public InternalStructure(T orgObj, string value) {
            this._obj = orgObj;
            this._value = value;
            ParseValue();
        }

        private void ParseValue() {
            var builder = new StringBuilder();
            var startIndex = 0;
            var isString = true;
            for (var i = 0; i < _value.Length; i++) {
                if (char.IsWhiteSpace(_value, i)) {
                    continue;
                }
                if (char.IsNumber(_value, i) == isString) { //iff diffferent, then store the current and change the type.
                    _order.Add(new TypeContent(isString, startIndex, i, this));
                    isString = !isString;
                    builder.Clear();
                    startIndex = i;
                }
                builder.Append(_value[i]);
            }

            if (builder.Length > 0) {
                _order.Add(new TypeContent(isString, startIndex, _value.Length, this));
            }


        }

        public int CompareTo(InternalStructure<T> rhs) {
            var minSize = Math.Min(_order.Count, rhs._order.Count);
            var res = minSize.PerformEachTimeUntil(i => _order[i].CompareTo(rhs._order[i]), i => i != 0, 0);
            return res != 0 ? res : _order.Count.CompareTo(rhs._order.Count); //if res != 0, return res, otherwise whichever is the smallest of the 2 order lists.
        }

        public T GetObj() {
            return _obj;
        }

        struct TypeContent {
            public bool IsNumber { get; set; }

            public string StringValue {
                get {
                    return _structure._value.Substring(_startIndex, _endIndex - _startIndex);
                }
            }
            private readonly InternalStructure<T> _structure;
            private readonly int _startIndex;
            private readonly int _endIndex;

            public TypeContent(bool isString, int startIndex, int endIndex, InternalStructure<T> structure) {
                this.IsNumber = !isString;
                this._startIndex = startIndex;
                this._endIndex = endIndex;
                this._structure = structure;
            }

            public int CompareTo(TypeContent other) {
                if (IsNumber) {
                    return other.IsNumber ? StringValue.IntValue().CompareTo(other.StringValue.IntValue()) : 1; //if number, compare as such, else the other one wins
                } else {
                    return other.IsNumber ? -1 : string.Compare(StringValue, other.StringValue, true); // if number,  we win (as string), otherwise, compare both as strings.
                }
            }
        }
    }
}
