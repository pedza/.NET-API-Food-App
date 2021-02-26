using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.FoddApp.DataAccess.Mongo
{
    public interface IMongoDbRepository<T>
    {
        IMongoQueryable<T> Collection { get; }
    }
}
