using Bogus;
using taskflow.API.Communication.Requests;
using taskflow.API.Enums;

namespace UseCases.Test.Communication.Requests
{
    public class RequestProjectJsonFake
    {
        public RequestProjectJson CreateRequestProjectJson(int userId)
        {
            var request = new Faker<RequestProjectJson>()
                            .RuleFor(request => request.Name, f => f.Name.FindName())
                            .RuleFor(request => request.StatusId, f => f.PickRandom<Status>())
                            .RuleFor(request => request.UserId, f => userId)
                            .Generate();

            return request;
        }
    }
}
