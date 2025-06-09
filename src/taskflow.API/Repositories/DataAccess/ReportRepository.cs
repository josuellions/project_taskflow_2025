using Microsoft.EntityFrameworkCore;
using taskflow.API.Communication.Responses;
using taskflow.API.Contracts;
using taskflow.API.Enums;
using taskflow.API.Exceptions;

namespace taskflow.API.Repositories.DataAccess
{
    public class ReportRepository : IReportRepository
    {
        private readonly TaskFlowDbContext _dbContext;

        public ReportRepository(TaskFlowDbContext dbContext) => _dbContext = dbContext;

        public async Task<IList<ResponseReportJson>> GetCurrent(DateTime date)
        {
            var QTDDAY = -30;
            var dateEnd = new DateTime(date.Year, date.Month, date.Day);
            var dateStart = dateEnd.AddDays(QTDDAY);

            var projetos = await _dbContext.Projeto
                        .Where(p => p.DataAt.Date >= dateStart.Date && p.DataAt.Date <= dateEnd.Date)
                        .Include(p => p.Tasks).ToListAsync();

            var users = await _dbContext.Usuario.ToListAsync();

            var result =  projetos
                .Join(_dbContext.Usuario,
                    project => project.UserId,
                    user => user.Id,
                    (project, user) =>
                    {
                        var filteredTasks = project.Tasks
                            .Where(t => t.DateAt.Date >= dateStart.Date && t.DateAt.Date <= dateEnd.Date)
                            .ToList();

                        var completedCount = filteredTasks.Count(t => t.StatusId == Status.CONCLUIDO);
                        var totalCount = filteredTasks.Count;

                        return new ResponseReportJson
                        {
                            Project = project,
                            User = user,
                            Tasks = filteredTasks,
                            DateStart = dateStart,
                            DateEnd = dateEnd,
                            Total = totalCount,
                            Completed = completedCount,
                            Porcentage = totalCount == 0 ? 0 :
                                Math.Round((decimal)completedCount / totalCount * 100, 2)
                        };
                    }).ToList();

            if (result == null)
            {
                throw new NotFoundException("O Dados do relatório não foi encontrado no banco dados!");
            }

            return result;
        }
    }
}
