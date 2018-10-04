using EntityLibrary.Context;
using System;

namespace EntityLibrary
{
    public class MyDbContextSeeder
    {
        public static void Seed(EntityContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //context.Entities.Add(new Entities.Entity()
            //{
            //    Id = 1,
            //    Type = "A",
            //    Content = "Some contents with Type A and ID 1",
            //    Created = DateTime.Now
            //});

            //context.Entities.Add(new Entities.Entity()
            //{
            //    Id = 2,
            //    Type = "A",
            //    Content = "Some contents with Type A and ID 2",
            //    Created = DateTime.Now
            //});

            //context.Entities.Add(new Entities.Entity()
            //{
            //    Id = 3,
            //    Type = "B",
            //    Content = "Some contents with Type A and ID 3",
            //    Created = DateTime.Now
            //});

            //context.Entities.Add(new Entities.Entity()
            //{
            //    Id = 4,
            //    Type = "C",
            //    Content = "Some contents with Type C and ID 4",
            //    Created = DateTime.Now
            //});

            context.SaveChanges();
        }
    }
}
