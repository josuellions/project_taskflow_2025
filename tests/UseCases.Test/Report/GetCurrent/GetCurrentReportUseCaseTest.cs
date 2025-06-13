using Bogus;
using FluentAssertions;
using Moq;
using taskflow.API.Communication.Requests;
using taskflow.API.Communication.Responses;
using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.Enums;
using taskflow.API.Exceptions;
using taskflow.API.UseCases.Report.GetCurrent;
using UseCases.Test.Communication.Requests;
using UseCases.Test.Repositories.DataAccess;

namespace UseCases.Test.Report.GetCurrent
{
    public class GetCurrentReportUseCaseTest : IClassFixture<ProjectRepositoryFake>, IClassFixture<RequestTaskJsonFake>
    {
        private readonly ProjectRepositoryFake _repositoryFake;
        private readonly RequestTaskJsonFake _requestTaskJsonFake;

        public GetCurrentReportUseCaseTest(ProjectRepositoryFake repositoryFake, RequestTaskJsonFake requestTaskJsonFake)
        {
            _repositoryFake = repositoryFake;
            _requestTaskJsonFake = requestTaskJsonFake;
        }

        [Fact]
        public async Task Success()
        {
            //var userEntity = new 
            var request = new Faker<RequestReportJson>()
            .RuleFor(request => request.UserId, f => f.Random.Number(1, 100))
            .RuleFor(request => request.UserTypeId, f => UserType.GERENTE)
            .RuleFor(request => request.DateStart, f => f.Date.Past(1).ToUniversalTime())
            .Generate();


            var useRepository = new Mock<IUserRepository>();
            useRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var repostRepository = new Mock<IReportRepository>();
            repostRepository.Setup(i => i.GetCurrent(request.DateStart)).ReturnsAsync(new List<ResponseReportJson>());

            var useCase = new GetCurrentReportUseCase(repostRepository.Object, useRepository.Object);

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Execute()
        {
            var request = new Faker<RequestReportJson>()
            .RuleFor(request => request.UserId, f => f.Random.Number(1, 100))
            .RuleFor(request => request.UserTypeId, f => UserType.GERENTE)
            .RuleFor(request => request.DateStart, f => f.Date.Past(1).ToUniversalTime())
            .Generate();

            var entityUser = new Faker<User>("pt_BR")
            .RuleFor(entityUser => entityUser.Id, f => f.Random.Number(1, 100))
            .RuleFor(entityUser => entityUser.Name, f => f.Name.FullName())
            .RuleFor(entityUser => entityUser.TypeId, f => UserType.GERENTE)
            .Generate();

            var entityProject = _repositoryFake.CreateProjectEntity(1, 100);

            var total = entityProject.Tasks.Count;
            var completed = entityProject.Tasks.Where(t => t.StatusId == Status.CONCLUIDO).Count();
            var procentage = ((completed / total) * 100);

            var responseReportJson = new ResponseReportJson
            {
                Total = total,
                Completed = completed,
                Porcentage = procentage,
                User = entityUser,
                Project = entityProject,
                Tasks = entityProject.Tasks.ToList(),
                DateStart = request.DateStart.AddDays(-30),
                DateEnd = request.DateStart,
            };

            var useRepository = new Mock<IUserRepository>();
            useRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var repostRepository = new Mock<IReportRepository>();
            repostRepository.Setup(i => i.GetCurrent(request.DateStart)).ReturnsAsync(new List<ResponseReportJson> { responseReportJson });

            var useCase = new GetCurrentReportUseCase(repostRepository.Object, useRepository.Object);

            var result = await useCase.Execute(request);


            var report = result.First();
            var tasl = report.Project;

            result.Should().NotBeNull();
            report.Total.Should().Be(total);
            report.Completed.Should().Be(completed);
            report.Porcentage.Should().Be(procentage);
            report.Project.Should().Be(entityProject);
            report.User.Should().BeEquivalentTo(entityUser);
            report.Tasks.Should().BeEquivalentTo(entityProject.Tasks);
            report.Project.Tasks.Should().BeEquivalentTo(entityProject.Tasks);
        }

        [Fact]
        public async Task ValidatorTypeUser()
        {
            var request = new Faker<RequestReportJson>("pt_BR")
            .RuleFor(request => request.UserId, f => f.Random.Number(1, 100))
            .RuleFor(request => request.UserTypeId, f => UserType.COLABORADOR)
            .RuleFor(request => request.DateStart, f => f.Date.Past(1).ToUniversalTime())
            .Generate();

            var useRepository = new Mock<IUserRepository>();
            useRepository.Setup(i => i.ExistUserWithId(request.UserId)).Returns(true);

            var repostRepository = new Mock<IReportRepository>();
            repostRepository.Setup(i => i.GetCurrent(request.DateStart)).ReturnsAsync(new List<ResponseReportJson>());

            var useCase = new GetCurrentReportUseCase(repostRepository.Object, useRepository.Object);

            var act = async () => await useCase.Execute(request);

            await act.Should().ThrowAsync<NotFoundException>().WithMessage("Tipo de usuário inválido ou sem permissão para acessar o relatório!");
        }
    }
}
