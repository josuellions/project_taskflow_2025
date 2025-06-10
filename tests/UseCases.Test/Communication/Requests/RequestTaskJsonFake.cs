using Bogus;
using taskflow.API.Communication.Requests;
using taskflow.API.Enums;

namespace UseCases.Test.Communication.Requests
{
    public class RequestTaskJsonFake
    {
        public RequestTaskJson CreateRequestTaskJson()
        {
            var request = new Faker<RequestTaskJson>()
           .RuleFor(request => request.Name, f => f.Name.FindName())
           .RuleFor(request => request.PriorityId, f => f.PickRandom<Priority>())
           .RuleFor(request => request.StatusId, f => f.PickRandom<Status>())
           .RuleFor(request => request.UserId, f => f.Random.Number(1, 100))
           .RuleFor(request => request.Description, f => f.Lorem.Word())
           .Generate();

            return request;
        }
    }
}
