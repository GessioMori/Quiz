using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Quiz.Infra.Interfaces;
using Quiz.Models.Entities;

namespace Quiz.Infra.Register.ClassMap
{
    internal class BaseEntityClassMap : IEntityClassMap
    {
        public void Register()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(BaseEntity)))
            {
                BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(c => c.Id)
                        .SetIdGenerator(GuidGenerator.Instance)
                        .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
                });
            }
        }
    }
}
