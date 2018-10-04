using EntityLibrary;
using EntityLibrary.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject
{
    [TestClass]
    public class FunctionalTesting
    {
        [TestMethod]
        public void Exception_On_Stored_Procedure_Call_InMemoryDB()
        {
            var options = new DbContextOptionsBuilder<EntityContext>()
                .UseInMemoryDatabase("DummyDB")
                .Options;

            using (var context = new EntityContext(options))
            {
                var entityData = new EntityData(context);
                Assert.ThrowsException<NotSupportedException>(() => entityData.Get("A"));
            }
        }

        [TestMethod]
        public void Fetching_Data_Without_Stored_Procedure_Call_InMemoryDB()
        {
            var options = new DbContextOptionsBuilder<EntityContext>()
                .UseInMemoryDatabase("DummyDB1")
                .Options;

            using (var context = new EntityContext(options))
            {
                var entityData = new EntityData(context);
                var res = entityData.GetData("A");

                Assert.AreEqual(2, res.Count);
            }
        }

        [TestMethod]
        public void Fetching_Data_For_Type_B()
        {
            var options = new DbContextOptionsBuilder<EntityContext>()
                .UseInMemoryDatabase("DummyDB2")
                .Options;

            using (var context = new EntityContext(options))
            {
                var entityData = new EntityData(context);
                var res = entityData.GetData("B");

                Assert.AreEqual(2, res.Count);
            }
        }

        [TestMethod]
        public void Fetching_Data_For_Type_C()
        {
            var options = new DbContextOptionsBuilder<EntityContext>()
                .UseInMemoryDatabase("DummyDB3")
                .Options;

            using (var context = new EntityContext(options))
            {
                var entityData = new EntityData(context);
                var res = entityData.GetData("C");

                Assert.AreEqual(3, res.Count);
            }
        }

        [TestMethod]
        public void Fetching_Data_For_Type_D()
        {
            var options = new DbContextOptionsBuilder<EntityContext>()
                .UseInMemoryDatabase("DummyDB4")
                .Options;

            using (var context = new EntityContext(options))
            {
                var entityData = new EntityData(context);
                var res = entityData.GetData("D");

                Assert.AreEqual(1, res.Count);
            }
        }

        [TestMethod]
        public void Fetching_Data_For_Type_E()
        {
            var options = new DbContextOptionsBuilder<EntityContext>()
                .UseInMemoryDatabase("DummyDB5")
                .Options;

            using (var context = new EntityContext(options))
            {
                var entityData = new EntityData(context);
                var res = entityData.GetData("E");

                Assert.AreEqual(0, res.Count);
            }
        }

        [TestMethod]
        public void Add_Data_For_Type_E()
        {
            var options = new DbContextOptionsBuilder<EntityContext>()
                .UseInMemoryDatabase("DummyDB6")
                .Options;

            using (var context = new EntityContext(options))
            {
                var entityData = new EntityData(context);
                context.Entities.Add(new EntityLibrary.Entities.Entity()
                {
                    Id = 10,
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
        public void Remove_Data_For_Type_A()
        {
            var options = new DbContextOptionsBuilder<EntityContext>()
                .UseInMemoryDatabase("DummyDB7")
                .Options;

            using (var context = new EntityContext(options))
            {
                var entityData = new EntityData(context);

                var entA = entityData.GetData("A");
                context.Entities.RemoveRange(entA);
                context.SaveChanges();

                // Get Data for Type A
                var res = entityData.GetData("A");

                Assert.AreEqual(0, res.Count);
            }
        }
    }
}
