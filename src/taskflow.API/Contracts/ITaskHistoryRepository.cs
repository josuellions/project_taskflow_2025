using Microsoft.EntityFrameworkCore;
using taskflow.API.Entities;

namespace taskflow.API.Contracts
{
    public interface ITaskHistoryRepository
    {
        Task SaveChangesAsync(int userId, Tarefa task, EntityState action);
    }
}
