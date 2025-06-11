using taskflow.API.Communication.Requests;
using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.Enums;
using taskflow.API.Exceptions;

namespace taskflow.API.UseCases.Tasks.PutCurrent
{
    public class PutCurrentTaskUseCase
    {
        private readonly ITaskRepository _repository;
        private readonly IUserRepository _repositoryUser;
        private readonly IProjectRepository _repositoryProject;

        public PutCurrentTaskUseCase(ITaskRepository repository, IUserRepository userRepository, IProjectRepository repositoryProject)
        {
            _repository = repository;
            _repositoryUser = userRepository;
            _repositoryProject = repositoryProject;
        }

        public async Task<Tarefa?> Execute(int id, RequestTaskJson request) 
        {
            Validator(id, request);

            var task = new Tarefa
            {
                Id = id,
                Name = request.Name,
                UserId = request.UserId,
                DateUp = DateTime.UtcNow,
                StatusId = (Status)request.StatusId,
                Description = request.Description,
            };

            var result = await _repository.Update(task);

            return result;
        }

        private void Validator(int id, RequestTaskJson request)
        {
            if (request == null)
            {
                throw new TaskFlowInException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ErrorOnValidationException("Informe um Nome para tarefa!");
            }

            if (Enum.IsDefined(typeof(Status), request.StatusId) == false)
            {
                throw new ErrorOnValidationException("Informe o StatuId valido de um para tarefa!");
            }

            var task = _repository.GetCurrentId(id);

            if (task!.PriorityId != request.PriorityId)
            {
                throw new ErrorOnValidationException("A Tarefa não permite aterar PriorityId!");
            }


            _repositoryUser.ExistUserWithId(request.UserId);
        }
    }
}
