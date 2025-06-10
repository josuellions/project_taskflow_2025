using Microsoft.EntityFrameworkCore;
using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.Enums;

namespace taskflow.API.Repositories.DataAccess
{
    public class TaskHistoryRepository : ITaskHistoryRepository
    {
        private readonly TaskFlowDbContext _dbContext;

        public TaskHistoryRepository(TaskFlowDbContext dbContext) => _dbContext = dbContext;


        public async Task SaveChangesAsync(int userId, Tarefa task, EntityState action)
        {
            var actionInfo = action switch
            {
                EntityState.Added => (ActionId: Actions.CRIADO, Description: $"Tarefa criada com nome {task.Name}"),
                EntityState.Modified => (ActionId: Actions.ALTERADO, Description: $"Tarefa modificada com nome {task.Name}"),
                EntityState.Deleted => (ActionId: Actions.EXCLUIDO, Description: $"Tarefa excluída com nome {task.Name}"),
                _ => throw new NotSupportedException($"Estado não suportado: {action}")
            };

            var history = new TaskHistory
            {
                TaskId = task.Id,
                UserId = userId,
                StatusId = task.StatusId,
                ActionId = actionInfo.ActionId,
                DateAt = DateTime.UtcNow,
                TaskDescription = task.Description,
                Description = actionInfo.Description,
            };

            _dbContext.TarefasHistoricos.Add(history);
            await _dbContext.SaveChangesAsync();
        }
    }
}
