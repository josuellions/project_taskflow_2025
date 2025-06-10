using taskflow.API.Communication.Requests;
using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.Enums;
using taskflow.API.Exceptions;

namespace taskflow.API.UseCases.Projects.PostCurrent
{
    public class PostCurrentProjectUseCase
    {
        private readonly IProjectRepository _repository;
        private readonly IUserRepository _repositoryUser;

        public PostCurrentProjectUseCase(IProjectRepository repository, IUserRepository repositoryUser)
        {
            _repository = repository;
            _repositoryUser = repositoryUser;
        }

        public int Execute(RequestProjectJson request)
        {
            Validator(request);

            var project = new Project
            {
                Name = request.Name,
                StatusId = (Status)request.StatusId,
                UserId = request.UserId,
                DataAt = DateTime.UtcNow,
                DataUp = DateTime.UtcNow,
            };

            _repository.Create(project);

            return project.Id;
        }

        private void Validator(RequestProjectJson request)
        {
            if (request == null)
            {
                throw new TaskFlowInException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ErrorOnValidationException("Informe um Nome para projeto!");
            }

            if (Enum.IsDefined(typeof(Status), request.StatusId) == false)
            {
                throw new ErrorOnValidationException("Informe o StatuId valido de um para projeto!");
            }

            _repositoryUser.ExistUserWithId(request.UserId);
        }
    }
}
