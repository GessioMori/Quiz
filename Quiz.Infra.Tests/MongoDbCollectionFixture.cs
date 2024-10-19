using Xunit;

namespace Quiz.Infra.Tests
{
    [CollectionDefinition("MongoDbCollection")]
    public class MongoDbCollectionFixture : ICollectionFixture<MongoDbFixture>
    {
    }
}
