using Project1.DAL.Model;

namespace Project1.Services.Interface
{
    public interface IUserService
    {
        Task CreateAsync(User user);
        Task DeleteAsync(int id);
        IQueryable<User> GetAll();
        Task<User> GetByIdAsync(int id);
        Task UpdateAsync(int id, User user);
    }
}
