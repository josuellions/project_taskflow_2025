using FluentAssertions;
using Moq;
using taskflow.API.Contracts;
using taskflow.API.UseCases.Projects.GetCurrent;
using UseCases.Test.Communication.Requests;
using UseCases.Test.Repositories.DataAccess;

namespace UseCases.Test.Projects.GetCurrent
{
    public class GetCurrentProjectUseCaseTest : IClassFixture<ProjectRepositoryFake>, IClassFixture<RequestProjectJsonFake>
    {
        private readonly ProjectRepositoryFake _repositoryFake;
        private readonly RequestProjectJsonFake _requestProjectJson;


        public GetCurrentProjectUseCaseTest(ProjectRepositoryFake repository, RequestProjectJsonFake requestProjectJson)
        {
            _repositoryFake = repository;
            _requestProjectJson = requestProjectJson;
        }

        [Fact]
        public void Success()
        {
            var entity = _repositoryFake.CreateProjectEntity(1, 100);

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
            project.Name.Should().NotBeNullOrWhiteSpace();
            project.Tasks.Should().NotBeNull();
        }

        [Theory]
        [InlineData(-2)]
        public void Exception(int startId)
        {
            var entity = _repositoryFake.CreateProjectEntity(startId, 0); ;

            var mock = new Mock<IProjectRepository>();
            mock.Setup(i => i.GetCurrentId(entity.Id)).Returns(entity);

            //ARRANGE
            var useCase = new GetCurrentProjectUseCase(mock.Object);

            //ACT
            var act = () => useCase.GetCurrentId(entity.Id);

            //ASSERT
            act.Should().Throw<NotImplementedException>().WithMessage("Projeto deve ter Id maior que zero!");
        }
    }
}
