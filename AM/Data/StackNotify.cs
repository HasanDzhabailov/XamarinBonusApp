using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace AM.Data {
    public class StackNotify<T> : ObservableCollection<T> {
        public virtual T Pop() {
            var item = this[Count - 1];
            Remove(item);
            return item;
        }
        public virtual void Push(T item) {
            base.Insert(0, item);
        }
    }
}
