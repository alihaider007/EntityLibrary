using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EntityLibrary.Context;
using EntityLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityLibrary
{
    public class EntityData : IEntityData
    {
        private EntityContext _context;

        public EntityData(EntityContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();

            // To create stored procedure at the time of initializing db context
            // https://stackoverflow.com/questions/5701608/unique-key-with-ef-code-first/5701702#5701702

            // try catch block to handle exception if the context is used as InMemory Database
            // since Sql Command cannot be executed under InMemory Database
            try
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    // As per the requirements, data / schema should be created by the library therefore stored procedure
                    // will be added below after database creation is done
                    command.Connection.Open();
                    command.CommandText = "SELECT COUNT(*) AS [Count] FROM sys.objects WHERE type = 'P' AND name = 'spGetEntities'";
                    DbDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var count = reader.GetInt32(reader.GetOrdinal("Count"));

                            if (count == 0)
                            {
                                StringBuilder str = new StringBuilder();
                                str.Append("CREATE PROCEDURE spGetEntities ");
                                str.Append("@type nvarchar(20) ");
                                str.Append("AS ");
                                str.Append("BEGIN ");
                                str.Append("SELECT [Id],[Created],[Type],[Content] FROM [dbo].[Entities] WHERE[Type] = @type END");
                                RawSqlString sql = str.ToString();
                                _context.Database.ExecuteSqlCommand(sql);
                            }
                        }
                    }
                    command.Connection.Close();
                }
            }
            catch { }
        }

        public List<Entity> Get(string type)
        {
            // The stored procedure call is supported only on the physical database by Entity Framework. It will not work in InMemory DB
            var param = new SqlParameter("Type", type);
            return _context.Entities.FromSql("exec spGetEntities @Type ", param).ToList();
        }

        // For the purpose of accessing data for unit testing using InMemory DB, following method has been added
        public List<Entity> GetData(string type)
        {
            return _context.Entities.Where(r => r.Type == type).ToList();
        }
    }
}
