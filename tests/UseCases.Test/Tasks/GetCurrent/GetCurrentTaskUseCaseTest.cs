using FluentAssertions;
using Moq;
using taskflow.API.Contracts;
using taskflow.API.Exceptions;
using taskflow.API.UseCases.Tasks.GetCurrent;
using UseCases.Test.Communication.Requests;
using UseCases.Test.Repositories.DataAccess;

namespace UseCases.Test.Tasks.GetCurrent
{
    public class GetCurrentTaskUseCaseTest : IClassFixture<TaskRepositoryFake>, IClassFixture<RequestTaskJsonFake>
    {
        private readonly TaskRepositoryFake _repositoryFake;
        private readonly RequestTaskJsonFake _requestTaskJsonFake;

        public GetCurrentTaskUseCaseTest(TaskRepositoryFake repositoryFake, RequestTaskJsonFake requestTaskJsonFake)
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
            taskRepository.Setup(i => i.GetCurrentId(entity.Id)).Returns(entity);

            var useCase = new GetCurrentTaskUseCase(taskRepository.Object);

            var result = useCase.Execute(entity.Id);

            result.Should().NotBeNull();
            result.Id.Should().Be(entity.Id);
            result.Name.Should().NotBeNullOrWhiteSpace();
            result.DateAt.Should().Be(entity.DateAt);
            result.DateUp.Should().Be(entity.DateUp);
        }

        [Theory]
        [InlineData(1)]
        public void ValidatorId(int projectId)
        {
            var request = _requestTaskJsonFake.CreateRequestTaskJson();
            var entity = _repositoryFake.CreateTaskEntity(request, projectId, -10, 0);

            var taskRepository = new Mock<ITaskRepository>();
            taskRepository.Setup(i => i.GetCurrentId(entity.Id)).Returns(entity);

            var useCase = new GetCurrentTaskUseCase(taskRepository.Object);

            var act = () => useCase.Execute(entity.Id);

            act.Should().Throw<NotFoundException>().WithMessage("Tarefa deve ter Id valido e maior que zero!");
        }
    }
}
