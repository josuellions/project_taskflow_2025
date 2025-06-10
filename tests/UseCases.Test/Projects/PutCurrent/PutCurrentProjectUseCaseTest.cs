using FluentAssertions;
using Moq;
using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.Exceptions;
using taskflow.API.UseCases.Projects.PutCurrent;
using UseCases.Test.Communication.Requests;
using UseCases.Test.Repositories.DataAccess;

namespace UseCases.Test.Projects.PutCurrent
{
    public class PutCurrentProjectUseCaseTest : IClassFixture<ProjectRepositoryFake>, IClassFixture<RequestProjectJsonFake>
    {
        private readonly ProjectRepositoryFake _repositoryFake;
        private readonly RequestProjectJsonFake _requestProjectJsonFake;

        public PutCurrentProjectUseCaseTest(ProjectRepositoryFake repositoryFake, RequestProjectJsonFake requestProjectJsonFake)
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
            request.Name = "Udate name project";

            var entity = _repositoryFake.CreateProjectEntity(1, 100);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.ExistProjectWithId(entity.Id)).Returns(true);
            projectRepository.Setup(i => i.Update(It.IsAny<Project>())).Returns((Project p) => p);

            var useCase = new PutCurrentProjectUseCase(projectRepository.Object, useRepository.Object);

            var result = useCase.Execute(entity.Id, request);

            result.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
            result.UserId.Should().Be(userId);
        }

        [Theory]
        [InlineData(1)]
        public void ExistProjectWithId(int userId)
        {
            //Create User
            var useRepository = new Mock<IUserRepository>();
            useRepository.Setup(i => i.ExistUserWithId(userId)).Returns(true);

            //Create Project
            var request = _requestProjectJsonFake.CreateRequestProjectJson(userId);
            request.Name = "Udate name project";

            var entity = _repositoryFake.CreateProjectEntity(1, 100);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.ExistProjectWithId(entity.Id)).Returns(true);
            projectRepository.Setup(i => i.Update(It.IsAny<Project>())).Returns((Project p) => p);

            var useCase = new PutCurrentProjectUseCase(projectRepository.Object, useRepository.Object);

            var result = useCase.Execute(entity.Id, request);

            result.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
            result.UserId.Should().Be(userId);
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
            request.Name = "";

            var entity = _repositoryFake.CreateProjectEntity(1, 100);

            var projectRepository = new Mock<IProjectRepository>();
            projectRepository.Setup(i => i.ExistProjectWithId(entity.Id)).Returns(true);
            projectRepository.Setup(i => i.Update(It.IsAny<Project>())).Returns((Project p) => p);

            var useCase = new PutCurrentProjectUseCase(projectRepository.Object, useRepository.Object);

            var act = () => useCase.Execute(entity.Id, request);

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
            projectRepository.Setup(i => i.ExistProjectWithId(entity.Id)).Returns(true);
            projectRepository.Setup(i => i.Update(It.IsAny<Project>())).Returns((Project p) => p);

            var useCase = new PutCurrentProjectUseCase(projectRepository.Object, useRepository.Object);

            var act = () => useCase.Execute(entity.Id, request);

            act.Should().Throw<ErrorOnValidationException>().WithMessage("Informe o StatuId valido para projeto!");
        }
    }
}
