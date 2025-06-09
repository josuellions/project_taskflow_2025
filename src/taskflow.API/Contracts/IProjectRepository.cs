using taskflow.API.Entities;

namespace taskflow.API.Contracts
{
    public interface IProjectRepository
    {
        int Create(Project project);
        List<Project>? GetCurrent();
        Project? GetCurrentId(int id);
        Project? Update(Project project);
        void Delete(int id);
        bool ExistPendingTasksInProject(int id);
        bool ExistProjectWithId(int id);
    }
}
