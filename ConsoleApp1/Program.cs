using EntityLibrary;
using EntityLibrary.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<EntityContext>()
                .UseSqlServer(GetConfig().GetConnectionString("EntityConnection"))
                .Options;

            using (var context = new EntityContext(options))
            {
                var entityData = new EntityData(context);
                var res = entityData.Get("A");

                foreach(var a in res)
                {
                    Console.WriteLine($"ID: {a.Id}, Type: {a.Type}, Content: {a.Content}, Created: {a.Created}");
                }
            }

            Console.ReadKey(); ;
        }

        public static IConfiguration GetConfig()
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
