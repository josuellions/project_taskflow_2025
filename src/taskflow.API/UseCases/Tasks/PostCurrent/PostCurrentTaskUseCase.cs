using taskflow.API.Communication.Requests;
using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.Enums;
using taskflow.API.Exceptions;
using taskflow.API.Filter;

namespace taskflow.API.UseCases.Tasks.PostCurrent
{
    public class PostCurrentTaskUseCase
    {
        private readonly ITaskRepository _repository;
        private readonly IUserRepository _repositoryUser;
        private readonly IProjectRepository _repositoryProject;

        public PostCurrentTaskUseCase(ITaskRepository repository, IUserRepository userRepository, IProjectRepository repositoryProject)
        {
            _repository = repository;
            _repositoryUser = userRepository;
            _repositoryProject = repositoryProject;
        }

        public async Task<int> Execute(int projetcId, RequestTaskJson request)
        {
            Validator(projetcId, request);

            var task = new Tarefa
            {
                Name = request.Name,
                ProjectId = projetcId,
                StatusId = Status.PENDENTE,
                PriorityId = (Priority)request.PriorityId,
                UserId = request.UserId,
                DateAt = DateTime.UtcNow,
                DateUp = DateTime.UtcNow,
                Description = request.Description,
            };

           var result = await _repository.Create(task);

            return result;
        }

        private void Validator(int projetoId, RequestTaskJson request)
        {
            if (request == null)
            {
                throw new TaskFlowInException(nameof(request));
            }

            var TaskValidation = new TaskValidation(_repository);

            if (TaskValidation.GetProjectTotalTask(projetoId))
            {
                throw new ConflictException("Este projeto já atingiu o limite máximo de 20 tarefas permitidas!");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ErrorOnValidationException("Informe um Nome para tarefa!");
            }

            var ProjectValidation = new ProjectValidation(_repositoryProject);

            if (ProjectValidation.GetProjectValidation(projetoId) == false)
            {
                throw new ErrorOnValidationException("Informe o ProjetoId valido para tarefa!");
            }

            if (Enum.IsDefined(typeof(Status), request.StatusId) == false)
            {
                throw new ErrorOnValidationException("Informe o StatuId valido de um para tarefa!");
            }

            if (Enum.IsDefined(typeof(Priority), request.PriorityId) == false)
            {
                throw new ErrorOnValidationException("Informe o PriorityId valido de um para tarefa!");
            }

            _repositoryUser.ExistUserWithId(request.UserId);
        }
    }
}
