using taskflow.API.Communication.Responses;

namespace taskflow.API.Contracts
{
    public interface IReportRepository
    {
        Task<IList<ResponseReportJson>> GetCurrent(DateTime dateStart);
    }
}
