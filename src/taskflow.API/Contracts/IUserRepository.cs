namespace taskflow.API.Contracts
{
    public interface IUserRepository
    {
        bool ExistUserWithId(int userId);
    }
}
