using FluentAssertions;
using Moq;
using taskflow.API.Contracts;
using taskflow.API.Enums;
using taskflow.API.Exceptions;
using taskflow.API.UseCases.Projects.DeleteCurrent;
using UseCases.Test.Communication.Requests;
using UseCases.Test.Repositories.DataAccess;
using Xunit;

namespace UseCases.Test.Projects.DeleteCurrent
{
    public class DeleteCurrentProjectUseCaseTest : IClassFixture<ProjectRepositoryFake>, IClassFixture<RequestProjectJsonFake>
    {
        private readonly ProjectRepositoryFake _repositoryFake;
        private readonly RequestProjectJsonFake _requestProjectJson;


        public DeleteCurrentProjectUseCaseTest(ProjectRepositoryFake repository, RequestProjectJsonFake requestProjectJson)
        {
            _repositoryFake = repository;
            _requestProjectJson = requestProjectJson;
        }

        [Fact]
        public void Success()
        {
            var entity = _repositoryFake.CreateProjectEntity(1, 100);

            var request = _requestProjectJson.CreateRequestProjectJson(entity.UserId);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.Create(entity)).Returns(entity.Id);
            projectRepository.Setup(i => i.ExistProjectWithId(entity.Id)).Returns(true);

            var useRepository = new Mock<IUserRepository>();
            useRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var useCase = new DeleteCurrentProjectUseCase(projectRepository.Object, useRepository.Object);

            var act = () => useCase.Execute(entity.Id, request);

            act.Should().NotThrow();
        }

        [Theory]
        [InlineData(1)]
        public void ValidatorExistProjectWithId(int projectId)
        {
            var entity = _repositoryFake.CreateProjectEntity(2, 10);

            var request = _requestProjectJson.CreateRequestProjectJson(entity.UserId);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.Create(entity)).Returns(entity.Id);
            projectRepository.Setup(i => i.ExistProjectWithId(entity.Id)).Returns(true);

            var useRepository = new Mock<IUserRepository>();
            useRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var useCase = new DeleteCurrentProjectUseCase(projectRepository.Object, useRepository.Object);

            var act = () => useCase.Execute(projectId, request);

            act.Should().Throw<ErrorOnValidationException>().WithMessage("Informe um projeto válido!");
        }


        [Fact]
        public void ExistPendingTasksInProject()
        {
            var entity = _repositoryFake.CreateProjectEntity(1, 10);
            var request = _requestProjectJson.CreateRequestProjectJson(entity.UserId);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.Create(entity)).Returns(entity.Id);
            projectRepository.Setup(i => i.ExistProjectWithId(entity.Id)).Returns(true);
            projectRepository.Setup(i => i.ExistPendingTasksInProject(entity.Id)).Returns(true);

            var useRepository = new Mock<IUserRepository>();
            useRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var useCase = new DeleteCurrentProjectUseCase(projectRepository.Object, useRepository.Object);

            var act = () => useCase.Execute(entity.Id, request);

            act.Should().Throw<ErrorOnValidationException>().WithMessage("Projeto contém tarefas pendentes!");
        }
    }
}
