using Moq;
using Bogus;
using FluentAssertions;
using taskflow.API.Entities;
using taskflow.API.Contracts;
using taskflow.API.UseCases.Projects.GetCurrent;
using taskflow.API.Enums;

namespace UseCases.Test.Projects.GetCurrent
{
    public class GetCurrentProjectUseCaseTest
    {
        private Project CreateProject(int startId, int endId)
        {
            var entity = new Faker<Project>()
            .RuleFor(project => project.Id, f => f.Random.Number(startId, endId))
            .RuleFor(project => project.Name, f => f.Name.FirstName())
            .RuleFor(project => project.UserId, f => f.Random.Number(1, 100))
            .RuleFor(project => project.StatusId, f => f.PickRandom<Status>())
            .RuleFor(project => project.DataAt, f => f.Date.Past())
            .RuleFor(project => project.DataUp, f => f.Date.Past())
            .RuleFor(project => project.Tasks, (f, project) => new List<Tarefa>
            {
                new Tarefa
                {
                    Id = f.Random.Number(1, 100),
                    Name = f.Lorem.Word(),
                    ProjectId = project.Id,
                    StatusId = f.PickRandom<Status>(),
                    PriorityId = f.PickRandom<Priority>(),
                    UserId = f.Random.Number(1, 100),
                    DateAt = f.Date.Past(),
                    DateUp = f.Date.Past()
                }
            }).Generate();

            return entity;
        }

        [Fact]
        public void Success()
        {
            var entity = CreateProject(1, 100);

            var mock = new Mock<IProjectRepository>();
            mock.Setup(i => i.GetCurrentId(entity.Id)).Returns(entity);

            //ARRANGE
            var useCase = new GetCurrentProjectUseCase(mock.Object);

            //ACT
            var project = useCase.GetCurrentId(entity.Id);

            //ASSERT
            project.Should().NotBeNull();
            project.Id.Should().Be(entity.Id);
            project.DataAt.Should().Be(entity.DataAt);
            project.DataUp.Should().Be(entity.DataUp);
            project.Tasks.Should().NotBeNull();
        }
    }
}
