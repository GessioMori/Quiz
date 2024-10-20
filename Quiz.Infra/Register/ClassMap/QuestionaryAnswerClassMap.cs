using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Quiz.Infra.Interfaces;
using Quiz.Models.Entities;

namespace Quiz.Infra.Register.ClassMap
{
    internal class QuestionaryAnswerClassMap : IEntityClassMap
    {
        public void Register()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(QuestionaryAnswer)))
            {
                BsonClassMap.RegisterClassMap<QuestionaryAnswer>(cm =>
                {
                    cm.AutoMap();
                    cm.MapMember(c => c.QuestionaryId)
                        .SetSerializer(new GuidSerializer(GuidRepresentation.Standard))
                        .SetIsRequired(true);
                    cm.MapMember(c => c.Answers)
                        .SetIsRequired(true);
                    cm.MapMember(c => c.CreatedAt).SetIsRequired(true);
                });
            }
        }
    }
}
