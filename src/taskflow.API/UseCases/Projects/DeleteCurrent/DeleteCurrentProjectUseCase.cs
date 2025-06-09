using taskflow.API.Communication.Requests;
using taskflow.API.Contracts;
using taskflow.API.Exceptions;

namespace taskflow.API.UseCases.Projects.DeleteCurrent
{
    public class DeleteCurrentProjectUseCase
    {
        private readonly IProjectRepository _repository;
        private readonly IUserRepository _repositoryUser;

        public DeleteCurrentProjectUseCase(IProjectRepository repository, IUserRepository repositoryUser)
        {
            _repository = repository;
            _repositoryUser = repositoryUser;
        }

        public void Execute(int id, RequestProjectJson request)
        {
            Validator(id, request);

            _repository.Delete(id);
        }

        private void Validator(int id, RequestProjectJson request)
        {
            if (request == null)
            {
                throw new TaskFlowInException(nameof(request));
            }

            if (_repository.ExistProjectWithId(id) == false)
            {
                throw new ErrorOnValidationException("Informe um projeto válido!");
            }

            if (_repository.ExistPendingTasksInProject(id))
            {
                throw new ErrorOnValidationException("Projeto contém tarefas pendentes!");
            }

            _repositoryUser.ExistUserWithId(request.UserId);
        }
    }
}
