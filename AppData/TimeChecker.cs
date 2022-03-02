using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Real2App.AppData
{
    [Table("TimeChecker")]
    public class TimeChecker
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Time { get; set; }
    }
}
