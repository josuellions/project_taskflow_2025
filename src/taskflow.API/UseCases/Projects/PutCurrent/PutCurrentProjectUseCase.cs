using taskflow.API.Communication.Requests;
using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.Enums;
using taskflow.API.Exceptions;

namespace taskflow.API.UseCases.Projects.PutCurrent
{
    public class PutCurrentProjectUseCase
    {
        private readonly IProjectRepository _repository;
        private readonly IUserRepository _repositoryUser;

        public PutCurrentProjectUseCase(IProjectRepository repository, IUserRepository repositoryUser) 
        {
            _repository = repository;
            _repositoryUser = repositoryUser;
        }

        public Project Execute(int id, RequestProjectJson request)
        {
            Validator(id, request);

            var project = new Project
            {
                Id = id,
                Name = request.Name,
                StatusId = request.StatusId,
                UserId = request.UserId,
                DataUp = DateTime.Now,
            };

            _repository.Update(project);

            return project;
        }


        private void Validator(int id, RequestProjectJson request)
        {
            if (request == null)
            {
                throw new TaskFlowInException(nameof(request));
            }

            if(_repository.ExistProjectWithId(id) == false)
            {
                throw new ErrorOnValidationException("Informe um projeto válido!");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ErrorOnValidationException("Informe um Nome para projeto!");
            }

            if (Enum.IsDefined(typeof(Status), request.StatusId) == false)
            {
                throw new ErrorOnValidationException("Informe o StatuId válido de um para projeto!");
            }

            _repositoryUser.ExistUserWithId(request.UserId);
        }

    }
}
