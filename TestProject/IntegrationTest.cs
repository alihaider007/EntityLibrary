using EntityLibrary;
using EntityLibrary.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace TestProject
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        [Ignore]
        public void Integration_Test_Fetch_Type_A_With_Stored_Procedure()
        {
            var options = new DbContextOptionsBuilder<EntityContext>()
                .UseSqlServer(Configuration.GetConfig().GetConnectionString("EntityConnection"))
                .Options;

            using (var context = new EntityContext(options))
            {
                var entityData = new EntityData(context);
                var res = entityData.Get("A");

                Assert.AreEqual(2, res.Count);
            }
        }

        [TestMethod]
        [Ignore]
        public void Integration_Test_Add_Type_E_Data()
        {
            var options = new DbContextOptionsBuilder<EntityContext>()
                .UseSqlServer(Configuration.GetConfig().GetConnectionString("EntityConnection"))
                .Options;

            using (var context = new EntityContext(options))
            {
                var entityData = new EntityData(context);
                context.Entities.Add(new EntityLibrary.Entities.Entity()
                {
                    Type = "E",
                    Content = "This is test data for type E with ID 10",
                    Created = DateTime.Now
                });
                context.SaveChanges();

                // Get Data for Type E
                var res = entityData.GetData("E");

                Assert.AreEqual(1, res.Count);
            }
        }

        [TestMethod]
        [Ignore]
        public void Integration_Test_Delete_Type_E_Data()
        {
            var options = new DbContextOptionsBuilder<EntityContext>()
                .UseSqlServer(Configuration.GetConfig().GetConnectionString("EntityConnection"))
                .Options;

            using (var context = new EntityContext(options))
            {
                var entityData = new EntityData(context);
                var resE = entityData.GetData("E");
                context.RemoveRange(resE);
                context.SaveChanges();

                // Get Data for Type E
                var res = entityData.GetData("E");

                Assert.AreEqual(0, res.Count);
            }
        }

        [TestMethod]
        public void Integration_Test_Invalid_ConnectionString()
        {
            var options = new DbContextOptionsBuilder<EntityContext>()
                .UseSqlServer(Configuration.GetConfig().GetConnectionString("Invalid"))
                .Options;

            using (var context = new EntityContext(options))
            {
                Assert.ThrowsException<SqlException>(() => new EntityData(context));
            }
        }

        [TestMethod]
        public void Integration_Test_ConnectionString_Is_Empty()
        {
            Assert.ThrowsException<ArgumentException>(() => new DbContextOptionsBuilder<EntityContext>()
                .UseSqlServer(Configuration.GetConfig().GetConnectionString("EmptyConnection"))
                .Options);
        }
    }

    public static class Configuration
    {
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
