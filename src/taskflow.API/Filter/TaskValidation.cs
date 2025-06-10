using taskflow.API.Contracts;
using taskflow.API.Exceptions;

namespace taskflow.API.Filter
{
    public class TaskValidation
    {
        private int LIMITE_MAXIMO_TASKS = 20;
        private readonly ITaskRepository _repository;

        public TaskValidation(ITaskRepository repository) => _repository = repository;

        public bool GetProjectTotalTask(int projectId)
        {
            try
            {
                var totalTasks = _repository.GetTotalTask(projectId);

                var isMaximo = totalTasks == LIMITE_MAXIMO_TASKS;

                return isMaximo;

            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }
    }
}
