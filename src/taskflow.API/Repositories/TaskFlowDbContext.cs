using Microsoft.EntityFrameworkCore;
using taskflow.API.Entities;

namespace taskflow.API.Repositories
{
    public class TaskFlowDbContext : DbContext
    {
        public TaskFlowDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TaskHistory> TarefasHistoricos { get; set; }
        public DbSet<Project> Projeto { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<User> Usuario { get; set; }
    }
}
