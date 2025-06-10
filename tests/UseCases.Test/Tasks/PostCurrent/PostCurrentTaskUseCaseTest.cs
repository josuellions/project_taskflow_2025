using Moq;
using Bogus;
using FluentAssertions;
using taskflow.API.Communication.Requests;
using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.Enums;
using taskflow.API.UseCases.Tasks.PostCurrent;
using UseCases.Test.Communication.Requests;
using UseCases.Test.Repositories.DataAccess;

namespace UseCases.Test.Tasks.PostCurrent
{
    public class PostCurrentTaskUseCaseTest : IClassFixture<TaskRepositoryFake>, IClassFixture<RequestTaskJsonFake>
    {
        private readonly TaskRepositoryFake _repositoryFake;
        private readonly RequestTaskJsonFake _requestTaskJsonFake;

        public PostCurrentTaskUseCaseTest(TaskRepositoryFake repositoryFake, RequestTaskJsonFake requestTaskJsonFake)
        {
            _repositoryFake = repositoryFake;
            _requestTaskJsonFake = requestTaskJsonFake;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Create(int projectId)
        {
            var request = _requestTaskJsonFake.CreateRequestTaskJson();

            var entity = _repositoryFake.CreateTaskEntity(projectId, 1, 100, request);

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
