using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Real2App.AppData
{
    public class TimerViewModel
    {
        SQLiteConnection database;
        public TimerViewModel(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<TimeChecker>();
        }
        public IEnumerable<TimeChecker> GetItems()
        {
            return database.Table<TimeChecker>().ToList();
        }
        public TimeChecker GetItem(int id)
        {
            return database.Get<TimeChecker>(id);
        }
        public TimeChecker SaveItem(TimeChecker item)
        {
            if (item != null)
            {
                database.Update(item);
                return item;
            }
            else
            {
                return item;
            }
        }
        public TimeChecker SaveNewItem(TimeChecker item)
        {
            database.Insert(item);
            return item;
        }
    }
}
