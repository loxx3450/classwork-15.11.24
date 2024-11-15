using classwork.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classwork.Storage
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=NewsApiDB;Username=postgres;Password=local;", b => b.MigrationsAssembly("classwork.API"))
                //.UseLazyLoadingProxies()
                .EnableSensitiveDataLogging();
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
