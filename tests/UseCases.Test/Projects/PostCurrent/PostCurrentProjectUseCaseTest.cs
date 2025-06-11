using FluentAssertions;
using Moq;
using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.Exceptions;
using taskflow.API.UseCases.Projects.PostCurrent;
using UseCases.Test.Communication.Requests;
using UseCases.Test.Repositories.DataAccess;

namespace UseCases.Test.Projects.PostCurrent
{
    public class PostCurrentProjectUseCaseTest : IClassFixture<ProjectRepositoryFake>, IClassFixture<RequestProjectJsonFake>
    {
        private readonly ProjectRepositoryFake _repositoryFake;
        private readonly RequestProjectJsonFake _requestProjectJsonFake;

        public PostCurrentProjectUseCaseTest(ProjectRepositoryFake repositoryFake, RequestProjectJsonFake requestProjectJsonFake)
        {
            _repositoryFake = repositoryFake;
            _requestProjectJsonFake = requestProjectJsonFake;
        }

        [Theory]
        [InlineData(1)]
        public void Success(int userId)
        {
            //Create User
            var useRepository = new Mock<IUserRepository>();
            useRepository.Setup(i => i.ExistUserWithId(userId)).Returns(true);

            //Create Project
            var request = _requestProjectJsonFake.CreateRequestProjectJson(userId);

            var entity = _repositoryFake.CreateProjectEntity(1, 100);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.Create(entity)).Returns(entity.Id);

            var useCase = new PostCurrentProjectUseCase(projectRepository.Object, useRepository.Object);

            var act = () => useCase.Execute(request);

            act.Should().NotThrow();
        }

        [Theory]
        [InlineData(1)]
        public void Create(int userId)
        {
            //Create User
            var useRepository = new Mock<IUserRepository>();
            useRepository.Setup(i => i.ExistUserWithId(userId)).Returns(true);

            //Create Project
            var request = _requestProjectJsonFake.CreateRequestProjectJson(userId);

            var entity = _repositoryFake.CreateProjectEntity(1, 100);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.Create(It.IsAny<Project>())).Returns(entity.Id);

            var useCase = new PostCurrentProjectUseCase(projectRepository.Object, useRepository.Object);

            var result = useCase.Execute(request);

            result.Should().Be(entity.Id);
        }

        [Theory]
        [InlineData(1)]
        public void IsNullOrWhiteSpace(int userId)
        {
            //Create User
            var useRepository = new Mock<IUserRepository>();
            useRepository.Setup(i => i.ExistUserWithId(userId)).Returns(true);

            //Create Project
            var request = _requestProjectJsonFake.CreateRequestProjectJson(userId);
            request.Name = string.Empty;

            var entity = _repositoryFake.CreateProjectEntity(1, 100);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.Create(It.IsAny<Project>())).Returns(entity.Id);

            var useCase = new PostCurrentProjectUseCase(projectRepository.Object, useRepository.Object);

            var act = () => useCase.Execute(request);

            act.Should().Throw<ErrorOnValidationException>().WithMessage("Informe um Nome para projeto!");
        }

        [Theory]
        [InlineData(1)]
        public void IsDefinedStatus(int userId)
        {
            //Create User
            var useRepository = new Mock<IUserRepository>();
            useRepository.Setup(i => i.ExistUserWithId(userId)).Returns(true);

            //Create Project
            var request = _requestProjectJsonFake.CreateRequestProjectJson(userId);
            request.StatusId = 0;

            var entity = _repositoryFake.CreateProjectEntity(1, 100);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.Create(It.IsAny<Project>())).Returns(entity.Id);

            var useCase = new PostCurrentProjectUseCase(projectRepository.Object, useRepository.Object);

            var act = () => useCase.Execute(request);

            act.Should().Throw<ErrorOnValidationException>().WithMessage("Informe o StatuId valido para projeto!");
        }
    }
}
