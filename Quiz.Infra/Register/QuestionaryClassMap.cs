using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Quiz.Infra.Interfaces;
using Quiz.Models.Entities;

namespace Quiz.Infra.Register
{
    internal class QuestionaryClassMap : IEntityClassMap
    {
        public void Register()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Questionary)))
            {
                BsonClassMap.RegisterClassMap<Questionary>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(c => c.Id)
                        .SetIdGenerator(GuidGenerator.Instance)
                        .SetSerializer(new GuidSerializer());
                    cm.MapMember(c => c.Title).SetIsRequired(true);
                    cm.MapMember(c => c.Questions).SetIsRequired(true);
                    cm.MapMember(c => c.IsAvailable).SetIsRequired(true);
                    cm.MapMember(c => c.CreatedAt).SetIsRequired(true);
                });
            }
        }
    }
}
