using taskflow.API.Contracts;
using taskflow.API.Exceptions;

namespace taskflow.API.Repositories.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskFlowDbContext _dbContext;

        public UserRepository(TaskFlowDbContext dbContext) => _dbContext = dbContext;

        public bool ExistUserWithId(int userId)
        {
            var result = _dbContext.Usuario.Any(user => user.Id.Equals(userId));

            if(result == false)
            {
                throw new NotFoundException("Usuário inválido ou não cadastrado!");
            }

            return result;
        }
    }
}
