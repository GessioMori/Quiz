using Microsoft.Extensions.DependencyInjection;
using Quiz.Models.Mapping;
using Quiz.Shared.InMemoryRepositories;
using Quiz.Shared.Interfaces.Repositories;

namespace Quiz.Services.Tests
{
    public class QuestionaryServiceFixture
    {
        public IServiceProvider ServiceProvider { get; set; }
        private readonly IServiceCollection _services;
        public QuestionaryServiceFixture()
        {
            this._services = new ServiceCollection();

            this._services.AddAutoMapper(typeof(MappingProfile));
            this._services.AddSingleton<IQuestionaryRepository, InMemoryQuestionaryRepository>();

            ServiceProvider = this._services.BuildServiceProvider();
        }
    }
}
