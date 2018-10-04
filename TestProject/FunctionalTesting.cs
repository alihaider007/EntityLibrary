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
        private readonly DbContextOptions<EntityContext> _options;
        private readonly EntityContext _context;
        private readonly EntityData _entityData;

        public FunctionalTesting()
        {
            _options = new DbContextOptionsBuilder<EntityContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new EntityContext(_options);
            _entityData = new EntityData(_context);
        }

        [TestMethod]
        public void Exception_On_Stored_Procedure_Call_InMemoryDB()
        {
            Assert.ThrowsException<NotSupportedException>(() => _entityData.Get("A"));
        }

        [TestMethod]
        public void Fetching_Data_Without_Stored_Procedure_Call_InMemoryDB()
        {
            var res = _entityData.GetData("A");
        }

        [TestMethod]
        public void Fetching_Data_For_Type_B()
        {
            var res = _entityData.GetData("B");

            Assert.AreEqual(2, res.Count);
        }

        [TestMethod]
        public void Fetching_Data_For_Type_C()
        {
            var res = _entityData.GetData("C");

            Assert.AreEqual(3, res.Count);
        }

        [TestMethod]
        public void Fetching_Data_For_Type_D()
        {
            var res = _entityData.GetData("D");

            Assert.AreEqual(1, res.Count);
        }

        [TestMethod]
        public void Fetching_Data_For_Type_E()
        {
            var res = _entityData.GetData("E");

            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void Add_Data_For_Type_E()
        {
            _context.Entities.Add(new EntityLibrary.Entities.Entity()
            {
                Id = 10,
                Type = "E",
                Content = "This is test data for type E with ID 10",
                Created = DateTime.Now
            });
            _context.SaveChanges();

            // Get Data for Type E
            var res = _entityData.GetData("E");

            Assert.AreEqual(1, res.Count);
        }

        [TestMethod]
        public void Remove_Data_For_Type_A()
        {
            var entA = _entityData.GetData("A");
            _context.Entities.RemoveRange(entA);
            _context.SaveChanges();

            // Get Data for Type A
            var res = _entityData.GetData("A");

            Assert.AreEqual(0, res.Count);
        }
    }
}
