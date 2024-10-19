using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Quiz.Infra.Tests
{
    public class MongoDbFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; private set; }
        private readonly IServiceCollection _services;

        public MongoDbFixture()
        {
            this._services = new ServiceCollection();

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Test.json")
                .Build();

            this._services.AddMongoRepositories(config);

            ServiceProvider = this._services.BuildServiceProvider();
        }

        public void Dispose()
        {
            IMongoDatabase database = ServiceProvider.GetRequiredService<IMongoDatabase>();
            database.Client.DropDatabase(database.DatabaseNamespace.DatabaseName);
        }
    }
}
