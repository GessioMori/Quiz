using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Quiz.Infra.Interfaces;
using Quiz.Models.Entities;

namespace Quiz.Infra.Register
{
    internal class QuestionClassMap : IEntityClassMap
    {
        public void Register()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Question)))
            {
                BsonClassMap.RegisterClassMap<Question>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(c => c.Id)
                        .SetIdGenerator(GuidGenerator.Instance)
                        .SetSerializer(new GuidSerializer());
                    cm.MapMember(c => c.Text).SetIsRequired(true);
                    cm.MapMember(c => c.Alternatives).SetIsRequired(true);
                    cm.MapMember(c => c.CorrectAlternative).SetIsRequired(true);
                });
            }
        }
    }
}
