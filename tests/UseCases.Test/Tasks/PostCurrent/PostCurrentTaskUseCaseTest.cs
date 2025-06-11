using FluentAssertions;
using Moq;
using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.Exceptions;
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
        public async Task Success(int projectId)
        {
            var request = _requestTaskJsonFake.CreateRequestTaskJson();

            var entity = _repositoryFake.CreateTaskEntity(request, projectId, 1, 100);

            var taskRepository = new Mock<ITaskRepository>();
            taskRepository.Setup(i => i.Create(It.IsAny<Tarefa>())).ReturnsAsync(entity.Id);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.ExistProjectWithId(projectId)).Returns(true);

            //ARRANGE
            var useCase = new PostCurrentTaskUseCase(taskRepository.Object, userRepository.Object, projectRepository.Object);

            //ACT
            var act = async () => await useCase.Execute(projectId, request);

            //ASSERT
            await act.Should().NotThrowAsync();
        }

        [Theory]
        [InlineData(1)]
        public async Task Create(int projectId)
        {
            var request = _requestTaskJsonFake.CreateRequestTaskJson();

            var entity = _repositoryFake.CreateTaskEntity(request, projectId, 1, 100);

            var taskRepository = new Mock<ITaskRepository>();
            taskRepository.Setup(i => i.Create(It.IsAny<Tarefa>())).ReturnsAsync(entity.Id);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.ExistProjectWithId(projectId)).Returns(true);

            var useCase = new PostCurrentTaskUseCase(taskRepository.Object, userRepository.Object, projectRepository.Object);

            var result = await useCase.Execute(projectId, request);

            result.Should().Be(entity.Id);
        }

        [Theory]
        [InlineData(1)]
        public async Task CreateGetProjectTotalTask(int projectId)
        {
            var request = _requestTaskJsonFake.CreateRequestTaskJson();

            var entity = _repositoryFake.CreateTaskEntity(request, projectId, 1, 100);

            var taskRepository = new Mock<ITaskRepository>();
            taskRepository.Setup(i => i.GetTotalTask(projectId)).Returns(20);
            taskRepository.Setup(i => i.Create(It.IsAny<Tarefa>())).ReturnsAsync(entity.Id);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.ExistProjectWithId(projectId)).Returns(true);

            var useCase = new PostCurrentTaskUseCase(taskRepository.Object, userRepository.Object, projectRepository.Object);

            var act = async () => await useCase.Execute(projectId, request);

            await act.Should().ThrowAsync<ConflictException>().WithMessage("Este projeto já atingiu o limite máximo de 20 tarefas permitidas!");
        }

        [Theory]
        [InlineData(1)]
        public async Task IsNullOrWhiteSpace(int projectId)
        {
            var request = _requestTaskJsonFake.CreateRequestTaskJson();
            request.Name = string.Empty;

            var entity = _repositoryFake.CreateTaskEntity(request, projectId, 1, 100);

            var taskRepository = new Mock<ITaskRepository>();
            taskRepository.Setup(i => i.Create(It.IsAny<Tarefa>())).ReturnsAsync(entity.Id);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.ExistProjectWithId(projectId)).Returns(true);

            var useCase = new PostCurrentTaskUseCase(taskRepository.Object, userRepository.Object, projectRepository.Object);

            var act = async () => await useCase.Execute(projectId, request);

            await act.Should().ThrowAsync<ErrorOnValidationException>().WithMessage("Informe um Nome para tarefa!");
        }

        [Theory]
        [InlineData(1)]
        public async Task CreateGetProjectValidation(int projectId)
        {
            var request = _requestTaskJsonFake.CreateRequestTaskJson();

            var entity = _repositoryFake.CreateTaskEntity(request, projectId, 1, 100);

            var taskRepository = new Mock<ITaskRepository>();
            taskRepository.Setup(i => i.Create(It.IsAny<Tarefa>())).ReturnsAsync(entity.Id);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.ExistProjectWithId(projectId)).Returns(false);

            var useCase = new PostCurrentTaskUseCase(taskRepository.Object, userRepository.Object, projectRepository.Object);

            var act = async () => await useCase.Execute(projectId, request);

            await act.Should().ThrowAsync<ErrorOnValidationException>().WithMessage("Informe o ProjetoId valido para tarefa!");
        }

        [Theory]
        [InlineData(1)]
        public async Task IsDefinedStatus(int projectId)
        {
            var request = _requestTaskJsonFake.CreateRequestTaskJson();
            request.StatusId = 0;

            var entity = _repositoryFake.CreateTaskEntity(request, projectId, 1, 100);

            var taskRepository = new Mock<ITaskRepository>();
            taskRepository.Setup(i => i.Create(It.IsAny<Tarefa>())).ReturnsAsync(entity.Id);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.ExistProjectWithId(projectId)).Returns(true);

            var useCase = new PostCurrentTaskUseCase(taskRepository.Object, userRepository.Object, projectRepository.Object);

            var act = async () => await useCase.Execute(projectId, request);

            await act.Should().ThrowAsync<ErrorOnValidationException>().WithMessage("Informe o StatuId valido de um para tarefa!");
        }

        [Theory]
        [InlineData(1)]
        public async Task IsDefinedPriority(int projectId)
        {
            var request = _requestTaskJsonFake.CreateRequestTaskJson();
            request.PriorityId = 0;

            var entity = _repositoryFake.CreateTaskEntity(request, projectId, 1, 100);

            var taskRepository = new Mock<ITaskRepository>();
            taskRepository.Setup(i => i.Create(It.IsAny<Tarefa>())).ReturnsAsync(entity.Id);

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.ExistProjectWithId(projectId)).Returns(true);

            var useCase = new PostCurrentTaskUseCase(taskRepository.Object, userRepository.Object, projectRepository.Object);

            var act = async () => await useCase.Execute(projectId, request);

            await act.Should().ThrowAsync<ErrorOnValidationException>().WithMessage("Informe o PriorityId valido de um para tarefa!");
        }
    }
}
