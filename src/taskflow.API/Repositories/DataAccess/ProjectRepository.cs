using Microsoft.EntityFrameworkCore;
using taskflow.API.Contracts;
using taskflow.API.Entities;
using taskflow.API.Exceptions;

namespace taskflow.API.Repositories.DataAccess
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TaskFlowDbContext _dbContext;

        public ProjectRepository(TaskFlowDbContext dbContext) => _dbContext = dbContext;

        public int Create(Project project)
        {
            _dbContext.Projeto.Add(project);
            _dbContext.SaveChanges();

            return project.Id;
        }

        public List<Project>? GetCurrent()
        {
            return _dbContext.Projeto
                .Include(project => project.Tasks)
                .ToList();
        }

        public Project? GetCurrentId(int id)
        {
            var result = _dbContext.Projeto
                .Include(project => project.Tasks)
                .FirstOrDefault(project => project.Id.Equals(id));

            if (result == null)
            {
                throw new NotFoundException("O Projeto não foi encontrado no banco dados!");
            }

            return result;
        }

        public Project? Update(Project project)
        {
            var result = _dbContext.Projeto.Find(project.Id);

            if (result == null)
            {
                throw new NotFoundException("O Projeto não foi encontrado no banco dados!");
            }

            result.Name = project.Name;
            result.StatusId = project.StatusId;
            result.DataUp = project.DataUp;

            _dbContext.SaveChanges();

            return result;
        }

        public void Delete(int id)
        {
            var result = _dbContext.Projeto
               .Include(project => project.Tasks)
               .FirstOrDefault(project => project.Id.Equals(id));

            if (result == null)
            {
                throw new NotFoundException("O Projeto não foi encontrado no banco dados!");
            }

            _dbContext.Tarefas.RemoveRange(result.Tasks);
            _dbContext.Projeto.Remove(result);

            _dbContext.SaveChanges();
        }

        public bool ExistPendingTasksInProject(int projectId)
        {
            return _dbContext.Projeto
                    .Where(p => p.Id == projectId)
                    .Any(p => p.Tasks.Any(t => t.StatusId != Enums.Status.CONCLUIDO));
        }

        public bool ExistProjectWithId(int id)
        {
            return _dbContext.Projeto.Any(p => p.Id == id);
        }
    }
}
