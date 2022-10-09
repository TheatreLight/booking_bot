namespace ApiDB.dal.Interface
{
    public interface IBookingService : IEntityBaseRepository<User>
    {
        Task<User> GetMovieByIdAsync(int id);
        Task<NewBookingDropdownsVM> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(NewBookingVM data);
        Task UpdateMovieAsync(NewBookingVM data);
    }
}
