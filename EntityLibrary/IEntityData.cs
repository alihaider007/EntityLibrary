using System;
using System.Collections.Generic;
using System.Text;
using EntityLibrary.Entities;

namespace EntityLibrary
{
    public interface IEntityData
    {
        List<Entity> Get(string type);

        List<Entity> GetData(string type);
    }
}
