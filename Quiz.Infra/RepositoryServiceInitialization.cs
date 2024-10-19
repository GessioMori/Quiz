using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Quiz.Infra.Register;
using Quiz.Infra.Repositories;
using Quiz.Shared.Interfaces.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositoryServiceInitialization
    {
        public static IServiceCollection AddMongoRepositories(this IServiceCollection services,
            IConfiguration config)
        {
            ClassMapInitializer.RegisterAllClassMaps();

            services.Configure<MongoDBSettings>(config.GetSection("MongoDBSettings"));

            services.AddSingleton<IMongoClient, MongoClient>(sp =>
                new MongoClient(config.GetValue<string>("MongoDBSettings:ConnectionString")));

            services.AddScoped<IMongoDatabase>(sp =>
                sp.GetRequiredService<IMongoClient>().GetDatabase(
                    config.GetValue<string>("MongoDBSettings:DatabaseName")));

            services.AddScoped<IQuestionaryRepository, QuestionaryRepository>();

            return services;
        }
    }
}
