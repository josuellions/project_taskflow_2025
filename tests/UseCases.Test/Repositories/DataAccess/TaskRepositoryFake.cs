using Bogus;
using taskflow.API.Communication.Requests;
using taskflow.API.Entities;
using taskflow.API.Enums;

namespace UseCases.Test.Repositories.DataAccess
{
    public class TaskRepositoryFake
    {
        public Tarefa CreateTaskEntity(RequestTaskJson request, int projectId, int startId = 1, int endId = 100)
        {
            var entity = new Faker<Tarefa>("pt_BR")
           .RuleFor(entity => entity.Id, f => f.Random.Number(startId, endId))
           .RuleFor(entity => entity.Name, request.Name)
           .RuleFor(entity => entity.ProjectId, projectId)
           .RuleFor(entity => entity.PriorityId, (Priority)request.PriorityId)
           .RuleFor(entity => entity.StatusId, f => (Status)request.StatusId)
           .RuleFor(entity => entity.UserId, f => request.UserId)
           .RuleFor(entity => entity.Description, f => f.Lorem.Sentences(50))
           .Generate();

            return entity;
        }
    }
}
