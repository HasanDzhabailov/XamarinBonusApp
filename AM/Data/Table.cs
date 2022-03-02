using System;
using System.Collections.Generic;
using System.Text;

namespace AM.Data {
    public class Table<T> {
        protected List<List<T>> list;
        public int ColumnCount { get; private set; }
        public int RowCount { get; private set; }
        public Table(int columnCount, int rowCount) {
            ColumnCount = columnCount; RowCount = rowCount;
            list = new List<List<T>>();
            for (int i = 0; i < rowCount; i++) {
                var l = new List<T>();
                for (int j = 0; j < columnCount; j++) {
                    l.Add(default);
                }
                list.Add(l);
            }
        }
        public void Add(T value, int row, int column) {
            list[row][column] = value;
        }
        public T Get(int row, int column) { return list[row][column]; }
        public T this[int row, int column] { get { return list[row][column]; } }
    }
}
