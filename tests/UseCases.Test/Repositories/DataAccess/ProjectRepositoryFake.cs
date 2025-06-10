using Bogus;
using taskflow.API.Entities;
using taskflow.API.Enums;

namespace UseCases.Test.Repositories.DataAccess
{
    public class ProjectRepositoryFake
    {
        public Project CreateProjectEntity(int startId, int endId)
        {
            var entity = new Faker<Project>("pt_BR")
            .RuleFor(project => project.Id, f => f.Random.Number(startId, endId))
            .RuleFor(project => project.Name, f => $"{f.Hacker.Verb()} {f.Commerce.ProductAdjective()} {f.Commerce.Product()}")
            .RuleFor(project => project.UserId, f => f.Random.Number(1, 100))
            .RuleFor(project => project.StatusId, f => f.PickRandom<Status>())
            .RuleFor(project => project.DataAt, f => f.Date.Past())
            .RuleFor(project => project.DataUp, f => f.Date.Past())
            .RuleFor(project => project.Tasks, (f, project) => new List<Tarefa>
            {
                new Tarefa
                {
                    Id = f.Random.Number(1, 100),
                    Name = $"{f.Hacker.Verb()} {f.Hacker.Noun()}",
                    ProjectId = project.Id,
                    StatusId = f.PickRandom<Status>(),
                    PriorityId = f.PickRandom<Priority>(),
                    UserId = f.Random.Number(1, 100),
                    Description = f.Lorem.Sentences(50),
                    DateAt = f.Date.Past(),
                    DateUp = f.Date.Past()
                }
            }).Generate();

            return entity;
        }

        public List<Project> GenerateProjectList(int startId, int endId, int count = 5)
        {
            return Enumerable.Range(0, count)
                .Select(_ => CreateProjectEntity(startId, endId))
                .ToList();
        }
    }
}
