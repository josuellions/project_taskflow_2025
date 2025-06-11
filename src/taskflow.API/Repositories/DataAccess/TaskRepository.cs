using Microsoft.EntityFrameworkCore;
using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.Exceptions;

namespace taskflow.API.Repositories.DataAccess
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskFlowDbContext _dbContext;
        private readonly ITaskHistoryRepository _repositoryHistory;

        public TaskRepository(TaskFlowDbContext dbContext, ITaskHistoryRepository repositoryHistory)
        {
            _dbContext = dbContext;
            _repositoryHistory = repositoryHistory;
        }

        public async Task<int> Create(Tarefa task)
        {
            await _dbContext.Tarefas.AddAsync(task);
            await _dbContext.SaveChangesAsync();

            await _repositoryHistory.SaveChangesAsync(task.UserId, task, EntityState.Added);

            return task.Id;
        }

        public int GetTotalTask(int projectId)
        {
            var totalTasks = _dbContext.Tarefas
                 .Where(t => t.ProjectId.Equals(projectId))
                 .Count();

            return totalTasks;
        }

        public Tarefa? GetCurrentId(int id)
        {
            var result = _dbContext.Tarefas.FirstOrDefault(t => t.Id.Equals((int)id));

            if (result is null)
            {
                throw new NotFoundException("A Tarefa não foi encontrada no banco dados!");
            }

            return result;
        }

        public async Task<Tarefa?> Update(Tarefa task)
        {
            var result = await _dbContext.Tarefas.FindAsync(task.Id);

            if (result == null)
            {
                throw new NotFoundException("A Tarefa não foi encontrado no banco dados!");
            }

            await _dbContext.SaveChangesAsync();

            await _repositoryHistory.SaveChangesAsync(task.UserId, task, EntityState.Modified);

            return result;
        }

        public async Task Delete(int id)
        {
            var result = await _dbContext.Tarefas
               .FirstOrDefaultAsync(t => t.Id.Equals(id));

            if (result == null)
            {
                throw new NotFoundException("O Projeto não foi encontrado no banco dados!");
            }

            _dbContext.Tarefas.Remove(result);
            await _dbContext.SaveChangesAsync();
            
            await _repositoryHistory.SaveChangesAsync(result.UserId, result, EntityState.Deleted);
        }
    }
}
