using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Xamarin.Essentials;

namespace Real2App.AppData.Models
{
    public class ApplicationContext : DbContext
    {
        private string _databasePath;

        public DbSet<ChildrenElementCarousel> ChildrenElementCarousels { get; set; }
        public DbSet<ParentElementCarousel> ParentElementCarousels { get; set; }

        public ApplicationContext(string databasePath)
        {
            //SQLitePCL.Batteries.Init();
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string dbPath = Path.Combine(FileSystem.AppDataDirectory, "stories.db3");
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
