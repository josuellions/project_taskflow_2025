using taskflow.API.Communication.Requests;
using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.Enums;
using taskflow.API.Exceptions;

namespace taskflow.API.UseCases.Tasks.DeleteCurrent
{
    public class DeleteCurrentTaskUseCase
    {
        private readonly ITaskRepository _repository;
        private readonly IUserRepository _repositoryUser;
        private readonly IProjectRepository _repositoryProject;

        public DeleteCurrentTaskUseCase(ITaskRepository repository, IUserRepository userRepository, IProjectRepository repositoryProject)
        {
            _repository = repository;
            _repositoryUser = userRepository;
            _repositoryProject = repositoryProject;
        }

        public void Execute(int id, RequestTaskJson request)
        {
            Validator(id, request);

            _repository.Delete(id);

        }

        private void Validator(int id, RequestTaskJson request)
        {
            if (request == null)
            {
                throw new TaskFlowInException(nameof(request));
            }

            _repositoryUser.ExistUserWithId(request.UserId);
        }
    }
}
