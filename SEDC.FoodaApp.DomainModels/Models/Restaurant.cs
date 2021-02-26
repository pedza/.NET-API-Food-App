using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using SEDC.FoodApp.DomainModels.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.FoodApp.DomainModels.Models
{
    public class Restaurant
    {

        [BsonId(IdGenerator=typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public Municipality Municipality { get; set; }

        public List<MenuItem> Menu { get; set; }
    }
}
