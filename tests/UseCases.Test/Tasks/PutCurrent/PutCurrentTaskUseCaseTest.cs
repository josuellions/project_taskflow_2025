using FluentAssertions;
using Moq;
using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.Enums;
using taskflow.API.Exceptions;
using taskflow.API.UseCases.Tasks.PostCurrent;
using taskflow.API.UseCases.Tasks.PutCurrent;
using UseCases.Test.Communication.Requests;
using UseCases.Test.Repositories.DataAccess;

namespace UseCases.Test.Tasks.PutCurrent
{
    public class PutCurrentTaskUseCaseTest : IClassFixture<TaskRepositoryFake>, IClassFixture<RequestTaskJsonFake>
    {
        private readonly TaskRepositoryFake _repositoryFake;
        private readonly RequestTaskJsonFake _requestTaskJsonFake;

        public PutCurrentTaskUseCaseTest(TaskRepositoryFake repositoryFake, RequestTaskJsonFake requestTaskJsonFake)
        {
            _repositoryFake = repositoryFake;
            _requestTaskJsonFake = requestTaskJsonFake;
        }

        [Theory]
        [InlineData(1)]
        public async Task Success(int projectId)
        {
            var request = _requestTaskJsonFake.CreateRequestTaskJson();
            var requestCreate = _requestTaskJsonFake.CreateRequestTaskJson();

            var entity = _repositoryFake.CreateTaskEntity(requestCreate, projectId, 1, 100);
            entity.PriorityId = request.PriorityId;

            var taskRepository = new Mock<ITaskRepository>();
            taskRepository.Setup(i => i.GetCurrentId(entity.Id)).Returns(entity);
            taskRepository.Setup(i => i.Update(It.IsAny<Tarefa>())).ReturnsAsync((Tarefa t) => t);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.ExistProjectWithId(projectId)).Returns(true);

            var useCase = new PutCurrentTaskUseCase(taskRepository.Object, userRepository.Object, projectRepository.Object);

            var result = await useCase.Execute(entity.Id, request);

            result.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
            result.UserId.Should().Be(request.UserId);
            result.StatusId.Should().Be(request.StatusId);
            result.Description.Should().Be(request.Description);
        }

        [Theory]
        [InlineData(1)]
        public async Task IsNullOrWhiteSpace(int projectId)
        {
            var request = _requestTaskJsonFake.CreateRequestTaskJson();
            request.Name = string.Empty;

            var requestCreate = _requestTaskJsonFake.CreateRequestTaskJson();

            var entity = _repositoryFake.CreateTaskEntity(requestCreate, projectId, 1, 100);
            entity.PriorityId = request.PriorityId;

            var taskRepository = new Mock<ITaskRepository>();
            taskRepository.Setup(i => i.GetCurrentId(entity.Id)).Returns(entity);
            taskRepository.Setup(i => i.Update(It.IsAny<Tarefa>())).ReturnsAsync((Tarefa t) => t);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.ExistProjectWithId(projectId)).Returns(true);

            var useCase = new PutCurrentTaskUseCase(taskRepository.Object, userRepository.Object, projectRepository.Object);

            var act = async () => await useCase.Execute(entity.Id, request);

            await act.Should().ThrowAsync<ErrorOnValidationException>().WithMessage("Informe um Nome para tarefa!");
        }

        [Theory]
        [InlineData(1)]
        public async Task IsDefinedStatus(int projectId)
        {
            var request = _requestTaskJsonFake.CreateRequestTaskJson();
            request.StatusId = 0;

            var requestCreate = _requestTaskJsonFake.CreateRequestTaskJson();

            var entity = _repositoryFake.CreateTaskEntity(requestCreate, projectId, 1, 100);
            entity.PriorityId = request.PriorityId;

            var taskRepository = new Mock<ITaskRepository>();
            taskRepository.Setup(i => i.GetCurrentId(entity.Id)).Returns(entity);
            taskRepository.Setup(i => i.Update(It.IsAny<Tarefa>())).ReturnsAsync((Tarefa t) => t);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.ExistProjectWithId(projectId)).Returns(true);

            var useCase = new PutCurrentTaskUseCase(taskRepository.Object, userRepository.Object, projectRepository.Object);

            var act = async () => await useCase.Execute(entity.Id, request);

            await act.Should().ThrowAsync<ErrorOnValidationException>().WithMessage("Informe o StatuId valido de um para tarefa!");
        }

        [Theory]
        [InlineData(1)]
        public async Task IsDefinedPriority(int projectId)
        {
            var request = _requestTaskJsonFake.CreateRequestTaskJson();

            var requestCreate = _requestTaskJsonFake.CreateRequestTaskJson();

            var entity = _repositoryFake.CreateTaskEntity(requestCreate, projectId, 1, 100);
            //entity.PriorityId = request.PriorityId;
            request.PriorityId = 0;

            var taskRepository = new Mock<ITaskRepository>();
            taskRepository.Setup(i => i.GetCurrentId(entity.Id)).Returns(entity);
            taskRepository.Setup(i => i.Update(It.IsAny<Tarefa>())).ReturnsAsync((Tarefa t) => t);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.ExistProjectWithId(projectId)).Returns(true);

            var useCase = new PutCurrentTaskUseCase(taskRepository.Object, userRepository.Object, projectRepository.Object);

            var act = async () => await useCase.Execute(entity.Id, request);

            await act.Should().ThrowAsync<ErrorOnValidationException>().WithMessage("A Tarefa não permite aterar PriorityId!");
        }
    }
}
