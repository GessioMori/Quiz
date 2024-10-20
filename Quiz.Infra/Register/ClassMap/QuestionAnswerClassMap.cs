using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Quiz.Infra.Interfaces;
using Quiz.Models.Entities;

namespace Quiz.Infra.Register.ClassMap
{
    internal class QuestionAnswerClassMap : IEntityClassMap
    {
        public void Register()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(QuestionAnswer)))
            {
                BsonClassMap.RegisterClassMap<QuestionAnswer>(cm =>
                {
                    cm.AutoMap();
                    cm.MapMember(c => c.QuestionId)
                        .SetSerializer(new GuidSerializer(GuidRepresentation.Standard))
                        .SetIsRequired(true);
                    cm.MapMember(c => c.Answer)
                        .SetIsRequired(true);
                });
            }
        }
    }
}
