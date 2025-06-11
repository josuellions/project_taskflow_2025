using FluentAssertions;
using Moq;
using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.UseCases.Tasks.DeleteCurrent;
using UseCases.Test.Communication.Requests;
using UseCases.Test.Repositories.DataAccess;

namespace UseCases.Test.Tasks.DeleteCurrent
{
    public class DeleteCurrentTaskUseCaseTest : IClassFixture<TaskRepositoryFake>, IClassFixture<RequestTaskJsonFake>
    {
        private readonly TaskRepositoryFake _repositoryFake;
        private readonly RequestTaskJsonFake _requestTaskJsonFake;

        public DeleteCurrentTaskUseCaseTest(TaskRepositoryFake repositoryFake, RequestTaskJsonFake requestTaskJsonFake)
        {
            _repositoryFake = repositoryFake;
            _requestTaskJsonFake = requestTaskJsonFake;
        }

        [Theory]
        [InlineData(1)]
        public void Success(int projectId)
        {
            var request = _requestTaskJsonFake.CreateRequestTaskJson();
            var entity = _repositoryFake.CreateTaskEntity(request, projectId, 1, 100);

            var taskRepository = new Mock<ITaskRepository>();
            taskRepository.Setup(i => i.Create(It.IsAny<Tarefa>())).ReturnsAsync(entity.Id);

            var useRepository = new Mock<IUserRepository>();
            useRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);


            var useCases = new DeleteCurrentTaskUseCase(taskRepository.Object, useRepository.Object);

            var act = () => useCases.Execute(entity.Id, request);

            act.Should().NotThrow();
        }
    }
}
