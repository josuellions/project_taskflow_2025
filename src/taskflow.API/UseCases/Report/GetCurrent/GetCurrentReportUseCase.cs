using taskflow.API.Communication.Requests;
using taskflow.API.Communication.Responses;
using taskflow.API.Contracts;
using taskflow.API.Enums;
using taskflow.API.Exceptions;

namespace taskflow.API.UseCases.Report.GetCurrent
{
    public class GetCurrentReportUseCase
    {
        private readonly IReportRepository _repository;
        private readonly IUserRepository _repositoryUser;

        public GetCurrentReportUseCase(IReportRepository repository, IUserRepository repositoryUser)
        {
            _repository = repository;
            _repositoryUser = repositoryUser;
        }

        public async Task<IList<ResponseReportJson>> Execute(RequestReportJson request)
        {
            Validator(request);

            var result = await _repository.GetCurrent(request.DateStart);

            return result;
        }


        private void Validator(RequestReportJson request)
        {
            if (request == null)
            {
                throw new TaskFlowInException(nameof(request));
            }

            if (UserType.GERENTE != (UserType)request.UserTypeId) 
            {
                throw new NotFoundException("Tipo de usuário inválido ou sem permissão para acessar o relatório!");
            }

            _repositoryUser.ExistUserWithId(request.UserId);
        }
    }
}
