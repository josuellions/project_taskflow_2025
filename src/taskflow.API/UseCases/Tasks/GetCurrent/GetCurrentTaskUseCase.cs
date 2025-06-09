using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.Exceptions;

namespace taskflow.API.UseCases.Tasks.GetCurrent
{

    public class GetCurrentTaskUseCase
    {
        private readonly ITaskRepository _repository;

        public GetCurrentTaskUseCase(ITaskRepository repository) => _repository = repository;
        
        public Tarefa? Execute(int id)
        {
            Validator(id);

            return _repository.GetCurrentId(id);
        }

        public void Validator(int id)
        {
            if (id <= 0)
            {
                throw new NotFoundException("Tarefa deve ter Id maior que zero!");
            }
        }
    }
}
