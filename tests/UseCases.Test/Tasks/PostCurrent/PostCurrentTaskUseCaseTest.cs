using Moq;
using Bogus;
using FluentAssertions;
using taskflow.API.Communication.Requests;
using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.Enums;
using taskflow.API.UseCases.Tasks.PostCurrent;

namespace UseCases.Test.Tasks.PostCurrent
{
    public class PostCurrentTaskUseCaseTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Create(int projectId)
        {
            var request = new Faker<RequestTaskJson>()
            .RuleFor(request => request.Name, f => f.Name.FindName())
            .RuleFor(request => request.PriorityId, f => f.PickRandom<Priority>())
            .RuleFor(request => request.StatusId, f => f.PickRandom<Status>())
            .RuleFor(request => request.UserId, f => f.Random.Number(1, 100))
            .RuleFor(request => request.Description, f => f.Lorem.Word())
            .Generate();

            var entity = new Faker<Tarefa>()
            .RuleFor(entity => entity.Id, f => f.Random.Number(1, 100))
            .RuleFor(entity => entity.Name, request.Name)
            .RuleFor(entity => entity.ProjectId, projectId)
            .RuleFor(entity => entity.PriorityId, (Priority) request.PriorityId)
            .RuleFor(entity => entity.StatusId, f => (Status) request.StatusId)
            .RuleFor(entity => entity.UserId, f => request.UserId)
            .RuleFor(entity => entity.Description, f => f.Lorem.Word())
            .Generate();

            var taskRepository = new Mock<ITaskRepository>();
            taskRepository.Setup(i => i.Create(entity)).ReturnsAsync(entity.Id);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.ExistProjectWithId(projectId)).Returns(true);


            //ARRANGE
            var useCase = new PostCurrentTaskUseCase(taskRepository.Object, userRepository.Object, projectRepository.Object);

            //ACT
            var act = () => useCase.Execute(projectId, request);

            //ASSERT
            act.Should().NotThrow();
        }
    }
}
