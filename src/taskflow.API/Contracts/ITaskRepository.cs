using taskflow.API.Entities;

namespace taskflow.API.Contracts
{
    public interface ITaskRepository
    {
        Task<int> Create(Tarefa task);
        int GetTotalTask(int projectId);
        Tarefa? GetCurrentId(int id);
        Task<Tarefa?> Update(Tarefa task);
        Task Delete(int id);
    }
}
