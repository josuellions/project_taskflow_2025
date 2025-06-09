using taskflow.API.Contracts;
using taskflow.API.Exceptions;

namespace taskflow.API.Filter
{
    public class UserValidation
    {
        private IUserRepository _repository;

        public  UserValidation(IUserRepository repository) => _repository = repository;

        public void GetUserValidation(int userId) {
            try {
                var exist = _repository.ExistUserWithId(userId);

                if (exist == false)
                {
                    throw new TaskFlowInException("Usuário invalido!");
                }
            }
            catch(NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }
    }
}
