namespace ApiDB.dal.Interface
{
    public interface IUserService : IEntityBaseRepository<User>
    {
        Task<User> GetMovieByIdAsync(int id);
 

    }
}
