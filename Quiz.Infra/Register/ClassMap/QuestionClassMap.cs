using MongoDB.Bson.Serialization;
using Quiz.Infra.Interfaces;
using Quiz.Models.Entities;

namespace Quiz.Infra.Register.ClassMap
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
                    cm.MapMember(c => c.Text).SetIsRequired(true);
                    cm.MapMember(c => c.Alternatives).SetIsRequired(true);
                    cm.MapMember(c => c.CorrectAlternative).SetIsRequired(true);
                });
            }
        }
    }
}
