using Microsoft.EntityFrameworkCore;

using EntityLibrary.Entities;
using System;
using Microsoft.Extensions.Configuration;

namespace EntityLibrary.Context
{
    public class EntityContext : DbContext
    {
        // Constructor added to support multiple set of databases from the caller
        // e.g: InMemory Database context can be used for Unit Testing purpose where database is not physically created
        // OnConfiguring method can be overriden to set the connectionstring from json configuration file but it will 
        // make impossible to do unit test without database so I have to use constructor db context instead
        // https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
        public EntityContext(DbContextOptions<EntityContext> options)
        : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Data seeding allows to provide initial data to populate a database
            // if the database already exists, EnsureCreated() will neither update the schema nor the seed data in the database
            // https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding
            modelBuilder.
                Entity<Entity>().
                HasData(
                new Entity() { Id = 1, Type = "A", Content = "Some contents with Type A and ID 1", Created = DateTime.Now },
                new Entity() { Id = 2, Type = "A", Content = "Some contents with Type A and ID 2", Created = DateTime.Now },
                new Entity() { Id = 3, Type = "B", Content = "Some contents with Type B and ID 3", Created = DateTime.Now },
                new Entity() { Id = 4, Type = "B", Content = "Some contents with Type B and ID 4", Created = DateTime.Now },
                new Entity() { Id = 5, Type = "C", Content = "Some contents with Type C and ID 5", Created = DateTime.Now },
                new Entity() { Id = 6, Type = "C", Content = "Some contents with Type C and ID 6", Created = DateTime.Now },
                new Entity() { Id = 7, Type = "C", Content = "Some contents with Type C and ID 7", Created = DateTime.Now },
                new Entity() { Id = 8, Type = "D", Content = "Some contents with Type D and ID 8", Created = DateTime.Now });
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(GetConfig().GetConnectionString("EntityConnection"));
        //}

        public DbSet<Entity> Entities { get; set; }

        private IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("configuration.json",
                optional: true,
                reloadOnChange: true);

            return builder.Build();
        }
    }
}
